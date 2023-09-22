/*
* GameConsole3Seconds (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
using GameLibrary;

namespace GameConsole3Seconds;

internal class Game : GameConsole<GameUI, GamePlay, ConsoleKey, bool>
{
    public Game() 
        : base("GameConsole3Seconds", 
            "Mendz, etmendz. All rights reserved.", 
            "Stop the clock at 3 seconds flat.",
            "Press [Esc] anytime to exit the app.",
            GamePlayReadyMode.IfReady)
    {
    }
}