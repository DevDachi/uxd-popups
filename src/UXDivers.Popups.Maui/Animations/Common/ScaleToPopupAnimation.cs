namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Base class for scale animations, animating scale on X, Y, or both axes.
    /// </summary>
    public class ScaleToPopupAnimation : PopupBaseAnimation
    {
        public static readonly BindableProperty ScaleXProperty = BindableProperty.Create(
            nameof(ScaleX),
            typeof(double?),
            typeof(ScaleToPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the target horizontal scale value.
        /// </summary>
        public double? ScaleX
        {
            get { return (double?)GetValue(ScaleXProperty); }
            set { SetValue(ScaleXProperty, value); }
        }

        public static readonly BindableProperty ScaleYProperty = BindableProperty.Create(
            nameof(ScaleY),
            typeof(double?),
            typeof(ScaleToPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the target vertical scale value.
        /// </summary>
        public double? ScaleY
        {
            get { return (double?)GetValue(ScaleYProperty); }
            set { SetValue(ScaleYProperty, value); }
        }

        public static readonly BindableProperty ScaleProperty = BindableProperty.Create(
            nameof(Scale),
            typeof(double?),
            typeof(ScaleToPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the target uniform scale value for both X and Y axes.
        /// </summary>
        public double? Scale
        {
            get { return (double?)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        private double _originalScale;
        private double _originalScaleX;
        private double _originalScaleY;

        /// <summary>
        /// Creates the scale animation that interpolates scale values.
        /// </summary>
        protected internal override Animation CreateAnimation(VisualElement target, PopupPage popup)
        {
            var finalAnimation = new Animation();

            if (Scale.HasValue)
            {
                finalAnimation.Add(0, 1, new Animation(d => target.Scale = d, _originalScale, Scale.Value));
            }
            
            if (ScaleX.HasValue)
            {
                finalAnimation.Add(0, 1, new Animation(d => target.ScaleX = d, _originalScaleX, ScaleX.Value));
            }
            
            if (ScaleY.HasValue)
            {
                finalAnimation.Add(0, 1, new Animation(d => target.ScaleY = d, _originalScaleY, ScaleY.Value));
            }

            return finalAnimation;
        }

        /// <summary>
        /// Prepares the animation by saving the original scale values.
        /// </summary>
        protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
        {
            _originalScale = target.Scale;
            _originalScaleX = target.ScaleX;
            _originalScaleY = target.ScaleY;
        }
    }
}
