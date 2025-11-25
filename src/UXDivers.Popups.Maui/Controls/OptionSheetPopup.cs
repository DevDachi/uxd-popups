using System.Collections;
using System.Windows.Input;
using UXDivers.Popups.Services;

namespace UXDivers.Popups.Maui.Controls;

/// <summary>
/// A bottom sheet popup displaying grouped selectable options with customizable item templates.
/// </summary>
public class OptionSheetPopup : PopupPage
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(OptionSheetPopup),
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
        typeof(OptionSheetPopup),
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
        typeof(OptionSheetPopup),
        null);

    /// <summary>
    /// Gets or sets the color of the close button icon.
    /// </summary>
    public Color CloseButtonIconColor
    {
        get { return (Color)GetValue(CloseButtonIconColorProperty); }
        set { SetValue(CloseButtonIconColorProperty, value); }
    }

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items),
        typeof(IEnumerable),
        typeof(OptionSheetPopup),
        null,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (bindable is OptionSheetPopup optionSheetPopup)
            {
                optionSheetPopup._groups = null;
                optionSheetPopup.OnPropertyChanged(nameof(Groups));
            }
        });

    /// <summary>
    /// Gets or sets the IEnumerable of option groups to display in the sheet.
    /// </summary>
    public IEnumerable Items
    {
        get { return (IEnumerable)GetValue(ItemsProperty); }
        set { SetValue(ItemsProperty, value); }
    }

    public static readonly BindableProperty ItemDataTemplateProperty = BindableProperty.Create(
        nameof(ItemDataTemplate),
        typeof(DataTemplate),
        typeof(OptionSheetPopup),
        null);

    /// <summary>
    /// Gets or sets the data template used to render each option item.
    /// </summary>
    public DataTemplate ItemDataTemplate
    {
        get { return (DataTemplate)GetValue(ItemDataTemplateProperty); }
        set { SetValue(ItemDataTemplateProperty, value); }
    }

    public static readonly BindableProperty CloseButtonCommandProperty = BindableProperty.Create(
        nameof(CloseButtonCommand),
        typeof(ICommand),
        typeof(OptionSheetPopup),
        defaultValue: new Command(async () => await IPopupService.Current.PopAsync()));

    /// <summary>
    /// Gets or sets the command executed when the close button is clicked. Defaults to PopAsync.
    /// </summary>
    public ICommand CloseButtonCommand
    {
        get { return (ICommand)GetValue(CloseButtonCommandProperty); }
        set { SetValue(CloseButtonCommandProperty, value); }
    }

    private IEnumerable<OptionSheetGroup>? _groups;
    public IEnumerable<OptionSheetGroup>? Groups => _groups ??= UpdateGroups();     

    private IEnumerable<OptionSheetGroup>? UpdateGroups()
    {
        if (Items == null)
        {
            return null;
        }

        var groups = new Dictionary<string, OptionSheetGroup>();

        foreach (var item in Items)
        {
            var groupName = string.Empty;
            if (item is IOptionSheetGroupableItem groupableItem && groupableItem.GroupName is string)
            {
                groupName = groupableItem.GroupName;
            }

            if (groups.ContainsKey(groupName))
            {
                groups[groupName].Items = groups[groupName].Items.Append(item);
            }
            else
            {
                groups[groupName] = new OptionSheetGroup
                {
                    Items = [item]
                };
            }
        }
        
        return groups.Values;
    }
}