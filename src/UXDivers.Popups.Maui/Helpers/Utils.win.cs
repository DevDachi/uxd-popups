using System.Collections.Concurrent;
using Microsoft.Maui.Platform;

namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Utility class for Windows-specific helper methods.
    /// </summary>
    internal partial class Utils
    {
        public static NavigationRootManager GetNavigationRootManager(IMauiContext mauiContext) =>
            mauiContext.Services.GetRequiredService<NavigationRootManager>();
        
        public static Microsoft.UI.Xaml.Window GetPlatformWindow(IMauiContext mauiContext) =>
            mauiContext.Services.GetRequiredService<Microsoft.UI.Xaml.Window>();
        
        public static IMauiContext MakeScoped(IMauiContext mauiContext, bool registerNewNavigationRoot)
        {
            var serviceProvider = new ScopedServiceProvider(mauiContext.Services);
            
            if (registerNewNavigationRoot)
            {
                // Registers NavigationRootManager using the same window configuration from MauiContext Windows,
                // by doing so it inherits title bar configuration to the new WindowRootView.
                serviceProvider.AddSpecific(new NavigationRootManager(GetPlatformWindow(mauiContext)));
            }

            return new MauiContext(serviceProvider);
        }

        private class ScopedServiceProvider : IServiceProvider
        {
            private readonly IServiceProvider _serviceProvider;
            private readonly ConcurrentDictionary<Type, (object, Func<object, object?>)> _scopeStatic = new();

            public ScopedServiceProvider(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }
            
            public object? GetService(Type serviceType)
            {
                if (!_scopeStatic.TryGetValue(serviceType, out var scope))
                {
                    return _serviceProvider.GetService(serviceType);
                }

                var (state, getter) = scope;
                return getter.Invoke(state);
            }

            public void AddSpecific(Type type, Func<object, object?> getter, object state)
            {
                _scopeStatic[type] = (state, getter);
            }
            
            public void AddSpecific<TService>(TService instance)
                where TService : class
            {
                AddSpecific(typeof(TService), static state => state, instance);
            }
        }
    }
}