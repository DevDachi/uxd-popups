namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Combined animation that fades in, scales in, and moves in a popup simultaneously.
    /// </summary>
    public class AppearingPopupAnimation : StoryboardAnimation
    {
        /// <summary>
        /// Gets or sets the direction from which the popup appears.
        /// </summary>
        public MoveDirection AppearingDirection { get; set; }

        /// <summary>
        /// Sets up the storyboard with fade-in, move-in, and scale-in animations running in parallel.
        /// </summary>
        protected override void SetupAnimations(VisualElement target, IPopupPage popup)
        {
            Strategy = StoryboardStrategy.RunAllAtStart;
            Animation1 = new FadeInPopupAnimation
            {
                Duration = 500
            };
            Animation2 = new MoveInPopupAnimation
            {
                MoveDirection = AppearingDirection,
                Duration = 500,
                TranslationFromCenter = 200
            };
            Animation3 = new ScaleInPopupAnimation
            {
                Duration = 500
            };
        }
    }
}
