using System.Windows.Input;
using UXDivers.Popups.Services;

namespace UXDivers.Popups.Maui.Controls;

/// <summary>
/// A popup with a prominent icon, title, text content, and an optional action button.
/// </summary>
public class IconTextPopup : PopupPage
{
  public static readonly BindableProperty IconTextProperty = BindableProperty.Create(
        nameof(IconText),
        typeof(string),
        typeof(IconTextPopup),
        null);

    /// <summary>
    /// Gets or sets the icon source displayed in the popup.
    /// </summary>
    public string IconText
    {
        get { return (string)GetValue(IconTextProperty); }
        set { SetValue(IconTextProperty, value); }
    }

    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
        nameof(IconColor),
        typeof(Color),
        typeof(IconTextPopup),
        null);

    /// <summary>
    /// Gets or sets the color of the icon.
    /// </summary>
    public Color IconColor
    {
        get { return (Color)GetValue(IconColorProperty); }
        set { SetValue(IconColorProperty, value); }
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(IconTextPopup),
        null);

    /// <summary>
    /// Gets or sets the title text displayed in the popup.
    /// </summary>
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(IconTextPopup),
        null);

    /// <summary>
    /// Gets or sets the main text content displayed in the popup.
    /// </summary>
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly BindableProperty ActionButtonCommandProperty = BindableProperty.Create(
        nameof(ActionButtonCommand),
        typeof(ICommand),
        typeof(IconTextPopup),
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
        typeof(IconTextPopup),
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
        typeof(IconTextPopup),
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
