/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines the game play.
/// </summary>
public interface IGamePlay
{
    /// <summary>
    /// Starts the game play.
    /// </summary>
    /// <returns>True if the game play is started, else false.</returns>
    public bool Start();

    /// <summary>
    /// Executes the game play action.
    /// </summary>
    /// <typeparam name="T">The type of action.</typeparam>
    /// <param name="action">The game play action to execute.</param>
    /// <returns>True if the game play action is executed, else false.</returns>
    public bool Action<T>(T action);

    /// <summary>
    /// Continues the game play.
    /// </summary>
    /// <returns>True to continue, else false.</returns>
    public bool Continue();

    /// <summary>
    /// Detects if it's game over.
    /// </summary>
    /// <returns>True if game over, else false.</returns>
    public bool GameOver();

    /// <summary>
    /// Ends the game play.
    /// </summary>
    public void End();
}