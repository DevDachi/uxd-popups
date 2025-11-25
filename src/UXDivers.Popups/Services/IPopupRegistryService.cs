namespace UXDivers.Popups.Services;

/// <summary>
/// Service for registering and managing popup-viewmodel associations.
/// </summary>
public interface IPopupRegistryService
{
    /// <summary>
    /// Gets or sets the current instance of the popup registry service.
    /// </summary>
    public static IPopupRegistryService Current { get; set; } = null!;

    /// <summary>
    /// Registers a popup with a viewmodel as transient.
    /// </summary>
    IPopupRegistryService AddTransient<TPopup, TViewModel>()
        where TPopup : IPopupPage
        where TViewModel : IPopupViewModel;

    /// <summary>
    /// Registers a popup with a viewmodel as singleton.
    /// </summary>
    IPopupRegistryService AddSingleton<TPopup, TViewModel>()
        where TPopup : IPopupPage
        where TViewModel : IPopupViewModel;

    /// <summary>
    /// Registers a popup with a viewmodel instance as singleton.
    /// </summary>
    IPopupRegistryService AddSingleton<TPopup, TViewModel>(TViewModel instance)
        where TPopup : IPopupPage
        where TViewModel : IPopupViewModel;

    /// <summary>
    /// Configures the service provider function for resolving dependencies.
    /// </summary>
    IPopupRegistryService UseServiceProvider(Func<Type, object?> getServiceFunction);
}
