# MVVM

This page explains how to use the MVVM (Model-View-ViewModel) pattern with UXDivers Popups, including the `IPopupViewModel` interface and automatic ViewModel creation.

## Overview

UXDivers Popups is designed with MVVM in mind. The library provides:

- `IPopupViewModel` interface for ViewModels to receive navigation parameters
- Automatic ViewModel resolution from the DI container
- Automatic `BindingContext` assignment
- Support for data binding in popup XAML

## IPopupViewModel Interface

The `IPopupViewModel` interface allows ViewModels to receive navigation parameters when a popup is shown:

```csharp
public interface IPopupViewModel
{
    Task OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters);
}
```

### Implementing IPopupViewModel

```csharp
public class MyPopupViewModel : IPopupViewModel
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public async Task OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
    {
        // Process navigation parameters
        if (parameters.TryGetValue("title", out var title))
        {
            Title = title?.ToString() ?? string.Empty;
        }

        if (parameters.TryGetValue("message", out var message))
        {
            Message = message?.ToString() ?? string.Empty;
        }

        // You can also perform async operations
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        // Load data from services
    }
}
```

---

## Automatic ViewModel Creation

When you register a popup with a ViewModel using DI, the library automatically creates and assigns the ViewModel when the popup is pushed.

### Setup

1. Create your ViewModel implementing `IPopupViewModel`:

```csharp
public class ProductDetailViewModel : IPopupViewModel
{
    private readonly IProductService _productService;

    public Product? Product { get; private set; }
    public bool IsLoading { get; private set; }

    public ProductDetailViewModel(IProductService productService)
    {
        _productService = productService;
    }

    public async Task OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
    {
        if (parameters.TryGetValue("productId", out var productId) && productId is int id)
        {
            IsLoading = true;
            Product = await _productService.GetProductAsync(id);
            IsLoading = false;
        }
    }
}
```

2. Create your popup with data binding:

**ProductDetailPopup.xaml**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<uxd:PopupPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:uxd="clr-namespace:UXDivers.Popups.Maui;assembly=UXDivers.Popups.Maui"
    xmlns:vm="clr-namespace:YourApp.ViewModels"
    x:Class="YourApp.Popups.ProductDetailPopup"
    x:DataType="vm:ProductDetailViewModel"
    BackgroundColor="{DynamicResource PopupBackdropColor}">

    <Border
        VerticalOptions="Center"
        HorizontalOptions="Center"
        BackgroundColor="{DynamicResource BackgroundSecondaryColor}"
        Padding="24">

        <VerticalStackLayout Spacing="16">
            <!-- Loading indicator -->
            <ActivityIndicator
                IsRunning="{Binding IsLoading}"
                IsVisible="{Binding IsLoading}" />

            <!-- Product details -->
            <VerticalStackLayout IsVisible="{Binding IsLoading, Converter={StaticResource InvertedBoolConverter}}">
                <Label
                    Text="{Binding Product.Name}"
                    Style="{DynamicResource TitleStyle}" />

                <Label
                    Text="{Binding Product.Description}"
                    Style="{DynamicResource SubTitleStyle}" />

                <Label
                    Text="{Binding Product.Price, StringFormat='${0:F2}'}"
                    Style="{DynamicResource TitleStyle}"
                    TextColor="{DynamicResource PrimaryColor}" />
            </VerticalStackLayout>
        </VerticalStackLayout>
    </Border>
</uxd:PopupPage>
```

3. Register the popup with its ViewModel:

```csharp
// In MauiProgram.cs
builder.Services.AddTransientPopup<ProductDetailPopup, ProductDetailViewModel>();
```

4. Show the popup with parameters:

```csharp
await IPopupService.Current.PushAsync<ProductDetailPopup>(new Dictionary<string, object?>
{
    { "productId", 42 }
});
```

### What Happens Automatically

When you call `PushAsync<ProductDetailPopup>()`:

1. The library resolves `ProductDetailPopup` from the DI container
2. The library checks if the popup already has a `BindingContext` assigned
3. If no BindingContext exists, the library resolves `ProductDetailViewModel` from the DI container and assigns it
4. `OnPopupNavigatedAsync` is called on the ViewModel with the navigation parameters
5. The popup is displayed with animations


### Alternative: Push an Instance

You can also push an instance of the popup:

```csharp
var popup = new ProductDetailPopup();

await IPopupService.Current.PushAsync(popup, new Dictionary<string, object?>
{
    { "productId", 42 }
});
```

### What Happens Automatically

When you call `PushAsync(popup, parameters)`:

1. The library checks if the popup already has a `BindingContext` assigned
2. If no BindingContext exists, the library resolves `ProductDetailViewModel` from the DI container and assigns it
3. `OnPopupNavigatedAsync` is called on the ViewModel with the navigation parameters
4. The popup is displayed with animations

---

## Pre-Assigned ViewModel (Skip Automatic Creation)

If a popup already has a `BindingContext` set (either in XAML, in the constructor, or before calling `PushAsync`), the library will **not** create or assign a new ViewModel. This allows you to manually control the ViewModel instance.

### Example: Assigning ViewModel in Constructor

```csharp
public partial class CustomPopup : PopupPage
{
    public CustomPopup(MyCustomViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;  // Pre-assign the ViewModel
    }
}

// Usage
var viewModel = new MyCustomViewModel { Title = "Custom Title" };
var popup = new CustomPopup(viewModel);

// The library will NOT create a new ViewModel since BindingContext is already set
await IPopupService.Current.PushAsync(popup);
```

### Example: Assigning ViewModel Before Push

```csharp
var popup = new ProductDetailPopup();

// Manually create and configure the ViewModel
var viewModel = new ProductDetailViewModel(productService)
{
    Product = cachedProduct  // Pre-populate with cached data
};
popup.BindingContext = viewModel;

// The library will NOT override the existing BindingContext
await IPopupService.Current.PushAsync(popup, new Dictionary<string, object?>
{
    { "productId", 42 }
});

// Note: OnPopupNavigatedAsync will still be called on the existing ViewModel
```


### Navigation Parameters Still Work

Even when using a pre-assigned ViewModel, `OnPopupNavigatedAsync` is still called with the navigation parameters:

```csharp
var viewModel = new ProductDetailViewModel(productService);
popup.BindingContext = viewModel;

// Parameters are passed to the existing ViewModel
await IPopupService.Current.PushAsync(popup, new Dictionary<string, object?>
{
    { "productId", 42 }
});

// viewModel.OnPopupNavigatedAsync() is called with the parameters
```

---

## ViewModel with Commands

Use commands in your ViewModel for user interactions:

```csharp
public class ConfirmationViewModel : IPopupViewModel
{
    public string Title { get; private set; } = "Confirm";
    public string Message { get; private set; } = "Are you sure?";

    public ICommand ConfirmCommand { get; }
    public ICommand CancelCommand { get; }

    private TaskCompletionSource<bool>? _resultSource;

    public ConfirmationViewModel()
    {
        ConfirmCommand = new Command(async () =>
        {
            _resultSource?.TrySetResult(true);
            await IPopupService.Current.PopAsync();
        });

        CancelCommand = new Command(async () =>
        {
            _resultSource?.TrySetResult(false);
            await IPopupService.Current.PopAsync();
        });
    }

    public Task OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
    {
        if (parameters.TryGetValue("title", out var title))
            Title = title?.ToString() ?? Title;

        if (parameters.TryGetValue("message", out var message))
            Message = message?.ToString() ?? Message;

        return Task.CompletedTask;
    }
}
```

**ConfirmationPopup.xaml**
```xml
<uxd:PopupPage
    x:DataType="vm:ConfirmationViewModel"
    ...>
    <VerticalStackLayout>
        <Label Text="{Binding Title}" />
        <Label Text="{Binding Message}" />

        <Grid ColumnDefinitions="*,*">
            <Button Text="Cancel" Command="{Binding CancelCommand}" />
            <Button Grid.Column="1" Text="Confirm" Command="{Binding ConfirmCommand}" />
        </Grid>
    </VerticalStackLayout>
</uxd:PopupPage>
```

---

## ViewModel with Observable Properties

For reactive UIs, use `INotifyPropertyChanged`:

```csharp
public class SearchPopupViewModel : IPopupViewModel, INotifyPropertyChanged
{
    private readonly ISearchService _searchService;
    private string _searchQuery = string.Empty;
    private ObservableCollection<SearchResult> _results = new();
    private bool _isSearching;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            if (_searchQuery != value)
            {
                _searchQuery = value;
                OnPropertyChanged();
                SearchCommand.ChangeCanExecute();
            }
        }
    }

    public ObservableCollection<SearchResult> Results
    {
        get => _results;
        set
        {
            _results = value;
            OnPropertyChanged();
        }
    }

    public bool IsSearching
    {
        get => _isSearching;
        set
        {
            _isSearching = value;
            OnPropertyChanged();
        }
    }

    public Command SearchCommand { get; }

    public SearchPopupViewModel(ISearchService searchService)
    {
        _searchService = searchService;

        SearchCommand = new Command(
            async () => await PerformSearchAsync(),
            () => !string.IsNullOrWhiteSpace(SearchQuery));
    }

    public Task OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
    {
        if (parameters.TryGetValue("initialQuery", out var query))
        {
            SearchQuery = query?.ToString() ?? string.Empty;
        }
        return Task.CompletedTask;
    }

    private async Task PerformSearchAsync()
    {
        IsSearching = true;
        var results = await _searchService.SearchAsync(SearchQuery);
        Results = new ObservableCollection<SearchResult>(results);
        IsSearching = false;
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

---

## Using Community Toolkit MVVM

The library works great with [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/):

```csharp
public partial class UserProfileViewModel : ObservableObject, IPopupViewModel
{
    private readonly IUserService _userService;

    [ObservableProperty]
    private User? _user;

    [ObservableProperty]
    private bool _isLoading;

    public UserProfileViewModel(IUserService userService)
    {
        _userService = userService;
    }

    public async Task OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
    {
        if (parameters.TryGetValue("userId", out var userId) && userId is int id)
        {
            IsLoading = true;
            User = await _userService.GetUserAsync(id);
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SaveChangesAsync()
    {
        if (User != null)
        {
            await _userService.UpdateUserAsync(User);
            await IPopupService.Current.PopAsync();
        }
    }

    [RelayCommand]
    private async Task CloseAsync()
    {
        await IPopupService.Current.PopAsync();
    }
}
```

---

## Popup Without ViewModel

Not all popups need a ViewModel. For simple popups, you can handle everything in the code-behind:

```csharp
public partial class SimpleAlertPopup : PopupPage
{
    public SimpleAlertPopup()
    {
        InitializeComponent();
    }

    public override void OnNavigatedTo(IReadOnlyDictionary<string, object?> parameters)
    {
        base.OnNavigatedTo(parameters);

        if (parameters.TryGetValue("message", out var message))
        {
            MessageLabel.Text = message?.ToString();
        }
    }

    private async void OnOkClicked(object sender, EventArgs e)
    {
        await IPopupService.Current.PopAsync(this);
    }
}
```

Register without ViewModel:

```csharp
builder.Services.AddTransientPopup<SimpleAlertPopup>();
```

