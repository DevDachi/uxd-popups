using System.Collections;
using System.Windows.Input;
using UXDivers.Popups.Services;

namespace UXDivers.Popups.Maui.Controls;

/// <summary>
/// A popup displaying a list of items with customizable template, title, and action button.
/// </summary>
public class ListActionPopup : PopupPage
{
      public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ListActionPopup),
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
        typeof(ListActionPopup),
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
        typeof(ListActionPopup),
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
        typeof(ListActionPopup),
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
        typeof(ListActionPopup),
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
        typeof(ListActionPopup),
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
        typeof(ListActionPopup),
        defaultValue: true);

    /// <summary>
    /// Gets or sets whether the action button is visible.
    /// </summary>
    public bool ShowActionButton
    {
        get { return (bool)GetValue(ShowActionButtonProperty); }
        set { SetValue(ShowActionButtonProperty, value); }
    }

    public static readonly BindableProperty ItemDataTemplateProperty = BindableProperty.Create(
        nameof(ItemDataTemplate),
        typeof(DataTemplate),
        typeof(ListActionPopup),
        null);

    /// <summary>
    /// Gets or sets the data template used to render each item in the list.
    /// </summary>
    public DataTemplate ItemDataTemplate
    {
        get { return (DataTemplate)GetValue(ItemDataTemplateProperty); }
        set { SetValue(ItemDataTemplateProperty, value); }
    }

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IEnumerable),
        typeof(ListActionPopup),
        null);

    /// <summary>
    /// Gets or sets the collection of items to display in the list.
    /// </summary>
    public IEnumerable ItemsSource
    {
        get { return (IEnumerable)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
}