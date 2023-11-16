/*
* GameLibrary (c) Mendz, etmendz. All rights reserved.
* SPDX-License-Identifier: MIT 
*/
using System.Reflection;

namespace GameLibrary;

/// <summary>
/// Implements IGameFlow to define a console app game that can consume the provided IGameUI and IGamePlay implementations.
/// </summary>
/// <typeparam name="TGameUI">The game UI.</typeparam>
/// <typeparam name="TGamePlay">The game play.</typeparam>
/// <param name="name">The game's name.</param>
/// <param name="copyright">The game copyright information.</param>
/// <param name="description">The game description.</param>
/// <param name="splashText">The game splash text. Default is null.</param>
/// <param name="gamePlayReadyMode">The game play ready mode. Default is <see cref="GamePlayReadyMode.IfReady"/>.</param>
/// <remarks>
/// The program main entry point can use the following boilerplate template to call Play():
/// <code>
/// new GameConsole&lt;GameUI, GamePlay&gt;(
///		"Game name", 
///		"All rights reserved.", 
///		"Game description.", 
///		"Press [Esc] anytime to exit.", 
///		GamePlayReadyMode.WhileReady
///	).Play();
/// </code>
/// </remarks>
public class GameConsole<TGameUI, TGamePlay>(
        string name, 
        string copyright, 
        string description, 
        string? splashText = null, 
        GamePlayReadyMode gamePlayReadyMode = GamePlayReadyMode.IfReady
    ) : IGameFlow
    where TGameUI : IGameUI<TGamePlay>, new()
    where TGamePlay : IGamePlay
{
    /// <summary>
    /// Gets the game's name.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets the game's copyright information.
    /// </summary>
    public string Copyright { get; set; } = copyright;

    /// <summary>
    /// Gets the game description.
    /// </summary>
    public string Description { get; set; } = description;

    /// <summary>
    /// Gets the game splash text.
    /// </summary>
    public string? SplashText { get; set; } = splashText;

    /// <summary>
    /// Gets the game play ready mode.
    /// </summary>
    public GamePlayReadyMode GamePlayReadyMode { get; set; } = gamePlayReadyMode;

    /// <summary>
    /// Gets or sets an indicator if the game is ready.
    /// </summary>
    public bool IsReady { get; set; }

    /// <summary>
    /// Gets or sets the "Ready?" text.
    /// </summary>
    public string? ReadyText { get; set; }

    /// <summary>
    /// Gets or sets the "Set?" text.
    /// </summary>
    public string? SetText { get; set; } = "Press [Enter] to start playing...";

    /// <summary>
    /// Gets or sets the "Go!" text.
    /// </summary>
    public string? GoText { get; set; }

    /// <summary>
    /// Plays the game. Implements the basic flow.
    /// </summary>
    /// <remarks>The default implementation uses GamePlayReadyMode to switch (if|while) Ready(), Set(), Go()!</remarks>
    public virtual void Play()
    {
        Console.CursorVisible = false;
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
        Console.CursorVisible = true;
    }

    /// <summary>
    /// Shows the splash.
    /// </summary>
    public virtual void Splash()
    {
        Console.WriteLine($"{Name} {Assembly.GetEntryAssembly()?.GetName().Version?.ToString()} (c) {DateTime.Now.Year} {Copyright}");
        Console.WriteLine(Description);
        GameConsole<TGameUI, TGamePlay>.ShowText(SplashText);
    }

    /// <summary>
    /// Ready?
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
            GameConsole<TGameUI, TGamePlay>.ShowText(ReadyText);
            IsReady = true;
        }
        return IsReady;
    }

    /// <summary>
    /// Set?
    /// </summary>
    /// <remarks>The default implementation prompts the player to press the [Enter] key to start playing.</remarks>
    public virtual void Set()
    {
        GameConsole<TGameUI, TGamePlay>.ShowText(SetText);
        new GameConsoleUX().GetKey(ConsoleKey.Enter);
    }

    /// <summary>
    /// Go! Implements the basic construct.
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
        GameConsole<TGameUI, TGamePlay>.ShowText(GoText);
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

    /// <summary>
    /// If the text is not empty, writes an empty line followed by the text value.
    /// </summary>
    /// <param name="text">The text to show.</param>
    protected static void ShowText(string? text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            Console.WriteLine();
            Console.WriteLine(text);
        }
    }
}