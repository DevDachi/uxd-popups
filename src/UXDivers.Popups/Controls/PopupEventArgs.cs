namespace UXDivers.Popups;

/// <summary>
/// Event args for popup-related events.
/// </summary>
public class PopupEventArgs : EventArgs
{
    /// <summary>
    /// Gets the popup page associated with the event.
    /// </summary>
    public IPopupPage PopupPage { get; }

    /// <summary>
    /// Initializes a new instance of the PopupEventArgs class.
    /// </summary>
    /// <param name="popupPage">The popup page associated with the event.</param>
    public PopupEventArgs(IPopupPage popupPage)
    {
        PopupPage = popupPage;
    }
}
