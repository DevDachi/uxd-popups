namespace UXDivers.Popups.Services;

/// <summary>
/// Interface for dispatching operations to the UI thread in a framework-agnostic way.
/// </summary>
public interface IUIThreadDispatcher
{
    /// <summary>
    /// Schedules the provided action on the UI thread from a worker thread.
    /// </summary>
    /// <param name="action">The Action() to be scheduled for processing on the UI thread.</param>
    /// <returns>true when the action has been dispatched successfully, otherwise false.</returns>
    bool Dispatch(Action action);

    /// <summary>
    /// Dispatches a function to the UI thread asynchronously.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="function">The function to execute on the UI thread.</param>
    /// <returns>A task that completes with the result of the function.</returns>
    Task<T> DispatchAsync<T>(Func<T> function);

    /// <summary>
    /// Dispatches an async function to the UI thread.
    /// </summary>
    /// <param name="asyncAction">The async action to execute on the UI thread.</param>
    /// <returns>A task that completes when the async action has been executed.</returns>
    Task DispatchAsync(Func<Task> asyncAction);

    /// <summary>
    /// Dispatches an async function to the UI thread with a return value.
    /// </summary>
    /// <typeparam name="T">The return type of the async function.</typeparam>
    /// <param name="asyncFunction">The async function to execute on the UI thread.</param>
    /// <returns>A task that completes with the result of the async function.</returns>
    Task<T> DispatchAsync<T>(Func<Task<T>> asyncFunction);

    /// <summary>
    /// Dispatches an async function to the UI thread.
    /// </summary>
    /// <param name="asyncAction">The async action to execute on the UI thread.</param>
    /// <returns>true when the action has been dispatched successfully, otherwise false.</returns>
    bool Dispatch(Func<Task> asyncAction);
}
