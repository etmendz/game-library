namespace GameLibrary;

/// <summary>
/// Specifies the game action mode.
/// </summary>
public enum GameActionMode
{
    /// <summary>
    /// In-gameplay actions.
    /// </summary>
    GamePlay,
    /// <summary>
    /// Non-gameplay actions while the game is paused. Example, the action to resume the game.
    /// </summary>
    GamePause,
    /// <summary>
    /// Non-gameplay actions while the game is stopped. Example, the action to undo the last move that led to a gameover.
    /// </summary>
    GameStop
}