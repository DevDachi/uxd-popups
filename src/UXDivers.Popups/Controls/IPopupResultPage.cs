namespace UXDivers.Popups;

/// <summary>
/// Interface representing a popup page that returns a result.
/// This interface is framework-agnostic and can be implemented by any UI framework.
/// </summary>
/// <typeparam name="T">The type of the result returned by the popup.</typeparam>
public interface IPopupResultPage<T> : IPopupPage
{
    /// <summary>
    /// Gets the result of the popup.
    /// </summary>
    T? Result { get; }

    /// <summary>
    /// Sets the result of the popup
    /// </summary>
    /// <param name="result">The result to set.</param>
    void SetResult(T? result);
}
