# Dependency Injection

This page explains how to register popups and their ViewModels with the .NET MAUI dependency injection container.

## Overview

UXDivers Popups integrates seamlessly with .NET MAUI's dependency injection system. You can:

- Register popups as transient or singleton services
- Associate popups with ViewModels
- Resolve popups from the DI container when navigating
- Have ViewModels automatically created and assigned to popups

## Setup

Configure the library in `MauiProgram.cs`:

```csharp
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseUXDiversPopups();  // Configure popups

        // Register your popups
        builder.Services
            .AddTransientPopup<MyCustomPopup>()
            .AddTransientPopup<LoginPopup, LoginViewModel>();

        return builder.Build();
    }
}
```

By default, the library uses the MAUI service provider for resolving popups and ViewModels. No additional configuration is required.

---

## Service Resolution

### Default: MAUI Service Provider

When you call `UseUXDiversPopups()`, the library automatically configures itself to use the MAUI dependency injection container for resolving popups and ViewModels. This provides full constructor injection support.

```csharp
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .UseUXDiversPopups();  // MAUI DI is used by default

    // Register services that ViewModels depend on
    builder.Services.AddSingleton<IUserService, UserService>();
    builder.Services.AddSingleton<IApiService, ApiService>();

    // Register popups with ViewModels
    builder.Services
        .AddTransientPopup<LoginPopup, LoginViewModel>()
        .AddTransientPopup<SettingsPopup, SettingsViewModel>();

    return builder.Build();
}
```

**Benefits:**
- Full constructor injection support for ViewModels
- Integrates with existing MAUI services
- Supports all service lifetimes (transient, singleton, scoped)

---

### Custom Service Provider

For advanced scenarios, you can override the default resolution strategy using `IPopupRegistryService.Current.UseServiceProvider()`. This allows integration with third-party DI containers or custom factory patterns.

```csharp
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .UseUXDiversPopups();

    // Register popups
    builder.Services.AddTransientPopup<LoginPopup, LoginViewModel>();

    // Override with custom service provider
    IPopupRegistryService.Current.UseServiceProvider(type =>
    {
        // Custom resolution logic
        return MyCustomContainer.Resolve(type);
    });

    return builder.Build();
}
```

**Example with custom factory logic:**

```csharp
IPopupRegistryService.Current.UseServiceProvider(type =>
{
    // Custom resolution logic for specific types
    if (type == typeof(LoginViewModel))
    {
        return new LoginViewModel(
            new AuthService(),
            new LoggingService()
        );
    }

    if (type == typeof(SettingsViewModel))
    {
        return new SettingsViewModel();
    }

    // Fallback: create instance using Activator (requires parameterless constructor)
    return Activator.CreateInstance(type)
        ?? throw new InvalidOperationException($"Cannot create instance of {type.Name}");
});
```

**Benefits:**
- Full control over instance creation
- Integration with any DI container (Autofac, Unity, DryIoc, etc.)
- Support for complex factory patterns

---

### Resolution Options Comparison

| Option | Constructor Injection | Best For |
|--------|----------------------|----------|
| MAUI Service Provider (Default) | Yes | Most MAUI apps |
| Custom Provider | Depends on implementation | Third-party DI, complex scenarios |

---

## Registering a Popup as Transient

Transient registration creates a new instance each time the popup is requested.

### Popup Only (No ViewModel)

```csharp
builder.Services.AddTransientPopup<MyCustomPopup>();
```

### Popup with ViewModel

```csharp
builder.Services.AddTransientPopup<MyCustomPopup, MyCustomPopupViewModel>();
```

This registers both the popup and ViewModel as transient services and creates an association between them.

---

## Registering a Popup as Singleton

Singleton registration reuses the same instance throughout the application lifetime.

### Popup Only (No ViewModel)

```csharp
builder.Services.AddSingletonPopup<SettingsPopup>();
```

### Popup with ViewModel

```csharp
builder.Services.AddSingletonPopup<SettingsPopup, SettingsViewModel>();
```

### With Pre-existing Popup Instance

```csharp
var settingsPopup = new SettingsPopup();
builder.Services.AddSingletonPopup(settingsPopup);
```

### With Pre-existing ViewModel Instance

```csharp
var viewModel = new SettingsViewModel(someService);
builder.Services.AddSingletonPopup<SettingsPopup, SettingsViewModel>(viewModel);
```

### With Pre-existing Popup Instance and ViewModel Type

```csharp
var popup = new SettingsPopup();
builder.Services.AddSingletonPopup<SettingsPopup, SettingsViewModel>(popup);
```

### With Both Pre-existing Instances

```csharp
var popup = new SettingsPopup();
var viewModel = new SettingsViewModel();
builder.Services.AddSingletonPopup(popup, viewModel);
```

---

## Registration Methods Summary

| Method | Description |
|--------|-------------|
| `AddTransientPopup<TPopup>()` | Register popup as transient (no ViewModel) |
| `AddTransientPopup<TPopup, TViewModel>()` | Register popup and ViewModel as transient |
| `AddSingletonPopup<TPopup>()` | Register popup as singleton (no ViewModel) |
| `AddSingletonPopup<TPopup, TViewModel>()` | Register popup and ViewModel as singleton |
| `AddSingletonPopup<TPopup>(instance)` | Register pre-existing popup instance |
| `AddSingletonPopup<TPopup, TViewModel>(vmInstance)` | Register popup type with pre-existing ViewModel |
| `AddSingletonPopup<TPopup, TViewModel>(popupInstance)` | Register pre-existing popup with ViewModel type |
| `AddSingletonPopup<TPopup, TViewModel>(popup, vm)` | Register both pre-existing instances |

---

## Using Registered Popups

Once registered, popups can be shown by type:

```csharp
// Show popup by type (resolved from DI)
await IPopupService.Current.PushAsync<MyCustomPopup>();

// With navigation parameters
await IPopupService.Current.PushAsync<MyCustomPopup>(new Dictionary<string, object?>
{
    { "itemId", 42 }
});

// With return type
bool result = await IPopupService.Current.PushAsync<ConfirmationPopup, bool>();
```

---

## Injecting Dependencies into ViewModels

When using `AddTransientPopup<TPopup, TViewModel>()` or `AddSingletonPopup<TPopup, TViewModel>()`, the ViewModel is resolved from the DI container, allowing constructor injection:

```csharp
public class MyPopupViewModel : IPopupViewModel
{
    private readonly IUserService _userService;
    private readonly INavigationService _navigationService;

    public MyPopupViewModel(
        IUserService userService,
        INavigationService navigationService)
    {
        _userService = userService;
        _navigationService = navigationService;
    }

    public async Task OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
    {
        // Use injected services
        var user = await _userService.GetCurrentUserAsync();
    }
}
```

Register the services and popup:

```csharp
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<INavigationService, NavigationService>();
builder.Services.AddTransientPopup<MyPopup, MyPopupViewModel>();
```

---

## Transient vs Singleton: When to Use Each

### Use Transient When:

- The popup displays different data each time it's shown
- The popup has state that should reset between uses
- You want a fresh instance for each navigation
- The popup is shown infrequently

```csharp
// Good for transient: A product detail popup that shows different products
builder.Services.AddTransientPopup<ProductDetailPopup, ProductDetailViewModel>();
```

### Use Singleton When:

- The popup maintains persistent state (like settings)
- The popup is expensive to create
- You want to preserve state between shows
- The popup is shown frequently

```csharp
// Good for singleton: App settings popup that should remember state
builder.Services.AddSingletonPopup<SettingsPopup, SettingsViewModel>();
```

---

## Complete Registration Example

Here's a comprehensive example showing various registration patterns:

```csharp
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseUXDiversPopups();

        // Register application services
        builder.Services
            .AddSingleton<IAuthService, AuthService>()
            .AddSingleton<IApiService, ApiService>()
            .AddSingleton<ISettingsService, SettingsService>();

        // Register popups
        builder.Services
            // Transient popups (new instance each time)
            .AddTransientPopup<LoginPopup, LoginViewModel>()
            .AddTransientPopup<ProductDetailPopup, ProductDetailViewModel>()
            .AddTransientPopup<ConfirmationPopup, ConfirmationViewModel>()

            // Singleton popups (same instance reused)
            .AddSingletonPopup<SettingsPopup, SettingsViewModel>()
            .AddSingletonPopup<AboutPopup>()  // No ViewModel needed

            // Popup without ViewModel
            .AddTransientPopup<SimpleNotificationPopup>();

        return builder.Build();
    }
}
```

---

## ViewModel Assignment

When a popup with an associated ViewModel is pushed, the library automatically:

1. Resolves the ViewModel from the DI container
2. Sets the popup's `BindingContext` to the ViewModel
3. Calls `OnPopupNavigatedAsync` on the ViewModel with navigation parameters

This process is handled by `MauiViewModelAssignmentStrategy` which is configured automatically when you call `UseUXDiversPopups()`.

For more details on ViewModel integration, see the [MVVM](MVVM.md) page.

---

## Best Practices

1. **Register during startup**: Register all popups in `MauiProgram.cs` before `builder.Build()` is called.

2. **Use transient for data-driven popups**: If a popup displays different data each time, use transient registration.

3. **Use singleton sparingly**: Only use singleton for popups that genuinely need to maintain state across shows.

4. **Leverage constructor injection**: Take advantage of DI by injecting services into your ViewModels rather than using service locator patterns.

5. **Register services before popups**: Ensure all services that ViewModels depend on are registered before registering the popups.
