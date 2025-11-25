using System.Globalization;

namespace UXDivers.Popups.Maui.DemoApp;

public partial class SliderTemplate : ContentView
{
    public static readonly BindableProperty PropertyNameProperty = BindableProperty.Create(
        nameof(PropertyName),
        typeof(string),
        typeof(SliderTemplate));

    public string PropertyName
    {
        get { return (string)GetValue(PropertyNameProperty); }
        set { SetValue(PropertyNameProperty, value); }
    }
    
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        nameof(Value),
        typeof(double),
        typeof(SliderTemplate)
        );

    public double Value
    {
        get { return (double)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value);  }
    }
    
    public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(
        nameof(MaxValue),
        typeof(double),
        typeof(SliderTemplate),
        1d
    );

    public double MaxValue
    {
        get { return (double)GetValue(MaxValueProperty); }
        set { SetValue(MaxValueProperty, value);  }
    }
    
    public static readonly BindableProperty MinValueProperty = BindableProperty.Create(
        nameof(MinValue),
        typeof(double),
        typeof(SliderTemplate),
        0d
    );

    public double MinValue
    {
        get { return (double)GetValue(MinValueProperty); }
        set { SetValue(MinValueProperty, value);  }
    }
    
    public SliderTemplate()
    {
        InitializeComponent();
    }
}