/*
* GameConsole3Guesses (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
using GameLibrary;

namespace GameConsole3Guesses;

internal class Game : GameConsole<GameUI, GamePlay, int, bool>
{
    public Game() 
        : base("GameConsole3Guesses", 
            "Mendz, etmendz. All rights reserved.", 
            "Guess the secret number.",
            "You'll have 3 chances to guess it right.",
            GamePlayReadyMode.WhileReady)
    {
        SetText = "Press [Enter] to play, or [Esc] to exit.";
    }
}