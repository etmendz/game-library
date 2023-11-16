/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines the game play -- with async methods.
/// </summary>
/// <remarks>
/// <para>The game play is essentially the actual game.</para>
/// <para>When implemented separate from any UI-specific aspects, the game play can be re-used in different UI/UX platforms.</para>
/// </remarks>
public interface IGamePlayAsync
{
    /// <summary>
    /// Starts the game play.
    /// </summary>
    /// <returns>True if the game play is started, else false.</returns>
    public Task<bool> Start();

    /// <summary>
    /// Executes the game play action.
    /// </summary>
    /// <param name="gameActionInfo">The game action info to execute, evaluate or process.</param>
    /// <returns>True if the game play action is executed, else false.</returns>
    public Task<bool> Action(GameActionInfo gameActionInfo);

    /// <summary>
    /// Continues the game play.
    /// </summary>
    /// <returns>True to continue, else false.</returns>
    public Task<bool> Continue();

    /// <summary>
    /// Detects if it's game over.
    /// </summary>
    /// <returns>True if game over, else false.</returns>
    public Task<bool> GameOver();

    /// <summary>
    /// Ends the game play.
    /// </summary>
    public Task End();
}