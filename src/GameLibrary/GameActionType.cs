namespace GameLibrary;

/// <summary>
/// Specifies the game action type.
/// </summary>
public enum GameActionType
{
    /// <summary>
    /// Gameplay controls like moves, attacks, selections, etc.
    /// </summary>
    Control,
    /// <summary>
    /// Responses or data entries to gameplay prompts.
    /// </summary>
    Response,
    /// <summary>
    /// Navigation of non-gameplay elements like menus, configurations, settings, etc.
    /// </summary>
    Navigation,
    /// <summary>
    /// Other actions.
    /// </summary>
    Other
}