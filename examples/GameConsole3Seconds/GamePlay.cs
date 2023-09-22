/*
* GameConsole3Seconds (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
using GameLibrary;
using System.Diagnostics;

namespace GameConsole3Seconds;

internal class GamePlay : IGamePlay<ConsoleKey, bool>
{
    public Stopwatch Stopwatch { get; private set; } = new();

    public bool IsWon {  get; set; }

    public bool Quit { get; set; }

    public bool Start()
    {
        Stopwatch.Start();
        return Stopwatch.IsRunning;
    }

    public bool Action(ConsoleKey action)
    {
        Stopwatch.Stop();
        long elapsed = Stopwatch.ElapsedMilliseconds;
        if (elapsed >= 3000 && elapsed <= 3009) IsWon = true;
        else IsWon = false;
        return !Stopwatch.IsRunning;
    }

    public bool Continue()
    {
        IsWon = false;
        Stopwatch.Restart();
        return Stopwatch.IsRunning;
    }

    public bool GameOver() => IsWon || Quit;

    public void End() => Quit = true;
}