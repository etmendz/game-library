/*
* GameConsole3Guesses (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
using GameLibrary;

namespace GameConsole3Guesses;

internal class GamePlay : IGamePlay<int, bool>
{
    public int Secret { get; private set; }

    public bool IsWon {  get; set; }
    
    public int Tries { get; set; }

    public bool Start()
    {
        Secret = GameRandomizer.Next(1, 11);
        IsWon = false;
        Tries = 0;
        return true;
    }

    public bool Action(int action)
    {
        if (action == Secret) IsWon = true;
        Tries++;
        return true;
    }

    public bool Continue() => !IsWon && Tries < 3;

    public bool GameOver() => IsWon || (!IsWon && Tries >= 3);

    public void End() => Secret = 0;
}