using UXDivers.Popups.Services;

namespace UXDivers.Popups.Maui;

/// <summary>
/// MAUI implementation of the UI thread dispatcher.
/// </summary>
internal class MauiUIThreadDispatcher : IUIThreadDispatcher
{
    /// <inheritdoc/>
    public bool Dispatch(Action action)
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        CheckApplicationInitialized();
        
        // Use MainThread to dispatch the action
        return Application.Current!.Dispatcher.Dispatch(action);
    }

    /// <inheritdoc/>
    public bool Dispatch(Func<Task> asyncAction)
    {
        if (asyncAction == null)
        {
            throw new ArgumentNullException(nameof(asyncAction));
        }

        CheckApplicationInitialized();
        
        // Use DispatchAsync and wait synchronously for completion
        return Application.Current!.Dispatcher.Dispatch(async () => await asyncAction());
    }

    /// <inheritdoc/>
    public Task<T> DispatchAsync<T>(Func<T> function)
    {
        if (function == null)
        {
            throw new ArgumentNullException(nameof(function));
        }

        CheckApplicationInitialized();
        
        return Application.Current!.Dispatcher.DispatchAsync(function);
    }

    /// <inheritdoc/>
    public Task DispatchAsync(Func<Task> asyncAction)
    {
        if (asyncAction == null)
        {
            throw new ArgumentNullException(nameof(asyncAction));
        }

        CheckApplicationInitialized();
        
        return Application.Current!.Dispatcher.DispatchAsync(asyncAction);
    }

    /// <inheritdoc/>
    public Task<T> DispatchAsync<T>(Func<Task<T>> asyncFunction)
    {
        if (asyncFunction == null)
        {
            throw new ArgumentNullException(nameof(asyncFunction));
        }

        CheckApplicationInitialized();
        
        return Application.Current!.Dispatcher.DispatchAsync(asyncFunction);
    }

    private static void CheckApplicationInitialized()
    {
        if (Application.Current == null)
        {
            throw new InvalidOperationException("Application.Current is null. Ensure this is called after the application has started.");
        }
    }
}
