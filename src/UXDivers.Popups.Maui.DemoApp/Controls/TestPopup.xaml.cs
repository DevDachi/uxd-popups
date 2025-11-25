using System.Windows.Input;
using UXDivers.Popups.Services;

namespace UXDivers.Popups.Maui.DemoApp;

public partial class TestPopup : PopupPage
{
    public static readonly BindableProperty IconTextProperty = BindableProperty.Create(
        nameof(IconText),
        typeof(string),
        typeof(TestPopup),
        null);

    public string IconText
    {
        get { return (string)GetValue(IconTextProperty); }
        set { SetValue(IconTextProperty, value); }
    }
    
    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
        nameof(IconColor),
        typeof(Color),
        typeof(TestPopup),
        null);

    public Color IconColor
    {
        get { return (Color)GetValue(IconColorProperty); }
        set { SetValue(IconColorProperty, value); }
    }
    
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(TestPopup),
        null);

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(TestPopup),
        null);

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    
    public static readonly BindableProperty CloseButtonIconTextProperty = BindableProperty.Create(
        nameof(CloseButtonIconText),
        typeof(string),
        typeof(TestPopup),
        null);

    public string CloseButtonIconText
    {
        get { return (string)GetValue(CloseButtonIconTextProperty); }
        set { SetValue(CloseButtonIconTextProperty, value); }
    }
    
    public static readonly BindableProperty CloseButtonIconColorProperty = BindableProperty.Create(
        nameof(CloseButtonIconColor),
        typeof(Color),
        typeof(TestPopup),
        null);

    public Color CloseButtonIconColor
    {
        get { return (Color)GetValue(CloseButtonIconColorProperty); }
        set { SetValue(CloseButtonIconColorProperty, value); }
    }

    public static readonly BindableProperty CloseButtonCommandProperty = BindableProperty.Create(
        nameof(CloseButtonCommand),
        typeof(ICommand),
        typeof(TestPopup),
        defaultValueCreator: _ => new Command(async () => await IPopupService.Current.PopAsync()));

    public ICommand CloseButtonCommand
    {
        get { return (ICommand)GetValue(CloseButtonCommandProperty); }
        set { SetValue(CloseButtonCommandProperty, value); }
    }
    
    public TestPopup()
    {
        InitializeComponent();
    }
}