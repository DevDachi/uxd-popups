namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Animates a popup by scaling it out from its normal size to a smaller size.
    /// </summary>
    public class ScaleOutPopupAnimation : ScaleToPopupAnimation
    {
        public static readonly BindableProperty ScaleToProperty = BindableProperty.Create(
            nameof(ScaleTo),
            typeof(double?),
            typeof(ScaleOutPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the ending scale value. Defaults to 0.6.
        /// </summary>
        public double? ScaleTo
        {
            get { return (double?)GetValue(ScaleToProperty); }
            set { SetValue(ScaleToProperty, value); }
        }

        /// <summary>
        /// Prepares the scale-out animation by setting the target scale to ScaleTo.
        /// </summary>
        protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
        {
            ScaleTo ??= 0.6;

            Scale = ScaleTo.Value;

            base.PrepareAnimation(target, popup);
        }
    }
}
