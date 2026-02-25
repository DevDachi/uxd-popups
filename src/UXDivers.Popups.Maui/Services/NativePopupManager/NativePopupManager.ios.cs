using CoreGraphics;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace UXDivers.Popups.Maui;

/// iOS-specific implementation of the native popup manager.
/// Handles the display and removal of native popups on iOS.
internal partial class NativePopupManager
{
    /// <summary>
    /// Stores the notification observer tokens needed to unsubscribe when the popup is closed.
    /// </summary>
    private sealed record KeyboardAvoidanceState(
        NSObject ShowObserver,
        NSObject HideObserver);

    // Keyed by the native view's Handle so we can clean up when closing.
    private readonly Dictionary<IntPtr, KeyboardAvoidanceState> _keyboardStates = new();

    /// <summary>
    /// Displays a native view for the given popup page.
    /// </summary>
    /// <param name="popup">The popup page to display.</param>
    /// <returns>A Task that resolves with the native view object.</returns>
    /// <exception cref="ArgumentException">Thrown if the popup cannot be converted to a native view.</exception>
    public Task<object> ShowNativeViewAsync(PopupPage popup)
    {
        var mauiContext = GetMauiContext();

        // Get the main application window
        var window = Utils.GetMainWindow();
        
        if (window == null)
        {
            throw new NullReferenceException("Couldn't find the main application window");
        }

        // Convert the popup page to a native view
        var nativePopup = popup.ToPlatform(mauiContext);
        nativePopup.InsetsLayoutMarginsFromSafeArea = false;

        // Wrap the popup in a background view if it is a PopupPage
        var nativeBackground = new PopupBackgroundView
        {
            UserInteractionEnabled = true,
            PopupContentView = popup.ActualContent?.ToPlatform(mauiContext) ?? throw new ArgumentException("Popup content could not be converted to a native view"),
            BackgroundTappedAction = popup.OnBackgroundTapped,
            PopupPage = popup
        };

        nativeBackground.AddSubview(nativePopup);

        nativePopup = nativeBackground;

        // Configure the native popup's layout and appearance
        nativePopup.ContentMode = UIViewContentMode.ScaleToFill;
        nativePopup.InsetsLayoutMarginsFromSafeArea = false;

        // Get safe area insets from the window
        var safeAreaInsets = window.SafeAreaInsets;

        // Calculate the frame based on SafeAreaInsets property
        var frame = window.Bounds;
        var popupSafeAreaInsets = popup.SafeAreaAsPadding;

        var topInset = popupSafeAreaInsets.HasFlag(SafeAreaAsPadding.Top) ? safeAreaInsets.Top : 0;
        var leftInset = popupSafeAreaInsets.HasFlag(SafeAreaAsPadding.Left) ? safeAreaInsets.Left : 0;
        var rightInset = popupSafeAreaInsets.HasFlag(SafeAreaAsPadding.Right) ? safeAreaInsets.Right : 0;
        var bottomInset = popupSafeAreaInsets.HasFlag(SafeAreaAsPadding.Bottom) ? safeAreaInsets.Bottom : 0;

        //Apply safe area insets as popup padding
        popup.Padding = new Thickness(
            popup.Padding.Left + leftInset,
            popup.Padding.Top + topInset,
            popup.Padding.Right + rightInset,
            popup.Padding.Bottom + bottomInset
        );

        // Adjust frame to respect safe area insets
        nativePopup.Frame = frame;
        nativePopup.AutoresizingMask = UIViewAutoresizing.None;
        popup.Arrange(new Rect(0, 0, frame.Width, frame.Height));
        nativePopup.SetNeedsLayout();

        // Add the native popup to the window
        window.AddSubview(nativePopup);

        // Subscribe to keyboard notifications so the popup scrolls content above the keyboard.
        if (popup.KeyboardAwareness)
        {
            SubscribeToKeyboardNotifications(popup, nativePopup, frame);
        }

        return Task.FromResult<object>(nativePopup);
    }

    private void SubscribeToKeyboardNotifications(PopupPage popup, UIView nativeView, CGRect frame)
    {
        // Capture the padding that was applied right after safe-area adjustment.
        var basePadding = popup.Padding;

        NSObject showObserver = NSNotificationCenter.DefaultCenter.AddObserver(
            UIKeyboard.WillShowNotification,
            notification => OnKeyboardWillShow(notification, nativeView, popup, frame, basePadding));

        NSObject hideObserver = NSNotificationCenter.DefaultCenter.AddObserver(
            UIKeyboard.WillHideNotification,
            notification => OnKeyboardWillHide(notification, nativeView, popup, frame, basePadding));

        _keyboardStates[nativeView.Handle] = new KeyboardAvoidanceState(showObserver, hideObserver);
    }

    private static void OnKeyboardWillShow(
        NSNotification notification,
        UIView nativeView,
        PopupPage popup,
        CGRect frame,
        Thickness basePadding)
    {
        // Guard against the callback firing after the native view has been disposed.
        if (nativeView.Handle == IntPtr.Zero)
            return;

        if (notification.UserInfo == null)
            return;

        if (notification.UserInfo[UIKeyboard.FrameEndUserInfoKey] is not NSValue keyboardFrameValue)
            return;

        // Keyboard frame is in window coordinates on iOS.
        var keyboardFrame = keyboardFrameValue.CGRectValue;

        // How much of the window's bottom does the keyboard cover?
        var keyboardCoverage = frame.Height - keyboardFrame.Top;
        if (keyboardCoverage <= 0)
            return;

        // Apply the new bottom padding so popup layout pushes content up.
        popup.Padding = new Thickness(
            basePadding.Left,
            basePadding.Top,
            basePadding.Right,
            basePadding.Bottom + keyboardCoverage);

        var duration = (notification.UserInfo[UIKeyboard.AnimationDurationUserInfoKey] as NSNumber)?.DoubleValue ?? 0.25;
        UIView.Animate(duration, () =>
        {
            popup.Arrange(new Rect(0, 0, frame.Width, frame.Height));
            nativeView.SetNeedsLayout();
            nativeView.LayoutIfNeeded();
        });
    }

    private static void OnKeyboardWillHide(
        NSNotification notification,
        UIView nativeView,
        PopupPage popup,
        CGRect frame,
        Thickness basePadding)
    {
        // Guard against the callback firing after the native view has been disposed.
        if (nativeView.Handle == IntPtr.Zero)
            return;

        // Restore the original padding (without keyboard offset).
        popup.Padding = basePadding;

        var duration = (notification.UserInfo?[UIKeyboard.AnimationDurationUserInfoKey] as NSNumber)?.DoubleValue ?? 0.25;
        UIView.Animate(duration, () =>
        {
            popup.Arrange(new Rect(0, 0, frame.Width, frame.Height));
            nativeView.SetNeedsLayout();
            nativeView.LayoutIfNeeded();
        });
    }

    /// <summary>
    /// Retrieves the content view of the popup. We do it this way because if the
    /// Popup has a ControlTemplate the property ActualContent doesn't take it into account.
    /// </summary>
    /// <param name="popupNativeView">The native view of the popup.</param>
    /// <returns>The content view of the popup, or null if no content is found.</returns>
    protected virtual UIView? GetPopupContent(UIView popupNativeView)
    {
        if (popupNativeView.Subviews.Length > 0)
        {
            return popupNativeView.Subviews[0];
        }

        return null;
    }

    /// <summary>
    /// Closes and removes the native view for the given popup.
    /// </summary>
    /// <param name="nativePopup">The native popup object to close.</param>
    /// <returns>A completed Task.</returns>
    /// <exception cref="ArgumentException">Thrown if the native popup is not a valid UIView.</exception>
    public Task CloseNativeViewAsync(object nativePopup)
    {
        var nativeView = nativePopup as UIView;

        if (nativeView == null)
        {
            throw new ArgumentException("Invalid native popup type, it should be or inherit from UIView");
        }

        if (nativeView.Handle == IntPtr.Zero)
        {
            return Task.CompletedTask; // The view is already disposed
        }

        // Unsubscribe keyboard observers for this popup before disposing.
        if (_keyboardStates.TryGetValue(nativeView.Handle, out var state))
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(state.ShowObserver);
            NSNotificationCenter.DefaultCenter.RemoveObserver(state.HideObserver);
            state.ShowObserver.Dispose();
            state.HideObserver.Dispose();
            _keyboardStates.Remove(nativeView.Handle);
        }

        // Remove the native view from its superview and dispose of it
        nativeView.RemoveFromSuperview();
        nativeView.Dispose();

        return Task.CompletedTask;
    }
}
