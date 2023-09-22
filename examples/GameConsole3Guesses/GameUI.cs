/*
* GameConsole3Guesses (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
using GameLibrary;

namespace GameConsole3Guesses;

internal class GameUI : IGameUI<GamePlay, int, bool>
{
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
        int guess;
        while (true)
        {
            if (Int32.TryParse(Console.ReadLine(), out guess)) break;
        }
        Console.CursorVisible = false;
        return GamePlay.Action(guess);
    }

    public bool Continue()
    {
        bool isOK = false;
        if (GamePlay.Continue())
        {
            Console.WriteLine();
            Console.WriteLine("Try again? (Y/N): ");
            isOK = new GameConsoleUX().GetYN() == ConsoleKey.Y;
        }
        return isOK;
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