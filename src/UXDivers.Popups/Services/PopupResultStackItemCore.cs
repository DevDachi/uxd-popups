namespace UXDivers.Popups.Services;

/// <summary>
/// Represents an item in the popup stack with a result.
/// </summary>
/// <typeparam name="T">The type of the result.</typeparam>
internal class PopupResultStackItemCore<T> : PopupStackItemCore
{
    /// <summary>
    /// Gets or sets the task completion source for the popup with result.
    /// </summary>
    public new TaskCompletionSource<T?> TaskSource { get; set; } = null!;

    /// <summary>
    /// Sets the result of the popup task to default.
    /// </summary>
    public override void SetResult()
    {
        if (PopupPage is IPopupResultPage<T> popupResultPage)
        {
            TaskSource.TrySetResult(popupResultPage.Result);
        }
    }

    /// <summary>
    /// Sets an exception for the popup task.
    /// </summary>
    /// <param name="exception">The exception to set.</param>
    public override void SetException(Exception exception)
    {
        TaskSource.SetException(exception);
    }
}