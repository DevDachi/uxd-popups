namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Base class for fade animations, animating opacity from initial to final value.
    /// </summary>
    public class FadeToPopupAnimation : PopupBaseAnimation
    {
        public static readonly BindableProperty FinalOpacityProperty = BindableProperty.Create(
            nameof(FinalOpacity),
            typeof(double?),
            typeof(FadeToPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the target opacity value (0-1).
        /// </summary>
        public double? FinalOpacity
        {
            get { return (double?)GetValue(FinalOpacityProperty); }
            set { SetValue(FinalOpacityProperty, value); }
        }

        public static readonly BindableProperty InitialOpacityProperty = BindableProperty.Create(
            nameof(InitialOpacity),
            typeof(double?),
            typeof(FadeToPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the starting opacity value (0-1).
        /// </summary>
        public double? InitialOpacity
        {
            get { return (double?)GetValue(InitialOpacityProperty); }
            set { SetValue(InitialOpacityProperty, value); }
        }

        /// <summary>
        /// Creates the fade animation that interpolates opacity from initial to final value.
        /// </summary>
        protected internal override Animation CreateAnimation(VisualElement target, PopupPage popup)
        {
            if (InitialOpacity == null || FinalOpacity == null)
            {
                throw new InvalidOperationException("InitialOpacity and FinalOpacity must be set before creating the animation.");
            }

            return new Animation(d => target.Opacity = d, InitialOpacity.Value, FinalOpacity.Value);
        }

        /// <summary>
        /// Prepares the animation by setting default opacity values if not specified.
        /// </summary>
        protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
        {
            InitialOpacity ??= target.Opacity;
            
            FinalOpacity ??= target.Opacity == 1 ? 0 : 1;
            
            target.Opacity = InitialOpacity.Value;
        }
    }
}
