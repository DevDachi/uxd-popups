namespace UXDivers.Popups.Maui;

/// <summary>
/// Helper class for converting <see cref="EasingType"/> to MAUI's <see cref="Easing"/>.
/// </summary>
public static class EasingHelper
{
    /// <summary>
    /// Converts an <see cref="EasingType"/> to the corresponding MAUI <see cref="Easing"/>.
    /// </summary>
    /// <param name="type">The easing type to convert.</param>
    /// <returns>The corresponding <see cref="Easing"/>.</returns>
    public static Easing ToMauiEasing(this EasingType type)
    {
        switch (type)
        {
            case EasingType.BounceIn:
                return Easing.BounceIn;
            case EasingType.BounceOut:
                return Easing.BounceOut;
            case EasingType.CubicIn:
                return Easing.CubicIn;
            case EasingType.CubicInOut:
                return Easing.CubicInOut;
            case EasingType.CubicOut:
                return Easing.CubicOut;
            case EasingType.SinIn:
                return Easing.SinIn;
            case EasingType.SinInOut:
                return Easing.SinInOut;
            case EasingType.SinOut:
                return Easing.SinOut;
            case EasingType.SpringIn:
                return Easing.SpringIn;
            case EasingType.SpringOut:
                return Easing.SpringOut;
            case EasingType.Linear:
            case EasingType.Default:
            default:
                return Easing.Linear;
        }
    }
}
