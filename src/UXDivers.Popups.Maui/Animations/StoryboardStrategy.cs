namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Specifies how animations in a storyboard should be executed.
    /// </summary>
    public enum StoryboardStrategy
    {
        /// <summary>
        /// Run all animations simultaneously in parallel.
        /// </summary>
        RunAllAtStart,

        /// <summary>
        /// Run all animations one after another in sequence.
        /// </summary>
        RunAllSequentially
    }
}
