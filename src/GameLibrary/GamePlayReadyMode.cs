/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Specifies the game play ready mode.
/// </summary>
public enum GamePlayReadyMode
{
    /// <summary>
    /// The game flows for a single game (starts only one game UI instance per launch).
    /// </summary>
    IfReady,
    /// <summary>
    /// The game flows for multiple games (can start more than one game UI instances per launch).
    /// </summary>
    WhileReady
}