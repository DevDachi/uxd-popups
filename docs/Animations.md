# Animations

This page explains the animation system in UXDivers Popups, including animation properties, basic animations, storyboards, composed animations, and how to create custom animations.

## Overview

UXDivers Popups provides a rich animation system with 14 built-in animation types. Animations can be applied to popups when they appear and disappear, making your UI feel smooth and polished.

### Animation Categories

| Category | Animations |
|----------|------------|
| **Fade** | FadeInPopupAnimation, FadeOutPopupAnimation |
| **Move** | MoveInPopupAnimation, MoveOutPopupAnimation |
| **Scale** | ScaleInPopupAnimation, ScaleOutPopupAnimation |
| **Rotate** | RotateToAnimation |
| **Base** | FadeToPopupAnimation, TranslateToPopupAnimation, ScaleToPopupAnimation |
| **Combined** | AppearingPopupAnimation, DisappearingPopupAnimation |
| **Storyboard** | StoryboardAnimation |

---

## Animation Properties

All animations share these common properties from `PopupBaseAnimation`:

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Duration` | `int` | 500 | Animation duration in milliseconds |
| `Easing` | `EasingType` | Default | Easing function for the animation |
| `AnimateOnlyContent` | `bool` | true | Whether to animate only the content or the entire popup |

### EasingType Values

| Value | Description |
|-------|-------------|
| `Default` | Default easing |
| `Linear` | Constant speed |
| `BounceIn` | Bounce effect at start |
| `BounceOut` | Bounce effect at end |
| `CubicIn` | Cubic acceleration |
| `CubicOut` | Cubic deceleration |
| `CubicInOut` | Cubic acceleration/deceleration |
| `SinIn` | Sinusoidal acceleration |
| `SinOut` | Sinusoidal deceleration |
| `SinInOut` | Sinusoidal acceleration/deceleration |
| `SpringIn` | Spring effect at start |
| `SpringOut` | Spring effect at end |

---

## Basic Animations

### Fade Animations

#### FadeInPopupAnimation

Fades the popup from transparent to opaque:

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:FadeInPopupAnimation Duration=300, Easing=CubicOut}">
    <!-- Content -->
</uxd:PopupPage>
```

Properties:
- Inherits all base properties
- `InitialOpacity`: Starting opacity (default: 0)
- `FinalOpacity`: Ending opacity (default: 1)

#### FadeOutPopupAnimation

Fades the popup from opaque to transparent:

```xml
<uxd:PopupPage
    DisappearingAnimation="{uxd:FadeOutPopupAnimation Duration=200, Easing=Linear}">
    <!-- Content -->
</uxd:PopupPage>
```

---

### Move Animations

#### MoveInPopupAnimation

Moves the popup in from a specified direction:

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:MoveInPopupAnimation MoveDirection=Bottom, Duration=300, Easing=CubicOut}">
    <!-- Content -->
</uxd:PopupPage>
```

Properties:
- `MoveDirection`: Direction to move from (`Left`, `Top`, `Right`, `Bottom`)
- `TranslationFromCenter`: Distance from center to start (auto-calculated if not set)

#### MoveOutPopupAnimation

Moves the popup out to a specified direction:

```xml
<uxd:PopupPage
    DisappearingAnimation="{uxd:MoveOutPopupAnimation MoveDirection=Bottom, Duration=400, Easing=CubicIn}">
    <!-- Content -->
</uxd:PopupPage>
```

### MoveDirection Values

| Value | Description |
|-------|-------------|
| `Left` | Move from/to left side |
| `Top` | Move from/to top side |
| `Right` | Move from/to right side |
| `Bottom` | Move from/to bottom side |

---

### Scale Animations

#### ScaleInPopupAnimation

Scales the popup from smaller to normal size:

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:ScaleInPopupAnimation Duration=300, ScaleFrom=0.6, Easing=CubicOut}">
    <!-- Content -->
</uxd:PopupPage>
```

Properties:
- `ScaleFrom`: Starting scale value (default: 0.6)

#### ScaleOutPopupAnimation

Scales the popup from normal to smaller size:

```xml
<uxd:PopupPage
    DisappearingAnimation="{uxd:ScaleOutPopupAnimation Duration=200, ScaleTo=0.8, Easing=CubicIn}">
    <!-- Content -->
</uxd:PopupPage>
```

Properties:
- `ScaleTo`: Ending scale value (default: 0.6)

---

### Rotation Animation

#### RotateToAnimation

Animates rotation on Z, X, and/or Y axes:

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:RotateToAnimation Rotation=360, Duration=500, Easing=CubicOut}">
    <!-- Content -->
</uxd:PopupPage>
```

Properties:
- `Rotation`: Z-axis rotation in degrees
- `RotationX`: X-axis rotation in degrees
- `RotationY`: Y-axis rotation in degrees

---

## Storyboard Animation

`StoryboardAnimation` combines multiple animations (up to 5) that can run in parallel or sequentially.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Animation1` - `Animation5` | `IBaseAnimation` | Animations in the storyboard |
| `Strategy` | `StoryboardStrategy` | Execution strategy |
| `AnimateOnlyContent` | `bool` | Animate only content |

### StoryboardStrategy Values

| Value | Description |
|-------|-------------|
| `RunAllAtStart` | Run all animations in parallel |
| `RunAllSequentially` | Run animations one after another |

### Parallel Execution (Default)

All animations run simultaneously:

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:StoryboardAnimation
        Strategy=RunAllAtStart,
        Animation1={uxd:FadeInPopupAnimation Duration=300},
        Animation2={uxd:ScaleInPopupAnimation Duration=300, ScaleFrom=0.8}}">
    <!-- Content -->
</uxd:PopupPage>
```

### Sequential Execution

Animations run one after another:

```xml
<uxd:PopupPage
    DisappearingAnimation="{uxd:StoryboardAnimation
        Strategy=RunAllSequentially,
        Animation1={uxd:ScaleToPopupAnimation Scale=1.2, Duration=150, Easing=Linear},
        Animation2={uxd:ScaleToPopupAnimation Scale=0.5, Duration=150, Easing=Linear},
        Animation3={uxd:MoveOutPopupAnimation MoveDirection=Left, Duration=500, Easing=SpringOut}}">
    <!-- Content -->
</uxd:PopupPage>
```

### Using More Animations

You can combine up to 5 animations in a storyboard:

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:StoryboardAnimation
        Strategy=RunAllAtStart,
        Animation1={uxd:FadeInPopupAnimation Duration=300},
        Animation2={uxd:ScaleInPopupAnimation Duration=300, ScaleFrom=0.9},
        Animation3={uxd:MoveInPopupAnimation MoveDirection=Bottom, Duration=400},
        Animation4={uxd:RotateToAnimation Rotation=5, Duration=300},
        Animation5={uxd:FadeToPopupAnimation FinalOpacity=1, Duration=200}}">
    <!-- Content -->
</uxd:PopupPage>
```

---

## Composed Animations

The library provides pre-built composed animations that combine multiple effects.

### AppearingPopupAnimation

Combined animation: fade in + scale in + move in (parallel execution):

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:AppearingPopupAnimation AppearingDirection=Bottom}">
    <!-- Content -->
</uxd:PopupPage>
```

Properties:
- `AppearingDirection`: Direction the popup appears from (`Left`, `Top`, `Right`, `Bottom`)

This animation internally creates:
- `FadeInPopupAnimation` (500ms)
- `MoveInPopupAnimation` (500ms, 200px translation)
- `ScaleInPopupAnimation` (500ms)

### DisappearingPopupAnimation

Combined animation: fade out + scale out + move out (parallel execution):

```xml
<uxd:PopupPage
    DisappearingAnimation="{uxd:DisappearingPopupAnimation DisappearingDirection=Bottom}">
    <!-- Content -->
</uxd:PopupPage>
```

Properties:
- `DisappearingDirection`: Direction the popup disappears to

---

## Creating a Custom Animation

To create a custom animation, extend `PopupBaseAnimation`:

```csharp
using UXDivers.Popups;

namespace YourApp.Animations;

/// <summary>
/// Custom animation that shakes the popup horizontally.
/// </summary>
public class ShakePopupAnimation : PopupBaseAnimation
{
    public static readonly BindableProperty ShakeDistanceProperty = BindableProperty.Create(
        nameof(ShakeDistance),
        typeof(double),
        typeof(ShakePopupAnimation),
        10.0);

    /// <summary>
    /// Gets or sets the distance to shake in pixels.
    /// </summary>
    public double ShakeDistance
    {
        get => (double)GetValue(ShakeDistanceProperty);
        set => SetValue(ShakeDistanceProperty, value);
    }

    public static readonly BindableProperty ShakeCountProperty = BindableProperty.Create(
        nameof(ShakeCount),
        typeof(int),
        typeof(ShakePopupAnimation),
        3);

    /// <summary>
    /// Gets or sets the number of shakes.
    /// </summary>
    public int ShakeCount
    {
        get => (int)GetValue(ShakeCountProperty);
        set => SetValue(ShakeCountProperty, value);
    }

    protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
    {
        // Store initial position if needed
        // Called before animation starts
    }

    protected internal override Animation CreateAnimation(VisualElement target, PopupPage popup)
    {
        var animation = new Animation();
        double segmentDuration = 1.0 / (ShakeCount * 2);
        double currentTime = 0;

        for (int i = 0; i < ShakeCount; i++)
        {
            // Move right
            animation.Add(currentTime, currentTime + segmentDuration,
                new Animation(v => target.TranslationX = v, 0, ShakeDistance));
            currentTime += segmentDuration;

            // Move left
            animation.Add(currentTime, currentTime + segmentDuration,
                new Animation(v => target.TranslationX = v, ShakeDistance, -ShakeDistance));
            currentTime += segmentDuration;
        }

        // Return to center
        animation.Add(currentTime, 1.0,
            new Animation(v => target.TranslationX = v, -ShakeDistance, 0));

        return animation;
    }
}
```

### Using the Custom Animation

In XAML (requires XML namespace registration):

```xml
<uxd:PopupPage
    xmlns:anim="clr-namespace:YourApp.Animations"
    AppearingAnimation="{anim:ShakePopupAnimation ShakeDistance=15, ShakeCount=4, Duration=400}">
    <!-- Content -->
</uxd:PopupPage>
```

In C#:

```csharp
var popup = new MyPopup
{
    AppearingAnimation = new ShakePopupAnimation
    {
        ShakeDistance = 15,
        ShakeCount = 4,
        Duration = 400,
        Easing = EasingType.CubicOut
    }
};
```

---

## Animation Examples

### Bottom Sheet Style

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:MoveInPopupAnimation MoveDirection=Bottom, Duration=300, Easing=CubicOut}"
    DisappearingAnimation="{uxd:MoveOutPopupAnimation MoveDirection=Bottom, Duration=200, Easing=CubicIn}">
    <!-- Content -->
</uxd:PopupPage>
```

### Dialog with Scale

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:ScaleInPopupAnimation Duration=200, ScaleFrom=0.9, Easing=CubicOut}"
    DisappearingAnimation="{uxd:ScaleOutPopupAnimation Duration=150, ScaleTo=0.9, Easing=CubicIn}">
    <!-- Content -->
</uxd:PopupPage>
```

### Toast with Fade

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:FadeInPopupAnimation Duration=400}"
    DisappearingAnimation="{uxd:FadeOutPopupAnimation Duration=300}">
    <!-- Content -->
</uxd:PopupPage>
```

### Slide from Right

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:MoveInPopupAnimation MoveDirection=Right, Duration=300, Easing=SinIn}"
    DisappearingAnimation="{uxd:MoveOutPopupAnimation MoveDirection=Left, Duration=300, Easing=SinOut}">
    <!-- Content -->
</uxd:PopupPage>
```

### Spring Effect

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:MoveInPopupAnimation MoveDirection=Right, Duration=400, Easing=SpringOut}"
    DisappearingAnimation="{uxd:MoveOutPopupAnimation MoveDirection=Left, Duration=300, Easing=CubicIn}">
    <!-- Content -->
</uxd:PopupPage>
```

### Complex Entrance with Storyboard

```xml
<uxd:PopupPage
    AppearingAnimation="{uxd:StoryboardAnimation
        Strategy=RunAllAtStart,
        Animation1={uxd:FadeInPopupAnimation Duration=300},
        Animation2={uxd:MoveInPopupAnimation MoveDirection=Bottom, Duration=400, Easing=CubicOut},
        Animation3={uxd:ScaleInPopupAnimation Duration=400, ScaleFrom=0.95, Easing=CubicOut}}">
    <!-- Content -->
</uxd:PopupPage>
```

### Dramatic Exit Sequence

A sequential animation with a quick scale up followed by scale down and fade:

```xml
<uxd:PopupPage
    DisappearingAnimation="{uxd:StoryboardAnimation
        Strategy=RunAllSequentially,
        Animation1={uxd:ScaleToPopupAnimation Scale=1.1, Duration=100, Easing=CubicOut},
        Animation2={uxd:ScaleToPopupAnimation Scale=0.8, Duration=200, Easing=CubicIn},
        Animation3={uxd:FadeOutPopupAnimation Duration=200, Easing=CubicIn}}">
    <!-- Content -->
</uxd:PopupPage>
```

### Nested Storyboard (Parallel inside Sequential)

For more complex effects, you can nest storyboards:

```xml
<uxd:PopupPage
    DisappearingAnimation="{uxd:StoryboardAnimation
        Strategy=RunAllSequentially,
        Animation1={uxd:ScaleToPopupAnimation Scale=1.1, Duration=100, Easing=CubicOut},
        Animation2={uxd:StoryboardAnimation
            Strategy=RunAllAtStart,
            Animation1={uxd:ScaleToPopupAnimation Scale=0.8, Duration=200, Easing=CubicIn},
            Animation2={uxd:FadeOutPopupAnimation Duration=200, Easing=CubicIn}}}">
    <!-- Content -->
</uxd:PopupPage>
```

---

## AnimateOnlyContent Property

By default, `AnimateOnlyContent` is `true`, which means only the popup's content (not the background overlay) is animated. Set to `false` to animate the entire popup including the background:

```xml
<!-- Animate only the content (default) -->
<uxd:PopupPage
    AppearingAnimation="{uxd:FadeInPopupAnimation Duration=300, AnimateOnlyContent=True}">
    <!-- Content -->
</uxd:PopupPage>

<!-- Animate the entire popup including background -->
<uxd:PopupPage
    AppearingAnimation="{uxd:FadeInPopupAnimation Duration=300, AnimateOnlyContent=False}">
    <!-- Content -->
</uxd:PopupPage>
```

This is useful when you want the background overlay to fade in/out along with the content.

---

## Default Popup Animations

Each pre-built popup control has default animations defined in `PopupStyles.xaml`. The base style `PopupBaseStyle` sets:

```xml
<Style x:Key="PopupBaseStyle" TargetType="uxd:PopupPage">
    <Setter Property="AppearingAnimation"
            Value="{uxd:MoveInPopupAnimation MoveDirection=Bottom, Duration=300, Easing=CubicOut}" />
    <Setter Property="DisappearingAnimation"
            Value="{uxd:MoveOutPopupAnimation MoveDirection=Bottom, Duration=400, Easing=CubicIn}" />
</Style>
```

Individual popup styles may override these defaults. See the [Popup Controls](Popup-Controls.md) page for specific default animations.
