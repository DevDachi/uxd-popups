namespace UXDivers.Popups.Services;

internal class PopupRegistry
{
    public Type? ViewModelType { get; init; }
    public RegistryLifetime? Lifetime { get; init; }
    public IPopupViewModel? ViewModelInstance { get; set; }
}