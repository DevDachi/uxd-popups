using Microsoft.Maui.Layouts;
using UXDivers.Popups.Maui.Controls;
using UXDivers.Popups.Services;

namespace UXDivers.Popups.Maui.DemoApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (DeviceInfo.Current.Idiom == DeviceIdiom.Tablet || DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
        {
            if (width > height)
            {
                //Cards Landscape
                foreach (var child in flexCardContainer)
                {
                    FlexLayout.SetBasis(child as View, new FlexBasis(0.20f, true));

                }

                //Images Landscape
                foreach (var child in flexImageContainer)
                {
                    FlexLayout.SetBasis(child as View, new FlexBasis(0.33f, true));
                }
            }
            else
            {
                //Cards Portrait
                foreach (var child in flexCardContainer)
                {
                    FlexLayout.SetBasis(child as View, new FlexBasis(0.50f, true));

                }

                //Images Portrait
                foreach (var child in flexImageContainer)
                {
                    FlexLayout.SetBasis(child as View, new FlexBasis(1f, true));
                }

            }

        }

        if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
        {
            if (width > height)
            {
                //Images Landscape
                foreach (var child in flexImageContainer)
                {
                    FlexLayout.SetBasis(child as View, new FlexBasis(0.33f, true));
                }
            }
            else
            {
                //Images Portrait
                foreach (var child in flexImageContainer)
                {
                    FlexLayout.SetBasis(child as View, new FlexBasis(1f, true));
                }
            }
        }
    }

    // Playground Popup
    private async void OnPlaygroundTapped(object sender, EventArgs e)
    {
        await IPopupService.Current.PushAsync(new PlaygroundPage());
    }
      
    // Action Modal
    private async void OnActionModalTapped(object sender, EventArgs e)
    {
        var parameters = new Dictionary<string, object?>
        {
            { "Settings", "Gorilla EarBuds" },
        };

        var popup = new HeadphonesActionSheetPopup()
        {
            Title = "Gorilla EarBuds",
            ActionButtonText = "Connect",
            ShowActionButton = true
        };
        
        await IPopupService.Current.PushAsync(popup, parameters);
    }

    // Floater
    private async void OnFloaterTapped(object sender, EventArgs e)
    {
        var popup = new FloaterPopup
        {
            Title = "Message Sent",
            Text = "Your message has been sent to Alex"
        };

        await IPopupService.Current.PushAsync(popup);
    }
    
    // Form
    private async void OnFormTapped(object sender, EventArgs e)
    { 
        var popup = new FormPopup()
        {
            Title ="Welcome Back",
            Text ="Sign in to access your dashboard and settings.",
            ActionButtonText ="Sign In",
            ShowActionButton = true,
            SecondaryActionText="Don’t have an account?",
            SecondaryActionLinkText="Sign Up", 
            Items = new List<FormField>
            {
                new ()
                {
                    Icon = MaterialSymbolsFont.Person,
                    IconColor = Application.Current?.Resources["TextColor"] as Color,
                    Placeholder = "Username"
                },
                new ()
                {
                    Icon = MaterialSymbolsFont.Vpn_key,
                    IconColor = Application.Current?.Resources["TextColor"] as Color,
                    Placeholder ="Password",
                    IsPassword = true
                }
            }
        };
        
        var formResults = await IPopupService.Current.PushAsync(popup);

        if (formResults == null)
        {
            return;
        }
        
        foreach (string formResult in formResults!)
        {
            Console.WriteLine(formResult);
        }
    }

    // Icon Text
    private async void OnIconTextTapped(object sender, EventArgs e)
    {
        var popup = new IconTextPopup()
        {
            IconText = MaterialSymbolsFont.Check_circle,
            IconColor = Application.Current?.Resources["IconBlue"] as Color ?? Colors.Blue,
            Title = "Process Completed",
            Text = "Everything’s saved. You can continue where you left off.", 
            ActionButtonText ="Continue"
        };
        
         await IPopupService.Current.PushAsync(popup);
    }

    // List Action
    private async void OnListActionTapped(object sender, EventArgs e)
    {
        var popup = new ListActionPopup()
        {
            Title ="Switch account",
            ActionButtonText ="Switch",
            ItemsSource = new List<ListActionItem>()
            {
                new ListActionItem 
                { 
                    UserImage = "user_1.png", 
                    User = "koston.ray", 
                    UserActivity ="1 chat - 21 notifications",
                    IsSelected = false
                }, 
                new ListActionItem 
                { 
                    UserImage = "user_2.png", 
                    User = "rachilly", 
                    UserActivity ="6 chat - 8 notifications",
                    IsSelected = false 
                }, 
                new ListActionItem 
                { 
                    UserImage = "user_3.png", 
                    User = "tommlee", 
                    UserActivity ="3 chat - 6 notifications",
                    IsSelected = true
                }
            }
        };
        
        await IPopupService.Current.PushAsync(popup);
    }

    // Options Sheet
    private async void OnOptionSheetTapped(object sender, EventArgs e)
    {
        var popup = new OptionSheetPopup
        {
            Title = "Share",
            Items = new List<OptionSheetItem>
            {
                new OptionSheetItem
                {
                    Text = "Copy",
                    GroupName = "commonActions",
                    Icon = MaterialSymbolsFont.Content_copy
                },
                new OptionSheetItem
                {
                    Text = "Duplicate",
                    GroupName = "commonActions",
                    Icon = MaterialSymbolsFont.Tab_duplicate
                },
                new OptionSheetItem
                {
                    Text = "Save to Files",
                    GroupName = "commonActions",
                    Icon = MaterialSymbolsFont.Arrow_circle_down
                },
                new OptionSheetItem
                {
                    Text = "Add to Album",
                    GroupName = "commonActions",
                    Icon = MaterialSymbolsFont.Add_photo_alternate
                },
                new OptionSheetItem
                {
                    Text = "Send by SMS",
                    GroupName = "shareActions",
                    Icon = MaterialSymbolsFont.Chat
                },
                new OptionSheetItem
                {
                    Text = "Use as Wallpaper",
                    GroupName = "shareActions",
                    Icon = MaterialSymbolsFont.Wallpaper
                },
                new OptionSheetItem
                {
                    Text = "Assign to Contact",
                    GroupName = "shareActions",
                    Icon = MaterialSymbolsFont.Person_add,
                },
                new OptionSheetItem
                {
                    Text = "Print",
                    GroupName = "fileActions",
                    Icon = MaterialSymbolsFont.Adf_scanner
                },
                new OptionSheetItem
                {
                    Text = "Delete",
                    GroupName = "fileActions",
                    Icon = MaterialSymbolsFont.Delete,
                    IconColor = Application.Current?.Resources["IconRed"] as Color
                }
            }
        };
        await IPopupService.Current.PushAsync(popup);
    }
  
    // Simple Action
    private async void OnSimpleActionTapped(object sender, EventArgs e)
    {
        var popup = new SimpleActionPopup()
        {
            Title = "Are You Sure?",
            Text = "There is no way back, this action can’t be undone.",
            ActionButtonText = "Delete",
            SecondaryActionButtonText = "Cancel"
        };
        
        await IPopupService.Current.PushAsync(popup);
    }
    
    // Simple Text
    private async void OnSimpleTextTapped(object sender, EventArgs e)
    {
        var parameters = new Dictionary<string, object?>
        {
            { "Title", "Sample Title" },
            { "Text", "Additional text goes here" }
        };
        
        await IPopupService.Current.PushAsync<CustomSimpleTextPopup>(parameters);
    }

    // Toast
    private async void OnToastTapped(object sender, EventArgs e)
    {
        var popup = new Toast()
        {
            Title = "Update Success"
        };
        
        await IPopupService.Current.PushAsync(popup);
    }

    private void OnUXDiversTapped(object sender, TappedEventArgs e)
    {
        Launcher.OpenAsync(new Uri("https://uxdivers.com"));
    }

    private void OnGrialTapped(object sender, TappedEventArgs e)
    {
        Launcher.OpenAsync(new Uri("https://grialkit.com"));
    }

    private void OnGrialStudioTapped(object sender, TappedEventArgs e)
    {
        Launcher.OpenAsync(new Uri("https://grialkit.com/studio"));
    }
}