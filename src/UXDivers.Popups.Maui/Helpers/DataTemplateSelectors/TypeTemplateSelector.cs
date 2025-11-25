namespace UXDivers.Popups.Maui;

[ContentProperty("Items")]
public class TypeTemplateSelector : DataTemplateSelector
{
    public List<TypeTemplateSelectorItem> Items { get; } = [];
    
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item != null)
        {
            var itemType = item.GetType();
            var matchedItem = Items.FirstOrDefault(i => i.TargetType == itemType);
            if (matchedItem != null)
            {
                if (matchedItem.Template == null)
                {
                    return MissingTemplate($"MISSING TEMPLATE FOR TYPE FOUND IN ${nameof(TypeTemplateSelectorItem)}: {item.GetType().FullName}");
                }

                return matchedItem.Template;
            }
        }

        return MissingTemplate($"MISSING TEMPLATE FOR TYPE: {item?.GetType().FullName ?? "null"}");
    }

    private DataTemplate MissingTemplate(string message)
    {
        return new DataTemplate(() => {
            return new ContentView
            {
                Content = new Label
                {
                    Text = message,
                    TextColor = Colors.Red,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }
            };
        });
    }
}
