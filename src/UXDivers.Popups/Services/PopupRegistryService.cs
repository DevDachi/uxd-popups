namespace UXDivers.Popups.Services;

// Internal registry for popup/viewmodel registration and resolution
internal class PopupRegistryService : IPopupRegistryService
{
    private static PopupRegistryService? _instance;

    public static PopupRegistryService Instance => _instance ??= new PopupRegistryService();

    private readonly Dictionary<Type, PopupRegistry> _registrations = new();

    private Func<Type, object?>? _getService;

    public IPopupRegistryService UseServiceProvider(Func<Type, object?> getServiceFunction)
    {
        _getService = getServiceFunction ?? throw new ArgumentNullException(nameof(getServiceFunction), "Service provider function cannot be null.");
        return this;
    }

    public IPopupRegistryService AddTransient<TPopup, TViewModel>()
        where TPopup : IPopupPage
        where TViewModel : IPopupViewModel
    {
        _registrations[typeof(TPopup)] = new PopupRegistry
        {
            ViewModelType = typeof(TViewModel),
            ViewModelInstance = null,
            Lifetime = RegistryLifetime.Transient
        };

        return this;
    }

    public IPopupRegistryService AddSingleton<TPopup, TViewModel>(TViewModel instance)
        where TPopup : IPopupPage
        where TViewModel : IPopupViewModel
    {
        _registrations[typeof(TPopup)] = new PopupRegistry
        {
            ViewModelType = typeof(TViewModel),
            ViewModelInstance = instance,
            Lifetime = RegistryLifetime.Singleton
        };

        return this;
    }

    public IPopupRegistryService AddSingleton<TPopup, TViewModel>()
        where TPopup : IPopupPage
        where TViewModel : IPopupViewModel
    {
        _registrations[typeof(TPopup)] = new PopupRegistry
        {
            ViewModelType = typeof(TViewModel),
            ViewModelInstance = null,
            Lifetime = RegistryLifetime.Singleton
        };

        return this;
    }

    internal void AddService<TPopup, TViewModel>()
        where TPopup : IPopupPage
        where TViewModel : IPopupViewModel
    {
        _registrations[typeof(TPopup)] = new PopupRegistry
        {
            ViewModelType = typeof(TViewModel),
            ViewModelInstance = null,
            Lifetime = null
        };
    }

    internal void AddService<TPopup>()
        where TPopup : IPopupPage
    {
        _registrations[typeof(TPopup)] = new PopupRegistry
        {
            ViewModelType = null,
            ViewModelInstance = null,
            Lifetime = null
        };
    }

    internal IPopupPage? CreatePopupInstance<TPopup>()
        where TPopup : IPopupPage
    {
        if (!_registrations.ContainsKey(typeof(TPopup)))
        {
            throw new InvalidOperationException($"No registration found for popup type {typeof(TPopup).FullName}. Please register the popup using IUXDPopupRegistryService.");
        }

        if (_getService != null)
        {
            return _getService(typeof(TPopup)) as IPopupPage;
        }

        return Activator.CreateInstance(typeof(TPopup)) as IPopupPage;
    }

    internal IPopupViewModel? CreateViewModel(IPopupPage popup)
    {
        var popupType = popup.GetType();

        // Try to get registration for this popup type
        if (!_registrations.TryGetValue(popupType, out var registry))
        {
            return null;
        }

        // Try service provider first if available
        if (_getService != null && registry.ViewModelType != null)
        {
            var serviceInstance = _getService(registry.ViewModelType);
            if (serviceInstance is IPopupViewModel viewModel)
            {
                return viewModel;
            }
        }

        // Fallback to internal registration
        if (registry.ViewModelType == null)
        {
            return null;
        }

        // Handle singleton lifetime
        if (registry.Lifetime == RegistryLifetime.Singleton)
        {
            // Create and cache singleton instance
            if (registry.ViewModelInstance != null)
            {
                return registry.ViewModelInstance;
            }

            var newInstance = Activator.CreateInstance(registry.ViewModelType) as IPopupViewModel;
            if (newInstance != null)
            {
                registry.ViewModelInstance = newInstance;
            }
            return newInstance;
        }

        // Handle transient lifetime
        return Activator.CreateInstance(registry.ViewModelType) as IPopupViewModel;
    }
}
