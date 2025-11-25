namespace UXDivers.Popups.Maui;


public class BindableLayoutProperties
{
    public static readonly BindableProperty FlexibleItemTemplateProperty =
        BindableProperty.CreateAttached("FlexibleItemTemplate",
            typeof(DataTemplate),
            typeof(BindableLayoutProperties),
            null,
            propertyChanged: OnFlexibleItemTemplateChanged);

    public static DataTemplate GetFlexibleItemTemplate(BindableObject bo)
    {
        return (DataTemplate)bo.GetValue(FlexibleItemTemplateProperty);
    }

    public static void SetFlexibleItemTemplate(BindableObject bo, DataTemplate value)
    {
        bo.SetValue(FlexibleItemTemplateProperty, value);
    }

    private static void OnFlexibleItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Layout layout)
        {
            return;
        }

        if (newValue is DataTemplateSelector selector)
        {
            BindableLayout.SetItemTemplate(layout, null);
            BindableLayout.SetItemTemplateSelector(layout, selector);
            return;
        }

        if (newValue is DataTemplate template)
        {
            BindableLayout.SetItemTemplateSelector(layout, null);
            BindableLayout.SetItemTemplate(layout, template);
            return;
        }
        
        BindableLayout.SetItemTemplate(layout, null);
        BindableLayout.SetItemTemplateSelector(layout, null);
    }
}