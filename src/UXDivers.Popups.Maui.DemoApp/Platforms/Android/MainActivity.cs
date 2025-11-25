using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.View;
using Microsoft.Maui.Platform;

namespace UXDivers.Popups.Maui.DemoApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (Window == null)
        {
            return;
        }
#pragma warning disable CA1422
        Window.SetStatusBarColor(Color.FromArgb("#0A0A0A").ToPlatform());
#pragma warning restore CA1422

        var controller = WindowCompat.GetInsetsController(Window, Window.DecorView);
        if (controller is not null)
            controller.AppearanceLightStatusBars = false;
    }
}
