using AdventOfCode2015.Tools;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 5 of the coding challenge.
/// </summary>
public static class Day5
{
    /// <summary>
    /// Contains substrings that are not allowed in the input for validation purposes.
    /// </summary>
    private static readonly List<string> _notAllowedSubStrings = ["ab", "cd", "pq", "xy"];

    /// <summary>
    /// Represents the list of vowel substrings used for internal processing.
    /// </summary>
    private static readonly List<string> _vowelsSubStrings = ["a", "e", "i", "o", "u"];

    /// <summary>
    /// Analyzes the input lines and counts how many meet the specified criteria for being considered 'nice' according
    /// to Part 1 of the puzzle.
    /// </summary>
    /// <param name="input">An array of strings, each representing a line to be evaluated against the 'nice' string criteria.</param>
    /// <returns>A string representation of the number of input lines that satisfy all the required conditions.</returns>
    public static string SolvePart1(string[] input)
    {
        TimerExtension.Start();
        int result = 0;
        foreach (var line in input)
        {
            // Check for not allowed substrings
            if (_notAllowedSubStrings.Any(sub => line.Contains(sub, StringComparison.Ordinal)))
            {
                continue;
            }
            
            int vowelCount = _vowelsSubStrings.Sum(vowel => line.Count(c => c.ToString() == vowel));
            // Check for at least 3 vowels
            if (vowelCount < 3)
            {
                continue;
            }

            for (int i = 0; i < line.Length - 1; i++)
            {
                // Check for at least one letter that appears twice in a row
                if (line[i] == line[i + 1])
                {
                    result++;
                    break;
                }
            }
        }
        return result.ToString();
    }

    /// <summary>
    /// Analyzes the input strings and counts how many meet both of two specific pattern criteria.
    /// </summary>
    /// <param name="input">An array of strings to evaluate. Each string is checked for the required patterns.</param>
    /// <returns>A string representation of the number of input strings that satisfy both pattern conditions.</returns>
    public static string SolvePart2(string[] input)
    {
        TimerExtension.Start();
        int result = 0;
        foreach (var line in input)
        {
            bool hasTwicePairs = false;
            bool hasLetterBetwen = false;
            
            string pair = string.Empty;

            // Check for a pair of any two letters that appears at least twice in the string without overlapping
            for (int i = 0; i < line.Length - 1; i++)
            {
                pair = line.Substring(i, 2);
                // Look for the same pair further in the string
                if (line.IndexOf(pair, i + 2, StringComparison.Ordinal) >= 0)
                {
                    hasTwicePairs = true;
                    break;
                }
            }

            // If no such pair exists, skip to the next line
            if (!hasTwicePairs)
            {
                continue;
            }

            // Check for at least one letter which repeats with exactly one letter between them
            for (int i = 0; i < line.Length - 2; i++)
            {
                if (line[i] == line[i + 2])
                {
                    hasLetterBetwen = true;
                    break;
                }
            }

            if (hasLetterBetwen && hasTwicePairs)
            {
                result++;
            }
        }
        return result.ToString();
    }
}
