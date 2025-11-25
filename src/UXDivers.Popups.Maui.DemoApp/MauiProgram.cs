using Microsoft.Extensions.Logging;
using UXDivers.Popups.Maui;
using Microsoft.Maui.Handlers;

namespace UXDivers.Popups.Maui.DemoApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseUXDiversPopups()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("MaterialSymbols.ttf", "MaterialSymbols");
                fonts.AddFont("Manrope-Regular.ttf", "Manrope");
                fonts.AddFont("Manrope-Semibold.ttf", "ManropeSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services
            .AddTransientPopup<CustomSimpleTextPopup>()
            .AddTransientPopup<HeadphonesActionSheetPopup, HeadphonesActionSheetViewModel>();


#if IOS || MACCATALYST
        EntryHandler.Mapper.AppendToMapping("NoBorder", (handler, view) =>
        {
            // Remove border
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;

            // Optional: transparent background
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;

            // Optional: add a tiny left padding so text isn't flush
            handler.PlatformView.LeftView = new UIKit.UIView(new CoreGraphics.CGRect(0, 0, 4, 0));
            handler.PlatformView.LeftViewMode = UIKit.UITextFieldViewMode.Always;
        });

        PickerHandler.Mapper.AppendToMapping("NoBorder", (handler, view) =>
        {
            // Remove border + make background transparent
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;

            // Optional: add a tiny left padding so text isn't flush to the edge
            handler.PlatformView.LeftView = new UIKit.UIView(new CoreGraphics.CGRect(0, 0, 4, 0));
            handler.PlatformView.LeftViewMode = UIKit.UITextFieldViewMode.Always;
        });
#endif

#if ANDROID
        EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
            // Remove background/underline + any focus tint
            handler.PlatformView.Background = null;
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            handler.PlatformView.BackgroundTintList =
                Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);

            // Optional: tweak padding so text isn't cramped
            handler.PlatformView.SetPadding(0, handler.PlatformView.PaddingTop, 0, handler.PlatformView.PaddingBottom);
        });

        PickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
            var pv = handler.PlatformView;

            // Remove default underline / background & tints
            pv.Background = null;
            pv.SetBackgroundColor(Android.Graphics.Color.Transparent);
            pv.BackgroundTintList =
                Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);

            // Optional: tighten side padding so text aligns with other controls
            pv.SetPadding(0, 0, 0, 0);
        });
#endif


        return builder.Build();
    }
}
