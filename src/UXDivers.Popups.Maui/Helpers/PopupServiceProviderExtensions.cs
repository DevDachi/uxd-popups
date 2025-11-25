using System.Diagnostics.CodeAnalysis;
using UXDivers.Popups.Services;

namespace UXDivers.Popups.Maui;

/// <summary>
/// Extension methods for registering popups and their view models with the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a popup and its view model as transient services.
    /// </summary>
    /// <typeparam name="TPopup">The popup page type.</typeparam>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddTransientPopup<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TPopup, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>(this IServiceCollection services)
        where TPopup : class, IPopupPage
        where TViewModel : class, IPopupViewModel
    {
        services.AddTransient<TViewModel>();
        services.AddTransient<TPopup>();

        PopupRegistryService.Instance.AddService<TPopup, TViewModel>();

        return services;
    }

    /// <summary>
    /// Registers a popup as a transient service without a view model.
    /// </summary>
    /// <typeparam name="TPopup">The popup page type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddTransientPopup<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TPopup>(this IServiceCollection services)
        where TPopup : class, IPopupPage
    {
        services.AddTransient<TPopup>();

        PopupRegistryService.Instance.AddService<TPopup>();

        return services;
    }

    /// <summary>
    /// Registers a popup and its view model as singleton services.
    /// </summary>
    /// <typeparam name="TPopup">The popup page type.</typeparam>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSingletonPopup<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TPopup, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>(this IServiceCollection services)
        where TPopup : class, IPopupPage
        where TViewModel : class, IPopupViewModel
    {
        services.AddSingleton<TViewModel>();
        services.AddSingleton<TPopup>();

        PopupRegistryService.Instance.AddService<TPopup, TViewModel>();

        return services;
    }

    /// <summary>
    /// Registers a popup as a singleton service without a view model.
    /// </summary>
    /// <typeparam name="TPopup">The popup page type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSingletonPopup<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TPopup>(this IServiceCollection services)
        where TPopup : class, IPopupPage
    {
        services.AddSingleton<TPopup>();

        PopupRegistryService.Instance.AddService<TPopup>();

        return services;
    }

    /// <summary>
    /// Registers a popup instance as a singleton service without a view model.
    /// </summary>
    /// <typeparam name="TPopup">The popup page type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSingletonPopup<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TPopup>(this IServiceCollection services, TPopup instance)
        where TPopup : class, IPopupPage
    {
        services.AddSingleton(instance);

        PopupRegistryService.Instance.AddService<TPopup>();

        return services;
    }

    /// <summary>
    /// Registers a popup as singleton with a pre-existing view model instance.
    /// </summary>
    /// <typeparam name="TPopup">The popup page type.</typeparam>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <param name="instance">The view model instance to register.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSingletonPopup<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TPopup, TViewModel>(this IServiceCollection services, TViewModel instance)
        where TPopup : class, IPopupPage
        where TViewModel : class, IPopupViewModel
    {
        services.AddSingleton(instance);
        services.AddSingleton<TPopup>();

        PopupRegistryService.Instance.AddService<TPopup, TViewModel>();

        return services;
    }

    /// <summary>
    /// Registers a view model as singleton with a pre-existing popup instance.
    /// </summary>
    /// <typeparam name="TPopup">The popup page type.</typeparam>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <param name="instance">The popup instance to register.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSingletonPopup<TPopup, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>(this IServiceCollection services, TPopup instance)
        where TPopup : class, IPopupPage
        where TViewModel : class, IPopupViewModel
    {
        services.AddSingleton<TViewModel>();
        services.AddSingleton(instance);

        PopupRegistryService.Instance.AddService<TPopup, TViewModel>();

        return services;
    }

    /// <summary>
    /// Registers both popup and view model as singletons with pre-existing instances.
    /// </summary>
    /// <typeparam name="TPopup">The popup page type.</typeparam>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <param name="popupInstance">The popup instance to register.</param>
    /// <param name="viewModelInstance">The view model instance to register.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSingletonPopup<TPopup, TViewModel>(this IServiceCollection services, TPopup popupInstance, TViewModel viewModelInstance)
        where TPopup : class, IPopupPage
        where TViewModel : class, IPopupViewModel
    {
        services.AddSingleton(viewModelInstance);
        services.AddSingleton(popupInstance);

        PopupRegistryService.Instance.AddService<TPopup, TViewModel>();

        return services;
    }
}
