[Wiki](https://github.com/etmendz/game-library/wiki)
# GameLibrary
The GameLibrary provides a simple framework for creating games that can follow a basic flow:

	Play() -> (if|while) Ready() -> Set() -> Go()

... which implements a basic construct:

	if (Start())
	{
		do
		{
			if (Action())
			{
				if (!Continue()) break;
			}
		} while (!GameOver());
	}
	End();

The basic flow and basic construct combined defines the game's runtime. The general philosophy playfully plays with the idea that playing games can begin with "Ready? Set? Go!". The game can then Start to monitor game Actions, Continue state and GameOver state, to finally End.

The design allows the game developer to focus on implementing the game play and the game UI.

Thus, for example, in a game console app's program main entry point, the developer can simply code:

	new GameConsole<GameUI, GamePlay, ConsoleKey, bool>(
		"Game name", 
		"All rights reserved.", 
		"Game description.", 
		"Press [Esc] anytime to exit.", 
		GamePlayReadyMode.WhileReady
	).Play();

## IGamePlay<in TActionIn, out TActionOut>
Defines a game play designed to align with the basic construct's Start(), Action(), Continue(), GameOver() and End() methods.

The game play Action() is defined by the *in* and *out* generic types passed to IGamePlay.

## IGameUI<TGamePlay, in TActionIn, out TActionOut>
Defines a game UI designed to align with the basic construct's Start(), Action(), Continue(), GameOver() and End() methods.

IGameUI accepts a type that implements IGamePlay, and provides additional methods to Render() and Refresh().

An implementation must define a parameterless constructor that can initialize an IGamePlay instance.

## GameRandomizer
Defines a game randomizer to generate random numbers.

Methods are provided to get the next random number, the next random number below a limit, and the next random number within a minimum/maximum range.

## GamePlayReadyMode
Specifies the game play ready mode or how the game flows.

### IfReady
Start only one game UI instance per launch.

Use for games where only one game can be started per launch. For these types of games, there is usually no real game over state.

An example is a dice guessing game, where the player guesses and rolls the dice:

1. Guessing the number right or wrong simply displays the result.
2. Then the game prompts the player to continue by guessing and rolling again:
    - Opting to continue simply continues the same game;
    - Otherwise, the game ends and closes.

### WhileReady
Allow starting more than one game UI instance per launch.

Use for games where the player can be prompted to start a new game. For these types of games, there is usually a real game over state.

An example is 2048, where the player performs moves to reach a goal:

1. If the player wins, then the game prompts the player to continue to the next level:
    - Opting to continue simply continues the same game;
	- Otherwise, the game prompts the player to start a new game.
2. If the player runs out of moves, then the game is over:
    - Then the game prompts the player to start a new game.

### GameConsole<TGameUI, TGamePlay, TActionIn, TActionOut>
GameConsole accepts a type that implements IGameUI, which requires a type that implements IGamePlay.

Play() implements the basic flow, using the GamePlayReadyMode to switch:

- IfReady
```
if (Ready())
{
	Set();
	Go();
}
```
- WhileReady
```
while (Ready())
{
	Set();
	Go();
}
```
Go() implements the basic construct as follows:

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

### GameConsoleUX
Provides the basic capabilities for game interactions via keyboard.

Methods are provided to enable Y/N input, arrow key input, specific key input and valid keys input.

---

(c) Mendz, etmendz. All rights reserved.