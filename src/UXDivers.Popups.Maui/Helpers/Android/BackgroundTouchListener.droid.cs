using Android.Views;
using Android.Widget;
using AView = Android.Views.View;

namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Listener for handling touch events on the popup background in Android.
    /// Detects taps outside the popup content and triggers the appropriate actions.
    /// </summary>
    internal class BackgroundTouchListener : Java.Lang.Object, AView.IOnTouchListener
    {
        private readonly PopupPage _popupPage;
        private readonly ViewGroup _nativeContainer;
        private readonly AView _nativeView;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundTouchListener"/> class.
        /// </summary>
        /// <param name="popup">The popup page associated with the listener.</param>
        /// <param name="nativeContainer">The native container view for the popup.</param>
        /// <param name="nativeView">The native view representing the popup content.</param>
        public BackgroundTouchListener(PopupPage popup, ViewGroup nativeContainer, AView nativeView)
        {
            _nativeView = nativeView;
            _nativeContainer = nativeContainer;
            _popupPage = popup;
        }

        /// <summary>
        /// Handles touch events on the popup background.
        /// </summary>
        /// <param name="v">The view that received the touch event.</param>
        /// <param name="e">The motion event containing touch details.</param>
        /// <returns>True if the event is consumed; otherwise, false.</returns>
        public bool OnTouch(AView? v, MotionEvent? e)
        {
            if (e == null)
            {
                return false;
            }

            var x = e.GetX();
            var y = e.GetY();

            var popupX = _nativeView.GetX();
            var popupY = _nativeView.GetY();
            var popupWidth = _nativeView.Width;
            var popupHeight = _nativeView.Height;

            // Check if the tap was inside the popup content
            if (x >= popupX && x <= popupX + popupWidth
                && y >= popupY && y <= popupY + popupHeight)
            {
                return true; // Catch touch events inside the popup
            }

            // If the background is input transparent, do not consume the event
            if (_popupPage.BackgroundInputTransparent)
            {
                return false;
            }

            // Trigger the background tap action on pointer up or touch up
            if (e.Action == MotionEventActions.PointerUp
                || e.Action == MotionEventActions.Up)
            {
                Task.Run(() => _popupPage.OnBackgroundTapped());
                return true; // Consume the event
            }

            return true; // Consume other touch events on the background
        }
    }
}
