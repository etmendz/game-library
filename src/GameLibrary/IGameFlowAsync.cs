/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines the game flow -- with async methods.
/// </summary>
/// <remarks>Implementations can be UI/UX platform specific.</remarks>
public interface IGameFlowAsync
{
    /// <summary>
    /// Plays the game.
    /// </summary>
    public Task Play();

    /// <summary>
    /// Ready.
    /// </summary>
    /// <returns>True of ready, else false.</returns>
    public Task<bool> Ready();

    /// <summary>
    /// Set.
    /// </summary>
    public Task Set();

    /// <summary>
    /// Go!
    /// </summary>
    public Task Go();
}