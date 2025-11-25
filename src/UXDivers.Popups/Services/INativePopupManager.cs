namespace UXDivers.Popups.Services;

/// <summary>
/// Interface defining the contract for managing native popups.
/// </summary>
public interface INativePopupManager
{
    /// <summary>
    /// Displays a native view for the given popup page.
    /// </summary>
    /// <param name="popupPage">The popup page to display.</param>
    /// <returns>A Task that resolves with the native view object.</returns>
    Task<object> ShowNativeViewAsync(IPopupPage popupPage);

    /// <summary>
    /// Closes and removes the native view for the given popup.
    /// </summary>
    /// <param name="nativePopup">The native popup object to close.</param>
    /// <returns>A completed Task.</returns>
    Task CloseNativeViewAsync(object nativePopup);
}
