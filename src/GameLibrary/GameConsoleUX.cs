﻿/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines the game console UX capabilities for interactions via keyboard.
/// </summary>
/// <param name="escExit">Indicates if the [Esc] key exits the application. Default is true.</param>
/// <remarks>
/// GameConsoleUX can be extended or used in the game's own game UX class.
/// In most cases, the values returned by the GameConsoleUX methods need to be evaluated by the game.
/// For example, <see cref="GetMove"/>'s return value may need to be mapped to the game's own enum of game move values.
/// </remarks>
public class GameConsoleUX(bool escExit = true)
{
    /// <summary>
    /// Gets or sets an indicator if the [Esc] key exits the application. Default is true.
    /// </summary>
    public bool EscExit { get; set; } = escExit;

    /// <summary>
    /// Gets the Enter key input.
    /// </summary>
    /// <returns>The key input.</returns>
    public ConsoleKey GetEnter() => GetKey(ConsoleKey.Enter);

    /// <summary>
    /// Gets the Esc key input.
    /// </summary>
    /// <returns>The key input.</returns>
    /// <remarks>This method ignores <see cref="EscExit"/>.</remarks>
    public ConsoleKey GetEsc() => GetKey(ConsoleKey.Escape, false);

    /// <summary>
    /// Gets a move (arrow) key input.
    /// </summary>
    /// <returns>The key input.</returns>
    public ConsoleKey GetMove() => GetKey([ConsoleKey.UpArrow, ConsoleKey.RightArrow, ConsoleKey.LeftArrow, ConsoleKey.DownArrow]);

    /// <summary>
    /// Gets a Y or N key input.
    /// </summary>
    /// <returns>The key input.</returns>
    public ConsoleKey GetYN() => GetKey([ConsoleKey.Y, ConsoleKey.N]);

    /// <summary>
    /// Gets a key input.
    /// </summary>
    /// <param name="validKey">The valid key input.</param>
    /// <param name="evalEscExit">Indicates to evaluate EscExit or not. Default is true.</param>
    /// <returns>The key input.</returns>
    /// <remarks>Pass evalEscExit = false to ignore <see cref="EscExit"/>. This can be used for example when [Esc] is a valid key input.</remarks>
    public ConsoleKey GetKey(ConsoleKey validKey, bool evalEscExit = true) => GetKey([validKey], evalEscExit);

    /// <summary>
    /// Gets a key input.
    /// </summary>
    /// <param name="validKeys">The valid key inputs.</param>
    /// <param name="evalEscExit">Indicates to evaluate EscExit or not. Default is true.</param>
    /// <returns>The key input.</returns>
    /// <remarks>Pass evalEscExit = false to ignore <see cref="EscExit"/>. This can be used for example when [Esc] is a valid key input.</remarks>
    public virtual ConsoleKey GetKey(HashSet<ConsoleKey> validKeys, bool evalEscExit = true)
    {
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;
            if (evalEscExit && EscExit && key == ConsoleKey.Escape) Environment.Exit(0);
        } while (!validKeys.Contains(key)); // Loop until the user presses a valid key.
        return key;
    }

    /// <summary>
    /// Gets any key input except the invalid keys.
    /// </summary>
    /// <param name="invalidKeys">The invalid key inputs.</param>
    /// <param name="evalEscExit">Indicates to evaluate EscExit or not. Default is true.</param>
    /// <returns>The key input.</returns>
    /// <remarks>
    /// <para>Pass evalEscExit = false to ignore <see cref="EscExit"/>. This can be used for example when [Esc] is a valid key input.</para>
    /// <para>Use when there are more keys allowed than not, that it makes sense to list the invalid keys instead.</para>
    /// </remarks>
    public virtual ConsoleKey GetKeyExcept(HashSet<ConsoleKey> invalidKeys, bool evalEscExit = true)
    {
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;
            if (evalEscExit && EscExit && key == ConsoleKey.Escape) Environment.Exit(0);
        } while (invalidKeys.Contains(key)); // Loop until the user presses a valid key.
        return key;
    }

    /// <summary>
    /// Gets a valid input key info, which may include the pressed modifier(s) Shift, Ctrl and/or Alt.
    /// </summary>
    /// <param name="validKeyInfos">The set of valid input key infos.</param>
    /// <param name="evalEscExit">Indicates to evaluate EscExit or not. Default is true.</param>
    /// <returns>If valid, returns the input key info. Else, or if there's no key available to check, returns null.</returns>
    /// <remarks>
    /// <para>Pass evalEscExit = false to ignore <see cref="EscExit"/>. This can be used for example when [Esc] is a valid key input.</para>
    /// <para>As a non-blocking method, use in a loop that can perform tasks until a valid input key info is returned.</para>
    /// <code>
    /// // Example usage in an IGameUI* implementation; to accept a press on the [Spacebar]...
    /// private readonly GameConsoleUX _gameConsoleUX = new();
    /// 
    /// private readonly HashSet<ConsoleKeyInfo> _validKeyInfos = [new(' ', ConsoleKey.Spacebar, false, false, false)];
    /// 
    /// public bool Action()
    /// {
    ///     ConsoleKeyInfo? cki = null;
    ///     while (cki is null)
    ///     {
    ///         Refresh();
    ///         cki = _gameConsoleUX.GetKeyInfo(_validKeyInfos);
    ///     }
    ///     return GamePlay.Action(new() { Input = cki.Value.Key });
    /// }
    /// </code>
    /// </remarks>
    public virtual ConsoleKeyInfo? GetKeyInfo(HashSet<ConsoleKeyInfo> validKeyInfos, bool evalEscExit = true)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (evalEscExit && EscExit && keyInfo.Key == ConsoleKey.Escape) Environment.Exit(0);
            if (validKeyInfos.Contains(keyInfo)) return keyInfo;
        }
        return null;
    }

    /// <summary>
    /// Gets an entry of type T.
    /// </summary>
    /// <typeparam name="T">A type that implements <seealso cref="IParsable{TSelf}"/>.</typeparam>
    /// <returns>The valid entry of type T.</returns>
    /// <remarks>Loops until an entry that can be parsed to type T is entered.</remarks>
    /// <code>
    /// // Example usage in an IGameUI* implementation; to accept a numeric entry...
    /// private readonly GameConsoleUX _gameConsoleUX = new();
    /// 
    /// public bool Action()
    /// {
    ///     Console.CursorVisible = true;
    ///     GameActionInfo gameActionInfo = new()
    ///     {
    ///         ActionType = GameActionType.Response,
    ///         Input = GameConsoleUX.GetParsableEntry<int>()
    ///     };
    ///     Console.CursorVisible = false;
    ///     return GamePlay.Action(gameActionInfo);
    /// }
    /// </code>
    public static T GetParsableEntry<T>()
        where T : IParsable<T>
    {
        T? entry;
        while (true)
        {
            if (T.TryParse(Console.ReadLine(), null, out entry)) break;
        }
        return entry;
    }
}