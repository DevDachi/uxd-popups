namespace UXDivers.Popups;

/// <summary>
/// Interface for ViewModels that want to receive navigation parameters when used in popups.
/// </summary>
public interface IPopupViewModel
{
    Task OnPopupNavigatedAsync(IReadOnlyDictionary<string, object?> parameters);
}
