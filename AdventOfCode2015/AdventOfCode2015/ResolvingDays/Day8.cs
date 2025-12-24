using AdventOfCode2015.Tools;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 8 of the coding challenge.
/// </summary>
public static class Day8
{
    /// <summary>
    /// Calculates the total number of characters of code for string literals minus the number of characters in memory
    /// for those values.
    /// </summary>
    /// <param name="input">An array of strings representing the string literals to analyze. Each element should be a valid string literal
    /// as it appears in code.</param>
    /// <returns>A string representation of the difference between the total number of characters of code and the total number of
    /// characters in memory for the provided string literals.</returns>
    public static string SolvePart1(string[] input)
    {
        TimerExtension.Start();
        int totalMemoryLength = 0;
        int totalCodeLength = 0;
        foreach (var line in input)
        {
            totalCodeLength += line.Length;
            totalMemoryLength += line.CalculateMemoryLength();
        }
        return (totalCodeLength - totalMemoryLength).ToString();
    }

    /// <summary>
    /// Calculates the total number of characters required to encode the input strings, minus the total number of
    /// characters in the original code representation.
    /// </summary>
    /// <param name="input">An array of strings representing the input lines to be analyzed. Each string should be a valid code
    /// representation to be encoded.</param>
    /// <returns>A string representation of the difference between the total encoded length and the total code length of the
    /// input strings.</returns>
    public static string SolvePart2(string[] input)
    {
        TimerExtension.Start();
        int totalEncodedLength = 0;
        int totalCodeLength = 0;
        foreach (var line in input)
        {
            totalCodeLength += line.Length;
            totalEncodedLength += line.CalculateEncodedLength();
        }
        return (totalEncodedLength - totalCodeLength).ToString();
    }

    /// <summary>
    /// Calculates the length of the string as it would be represented in memory, interpreting escape sequences
    /// according to C-style string rules.
    /// </summary>
    /// <param name="input">The string to evaluate. The string should be enclosed in double quotes and may contain escape sequences such as
    /// \x, \\, or \".</param>
    /// <returns>The number of characters that the string would occupy in memory after processing escape sequences.</returns>
    private static int CalculateMemoryLength(this string input)
    {
        int memoryLength = 0;
        for (int i = 1; i < input.Length - 1; i++)
        {
            if (input[i] == '\\')
            {
                if (input[i + 1] == 'x')
                {
                    memoryLength++;
                    i += 3;
                }
                else
                {
                    memoryLength++;
                    i += 1;
                }
            }
            else
            {
                memoryLength++;
            }
        }
        return memoryLength;
    }

    /// <summary>
    /// Calculates the length of the string when encoded with surrounding double quotes and escaped backslashes and
    /// double quotes, as per standard string literal encoding.
    /// </summary>
    /// <param name="input">The string to evaluate. Cannot be null.</param>
    /// <returns>The number of characters required to represent the encoded string, including surrounding quotes and escape
    /// sequences.</returns>
    private static int CalculateEncodedLength(this string input)
    {
        int encodedLength = 2;
        foreach (char c in input)
        {
            if (c == '\\' || c == '"')
            {
                encodedLength += 2;
            }
            else
            {
                encodedLength += 1;
            }
        }
        return encodedLength;
    }
}
