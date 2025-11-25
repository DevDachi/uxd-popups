namespace UXDivers.Popups.Services;

/// <summary>
/// Event args for popup stack changes.
/// </summary>
public class PopupStackChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the current popup stack.
    /// </summary>
    public IReadOnlyList<IPopupPage> Stack { get; }

    /// <summary>
    /// Initializes a new instance of the PopupStackChangedEventArgs class.
    /// </summary>
    /// <param name="stack">The current popup stack.</param>
    public PopupStackChangedEventArgs(IReadOnlyList<IPopupPage> stack)
    {
        Stack = stack;
    }
}
