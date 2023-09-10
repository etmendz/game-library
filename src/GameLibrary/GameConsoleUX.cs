/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines the game console UX capabilities for game flow and game play interactions.
/// </summary>
/// <param name="escExit">Indicates if the [Esc] key exits the application. Default is true.</param>
public class GameConsoleUX(bool escExit = true)
{
    /// <summary>
    /// Gets or sets an indicator if the [Esc] key exits the application.
    /// </summary>
    public bool EscExit { get; set; } = escExit;

    /// <summary>
    /// Gets a Y or N key input.
    /// </summary>
    /// <returns>The key input.</returns>
    public ConsoleKey GetYN() => GetKey(new HashSet<ConsoleKey>() { ConsoleKey.Y, ConsoleKey.N });

    /// <summary>
    /// Gets a move (arrow) key input.
    /// </summary>
    /// <returns>The key input.</returns>
    public ConsoleKey GetMove() => GetKey(new HashSet<ConsoleKey>() { ConsoleKey.UpArrow, ConsoleKey.RightArrow, ConsoleKey.LeftArrow, ConsoleKey.DownArrow });

    /// <summary>
    /// Gets a key input.
    /// </summary>
    /// <param name="validKey">The valid key input.</param>
    /// <returns>The key input.</returns>
    public ConsoleKey GetKey(ConsoleKey validKey) => GetKey(new HashSet<ConsoleKey> { validKey });

    /// <summary>
    /// Gets a key input.
    /// </summary>
    /// <param name="validKeys">The valid key inputs.</param>
    /// <returns>The key input.</returns>
    public virtual ConsoleKey GetKey(HashSet<ConsoleKey> validKeys)
    {
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;
            if (EscExit && key == ConsoleKey.Escape) Environment.Exit(0);
        } while (!validKeys.Contains(key)); // Loop until the user presses a valid key.
        return key;
    }
}