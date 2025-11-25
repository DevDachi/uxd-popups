using System.Windows.Input;
using UXDivers.Popups.Services;

namespace UXDivers.Popups.Maui.Controls;

/// <summary>
/// A simple popup displaying title, text content, and a close button.
/// </summary>
public class SimpleTextPopup : PopupPage
{
     public static readonly BindableProperty CloseButtonIconTextProperty = BindableProperty.Create(
        nameof(CloseButtonIconText),
        typeof(string),
        typeof(SimpleTextPopup),
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
        typeof(SimpleTextPopup),
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
        typeof(SimpleTextPopup),
        defaultValue: new Command(async () => await IPopupService.Current.PopAsync()));

    /// <summary>
    /// Gets or sets the command executed when the close button is clicked. Defaults to PopAsync.
    /// </summary>
    public ICommand CloseButtonCommand
    {
        get { return (ICommand)GetValue(CloseButtonCommandProperty); }
        set { SetValue(CloseButtonCommandProperty, value); }
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(SimpleTextPopup),
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
        typeof(SimpleTextPopup),
        null);

    /// <summary>
    /// Gets or sets the main text content displayed in the popup.
    /// </summary>
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
}