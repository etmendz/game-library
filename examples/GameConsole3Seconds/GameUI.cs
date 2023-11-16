/*
* GameConsole3Seconds (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
using GameLibrary;

namespace GameConsole3Seconds;

internal class GameUI : IGameUI<GamePlay>
{
    /// <summary>
    /// Note that a game can have different instances of GameConsoleUX for different game action mode and type combinations.
    /// For example, one instance can have EscExit = true, and another instance with EscExit = false.
    /// </summary>
    private readonly GameConsoleUX _gameConsoleUX = new();

    private readonly HashSet<ConsoleKeyInfo> _validKeyInfos = [new(' ', ConsoleKey.Spacebar, false, false, false)];

    private bool _refresh = false;

    public GamePlay GamePlay { get; set; }

    public GameUI() => GamePlay = new();

    public void Render()
    {
        if (!_refresh)
        {
            Console.Clear();
            Console.WriteLine("Press the [Spacebar] to stop the clock at 3 seconds on the dot.");
            Console.WriteLine();
            _refresh = true; // Turn on refresh mode...
        }
        int left = Console.CursorLeft;
        int top = Console.CursorTop;
        Console.WriteLine(TimeSpan.FromMilliseconds(GamePlay.Stopwatch.ElapsedMilliseconds).ToString("mm':'ss'.'ff"));
        Console.SetCursorPosition(left, top);
    }

    public void Refresh() => Render();

    public bool Action()
    {
        ConsoleKeyInfo ? cki = null;
        while (cki is null)
        {
            Refresh();
            cki = _gameConsoleUX.GetKeyInfo(_validKeyInfos);
        }
        Console.WriteLine();
        return GamePlay.Action(new() { Input = cki.Value.Key });
    }

    public bool Continue()
    {
        Console.WriteLine();
        ConsoleColor foregroundColor = Console.ForegroundColor;
        if (GamePlay.IsWon)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You win!!!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You lose!");
        }
        Console.ForegroundColor = foregroundColor;
        Console.WriteLine();
        Console.WriteLine("Try again? (Y/N): ");
        if (_gameConsoleUX.GetYN() == ConsoleKey.Y)
        {
            _refresh = false; // Turn off refresh mode...
            GamePlay.Continue();
            Refresh();
        }
        else GamePlay.End();
        return !GamePlay.Quit;
    }
}