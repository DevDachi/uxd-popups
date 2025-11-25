namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Animates a popup by fading it out from opaque to transparent.
    /// </summary>
    public class FadeOutPopupAnimation : FadeToPopupAnimation
    {
        /// <summary>
        /// Prepares the fade-out animation by setting initial opacity to 1 and final opacity to 0.
        /// </summary>
        protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
        {
            InitialOpacity = 1;
            FinalOpacity = 0;

            base.PrepareAnimation(target, popup);
        }
    }
}
