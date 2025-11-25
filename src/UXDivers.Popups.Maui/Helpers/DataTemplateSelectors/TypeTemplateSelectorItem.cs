namespace UXDivers.Popups.Maui;

[ContentProperty("Template")]
public class TypeTemplateSelectorItem
{
    public Type? TargetType { get; set; }
    public DataTemplate? Template { get; set; }
}