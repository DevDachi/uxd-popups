namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Animates a popup by moving it in from a specified direction to its final position.
    /// </summary>
    public class MoveInPopupAnimation : TranslateToPopupAnimation
    {
        public static readonly BindableProperty TranslationFromCenterProperty = BindableProperty.Create(
            nameof(TranslationFromCenter),
            typeof(double?),
            typeof(MoveInPopupAnimation),
            null);

        /// <summary>
        /// Gets or sets the distance from center to start the animation. Auto-calculated if not set.
        /// </summary>
        public double? TranslationFromCenter
        {
            get { return (double?)GetValue(TranslationFromCenterProperty); }
            set { SetValue(TranslationFromCenterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the direction from which the popup moves in.
        /// </summary>
        public MoveDirection MoveDirection { get; set; }

        /// <summary>
        /// Prepares the move-in animation by positioning the element off-screen based on direction.
        /// </summary>
        protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
        {
            TranslationFromCenter ??= Utils.CalculateTranslationAnimationDefaultDistance(MoveDirection, popup);

            TranslationX = target.TranslationX;
            TranslationY = target.TranslationY;

            switch (MoveDirection)
            {
                case MoveDirection.Left:
                    target.TranslationX = TranslationX.Value - TranslationFromCenter.Value;
                    break;
                case MoveDirection.Top:
                    target.TranslationY = TranslationY.Value - TranslationFromCenter.Value;
                    break;
                case MoveDirection.Right:
                    target.TranslationX = TranslationX.Value + TranslationFromCenter.Value;
                    break;
                case MoveDirection.Bottom:
                    target.TranslationY = TranslationY.Value + TranslationFromCenter.Value;
                    break;
            }
            
            base.PrepareAnimation(target, popup);
        }
    }
}
