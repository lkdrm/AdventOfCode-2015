using AdventOfCode2015.Tools;
using System.Text;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 10 of the coding challenge.
/// </summary>
public static class Day10
{
    private const int DefaultIterationsPart1 = 40;
    private const int DefaultIterationsPart2 = 50;

    /// <summary>
    /// Processes the input string using the look-and-say sequence for a predefined number of iterations and returns the
    /// length of the resulting string as a string.
    /// </summary>
    /// <param name="input">The initial sequence to process. Cannot be null.</param>
    /// <returns>A string representation of the length of the final sequence after processing.</returns>
    public static string SolvePart1(string input)
    {
        TimerExtension.Start();
        string result = GenerateLookAndSay(input, DefaultIterationsPart1);
        return result.Length.ToString();
    }

    /// <summary>
    /// Processes the input string using the look-and-say sequence for the number of iterations specified for part 2 and
    /// returns the length of the resulting string as a string.
    /// </summary>
    /// <param name="input">The initial sequence to process using the look-and-say algorithm. Cannot be null.</param>
    /// <returns>A string representation of the length of the final sequence after applying the look-and-say algorithm for the
    /// required number of iterations.</returns>
    public static string SolvePart2(string input)
    {
        TimerExtension.Start();
        string result = GenerateLookAndSay(input, DefaultIterationsPart2);
        return result.Length.ToString();
    }

    /// <summary>
    /// Generates the result of applying the look-and-say sequence to the specified input for a given number of
    /// iterations.
    /// </summary>
    /// <param name="iterations">The number of times to apply the look-and-say transformation. Must be zero or greater.</param>
    /// <returns>A string representing the result of applying the look-and-say sequence to the input for the specified number of
    /// iterations.</returns>
    private static string GenerateLookAndSay(string input, int iterations)
    {
        string current = input.Trim();

        for (int iteration = 0; iteration < iterations; iteration++)
        {
            current = GenerateNextLookAndSayTerm(current);
        }

        return current;
    }

    /// <summary>
    /// Generates the next term in the look-and-say sequence based on the specified input string.
    /// </summary>
    /// <param name="input">The input string representing the current term in the look-and-say sequence. Must not be null or empty and
    /// should consist of digit characters only.</param>
    /// <returns>A string representing the next term in the look-and-say sequence derived from the input.</returns>
    private static string GenerateNextLookAndSayTerm(string input)
    {
        StringBuilder result = new StringBuilder();
        int i = 0;

        while (i < input.Length)
        {
            char currentChar = input[i];
            int count = 1;

            while (i + count < input.Length && input[i + count] == currentChar)
            {
                count++;
            }

            result.Append(count);
            result.Append(currentChar);

            i += count;
        }

        return result.ToString();
    }
}
