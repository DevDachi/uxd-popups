namespace UXDivers.Popups.Maui;

/// <summary>
/// Combines multiple animations into a storyboard that can run sequentially or in parallel.
/// </summary>
[AcceptEmptyServiceProvider]
public class StoryboardAnimation : BindableObject, IBaseAnimation, IMarkupExtension<IBaseAnimation>
{
    public static readonly BindableProperty AnimateOnlyContentProperty = BindableProperty.Create(
        nameof(AnimateOnlyContent),
        typeof(bool),
        typeof(StoryboardAnimation),
        true);

    /// <summary>
    /// Gets the easing type for the storyboard.
    /// </summary>
    public EasingType Easing { get; }

    /// <summary>
    /// Gets the total duration of the storyboard in milliseconds.
    /// </summary>
    public int Duration { get; private set; }

    /// <summary>
    /// Gets or sets whether the animation should only affect the popup content.
    /// </summary>
    public bool AnimateOnlyContent
    {
        get { return (bool)GetValue(AnimateOnlyContentProperty); }
        set { SetValue(AnimateOnlyContentProperty, value); }
    }

    /// <summary>
    /// Gets or sets the first animation in the storyboard.
    /// </summary>
    public IBaseAnimation? Animation1 { get; set; }

    /// <summary>
    /// Gets or sets the second animation in the storyboard.
    /// </summary>
    public IBaseAnimation? Animation2 { get; set; }

    /// <summary>
    /// Gets or sets the third animation in the storyboard.
    /// </summary>
    public IBaseAnimation? Animation3 { get; set; }

    /// <summary>
    /// Gets or sets the fourth animation in the storyboard.
    /// </summary>
    public IBaseAnimation? Animation4 { get; set; }

    /// <summary>
    /// Gets or sets the fifth animation in the storyboard.
    /// </summary>
    public IBaseAnimation? Animation5 { get; set; }

    /// <summary>
    /// Gets or sets the execution strategy (RunAllAtStart for parallel, Sequential for series).
    /// </summary>
    public StoryboardStrategy Strategy { get; set; }

    /// <summary>
    /// Runs the storyboard animation on the specified popup.
    /// </summary>
    public Task RunAnimation(IPopupPage popup)
    {
        if (popup is not PopupPage mauiPopup)
        {
            throw new ArgumentException("The popup to animate should be or inherit from PopupPage.", nameof(popup));
        }

        return RunAnimation(mauiPopup);
    }
    
    private Task RunAnimation(PopupPage popup)
    {   
        if (Strategy == StoryboardStrategy.RunAllAtStart)
        {
            Duration = new List<int>()
            {
                Animation1?.Duration ?? 0,
                Animation2?.Duration ?? 0,
                Animation3?.Duration ?? 0,
                Animation4?.Duration ?? 0,
                Animation5?.Duration ?? 0
            }.Max();
            
            return Task.WhenAll(
            [
                Animation1?.RunAnimation(popup) ?? Task.CompletedTask,
                Animation2?.RunAnimation(popup) ?? Task.CompletedTask,
                Animation3?.RunAnimation(popup) ?? Task.CompletedTask,
                Animation4?.RunAnimation(popup) ?? Task.CompletedTask,
                Animation5?.RunAnimation(popup) ?? Task.CompletedTask,
            ]);
        }
        
        Duration = new List<int>()
        {
            Animation1?.Duration ?? 0,
            Animation2?.Duration ?? 0,
            Animation3?.Duration ?? 0,
            Animation4?.Duration ?? 0,
            Animation5?.Duration ?? 0
        }.Sum();

        return RunSequentially(popup);
    }

    /// <summary>
    /// Prepares all animations in the storyboard.
    /// </summary>
    public void PrepareAnimation(IPopupPage popup)
    {
        if (popup is not PopupPage mauiPopup)
        {
            throw new ArgumentException("The popup to animate should be or inherit from PopupPage.", nameof(popup));
        }
        
        PrepareAnimation(mauiPopup);
    }

    public void PrepareAnimation(PopupPage popup)
    {
        var animationTarget = Utils.GetAnimationTarget(popup, AnimateOnlyContent);
        
        SetupAnimations(animationTarget, popup);

        PrepareAnimations(popup);
    }

    /// <summary>
    /// Sets up the animations. Override this method to configure Animation1-5 properties.
    /// </summary>
    protected virtual void SetupAnimations(VisualElement target, IPopupPage popup)
    {
    }

    private void PrepareAnimations(IPopupPage popup)
    {
        Animation1?.PrepareAnimation(popup);
        Animation2?.PrepareAnimation(popup);
        Animation3?.PrepareAnimation(popup);
        Animation4?.PrepareAnimation(popup);
        Animation5?.PrepareAnimation(popup);
    }

    private async Task RunSequentially(IPopupPage target)
    {
        await (Animation1?.RunAnimation(target) ?? Task.CompletedTask);
        await (Animation2?.RunAnimation(target) ?? Task.CompletedTask);
        await (Animation3?.RunAnimation(target) ?? Task.CompletedTask);
        await (Animation4?.RunAnimation(target) ?? Task.CompletedTask);
        await (Animation5?.RunAnimation(target) ?? Task.CompletedTask);
    }
    
    public IBaseAnimation ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
    
    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}