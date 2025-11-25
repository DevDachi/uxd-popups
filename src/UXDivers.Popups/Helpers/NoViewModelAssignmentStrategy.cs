using UXDivers.Popups.Services;

namespace UXDivers.Popups;

/// <summary>
/// A no-op implementation of the ViewModel assignment strategy.
/// Used for frameworks that don't support or need ViewModel assignment.
/// </summary>
public class NoViewModelAssignmentStrategy : IViewModelAssignmentStrategy
{
    /// <summary>
    /// Gets a singleton instance of the no-op strategy.
    /// </summary>
    public static NoViewModelAssignmentStrategy Instance { get; } = new();

    /// <inheritdoc/>
    public bool SupportsViewModelAssignment => false;

    /// <inheritdoc/>
    public bool HasViewModel(IPopupPage popupPage) => false;

    public Task SetParameters(IPopupPage popupPage, IReadOnlyDictionary<string, object?> parameters)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public bool TryAssignViewModel(IPopupPage popupPage, object viewModel)
    {
        // No-op implementation
        return false;
    }
}