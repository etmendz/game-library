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

The basic flow and basic construct combined establishes a structure for the game's runtime. The general philosophy playfully plays with the idea that playing games can begin with "Ready? Set? Go!". The game can then Start to monitor game Actions, Continue state and GameOver state, to finally End.

The design allows the game developer to focus on implementing the game play and the game UI, which can then be consumed by a game flow implementation.

Thus, for example, in a game console app's program main entry point, the developer can simply code:

	new GameConsole<GameUI, GamePlay>(
		"Game name", 
		"All rights reserved.", 
		"Game description.", 
		"Press [Esc] anytime to exit.", 
		GamePlayReadyMode.WhileReady
	).Play();

## GameActionMode
Specifies the game action mode if for GamePlay, GamePause or GameStop.

## GameActionType
Specifies the game action type if for Control, Response, Navigation or Other.

## GameActionInfo
Represents a game action, which has an input, an output and/or a result for the given game action mode and game action type.

The default game action mode is GamePlay.

The default game action type is Control.

Typically, an IGameUI.Action() creates an instance of a GameActionInfo to pass to IGamePlay.Action() for execution, evaluation or processing.

## IGamePlay and IGamePlayAsync
Defines a game play designed to align with the basic construct's Start(), Action(), Continue(), GameOver() and End() methods.

The game play is essentially the actual game.

When implemented sans any UI-specific aspects, the game play can be re-used in different UI/UX platforms.

IGamePlayAsync is provided for async implementations.

## IGameUI\<TGamePlay\> and IGameUIAsync\<TGamePlay\>
Defines a game UI designed to align with the basic construct's Start(), Action(), Continue(), GameOver() and End() methods.

IGameUI accepts a type that implements IGamePlay, and provides additional methods to Render() and Refresh().

An implementation must define a parameterless constructor that can initialize an IGamePlay instance.

Implementations can be UI/UX platform specific.

IGameUIAsync is provided for async implementations.

## IGameFlow and IGameFlowAsync
Defines a game flow designed to align with the basic flow's Play(), Ready(), Set() and Go() methods.

Play() implements the basic flow.

Go() implements the basic construct.

Implementations can be UI/UX platform specific.

IGameFlowAsync is provided for async implementations.

## GamePlayReadyMode
Specifies the game play ready mode or how the game flows to consume the Ready(), Set() and Go() methods.

### IfReady
Start only one game UI instance per launch.

Use for games where only one game can be started per launch. For these types of games, there is usually no real game over state.

An example is a dice guessing game, where the player guesses and rolls the dice:

1. Guessing the number right or wrong simply displays the result.
2. Then the game prompts the player to continue by guessing and rolling again:
    - Opting to continue simply continues the same game;
    - Otherwise, the game ends and closes.

See: [GameConsoleDice](https://github.com/etmendz/game-console-dice)

### WhileReady
Allow starting more than one game UI instance per launch.

Use for games where the player can be prompted to start a new game. For these types of games, there is usually a real game over state.

An example is 2048, where the player performs moves to reach a goal:

1. If the player wins, then the game prompts the player to continue to the next level:
    - Opting to continue simply continues the same game;
	- Otherwise, the game prompts the player to start a new game.
2. If the player runs out of moves, then the game is over:
    - Then the game prompts the player to start a new game.

See: [GameConsole2048](https://github.com/etmendz/game-console-2048)

## GameConsole\<TGameUI, TGamePlay\>
GameConsole implements IGameFlow for game console apps.

GameConsole accepts a type that implements IGameUI, which requires a type that implements IGamePlay.

Play() implements the basic flow using the GamePlayReadyMode to switch:

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

## GameConsoleUX
Provides the basic capabilities for game console app interactions via keyboard.

Methods are provided to enable Y/N input, arrow key input, specific key input and valid keys input.

# Examples
Two game projects are included in the GameLibrary solution: GameConsole3Guesses and GameConsole3Seconds.

They can be found under the repo's [examples/](https://github.com/etmendz/game-library/tree/main/examples) subfolder.

## GameConsole3Guesses
[GameConsole3Guesses](https://github.com/etmendz/game-library/tree/main/examples/GameConsole3Guesses) is a simple number guessing game.

The player wins when the secret number is guessed correctly. The player has 3 chances to guess the secret number, else it's game over.

The game uses GamePlayReadyMode.WhileReady. This means that every time the player opts to start playing (again), the program creates a new game UI instance. After the game ends, the game UI is also released from memory.

After each game, the flow goes back to the Ready(), Set(), Go() loop.

When prompted for control input, pressing the [Esc] key exits the game.

The code pattern can be described as follows:

- Start() renders the UI
- Action() refreshes the UI, prompts for action input and processes the result
- Continue() evaluates the game result and prompts for control input (to try again?)
- GameOver() checks for the game over state
- End() shows the game result and ends the game.

## GameConsole3Seconds
[GameConsole3Seconds](https://github.com/etmendz/game-library/tree/main/examples/GameConsole3Seconds) challenges the player to stop the clock at 3 seconds flat.

When started, the player simply presses the [Spacebar] to stop the clock. If it stops at 3 seconds on the dot, the player wins.

The game uses GamePlayReadyMode.IfReady. This means that there is only one instance of the game UI.

When the player opts to try again, the same game UI instance restarts the clock.

There is no real game over state. Game over is essentially when the player quits.

The code pattern can be described as follows:

- Start() renders the UI
- Action() refreshes the UI, prompts for action input and processes the result
- Continue() evaluates the game result, shows the game result and prompts for control input (to try again?); if the player opts to continue, the clock is restarted and the UI is re-rendered.
- GameOver() checks for the game over state (if the player opted to quit)
- End() ends the game.

# Boilerplate
Boilerplate template codes are available in the repo's [boilerplate/](https://github.com/etmendz/game-library/tree/main/boilerplate) subfolder.

# Release Notes

## v1.1.0
This is the first release version, published after Microsoft released .NET 8.

- Breaking: GameRandomizer deprecated. Directly use [System.Random.Shared](https://learn.microsoft.com/en-us/dotnet/api/system.random.shared) instead.
- Breaking: IGamePlay.Action() is no longer variant, instead defined as: bool Action(GameActionInfo).
- Breaking: IGamePlay no longer needs variant types for Action().
- Breaking: IGameUI no longer needs variant types for IGamePlay.
- Breaking: GameConsole no longer needs variant types for TGameUI and TGamePlay.
- New: Introduced GameActionMode and GameActionType enumerations.
- New: Introduced GameActionInfo, a class representing a game action.
- New: Introduced IGamePlayAsync, IGameUIAsync and IGameFlowAsync.
- New: GameConsoleUX adds GetParsableEntry(), which can block/loop until a valid date or numeric value is entered.
- Updated: Game.cs.txt boilerplate template updated in line with GameConsole's breaking changes.
- Updated: Example projects codes updated in line with the breaking changes listed above.
- Updated: Documentation updated.

## v1.0.x
These are the preview versions, implemented using .NET 8 preview versions.

---

(c) Mendz, etmendz. All rights reserved.