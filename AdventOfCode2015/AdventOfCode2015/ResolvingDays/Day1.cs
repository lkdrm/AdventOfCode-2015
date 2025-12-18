using AdventOfCode2015.Tools;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// This class is used to solve the first day of the Advent of Code 2015 challenge.
/// </summary>
public static class Day1
{
    /// <summary>
    /// Calculates the final floor number based on a sequence of '(' and ')' characters.
    /// </summary>
    /// <param name="input">A string containing a sequence of characters, where '(' increases and ')' decreases the floor count. Other
    /// characters are ignored.</param>
    /// <returns>A string representation of the resulting floor number after processing the input sequence.</returns>
    public static string SolvePart1(string input)
    {
        TimerExtension.Start();
        int floor = 0;
        foreach (char c in input)
        {
            if (c == '(')
            {
                floor++;
            }
            else if (c == ')')
            {
                floor--;
            }
        }
        return floor.ToString();
    }

    /// <summary>
    /// Calculates the final floor number based on a sequence of '(' and ')' characters.
    /// </summary>
    /// <param name="input">A string containing a sequence of characters, where '(' increases and ')' decreases the floor count. Other
    /// characters are ignored.</param>
    /// <returns>A string representation of the resulting floor number after processing the input sequence.</returns>
    public static string SolvePart2(string input)
    {
        TimerExtension.Start();
        int floor = 0;
        int position = 0;
        foreach (char c in input)
        {
            position++;
            if (c == '(')
            {
                floor++;
            }
            else if (c == ')')
            {
                floor--;
            }

            if (floor == -1)
            {
                return position.ToString();
            }
        }
        return floor.ToString();
    }
}
