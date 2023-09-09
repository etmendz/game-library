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

The basic flow and basic construct combined defines the game's runtime. The general philosophy playfully plays with the idea that playing games can begin with "Ready? Set? Go!". The game can then Start to monitor game Actions, Continue state, GameOver state, and finally End.

The design allows the game developer to focus on implementing the game play and the game UI.

Thus, for example, in a game console app's program main entry point, the developer can simply code:

	new Game<GameUI, GamePlay>(name, copyright, description, splashText, gamePlayReadyMode).Play();

## IGamePlay
Defines a game play designed to align with the basic construct's Start(), Action(), Continue(), GameOver() and End() methods.

## IGameUI
Defines a game UI designed to align with the basic construct's Start(), Action(), Continue(), GameOver() and End() methods.

IGameUI accepts a type that implements IGamePlay, and provides additional methods to Initialize(), Render() and Refresh().

An implementation must define a parameterless constructor that calls Initialize().

## GameRandomizer
Defines a game randomizer to generate random numbers.

## Console App Games
The following enables console app game development using the GameLibrary.

### Game
Plays the game.

Game accepts a type that implements IGameUI, which requires a type that implements IGamePlay.

Provides the Play() method which uses the given GamePlayReadyMode to switch the game flow.

#### IfReady
Start only one game UI instance per launch.

	if (Ready())
	{
		Set();
		Go();
	}

#### WhileReady
Allow starting more than one game UI instance per launch.

	while (Ready())
	{
		Set();
		Go();
	}

### GameUX
Provides the basic capabilities for game interactions via keyboard.

Methods are provided to enable Y/N input, arrow key input, specific key input and valid keys input.

---

(c) Mendz, etmendz. All rights reserved.