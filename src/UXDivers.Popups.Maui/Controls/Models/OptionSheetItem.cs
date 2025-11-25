using System.Windows.Input;

namespace UXDivers.Popups.Maui.Controls;

/// <summary>
/// Represents a selectable item in an option sheet with text, icon, and command.
/// </summary>
public class OptionSheetItem : IOptionSheetGroupableItem
{
    /// <summary>
    /// Gets or sets the text displayed for the option.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the icon source displayed next to the text.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the color of the icon.
    /// </summary>
    public Color? IconColor { get; set; }

    /// <summary>
    /// Gets or sets the command executed when the option is selected.
    /// </summary>
    public ICommand? Command { get; set; }

    /// <summary>
    /// Gets or sets the parameter passed to the command.
    /// </summary>
    public object? CommandParameter { get; set; }

    public string? GroupName { get; set; }
}
