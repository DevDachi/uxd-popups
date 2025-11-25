using System.Windows.Input;
using UXDivers.Popups.Services;
namespace UXDivers.Popups.Maui.Controls;

/// <summary>
/// A modal popup with a title, close button, and customizable action button with content area.
/// </summary>
public class ActionModalPopup : PopupPage
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ActionModalPopup),
        null);

    /// <summary>
    /// Gets or sets the title text displayed in the popup.
    /// </summary>
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly BindableProperty CloseButtonIconTextProperty = BindableProperty.Create(
        nameof(CloseButtonIconText),
        typeof(string),
        typeof(ActionModalPopup),
        null);

    /// <summary>
    /// Gets or sets the icon source for the close button.
    /// </summary>
    public string CloseButtonIconText
    {
        get { return (string)GetValue(CloseButtonIconTextProperty); }
        set { SetValue(CloseButtonIconTextProperty, value); }
    }

    public static readonly BindableProperty CloseButtonIconColorProperty = BindableProperty.Create(
        nameof(CloseButtonIconColor),
        typeof(Color),
        typeof(ActionModalPopup),
        null);

    /// <summary>
    /// Gets or sets the color of the close button icon.
    /// </summary>
    public Color CloseButtonIconColor
    {
        get { return (Color)GetValue(CloseButtonIconColorProperty); }
        set { SetValue(CloseButtonIconColorProperty, value); }
    }

    public static readonly BindableProperty CloseButtonCommandProperty = BindableProperty.Create(
        nameof(CloseButtonCommand),
        typeof(ICommand),
        typeof(ActionModalPopup),
        defaultValue: new Command(async () => await IPopupService.Current.PopAsync()));

    /// <summary>
    /// Gets or sets the command executed when the close button is clicked. Defaults to PopAsync.
    /// </summary>
    public ICommand CloseButtonCommand
    {
        get { return (ICommand)GetValue(CloseButtonCommandProperty); }
        set { SetValue(CloseButtonCommandProperty, value); }
    }

    public static readonly BindableProperty ActionButtonCommandProperty = BindableProperty.Create(
        nameof(ActionButtonCommand),
        typeof(ICommand),
        typeof(ActionModalPopup),
        defaultValue: new Command(async () => await IPopupService.Current.PopAsync()));

    /// <summary>
    /// Gets or sets the command executed when the action button is clicked. Defaults to PopAsync.
    /// </summary>
    public ICommand ActionButtonCommand
    {
        get { return (ICommand)GetValue(ActionButtonCommandProperty); }
        set { SetValue(ActionButtonCommandProperty, value); }
    }

    public static readonly BindableProperty ActionButtonTextProperty = BindableProperty.Create(
        nameof(ActionButtonText),
        typeof(string),
        typeof(ActionModalPopup),
        null);

    /// <summary>
    /// Gets or sets the text displayed on the action button.
    /// </summary>
    public string ActionButtonText
    {
        get { return (string)GetValue(ActionButtonTextProperty); }
        set { SetValue(ActionButtonTextProperty, value); }
    }

    public static readonly BindableProperty ShowActionButtonProperty = BindableProperty.Create(
        nameof(ShowActionButton),
        typeof(bool),
        typeof(ActionModalPopup),
        defaultValue: true);

    /// <summary>
    /// Gets or sets whether the action button is visible.
    /// </summary>
    public bool ShowActionButton
    {
        get { return (bool)GetValue(ShowActionButtonProperty); }
        set { SetValue(ShowActionButtonProperty, value); }
    }
}
