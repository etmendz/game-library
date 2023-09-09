/*
* GameLibrary (c) Mendz, etmendz. All rights reserved. 
* SPDX-License-Identifier: MIT 
*/
namespace GameLibrary;

/// <summary>
/// Defines a game randomizer.
/// </summary>
public static class GameRandomizer
{
    /// <summary>
    /// Gets a random number.
    /// </summary>
    /// <returns>A random number.</returns>
    public static int Next() => Random.Shared.Next();

    /// <summary>
    /// Gets a random number less than the given limit.
    /// </summary>
    /// <param name="limit">The limit.</param>
    /// <returns>A random number less than the given limit.</returns>
    public static int Next(int limit) => Random.Shared.Next(limit);

    /// <summary>
    /// Gets a random number within a given range.
    /// </summary>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The exclusive maximum value.</param>
    /// <returns>A random number within a given range.</returns>
    public static int Next(int min, int max) => Random.Shared.Next(min, max);
}