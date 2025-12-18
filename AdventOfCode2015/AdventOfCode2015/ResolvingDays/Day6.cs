using AdventOfCode2015.Tools;
using System.Text.RegularExpressions;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 6 of the coding challenge.
/// </summary>
public class Day6
{
    /// <summary>
    /// Specifies the available commands for controlling a light's state.
    /// </summary>
    private enum LightsCommand
    {
        /// <summary>
        /// Lights are on.
        /// </summary>
        TurnOn,

        /// <summary>
        /// Lights are off.
        /// </summary>
        TurnOff,

        /// <summary>
        /// Switch them.
        /// </summary>
        Toggle
    };

    /// <summary>
    /// Generate a grid with 1000x1000 with default state(lights => OFF)
    /// </summary>
    private static readonly bool[,] _gridLightsPart = new bool[1000, 1000];

    /// <summary>
    /// Represents the brightness values for each cell in the 1000 by 1000 grid with default value(brightness => 0).
    /// </summary>
    private static readonly int[,] _gridBrightness = new int[1000, 1000];

    /// <summary>
    /// Represents the regular expression used to parse light instruction commands in the format "turn on", "turn off",
    /// or "toggle" followed by coordinate ranges.
    /// </summary>
    private static readonly Regex _lightInstructionRegex = new(@"(turn on|turn off|toggle) (\d+),(\d+) through (\d+),(\d+)");

    /// <summary>
    /// Processes a series of lighting instructions and returns the total number of lights that are turned on after all
    /// instructions have been applied.
    /// </summary>
    /// <param name="input">An array of strings, each representing a lighting instruction to apply to the grid. Each instruction specifies
    /// an operation and a rectangular region.</param>
    /// <returns>A string representation of the total number of lights that are on after processing all instructions.</returns>
    public static string SolvePart1(string[] input)
    {
        TimerExtension.Start();
        foreach (var line in input)
        {
            // Parse the instruction line to get the command and coordinates
            (LightsCommand lightsCommand, int x1, int y1, int x2, int y2) = ParseLightInstruction(line);

            for (int y = y1; y <= y2; y++)
            {
                for (int x = x1; x <= x2; x++)
                {
                    switch (lightsCommand)
                    {
                        case LightsCommand.TurnOn:
                            _gridLightsPart[y, x] = true;
                            break;
                        case LightsCommand.TurnOff:
                            _gridLightsPart[y, x] = false;
                            break;
                        case LightsCommand.Toggle:
                            _gridLightsPart[y, x] = !_gridLightsPart[y, x];
                            break;
                    }
                }
            }
        }
        return LitCount().ToString();
    }

    /// <summary>
    /// Processes a series of lighting instructions and calculates the total brightness of all lights after applying the
    /// commands according to Part 2 rules.
    /// </summary>
    /// <param name="input">An array of strings, each representing a lighting instruction in the specified format.</param>
    /// <returns>A string representation of the total brightness of all lights after processing all instructions.</returns>
    public static string SolvePart2(string[] input)
    {
        TimerExtension.Start();
        foreach (var line in input)
        {
            // Parse the instruction line to get the command and coordinates
            (LightsCommand lightsCommand, int x1, int y1, int x2, int y2) = ParseLightInstruction(line);

            for (int y = y1; y <= y2; y++)
            {
                for (int x = x1; x <= x2; x++)
                {
                    switch (lightsCommand)
                    {
                        case LightsCommand.TurnOn:
                            _gridBrightness[y, x] += 1;
                            break;
                        case LightsCommand.TurnOff:
                            _gridBrightness[y, x] = Math.Max(0, _gridBrightness[y, x] - 1);
                            break;
                        case LightsCommand.Toggle:
                            _gridBrightness[y, x] += 2;
                            break;
                    }
                }
            }
        }
        return BrightnessCount().ToString();
    }

    /// <summary>
    /// Counts the total number of lit lights in the grid.
    /// </summary>
    /// <returns>The number of lights that are currently lit in the grid.</returns>
    private static int LitCount()
    {
        int lits = 0;
        int gridLenght = 1000;
        for (int y = 0; y < gridLenght; y++)
        {
            for (int x = 0; x < gridLenght; x++)
            {
                if (_gridLightsPart[y, x])
                {
                    lits++;
                }
            }
        }
        return lits;
    }

    /// <summary>
    /// Calculates the total brightness value across the entire grid.
    /// </summary>
    /// <returns>The sum of all brightness values in the grid.</returns>
    private static int BrightnessCount()
    {
        int brightness = 0;
        int gridLenght = 1000;
        for (int y = 0; y < gridLenght; y++)
        {
            for (int x = 0; x < gridLenght; x++)
            {
                brightness += _gridBrightness[y, x];
            }
        }
        return brightness;
    }

    /// <summary>
    /// Parses a light instruction string and extracts the command and coordinate values.
    /// </summary>
    /// <param name="line">The instruction line to parse. Must match the expected command and coordinate format.</param>
    /// <returns>A tuple containing the parsed lights command and the four integer coordinates (x1, y1, x2, y2) extracted from
    /// the instruction.</returns>
    private static (LightsCommand lightsCommand, int x1, int y1, int x2, int y2) ParseLightInstruction(string line)
    {
        var match = _lightInstructionRegex.Match(line);

        LightsCommand command = match.Groups[1].Value switch
        {
            "turn on" => LightsCommand.TurnOn,
            "turn off" => LightsCommand.TurnOff,
            "toggle" => LightsCommand.Toggle,
            _ => throw new InvalidOperationException("Invalid command")
        };

        int x1 = int.Parse(match.Groups[2].Value);
        int y1 = int.Parse(match.Groups[3].Value);
        int x2 = int.Parse(match.Groups[4].Value);
        int y2 = int.Parse(match.Groups[5].Value);

        return (command, x1, y1, x2, y2);
    }
}
