namespace GameLibrary;

/// <summary>
/// Represents a game action.
/// </summary>
public class GameActionInfo
{
    /// <summary>
    /// Gets or sets the action mode.
    /// </summary>
    public GameActionMode ActionMode { get; set; } = GameActionMode.GamePlay;

    /// <summary>
    /// Gets or sets the action type.
    /// </summary>
    public GameActionType ActionType { get; set; } = GameActionType.Control; 

    /// <summary>
    /// Gets or sets the game action input.
    /// </summary>
    public object? Input { get; set; }

    /// <summary>
    /// Gets or sets the game action output.
    /// </summary>
    public object? Output { get; set; }

    /// <summary>
    /// Gets or sets the game action result.
    /// </summary>
    public object? Result { get; set; }

    /// <summary>
    /// Gets the input as the type specified. Basically, a casting mnemonic.
    /// </summary>
    /// <typeparam name="T">The type to cast to.</typeparam>
    /// <returns>The input cast to type T.</returns>
    public virtual T? GetInputAs<T>() => GetValueAs<T>(Input);

    /// <summary>
    /// Gets the output as the type specified. Basically, a casting mnemonic.
    /// </summary>
    /// <typeparam name="T">The type to cast to.</typeparam>
    /// <returns>The output cast to type T.</returns>
    public virtual T? GetOutputAs<T>() => GetValueAs<T>(Output);

    /// <summary>
    /// Gets the result as the type specified. Basically, a casting mnemonic.
    /// </summary>
    /// <typeparam name="T">The type to cast to.</typeparam>
    /// <returns>The result cast to type T.</returns>
    public virtual T? GetResultAs<T>() => GetValueAs<T>(Result);

    /// <summary>
    /// Gets the value as the type specified. Basically, a casting mnemonic.
    /// </summary>
    /// <typeparam name="T">The type to cast to.</typeparam>
    /// <param name="value">The value to cast.</param>
    /// <returns>The value cast to type T.</returns>
    protected static T? GetValueAs<T>(object? value) => (T?)value;
}