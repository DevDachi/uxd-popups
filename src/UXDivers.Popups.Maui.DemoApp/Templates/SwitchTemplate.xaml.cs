using System.ComponentModel;
using System.Windows.Input;

namespace UXDivers.Popups.Maui.DemoApp;

public partial class SwitchTemplate : ContentView
{
    public static readonly BindableProperty NameProperty = BindableProperty.Create(
        nameof(PropertyName),
        typeof(string),
        typeof(SwitchTemplate));

    public string PropertyName
    {
        get { return (string)GetValue(NameProperty); }
        set { SetValue(NameProperty, value); }
    }

    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(PropertyDescription),
        typeof(string),
        typeof(SwitchTemplate));

    public string PropertyDescription
    {
        get { return (string)GetValue(DescriptionProperty); }
        set { SetValue(DescriptionProperty, value); }
    }

    public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(
        nameof(IsToggled),
        typeof(bool),
        typeof(SwitchTemplate));

    public bool IsToggled
    {
        get { return (bool)GetValue(IsToggledProperty); }
        set { SetValue(IsToggledProperty, value);  }
    }
    
    public SwitchTemplate()
    {
        InitializeComponent();
    
    }
}