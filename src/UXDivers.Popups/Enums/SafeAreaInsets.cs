namespace UXDivers.Popups;

/// <summary>
/// Specifies which edges of the popup should respect safe area insets.
/// This is a flag enum, allowing multiple values to be combined.
/// </summary>
[Flags]
public enum SafeAreaAsPadding
{
    /// <summary>
    /// No safe area insets are applied.
    /// </summary>
    None = 0,

    /// <summary>
    /// Apply safe area insets to the top edge.
    /// </summary>
    Top = 1 << 0,  // 1

    /// <summary>
    /// Apply safe area insets to the left edge.
    /// </summary>
    Left = 1 << 1,  // 2

    /// <summary>
    /// Apply safe area insets to the right edge.
    /// </summary>
    Right = 1 << 2,  // 4

    /// <summary>
    /// Apply safe area insets to the bottom edge.
    /// </summary>
    Bottom = 1 << 3,  // 8

    /// <summary>
    /// Apply safe area insets to all edges (Top, Left, Right, and Bottom).
    /// </summary>
    All = Top | Left | Right | Bottom  // 15
}
