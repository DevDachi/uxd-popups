namespace UXDivers.Popups;

/// <summary>
/// Interface defining the contract for popup animations.
/// </summary>
public interface IBaseAnimation
{
    /// <summary>
    /// Runs the animation on the specified popup page.
    /// </summary>
    /// <param name="popup">The popup page to animate.</param>
    /// <returns>A Task that completes when the animation finishes.</returns>
    Task RunAnimation(IPopupPage popup);
    
    void PrepareAnimation(IPopupPage popup);

    int Duration { get; }
    
    EasingType Easing { get; }
    
    bool AnimateOnlyContent { get; }
}
