namespace UXDivers.Popups.Maui.DemoApp;

public class HeadphonesActionSheetViewModel : IPopupViewModel
{
    public async Task OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters)
    {
        foreach (var item in parameters)
        {
            System.Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
        }
    }
}   