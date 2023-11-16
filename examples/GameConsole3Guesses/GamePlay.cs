/*
* GameConsole3Guesses (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
using GameLibrary;

namespace GameConsole3Guesses;

internal class GamePlay : IGamePlay
{
    public int Secret { get; private set; }

    public bool IsWon {  get; set; }
    
    public int Tries { get; set; }

    public bool Start()
    {
        Secret = Random.Shared.Next(1, 11);
        IsWon = false;
        Tries = 0;
        return true;
    }

    public bool Action(GameActionInfo gameActionInfo)
    {
        if (gameActionInfo.GetInputAs<int>() == Secret) IsWon = true;
        Tries++;
        return true;
    }

    public bool Continue() => !IsWon && Tries < 3;

    public bool GameOver() => IsWon || (!IsWon && Tries >= 3);

    public void End() => Secret = 0;
}