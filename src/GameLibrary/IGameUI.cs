/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines the game UI.
/// </summary>
/// <typeparam name="TGamePlay">The game play.</typeparam>
public interface IGameUI<TGamePlay>
    where TGamePlay : IGamePlay, new()
{
    /// <summary>
    /// Gets or sets the game play.
    /// </summary>
    public TGamePlay GamePlay { get; set; }

    /// <summary>
    /// Starts the game.
    /// </summary>
    /// <returns>True if the game is started, else false.</returns>
    public bool Start() => GamePlay.Start();

    /// <summary>
    /// Renders the game UI.
    /// </summary>
    public void Render();

    /// <summary>
    /// Refreshes the game UI,
    /// </summary>
    public void Refresh() => Render();

    /// <summary>
    /// Executes the game action.
    /// </summary>
    /// <returns>True if the game action is executed, else false.</returns>
    public bool Action();

    /// <summary>
    /// Continues the game.
    /// </summary>
    /// <returns>True to continue, else false.</returns>
    public bool Continue();

    /// <summary>
    /// Detects if it's game over.
    /// </summary>
    /// <returns>True if game over, else false.</returns>
    public bool GameOver() => GamePlay.GameOver();

    /// <summary>
    /// Ends the game.
    /// </summary>
    public void End() => GamePlay.End();
}