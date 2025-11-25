using UXDivers.Popups.Services;

namespace UXDivers.Popups.Maui;

/// <summary>
/// MAUI implementation of the ViewModel assignment strategy.
/// Uses the BindingContext property to assign ViewModels to popup pages.
/// </summary>
internal class MauiViewModelAssignmentStrategy : IViewModelAssignmentStrategy
{
    /// <inheritdoc/>
    public bool SupportsViewModelAssignment => true;

    /// <inheritdoc/>
    public bool HasViewModel(IPopupPage popupPage)
    {
        if (popupPage is BindableObject bindableObject)
        {
            return bindableObject.BindingContext != null;
        }

        return false;
    }

    /// <inheritdoc/>
    public Task SetParameters(IPopupPage popupPage, IReadOnlyDictionary<string, object?> parameters)
    {
        if (popupPage is BindableObject bindableObject && bindableObject.BindingContext is IPopupViewModel viewModel)
        {
            return viewModel.OnPopupNavigatedAsync(parameters);
        }
        
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public bool TryAssignViewModel(IPopupPage popupPage, object viewModel)
    {
        if (popupPage is BindableObject bindableObject)
        {
            bindableObject.BindingContext = viewModel;
            return true;
        }

        return false;
    }
}
