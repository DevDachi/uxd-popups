using System.Globalization;

namespace UXDivers.Popups.Maui.Converters;

public class EmptyStringBoolConverter : IValueConverter
{
    public bool Inverted { get; set;  }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string stringValue)
        {
            return Inverted ? string.IsNullOrEmpty(stringValue) : !string.IsNullOrEmpty(stringValue);
        }

        if (value == null)
        {
            return Inverted ? true : false;
        }
        
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}