namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Base class for translation animations, animating position on X and/or Y axes.
    /// </summary>
    public class TranslateToPopupAnimation : PopupBaseAnimation
    {
        public static readonly BindableProperty TranslationXProperty = BindableProperty.Create(
            nameof(TranslationX),
            typeof(double?),
            typeof(TranslateToPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the target horizontal translation offset.
        /// </summary>
        public double? TranslationX
        {
            get { return (double?)GetValue(TranslationXProperty); }
            set { SetValue(TranslationXProperty, value); }
        }

        public static readonly BindableProperty TranslationYProperty = BindableProperty.Create(
            nameof(TranslationY),
            typeof(double?),
            typeof(TranslateToPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the target vertical translation offset.
        /// </summary>
        public double? TranslationY
        {
            get { return (double?)GetValue(TranslationYProperty); }
            set { SetValue(TranslationYProperty, value); }
        }

        private double _originalTranslationX;
        private double _originalTranslationY;

        /// <summary>
        /// Translation only affects popup content, not the entire page.
        /// </summary>
        public override bool AnimateOnlyContent => true;

        /// <summary>
        /// Creates the translation animation that interpolates position values.
        /// </summary>
        internal protected override Animation CreateAnimation(VisualElement target, PopupPage popup)
        {
            var finalAnimation = new Animation();

            if (TranslationX.HasValue)
            {
                finalAnimation.Add(0, 1, new Animation(d => target.TranslationX = d, _originalTranslationX, TranslationX.Value));
            }
            
            if (TranslationY.HasValue)
            {
                finalAnimation.Add(0, 1, new Animation(d => target.TranslationY = d, _originalTranslationY, TranslationY.Value));
            }

            return finalAnimation;
        }

        /// <summary>
        /// Prepares the animation by saving the original translation values.
        /// </summary>
        protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
        {
            _originalTranslationX = target.TranslationX;
            _originalTranslationY = target.TranslationY;
        }
    }
}
