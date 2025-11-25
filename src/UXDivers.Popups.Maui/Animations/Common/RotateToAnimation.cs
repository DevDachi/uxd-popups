namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Base class for rotation animations, animating rotation on Z, X, and/or Y axes.
    /// </summary>
    public class RotateToAnimation : PopupBaseAnimation
    {
        public static readonly BindableProperty RotationProperty = BindableProperty.Create(
            nameof(Rotation),
            typeof(double?),
            typeof(RotateToAnimation),
            null);

        /// <summary>
        /// Gets or sets the target Z-axis rotation in degrees.
        /// </summary>
        public double? Rotation
        {
            get { return (double?)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        public static readonly BindableProperty RotationXProperty = BindableProperty.Create(
            nameof(RotationX),
            typeof(double?),
            typeof(RotateToAnimation),
            null);

        /// <summary>
        /// Gets or sets the target X-axis rotation in degrees.
        /// </summary>
        public double? RotationX
        {
            get { return (double?)GetValue(RotationXProperty); }
            set { SetValue(RotationXProperty, value); }
        }

        public static readonly BindableProperty RotationYProperty = BindableProperty.Create(
            nameof(RotationY),
            typeof(double?),
            typeof(RotateToAnimation),
            null);

        /// <summary>
        /// Gets or sets the target Y-axis rotation in degrees.
        /// </summary>
        public double? RotationY
        {
            get { return (double?)GetValue(RotationYProperty); }
            set { SetValue(RotationYProperty, value); }
        }

        private double _originalRotation;
        private double _originalRotationX;
        private double _originalRotationY;

        /// <summary>
        /// Creates the rotation animation that interpolates rotation values.
        /// </summary>
        internal protected override Animation CreateAnimation(VisualElement target, PopupPage popup)
        {
            var finalAnimation = new Animation();

            if (Rotation.HasValue)
            {
                finalAnimation.Add(0, 1, new Animation(d => target.Rotation = d, _originalRotation, Rotation.Value));
            }

            if (RotationX.HasValue)
            {
                finalAnimation.Add(0, 1, new Animation(d => target.RotationX = d, _originalRotationX, RotationX.Value));
            }
            
            if (RotationY.HasValue)
            {
                finalAnimation.Add(0, 1, new Animation(d => target.RotationY = d, _originalRotationY, RotationY.Value));
            }

            return finalAnimation;
        }

        /// <summary>
        /// Prepares the animation by saving the original rotation values.
        /// </summary>
        protected internal override void PrepareAnimation(VisualElement target, PopupPage popup)
        {
            _originalRotation = target.Rotation;
            _originalRotationX = target.RotationX;
            _originalRotationY = target.RotationY;
        }
    }
}
