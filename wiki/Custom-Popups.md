# Custom Popups

This page explains how to create custom popups by inheriting from `PopupPage` and `PopupResultPage<T>`, how to modify styles, override control templates, and customize the theme.

## Creating a Custom Popup from PopupPage

The simplest way to create a custom popup is to inherit from `PopupPage`:

### XAML Definition

**MyCustomPopup.xaml**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<uxd:PopupPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:uxd="clr-namespace:UXDivers.Popups.Maui;assembly=UXDivers.Popups.Maui"
    x:Class="YourApp.Popups.MyCustomPopup"
    BackgroundColor="{DynamicResource PopupBackdropColor}"
    AppearingAnimation="{uxd:MoveInPopupAnimation MoveDirection=Bottom, Duration=300, Easing=CubicOut}"
    DisappearingAnimation="{uxd:MoveOutPopupAnimation MoveDirection=Bottom, Duration=300, Easing=CubicIn}"
    CloseWhenBackgroundIsClicked="True">

    <Border
        VerticalOptions="End"
        HorizontalOptions="Fill"
        BackgroundColor="{DynamicResource BackgroundColor}"
        Padding="24">
        <Border.StrokeShape>
            <RoundRectangle CornerRadius="20,20,0,0" />
        </Border.StrokeShape>

        <VerticalStackLayout Spacing="16">
            <Label
                Text="Custom Popup"
                Style="{DynamicResource TitleStyle}"
                HorizontalTextAlignment="Center" />

            <Label
                Text="This is a custom popup with your own design!"
                Style="{DynamicResource SubTitleStyle}"
                HorizontalTextAlignment="Center" />

            <Button
                Text="Close"
                Style="{DynamicResource PrimaryActionButtonStyle}"
                Clicked="OnCloseClicked" />
        </VerticalStackLayout>
    </Border>
</uxd:PopupPage>
```

### Code-Behind

**MyCustomPopup.xaml.cs**
```csharp
using UXDivers.Popups;
using UXDivers.Popups.Services;

namespace YourApp.Popups;

public partial class MyCustomPopup : PopupPage
{
    public MyCustomPopup()
    {
        InitializeComponent();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await IPopupService.Current.PopAsync(this);
    }

    // Override lifecycle methods if needed
    public override void OnNavigatedTo(IReadOnlyDictionary<string, object?> parameters)
    {
        base.OnNavigatedTo(parameters);
        // Handle navigation parameters
    }
}
```

---

## Creating a Custom Popup with Return Value

For popups that need to return data, inherit from `PopupResultPage<T>`:

**ConfirmationPopup.xaml**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<uxd:PopupResultPage
    x:TypeArguments="x:Boolean"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:uxd="clr-namespace:UXDivers.Popups.Maui;assembly=UXDivers.Popups.Maui"
    x:Class="YourApp.Popups.ConfirmationPopup"
    BackgroundColor="{DynamicResource PopupBackdropColor}"
    CloseWhenBackgroundIsClicked="False">

    <Border
        VerticalOptions="Center"
        HorizontalOptions="Center"
        BackgroundColor="{DynamicResource BackgroundSecondaryColor}"
        Padding="24"
        WidthRequest="300">
        <Border.StrokeShape>
            <RoundRectangle CornerRadius="16" />
        </Border.StrokeShape>

        <VerticalStackLayout Spacing="16">
            <Label
                Text="Confirm Action"
                Style="{DynamicResource TitleStyle}"
                HorizontalTextAlignment="Center" />

            <Label
                x:Name="MessageLabel"
                Style="{DynamicResource SubTitleStyle}"
                HorizontalTextAlignment="Center" />

            <Grid ColumnDefinitions="*,*" ColumnSpacing="12">
                <Button
                    Text="Cancel"
                    Style="{DynamicResource SecondaryActionButtonStyle}"
                    Clicked="OnCancelClicked" />
                <Button
                    Grid.Column="1"
                    Text="Confirm"
                    Style="{DynamicResource PrimaryActionButtonStyle}"
                    Clicked="OnConfirmClicked" />
            </Grid>
        </VerticalStackLayout>
    </Border>
</uxd:PopupResultPage>
```

**ConfirmationPopup.xaml.cs**
```csharp
using UXDivers.Popups;
using UXDivers.Popups.Services;

namespace YourApp.Popups;

public partial class ConfirmationPopup : PopupResultPage<bool>
{
    public ConfirmationPopup()
    {
        InitializeComponent();
    }

    public override void OnNavigatedTo(IReadOnlyDictionary<string, object?> parameters)
    {
        base.OnNavigatedTo(parameters);

        if (parameters.TryGetValue("message", out var message))
        {
            MessageLabel.Text = message?.ToString() ?? "Are you sure?";
        }
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        SetResult(true);
        await IPopupService.Current.PopAsync(this);
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        SetResult(false);
        await IPopupService.Current.PopAsync(this);
    }
}
```

**Usage:**
```csharp
var popup = new ConfirmationPopup();
var parameters = new Dictionary<string, object?>
{
    { "message", "Do you want to delete this item?" }
};

bool confirmed = await IPopupService.Current.PushAsync(popup, parameters);

if (confirmed)
{
    // User confirmed, proceed with action
}
```

---

## Inheriting and Modifying Default Styles

The library provides default styles for each popup type. You can create a custom popup that inherits from a pre-built control and modify its style.

### Inheriting from a Pre-built Popup

```csharp
using UXDivers.Popups.Maui.Controls;

namespace YourApp.Popups;

public class CustomToast : Toast
{
    public CustomToast()
    {
        // Customize properties
        Title = "Default Title";
    }
}
```

### Modifying the Style in XAML

Create a new style that inherits from the default style:

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <uxd:DarkTheme />
            <uxd:PopupStyles />
        </ResourceDictionary.MergedDictionaries>

        <!-- Custom Toast Style inheriting from default -->
        <Style x:Key="CustomToastStyle" TargetType="local:Toast" BasedOn="{StaticResource DefaultToastStyle}">
            <Setter Property="BackgroundColor" Value="#1A1A2E" />
            <Setter Property="AppearingAnimation" Value="{uxd:ScaleInPopupAnimation Duration=200}" />
        </Style>
    </ResourceDictionary>
</Application.Resources>
```

Apply the style:
```xml
<local:Toast Style="{StaticResource CustomToastStyle}" Title="Custom Styled Toast" />
```

Or if you want to override the default style just don't add a 'x:key' to the style.

---

## Overriding the Control Template

Each pre-built popup has a control template that defines its visual structure. You can completely replace this template to create a different look.

### Understanding the Default Template Structure

The default popups use `ControlTemplate` with bindings to their properties. Here's a simplified example of the Toast template:

```xml
<ControlTemplate>
    <Grid x:DataType="local:Toast" VerticalOptions="Start">
        <!-- Background -->
        <Border BackgroundColor="{DynamicResource BackgroundColor}">
            <!-- Content -->
            <HorizontalStackLayout>
                <Label Text="{Binding IconText, Source={RelativeSource TemplatedParent}}" />
                <Label Text="{Binding Title, Source={RelativeSource TemplatedParent}}" />
            </HorizontalStackLayout>
        </Border>
    </Grid>
</ControlTemplate>
```

### Creating a Custom Control Template

Override the template in your style:

```xml
<Style x:Key="MyCustomToastStyle" TargetType="controls:Toast" BasedOn="{StaticResource PopupBaseStyle}">
    <Setter Property="ControlTemplate">
        <ControlTemplate>
            <Grid
                x:DataType="controls:Toast"
                VerticalOptions="End"
                HorizontalOptions="Fill"
                Padding="20">

                <!-- Custom Background -->
                <Border
                    BackgroundColor="#2D2D44"
                    StrokeShape="RoundRectangle 12"
                    Padding="16,12"
                    Margin="-20">

                    <HorizontalStackLayout Spacing="12">
                        <!-- Custom Icon Layout -->
                        <Border
                            BackgroundColor="#4CAF50"
                            StrokeShape="RoundRectangle 20"
                            WidthRequest="40"
                            HeightRequest="40"
                            Padding="0">
                            <Label
                                Text="{Binding IconText, Source={RelativeSource TemplatedParent}}"
                                TextColor="White"
                                FontFamily="{DynamicResource IconsFontFamily}"
                                HorizontalOptions="Center"
                                VerticalOptions="Center"
                                FontSize="20" />
                        </Border>

                        <!-- Title with custom styling -->
                        <Label
                            Text="{Binding Title, Source={RelativeSource TemplatedParent}}"
                            TextColor="White"
                            FontSize="16"
                            FontAttributes="Bold"
                            VerticalOptions="Center" />
                    </HorizontalStackLayout>
                </Border>
            </Grid>
        </ControlTemplate>
    </Setter>
</Style>
```

### Using ContentPresenter

For popups like `ActionModalPopup` that support custom content, use `ContentPresenter`:

```xml
<ControlTemplate>
    <Grid RowDefinitions="Auto,*,Auto">
        <!-- Header -->
        <Label Text="{Binding Title, Source={RelativeSource TemplatedParent}}" />

        <!-- Content Area - displays PopupContent -->
        <ContentPresenter Grid.Row="1" />

        <!-- Footer with action button -->
        <Button
            Grid.Row="2"
            Text="{Binding ActionButtonText, Source={RelativeSource TemplatedParent}}"
            Command="{Binding ActionButtonCommand, Source={RelativeSource TemplatedParent}}" />
    </Grid>
</ControlTemplate>
```

---

## Customizing the DarkTheme

The `DarkTheme` resource dictionary contains all the colors, fonts, spacing, and sizing resources used by the popups.

### Theme Resource Categories

#### Colors

```xml
<!-- Override in your App.xaml -->
<Color x:Key="PrimaryColor">#FF6B6B</Color>
<Color x:Key="PrimaryVariantColor">#4A1D1D</Color>

<Color x:Key="BackgroundColor">#121212</Color>
<Color x:Key="BackgroundSecondaryColor">#1E1E1E</Color>
<Color x:Key="BackgroundTertiaryColor">#2A2A2A</Color>

<Color x:Key="PopupBackdropColor">#CC000000</Color>
<Color x:Key="PopupBorderColor">#333333</Color>

<Color x:Key="TextColor">#FFFFFF</Color>
<Color x:Key="TextSecondaryColor">#B0B0B0</Color>
<Color x:Key="TextTertiaryColor">#808080</Color>
```

#### Fonts

```xml
<x:String x:Key="IconsFontFamily">FluentUI</x:String>
<x:String x:Key="AppFontFamily">Inter-Regular</x:String>
<x:String x:Key="AppSemiBoldFamily">Inter-SemiBold</x:String>
```

#### Spacing

```xml
<Thickness x:Key="AirSpacing">30</Thickness>
<Thickness x:Key="PopupAirSpacing">24</Thickness>

<x:Double x:Key="SpacingXBig">24</x:Double>
<x:Double x:Key="SpacingBig">20</x:Double>
<x:Double x:Key="SpacingMedium">16</x:Double>
<x:Double x:Key="SpacingSmall">12</x:Double>
```

#### Corner Radius

```xml
<CornerRadius x:Key="CornerRadiusXBig">20</CornerRadius>
<CornerRadius x:Key="CornerRadiusBig">16</CornerRadius>
<x:Int32 x:Key="BaseButtonCornerRadius">20</x:Int32>
```

#### Font Sizes

```xml
<OnIdiom x:Key="TinyFontSize" x:TypeArguments="x:Double" Phone="10" Tablet="14" Desktop="14" />
<OnIdiom x:Key="SmallFontSize" x:TypeArguments="x:Double" Phone="12" Tablet="16" Desktop="16" />
<OnIdiom x:Key="BaseFontSize" x:TypeArguments="x:Double" Phone="14" Tablet="16" Desktop="16" />
<OnIdiom x:Key="LargeFontSize" x:TypeArguments="x:Double" Phone="16" Tablet="18" Desktop="18" />
<OnIdiom x:Key="SmallTitleFontSize" x:TypeArguments="x:Double" Phone="18" Tablet="22" Desktop="22" />
<OnIdiom x:Key="TitleFontSize" x:TypeArguments="x:Double" Phone="24" Tablet="32" Desktop="32" />
```

### Creating a Light Theme

You can create a light theme by overriding the color resources:

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <uxd:DarkTheme />
            <uxd:PopupStyles />
        </ResourceDictionary.MergedDictionaries>

        <!-- Light Theme Overrides -->
        <Color x:Key="BackgroundColor">#FFFFFF</Color>
        <Color x:Key="BackgroundSecondaryColor">#F5F5F5</Color>
        <Color x:Key="BackgroundTertiaryColor">#EEEEEE</Color>

        <Color x:Key="PopupBackdropColor">#88000000</Color>
        <Color x:Key="PopupBorderColor">#E0E0E0</Color>

        <Color x:Key="TextColor">#212121</Color>
        <Color x:Key="TextSecondaryColor">#757575</Color>
        <Color x:Key="TextTertiaryColor">#9E9E9E</Color>
    </ResourceDictionary>
</Application.Resources>
```

### Overriding Icon Glyphs

You can override icon glyphs using string values or static references to a font class.

**Using x:Static references (recommended):**

```xml
<!-- Define a static class with icon constants -->
<!-- In your code: public static class MaterialSymbolsFont { public const string Close = "\ue5cd"; } -->

<x:Static x:Key="UXDPopupsCloseIconButton" Member="local:MaterialSymbolsFont.Close" />
<x:Static x:Key="UXDPopupsCheckCircleIconButton" Member="local:MaterialSymbolsFont.Check_circle" />
```

**Complete DemoApp Example:**

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <uxdc:DarkTheme />
            <uxdc:PopupStyles />
        </ResourceDictionary.MergedDictionaries>

        <!-- Font Configuration -->
        <x:String x:Key="IconsFontFamily">MaterialSymbols</x:String>
        <x:String x:Key="AppFontFamily">Manrope</x:String>
        <x:String x:Key="AppSemiBoldFamily">ManropeSemibold</x:String>

        <!-- Icon Glyphs using static references -->
        <x:Static x:Key="UXDPopupsCloseIconButton" Member="local:MaterialSymbolsFont.Close" />
        <x:Static x:Key="UXDPopupsCheckCircleIconButton" Member="local:MaterialSymbolsFont.Check_circle" />

        <!-- Custom Icon Colors -->
        <Color x:Key="IconOrange">#FF7134</Color>
        <Color x:Key="IconMagenta">#FF1AD9</Color>
        <Color x:Key="IconCyan">#05D9FF</Color>
        <Color x:Key="IconGreen">#2FFF74</Color>
        <Color x:Key="IconPurple">#BD3BFF</Color>
        <Color x:Key="IconBlue">#1C7BFF</Color>
        <Color x:Key="IconLime">#C8FF01</Color>
        <Color x:Key="IconRed">#FF0000</Color>
    </ResourceDictionary>
</Application.Resources>
```

---

## Pre-built Popup Controls

For detailed documentation on each pre-built popup control, see the [Popup Controls](Popup-Controls.md) page:

| Control | Description |
|---------|-------------|
| [Toast](Popup-Controls.md#toast) | Brief notification with icon and title |
| [FloaterPopup](Popup-Controls.md#floaterpopup) | Floating alert with icon and message |
| [SimpleTextPopup](Popup-Controls.md#simpletextpopup) | Informational popup with title and text |
| [SimpleActionPopup](Popup-Controls.md#simpleactionpopup) | Confirmation dialog with two buttons |
| [IconTextPopup](Popup-Controls.md#icontextpopup) | Prominent icon with title, text, and action |
| [ActionModalPopup](Popup-Controls.md#actionmodalpopup) | Modal with close button and content area |
| [ListActionPopup](Popup-Controls.md#listactionpopup) | Scrollable list with action button |
| [OptionSheetPopup](Popup-Controls.md#optionsheetpopup) | Bottom sheet with selectable options |
| [FormPopup](Popup-Controls.md#formpopup) | User input form returning results |
