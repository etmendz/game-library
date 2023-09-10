/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines the game flow.
/// </summary>
/// <remarks>Implementations can be UI/UX platform specific.</remarks>
public interface IGameFlow
{
    /// <summary>
    /// Plays the game.
    /// </summary>
    public void Play();

    /// <summary>
    /// Ready.
    /// </summary>
    /// <returns>True of ready, else false.</returns>
    public bool Ready();

    /// <summary>
    /// Set.
    /// </summary>
    public void Set();

    /// <summary>
    /// Go!
    /// </summary>
    public void Go();
}