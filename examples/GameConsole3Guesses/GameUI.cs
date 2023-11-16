/*
* GameConsole3Guesses (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
using GameLibrary;

namespace GameConsole3Guesses;

internal class GameUI : IGameUI<GamePlay>
{
    /// <summary>
    /// Note that a game can have different instances of GameConsoleUX for different game action mode and type combinations.
    /// For example, one instance can have EscExit = true, and another instance with EscExit = false.
    /// </summary>
    private readonly GameConsoleUX _gameConsoleUX = new();

    public GamePlay GamePlay { get; set; }

    public GameUI() => GamePlay = new();

    public void Render()
    {
        Console.Clear();
        Console.WriteLine("Guess the secret number between 1 to 10.");
    }

    public void Refresh() => Render();

    public bool Action()
    {
        Console.WriteLine();
        Console.Write($"Guess #{GamePlay.Tries + 1}: ");
        Console.CursorVisible = true;
        GameActionInfo gameActionInfo = new()
        {
            ActionType = GameActionType.Response,
            Input = GameConsoleUX.GetParsableEntry<int>()
        };
        Console.CursorVisible = false;
        return GamePlay.Action(gameActionInfo);
    }

    public bool Continue()
    {
        if (GamePlay.Continue())
        {
            Console.WriteLine();
            Console.WriteLine("Try again? (Y/N): ");
            return _gameConsoleUX.GetYN() == ConsoleKey.Y;
        }
        return true;
    }

    public void End()
    {
        Console.WriteLine();
        Console.WriteLine($"The secret number was {GamePlay.Secret}.");
        Console.WriteLine();
        ConsoleColor foregroundColor = Console.ForegroundColor;
        if (GamePlay.IsWon)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You got it!!!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You failed!");
        }
        Console.ForegroundColor = foregroundColor;
    }
}