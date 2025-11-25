namespace UXDivers.Popups.Maui;

/// <summary>
/// Base class for popup animations, providing common properties and functionality.
/// </summary>
[AcceptEmptyServiceProvider]
public abstract class PopupBaseAnimation : BindableObject, IBaseAnimation, IMarkupExtension<IBaseAnimation>
{
    /// <summary>
    /// Bindable property for the animation duration in milliseconds.
    /// </summary>
    public static readonly BindableProperty DurationProperty = BindableProperty.Create(
        nameof(Duration),
        typeof(int),
        typeof(PopupBaseAnimation),
        500);

    /// <summary>
    /// Gets or sets the duration of the animation in milliseconds.
    /// </summary>
    public int Duration
    {
        get { return (int)GetValue(DurationProperty); }
        set { SetValue(DurationProperty, value); }
    }

    /// <summary>
    /// Bindable property for the easing type of the animation.
    /// </summary>
    public static readonly BindableProperty EasingProperty = BindableProperty.Create(
        nameof(Easing),
        typeof(EasingType),
        typeof(PopupBaseAnimation),
        EasingType.Default);

    /// <summary>
    /// Gets or sets the easing type of the animation.
    /// </summary>
    public EasingType Easing
    {
        get { return (EasingType)GetValue(EasingProperty); }
        set { SetValue(EasingProperty, value); }
    }

    /// <summary>
    /// Bindable property indicating whether the animation should only affect the popup content.
    /// </summary>
    public static readonly BindableProperty AnimateOnlyContentProperty = BindableProperty.Create(
        nameof(AnimateOnlyContent),
        typeof(bool),
        typeof(PopupBaseAnimation),
        true);

    /// <summary>
    /// Gets or sets a value indicating whether the animation should only affect the popup content.
    /// </summary>
    public virtual bool AnimateOnlyContent
    {
        get { return (bool)GetValue(AnimateOnlyContentProperty); }
        set { SetValue(AnimateOnlyContentProperty, value); }
    }

    public void PrepareAnimation(IPopupPage popup)
    {
        if (popup is not PopupPage mauiPopup)
        {
            throw new ArgumentException("The popup to animate should be or inherit from PopupPage.", nameof(popup));
        }
        
        PrepareAnimation(mauiPopup);
    }
    
    private void PrepareAnimation(PopupPage popup)
    {
        var animationTarget = Utils.GetAnimationTarget(popup, AnimateOnlyContent);
        
        PrepareAnimation(animationTarget, popup);
    }

    /// <summary>
    /// Runs the animation on the specified popup page.
    /// </summary>
    /// <param name="popup">The popup page to animate.</param>
    /// <returns>A Task that completes when the animation finishes.</returns>
    /// <exception cref="ArgumentException">Thrown if the target or its content is not a VisualElement.</exception>
    public Task RunAnimation(IPopupPage popup)
    {
        if (popup is not PopupPage mauiPopup)
        {
            throw new ArgumentException("The popup to animate should be or inherit from PopupPage.", nameof(popup));
        }

        return RunAnimation(mauiPopup);
    }
    
    private Task RunAnimation(PopupPage target) 
    {
        var tcs = new TaskCompletionSource();

        // Get the actual animation target
        var animationTarget = Utils.GetAnimationTarget(target, AnimateOnlyContent);
        
        // Validate duration
        if (Duration <= 0)
        {
            tcs.TrySetResult();
            return tcs.Task;
        }

        var animation = CreateAnimation(animationTarget, target);
        
        // Null check for animation
        if (animation == null)
        {
            tcs.TrySetResult();
            return tcs.Task;
        }

        // Commit animation on the SAME element it was created for
        animation.Commit(animationTarget, GetType().Name, 16, (uint)Duration, Easing.ToMauiEasing(), (_, _) =>
        {
            tcs.TrySetResult();
        });

        // Failsafe timeout a bit longer than the animation
        _ = Task.Delay(Duration + 1000).ContinueWith(_ =>
        {
            if (!tcs.Task.IsCompleted)
                tcs.TrySetResult();
        });
        
        return tcs.Task;
    }
    
    /// <summary>
    /// Provides the current instance as the value for the markup extension.
    /// </summary>
    /// <param name="serviceProvider">The service provider for the markup extension.</param>
    /// <returns>The current instance.</returns>
    public IBaseAnimation ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    /// <summary>
    /// Provides the current instance as the value for the markup extension.
    /// </summary>
    /// <param name="serviceProvider">The service provider for the markup extension.</param>
    /// <returns>The current instance.</returns>
    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }

    /// <summary>
    /// Creates the animation logic for the target element.
    /// </summary>
    /// <param name="target">The target element to animate.</param>
    /// <returns>An action representing the animation logic.</returns>
    protected internal abstract Animation CreateAnimation(VisualElement target, PopupPage popup);
    
    protected internal abstract void PrepareAnimation(VisualElement target, PopupPage popup);
}
