/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines the game UI -- with async methods.
/// </summary>
/// <typeparam name="TGamePlay">The game play -- with async methods.</typeparam>
/// <remarks>Implementations can be UI/UX platform specific.</remarks>
public interface IGameUIAsync<TGamePlay>
    where TGamePlay : IGamePlayAsync
{
    /// <summary>
    /// Gets or sets the game play.
    /// </summary>
    public TGamePlay GamePlay { get; set; }

    /// <summary>
    /// Starts the game.
    /// </summary>
    /// <returns>True if the game is started, else false.</returns>
    public Task<bool> Start();

    /// <summary>
    /// Renders the game UI.
    /// </summary>
    public Task Render();

    /// <summary>
    /// Refreshes the game UI.
    /// </summary>
    public Task Refresh();

    /// <summary>
    /// Executes the game action.
    /// </summary>
    /// <returns>True if the game play action is executed, else false.</returns>
    public Task<bool> Action();

    /// <summary>
    /// Continues the game.
    /// </summary>
    /// <returns>True to continue, else false.</returns>
    public Task<bool> Continue(); // => GamePlay.Continue();

    /// <summary>
    /// Detects if it's game over.
    /// </summary>
    /// <returns>True if game over, else false.</returns>
    /// <remarks>The default implementation awaits GamePlay.GameOver().</remarks>
    public async Task<bool> GameOver() => await GamePlay.GameOver();

    /// <summary>
    /// Ends the game.
    /// </summary>
    /// <remarks>The default implementation awaits GamePlay.End().</remarks>
    public async Task End() => await GamePlay.End();
}