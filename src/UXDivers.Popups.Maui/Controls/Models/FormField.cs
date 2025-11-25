namespace UXDivers.Popups.Maui.Controls
{
    /// <summary>
    /// Represents a form input field with optional icon and password masking.
    /// </summary>
    public class FormField
    {
        /// <summary>
        /// Gets or sets the placeholder text displayed when the field is empty.
        /// </summary>
        public string? Placeholder { get; set; }

        /// <summary>
        /// Gets or sets the icon source displayed next to the field.
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the color of the field icon.
        /// </summary>
        public Color? IconColor { get; set; }

        /// <summary>
        /// Gets or sets the current value of the field.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets whether the field should mask input as a password.
        /// </summary>
        public bool IsPassword { get; set; }
    }
}
