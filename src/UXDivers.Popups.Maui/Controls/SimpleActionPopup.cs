using System.Windows.Input;
using UXDivers.Popups.Services;
namespace UXDivers.Popups.Maui.Controls;

/// <summary>
/// A popup with title, text, and up to two action buttons for user confirmation or selection.
/// </summary>
public class SimpleActionPopup : PopupPage
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(SimpleActionPopup),
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
        typeof(SimpleActionPopup),
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
        typeof(SimpleActionPopup),
        defaultValue: new Command(async () => await IPopupService.Current.PopAsync()));

    /// <summary>
    /// Gets or sets the command executed when the primary action button is clicked. Defaults to PopAsync.
    /// </summary>
    public ICommand ActionButtonCommand
    {
        get { return (ICommand)GetValue(ActionButtonCommandProperty); }
        set { SetValue(ActionButtonCommandProperty, value); }
    }

    public static readonly BindableProperty ActionButtonTextProperty = BindableProperty.Create(
        nameof(ActionButtonText),
        typeof(string),
        typeof(SimpleActionPopup),
        null);

    /// <summary>
    /// Gets or sets the text displayed on the primary action button.
    /// </summary>
    public string ActionButtonText
    {
        get { return (string)GetValue(ActionButtonTextProperty); }
        set { SetValue(ActionButtonTextProperty, value); }
    }

    public static readonly BindableProperty ShowActionButtonProperty = BindableProperty.Create(
        nameof(ShowActionButton),
        typeof(bool),
        typeof(SimpleActionPopup),
        defaultValue: true);

    /// <summary>
    /// Gets or sets whether the primary action button is visible.
    /// </summary>
    public bool ShowActionButton
    {
        get { return (bool)GetValue(ShowActionButtonProperty); }
        set { SetValue(ShowActionButtonProperty, value); }
    }

    public static readonly BindableProperty SecondaryActionButtonCommandProperty = BindableProperty.Create(
        nameof(SecondaryActionButtonCommand),
        typeof(ICommand),
        typeof(SimpleActionPopup),
        defaultValue: new Command(async () => await IPopupService.Current.PopAsync()));

    /// <summary>
    /// Gets or sets the command executed when the secondary action button is clicked. Defaults to PopAsync.
    /// </summary>
    public ICommand SecondaryActionButtonCommand
    {
        get { return (ICommand)GetValue(SecondaryActionButtonCommandProperty); }
        set { SetValue(SecondaryActionButtonCommandProperty, value); }
    }

    public static readonly BindableProperty SecondaryActionButtonTextProperty = BindableProperty.Create(
        nameof(SecondaryActionButtonText),
        typeof(string),
        typeof(SimpleActionPopup),
        null);

    /// <summary>
    /// Gets or sets the text displayed on the secondary action button.
    /// </summary>
    public string SecondaryActionButtonText
    {
        get { return (string)GetValue(SecondaryActionButtonTextProperty); }
        set { SetValue(SecondaryActionButtonTextProperty, value); }
    }

    public static readonly BindableProperty ShowSecondaryActionButtonProperty = BindableProperty.Create(
        nameof(ShowSecondaryActionButton),
        typeof(bool),
        typeof(SimpleActionPopup),
        defaultValue: true);

    /// <summary>
    /// Gets or sets whether the secondary action button is visible.
    /// </summary>
    public bool ShowSecondaryActionButton
    {
        get { return (bool)GetValue(ShowSecondaryActionButtonProperty); }
        set { SetValue(ShowSecondaryActionButtonProperty, value); }
    }
}