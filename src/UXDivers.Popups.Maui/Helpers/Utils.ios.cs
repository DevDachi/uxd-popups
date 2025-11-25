using UIKit;

namespace UXDivers.Popups.Maui
{
    /// <summary>
    /// Utility class for iOS-specific helper methods.
    /// </summary>
    internal partial class Utils
    {
        /// <summary>
        /// Retrieves the main application window.
        /// </summary>
        /// <returns>The main <see cref="UIView"/> representing the key window.</returns>
        public static UIView? GetMainWindow()
        {
            // For iOS versions below 15 or Mac Catalyst versions below 15
            if (!OperatingSystem.IsIOSVersionAtLeast(15) && !OperatingSystem.IsMacCatalystVersionAtLeast(15))
            {
                return UIApplication.SharedApplication.Windows.FirstOrDefault(w => w.IsKeyWindow);
            }

            // For iOS 15+ or Mac Catalyst 15+, use ConnectedScenes to find the key window
            return UIApplication.SharedApplication.ConnectedScenes.ToArray()
                .Where(s => s is UIWindowScene) // Filter for UIWindowScene
                .SelectMany(x => ((UIWindowScene)x).Windows) // Get all windows in the scene
                .FirstOrDefault(w => w.IsKeyWindow); // Find the key window
        }
    }
}
