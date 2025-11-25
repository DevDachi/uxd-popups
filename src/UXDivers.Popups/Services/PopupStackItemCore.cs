namespace UXDivers.Popups.Services;

/// <summary>
/// Represents an item in the popup stack.
/// </summary>
internal class PopupStackItemCore
{
    /// <summary>
    /// Gets or sets the popup page.
    /// </summary>
    public IPopupPage PopupPage { get; set; } = null!;

    /// <summary>
    /// Gets or sets the task completion source for the popup.
    /// </summary>
    public TaskCompletionSource TaskSource { get; set; } = null!;

    /// <summary>
    /// Gets or sets the native popup object.
    /// </summary>
    public object? NativePopup { get; set; }

    /// <summary>
    /// Sets the result of the popup task.
    /// </summary>
    public virtual void SetResult()
    {
        TaskSource.TrySetResult();
    }

    /// <summary>
    /// Sets an exception for the popup task.
    /// </summary>
    /// <param name="exception">The exception to set.</param>
    public virtual void SetException(Exception exception)
    {
        TaskSource.SetException(exception);
    }
}