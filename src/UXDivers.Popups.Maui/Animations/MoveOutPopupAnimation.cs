namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Animates a popup by moving it out from its position to off-screen in a specified direction.
    /// </summary>
    public class MoveOutPopupAnimation : TranslateToPopupAnimation
    {
        public static readonly BindableProperty TranslationFromCenterProperty = BindableProperty.Create(
            nameof(TranslationFromCenter),
            typeof(double?),
            typeof(MoveOutPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the distance from center to end the animation. Auto-calculated if not set.
        /// </summary>
        public double? TranslationFromCenter
        {
            get { return (double?)GetValue(TranslationFromCenterProperty); }
            set { SetValue(TranslationFromCenterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the direction in which the popup moves out.
        /// </summary>
        public MoveDirection MoveDirection { get; set; }

        /// <summary>
        /// Prepares the move-out animation by calculating the target off-screen position based on direction.
        /// </summary>
        protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
        {
            TranslationFromCenter ??= Utils.CalculateTranslationAnimationDefaultDistance(MoveDirection, popup);
            
            switch (MoveDirection)
            {
                case MoveDirection.Left:
                    TranslationX = -TranslationFromCenter.Value;
                    TranslationY = 0;
                    break;
                case MoveDirection.Top:
                    TranslationY = -TranslationFromCenter.Value;
                    TranslationX = 0;
                    break;
                case MoveDirection.Right:
                    TranslationX = TranslationFromCenter.Value;
                    TranslationY = 0;
                    break;
                case MoveDirection.Bottom:
                    TranslationY = TranslationFromCenter.Value;
                    TranslationX = 0;
                    break;
            }
            
            base.PrepareAnimation(target, popup);
        }
    }
}
