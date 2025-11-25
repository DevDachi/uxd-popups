using UXDivers.Popups.Maui.Controls;

namespace UXDivers.Popups.Maui.DemoApp;

public class CustomSimpleTextPopup : SimpleTextPopup
{
    public CustomSimpleTextPopup()
    {
    }

    public override void OnNavigatedTo(IReadOnlyDictionary<string, object?> parameters)
    {
        base.OnNavigatedTo(parameters);

        if (parameters.ContainsKey("Title"))
        {
            Title = parameters["Title"] as string ?? string.Empty;
        }

        if (parameters.ContainsKey("Text"))
        {
            Text = parameters["Text"] as string ?? string.Empty;
        }
    }
}