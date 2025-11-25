using System.Collections;
using System.Windows.Input;
using UXDivers.Popups.Services;
namespace UXDivers.Popups.Maui.Controls;

/// <summary>
/// A form popup that collects user input from multiple fields and returns the values as a list of strings.
/// </summary>
public class FormPopup : PopupResultPage<List<string?>>
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(FormPopup),
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
        typeof(FormPopup),
        null);

    /// <summary>
    /// Gets or sets the descriptive text displayed in the popup.
    /// </summary>
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly BindableProperty ActionButtonCommandProperty = BindableProperty.Create(
        nameof(ActionButtonCommand),
        typeof(ICommand),
        typeof(FormPopup),
        defaultValueCreator: bindable => new Command(async () => await ((bindable as FormPopup)?.OnMainActionClicked() ?? Task.CompletedTask)));

    /// <summary>
    /// Gets or sets the command executed when the action button is clicked. Defaults to OnMainActionClicked.
    /// </summary>
    public ICommand ActionButtonCommand
    {
        get { return (ICommand)GetValue(ActionButtonCommandProperty); }
        set { SetValue(ActionButtonCommandProperty, value); }
    }

    public static readonly BindableProperty ActionButtonTextProperty = BindableProperty.Create(
        nameof(ActionButtonText),
        typeof(string),
        typeof(FormPopup),
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
        typeof(FormPopup),
        defaultValue: true);

    /// <summary>
    /// Gets or sets whether the action button is visible.
    /// </summary>
    public bool ShowActionButton
    {
        get { return (bool)GetValue(ShowActionButtonProperty); }
        set { SetValue(ShowActionButtonProperty, value); }
    }

    public static readonly BindableProperty SecondaryActionTextProperty = BindableProperty.Create(
        nameof(SecondaryActionText),
        typeof(string),
        typeof(FormPopup),
        null);

    /// <summary>
    /// Gets or sets the text for the secondary action label.
    /// </summary>
    public string SecondaryActionText
    {
        get { return (string)GetValue(SecondaryActionTextProperty); }
        set { SetValue(SecondaryActionTextProperty, value); }
    }

    public static readonly BindableProperty SecondaryActionLinkTextProperty = BindableProperty.Create(
        nameof(SecondaryActionLinkText),
        typeof(string),
        typeof(FormPopup),
        null);

    /// <summary>
    /// Gets or sets the clickable link text for the secondary action.
    /// </summary>
    public string SecondaryActionLinkText
    {
        get { return (string)GetValue(SecondaryActionLinkTextProperty); }
        set { SetValue(SecondaryActionLinkTextProperty, value); }
    }

    public static readonly BindableProperty SecondaryActionLinkCommandProperty = BindableProperty.Create(
        nameof(SecondaryActionLinkCommand),
        typeof(ICommand),
        typeof(FormPopup),
        defaultValue: null);

    /// <summary>
    /// Gets or sets the command executed when the secondary action link is clicked.
    /// </summary>
    public ICommand SecondaryActionLinkCommand
    {
        get { return (ICommand)GetValue(SecondaryActionLinkCommandProperty); }
        set { SetValue(SecondaryActionLinkCommandProperty, value); }
    }

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items),
        typeof(IEnumerable),
        typeof(FormPopup),
        defaultValue: null);

    /// <summary>
    /// Gets or sets the list of form fields to display in the popup.
    /// </summary>
    public IEnumerable Items
    {
        get { return (IEnumerable)GetValue(ItemsProperty); }
        set { SetValue(ItemsProperty, value); }
    }

    public static readonly BindableProperty ItemDataTemplateProperty = BindableProperty.Create(
        nameof(ItemDataTemplate),
        typeof(DataTemplate),
        typeof(FormPopup),
        defaultValue: null);

    /// <summary>
    /// Gets or sets the list of form fields to display in the popup.
    /// </summary>
    public DataTemplate ItemDataTemplate
    {
        get { return (DataTemplate)GetValue(ItemDataTemplateProperty); }
        set { SetValue(ItemDataTemplateProperty, value); }
    }

    public FormPopup()
    {
        Result = null;
    }

    /// <summary>
    /// Handles the main action button click, collects field values, and returns them as Result.
    /// </summary>
    protected virtual Task OnMainActionClicked()
    {
        Result = new List<string?>();
        
        foreach (var item in Items)
        {
            if (item is FormField formField)
            {
                Result.Add(formField.Value);
            }
        }

        return IPopupService.Current.PopAsync(this);
    }
}