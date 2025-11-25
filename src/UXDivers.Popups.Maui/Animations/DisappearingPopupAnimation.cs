namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Combined animation that fades out, scales out, and moves out a popup simultaneously.
    /// </summary>
    public class DisappearingPopupAnimation: StoryboardAnimation
    {
        /// <summary>
        /// Gets or sets the direction in which the popup disappears.
        /// </summary>
        public MoveDirection DisappearingDirection { get; set; }

        /// <summary>
        /// Sets up the storyboard with fade-out, move-out, and scale-out animations running in parallel.
        /// </summary>
        protected override void SetupAnimations(VisualElement target, IPopupPage popup)
        {
            Strategy = StoryboardStrategy.RunAllAtStart;
            Animation1 = new FadeOutPopupAnimation
            {
                Duration = 500
            };
            Animation2 = new MoveOutPopupAnimation
            {
                MoveDirection = DisappearingDirection,
                Duration = 500,
                TranslationFromCenter = 200
            };
            Animation3 = new ScaleOutPopupAnimation
            {
                Duration = 500
            };
        }
    }
}
