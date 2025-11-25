namespace UXDivers.Popups.Maui
{
    internal partial class Utils
    {
        public static double CalculateTranslationAnimationDefaultDistance(MoveDirection moveDirection, PopupPage popup)
        {
            if (popup is not VisualElement element || popup.ActualContent is not VisualElement child)
            {
                return 0;
            }

            switch (moveDirection)
            {
                case MoveDirection.Left:
                case MoveDirection.Right:
                    return (element.Width + child.Width) / 2;
                case MoveDirection.Top:
                case MoveDirection.Bottom:
                default:
                    return (element.Height + child.Height) / 2;
            }
        }
        
        public static VisualElement GetAnimationTarget(PopupPage target, bool animateOnlyContent)
        {
            if (animateOnlyContent && target.ActualContent is VisualElement animatable)
            {
                return animatable;
            }
            
            if (target is VisualElement visualElement)
            {
                return visualElement;
            }

            throw new ArgumentException("target or content must be a VisualElement");
        }
    }
}
