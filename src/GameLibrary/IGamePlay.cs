/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines the game play.
/// </summary>
/// <typeparam name="TActionIn">The action input type.</typeparam>
/// <typeparam name="TActionOut">The action output or result type.</typeparam>
/// <remarks>
/// <para>The game play is essentially the actual game.</para>
/// <para>When implemented separate from any UI-specific aspects, the game play can be re-used in different UI/UX platforms.</para>
/// </remarks>
public interface IGamePlay<in TActionIn, out TActionOut>
{
    /// <summary>
    /// Starts the game play.
    /// </summary>
    /// <returns>True if the game play is started, else false.</returns>
    public bool Start();

    /// <summary>
    /// Executes the game play action.
    /// </summary>
    /// <param name="action">The action input.</param>
    /// <returns>The action output or result.</returns>
    public TActionOut Action(TActionIn action);

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