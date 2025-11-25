using System.Diagnostics.CodeAnalysis;

namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Represents a popup page that returns a result of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the popup.</typeparam>
    public class PopupResultPage<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T> : PopupPage, IPopupResultPage<T>
    {
        /// <summary>
        /// Bindable property for the result of the popup.
        /// </summary>
        public static readonly BindableProperty ResultProperty = BindableProperty.Create(
            nameof(Result),
            typeof(T?),
            typeof(PopupResultPage<T>));

        /// <summary>
        /// Gets or sets the result of the popup.
        /// </summary>
        public T? Result
        {
            get { return (T?)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        /// <summary>
        /// Sets the result of the popup
        /// </summary>
        /// <param name="result">The result to set.</param>
        public void SetResult(T? result)
        {
            Result = result;
        }
    }
}
