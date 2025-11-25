namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Animates a popup by fading it in from transparent to opaque.
    /// </summary>
    public class FadeInPopupAnimation : FadeToPopupAnimation
    {
        /// <summary>
        /// Prepares the fade-in animation by setting initial opacity to 0 and final opacity to 1.
        /// </summary>
        protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
        {
            InitialOpacity = 0;
            FinalOpacity = 1;

            base.PrepareAnimation(target, popup);
        }
    }
}
