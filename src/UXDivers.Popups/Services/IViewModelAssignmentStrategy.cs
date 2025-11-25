namespace UXDivers.Popups.Services;

/// <summary>
/// Strategy interface for assigning ViewModels to popup pages.
/// Different UI frameworks can implement their own assignment logic.
/// </summary>
public interface IViewModelAssignmentStrategy
{
    /// <summary>
    /// Determines whether the specified popup page has a ViewModel assigned.
    /// </summary>
    /// <param name="popupPage">The popup page to check.</param>
    /// <returns>True if a ViewModel is assigned; otherwise, false.</returns>
    bool HasViewModel(IPopupPage popupPage);

    /// <summary>
    /// Assigns a ViewModel to the specified popup page.
    /// </summary>
    /// <param name="popupPage">The popup page to assign the ViewModel to.</param>
    /// <param name="viewModel">The ViewModel to assign.</param>
    /// <returns>True if the assignment was successful; otherwise, false.</returns>
    bool TryAssignViewModel(IPopupPage popupPage, object viewModel);

    Task SetParameters(IPopupPage popupPage, IReadOnlyDictionary<string, object?> parameters);

    /// <summary>
    /// Gets a value indicating whether this strategy supports ViewModel assignment.
    /// Some frameworks (like Blazor) may not use traditional MVVM patterns.
    /// </summary>
    bool SupportsViewModelAssignment { get; }
}
