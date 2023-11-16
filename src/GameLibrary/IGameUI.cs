/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines the game UI.
/// </summary>
/// <typeparam name="TGamePlay">The game play.</typeparam>
/// <remarks>Implementations can be UI/UX platform specific.</remarks>
public interface IGameUI<TGamePlay>
    where TGamePlay : IGamePlay
{
    /// <summary>
    /// Gets or sets the game play.
    /// </summary>
    public TGamePlay GamePlay { get; set; }

    /// <summary>
    /// Starts the game.
    /// </summary>
    /// <returns>True if the game is started, else false.</returns>
    /// <remarks>
    /// The default implementation is as follows:
    /// <code>
    /// bool start = GamePlay.Start();
    /// if (start) Render();
    /// return start;
    /// </code>
    /// </remarks>
    public bool Start()
    {
        bool start = GamePlay.Start();
        if (start) Render();
        return start;
    }

    /// <summary>
    /// Renders the game UI.
    /// </summary>
    /// <remarks>
    /// The default implementation of Start() calls Render().
    /// If not called at Start(), the first call should be made in Action().
    /// </remarks>
    public void Render();

    /// <summary>
    /// Refreshes the game UI.
    /// </summary>
    /// <remarks>
    /// A typical implementation simply calls Render().
    /// In this sense, where Render() is called at Start(), Refresh() is a mnemonic to re-render during game play.
    /// Thus, Refresh() is usually called in Action() and/or Continue().
    /// </remarks>
    public void Refresh(); // => Render();

    /// <summary>
    /// Executes the game action.
    /// </summary>
    /// <returns>True if the game play action is executed, else false.</returns>
    public bool Action();

    /// <summary>
    /// Continues the game.
    /// </summary>
    /// <returns>True to continue, else false.</returns>
    public bool Continue(); // => GamePlay.Continue();

    /// <summary>
    /// Detects if it's game over.
    /// </summary>
    /// <returns>True if game over, else false.</returns>
    /// <remarks>The default implementation calls GamePlay.GameOver().</remarks>
    public bool GameOver() => GamePlay.GameOver();

    /// <summary>
    /// Ends the game.
    /// </summary>
    /// <remarks>The default implementation calls GamePlay.End().</remarks>
    public void End() => GamePlay.End();
}