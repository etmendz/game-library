/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
using System.Reflection;

namespace GameLibrary;

/// <summary>
/// Defines a game console app.
/// </summary>
/// <typeparam name="TGameUI">The game UI.</typeparam>
/// <typeparam name="TGamePlay">The game play.</typeparam>
/// <typeparam name="TActionIn">The action input type.</typeparam>
/// <typeparam name="TActionOut">The action output or result type.</typeparam>
/// <param name="name">The game's name.</param>
/// <param name="copyright">The game copyright information.</param>
/// <param name="description">The game description.</param>
/// <param name="splashText">The game splash text.</param>
/// <param name="gamePlayReadyMode">The game play ready mode.</param>
public class GameConsole<TGameUI, TGamePlay, TActionIn, TActionOut>(string name, string copyright, string description, string? splashText = null, GamePlayReadyMode gamePlayReadyMode = GamePlayReadyMode.IfReady)
    where TGameUI : IGameUI<TGamePlay, TActionIn, TActionOut>, new()
    where TGamePlay : IGamePlay<TActionIn, TActionOut>, new()
{
    /// <summary>
    /// Gets the game's name.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the game's copyright information.
    /// </summary>
    public string Copyright { get; } = copyright;

    /// <summary>
    /// Gets the game description.
    /// </summary>
    public string Description { get; } = description;

    /// <summary>
    /// Gets the game splash text.
    /// </summary>
    public string? SplashText { get; } = splashText;

    /// <summary>
    /// Gets the game play ready mode.
    /// </summary>
    public GamePlayReadyMode GamePlayReadyMode { get; } = gamePlayReadyMode;

    /// <summary>
    /// Gets or sets an indicator if the game is ready.
    /// </summary>
    public bool IsReady { get; set; }

    /// <summary>
    /// Plays the game.
    /// </summary>
    /// <remarks>The default implementation basically calls (if|while) Ready(), Set(), Go()!</remarks>
    public virtual void Play()
    {
        switch (GamePlayReadyMode)
        {
            case GamePlayReadyMode.IfReady:
                // For games where only one game can be played per launch.
                // For these types of games, there is usually no real game over state.
                // Ex.: dice guessing game, where the player guesses and rolls the dice...
                //      guessing the dice roll right or wrong simply displays the results;
                //      then the game prompts the player to guess and roll again;
                //      opting to continue continues the same game;
                //      otherwise, the game ends and closes.
                if (Ready())
                {
                    Set();
                    Go();
                }
                break;
            case GamePlayReadyMode.WhileReady:
                // For games where the player can be prompted to start a new game.
                // For these types of games, there is usually a real game over state.
                // Ex. 2048, where the player performs moves to reach a goal...
                //     if the player wins, then the game prompts the player to continue to the next level;
                //     opting to continue continues the same game;
                //     otherwise, the game prompts the player to start a new game.
                //     if the player runs out of moves, the game is over;
                //     then the game prompts the player to start a new game.
                while (Ready())
                {
                    Set();
                    Go();
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Shows the splash.
    /// </summary>
    public virtual void Splash()
    {
        Console.WriteLine($"{Name} {Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion} (c) {DateTime.Now.Year} {Copyright}");
        Console.WriteLine(Description);
        if (!string.IsNullOrEmpty(SplashText))
        {
            Console.WriteLine();
            Console.WriteLine(SplashText);
        }
    }

    /// <summary>
    /// Ready.
    /// </summary>
    /// <returns>True when ready.</returns>
    /// <remarks>
    /// The default implementation is as follows:
    /// <code>
    /// if (!IsReady)
    /// {
    ///     Splash();
    ///     IsReady = true;
    /// }
    /// return IsReady;
    /// </code>
    /// </remarks>
    public virtual bool Ready()
    {
        if (!IsReady)
        {
            Splash();
            IsReady = true;
        }
        return IsReady;
    }

    /// <summary>
    /// Set.
    /// </summary>
    /// <remarks>The default implementation prompts the player to press the [Enter] key to start playing.</remarks>
    public virtual void Set()
    {
        Console.WriteLine();
        Console.WriteLine("Press [Enter] to start playing...");
        new GameConsoleUX().GetKey(ConsoleKey.Enter);
    }

    /// <summary>
    /// Go!
    /// </summary>
    /// <remarks>
    /// The default implementation is as follows:
    /// <code>
    /// TGameUI gameUI = new();
    /// if (gameUI.Start())
    /// {
    ///     do
    ///     {
    ///         if (gameUI.Action())
    ///         {
    ///             if (!gameUI.Continue()) break;
    ///         }
    ///     } while (!gameUI.GameOver()) ;
    /// }
    /// gameUI.End();
    /// </code>
    /// </remarks>
    public virtual void Go()
    {
        TGameUI gameUI = new();
        if (gameUI.Start())
        {
            do
            {
                if (gameUI.Action())
                {
                    if (!gameUI.Continue()) break;
                }
            } while (!gameUI.GameOver());
        }
        gameUI.End();
    }
}