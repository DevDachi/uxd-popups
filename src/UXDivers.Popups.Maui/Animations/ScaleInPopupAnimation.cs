namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Animates a popup by scaling it in from a smaller size to its normal size.
    /// </summary>
    public class ScaleInPopupAnimation : ScaleToPopupAnimation
    {
        public static readonly BindableProperty ScaleFromProperty = BindableProperty.Create(
            nameof(ScaleFrom),
            typeof(double?),
            typeof(ScaleInPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the starting scale value. Defaults to 0.6.
        /// </summary>
        public double? ScaleFrom
        {
            get { return (double?)GetValue(ScaleFromProperty); }
            set { SetValue(ScaleFromProperty, value); }
        }

        /// <summary>
        /// Prepares the scale-in animation by setting the initial scale to ScaleFrom.
        /// </summary>
        protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
        {
            Scale = target.Scale;

            ScaleFrom ??= 0.6;

            target.Scale = ScaleFrom.Value;

            base.PrepareAnimation(target, popup);
        }
    }
}
