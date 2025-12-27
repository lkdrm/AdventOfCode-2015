using AdventOfCode2015.Tools;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 11 of the coding challenge.
/// </summary>
public static class Day11
{
    private static string _resultFromPart1;

    /// <summary>
    /// Finds the next valid password based on the specified input string according to the puzzle's rules.
    /// </summary>
    /// <param name="input">The current password as a string. Cannot be null.</param>
    /// <returns>A string representing the next valid password that satisfies all required criteria.</returns>
    public static string SolvePart1(string input)
    {
        TimerExtension.Start();
        var result = FindNextValidPassword(input.Trim());
        _resultFromPart1 = result;
        return result;
    }

    /// <summary>
    /// Finds the next valid password according to the rules defined for part 2 of the puzzle.
    /// </summary>
    /// <param name="input">The input string representing the initial password or puzzle data. This parameter is not used in this method but
    /// is required to match the expected method signature.</param>
    /// <returns>A string containing the next valid password that satisfies all criteria for part 2.</returns>
    public static string SolvePart2(string input)
    {
        TimerExtension.Start();
        var result = FindNextValidPassword(_resultFromPart1);
        return result;
    }

    /// <summary>
    /// Finds the next password that meets all validity requirements, starting from the specified password.
    /// </summary>
    /// <param name="currentPassword">The current password to use as the starting point for the search. Cannot be null or empty.</param>
    /// <returns>A string containing the next valid password according to the defined rules.</returns>
    private static string FindNextValidPassword(string currentPassword)
    {
        string password = currentPassword;

        do
        {
            password = IncrementPassword(password);

            if (password.Contains('i') || password.Contains('o') || password.Contains('l'))
            {
                password = SkipForbiddenLetters(password);
            }
        }
        while (!IsValidPassword(password));

        return password;
    }

    /// <summary>
    /// Generates the next password in sequence by incrementing the specified password string according to custom rules.
    /// </summary>
    /// <param name="password">The current password to increment. Must consist of lowercase alphabetic characters ('a'–'z').</param>
    /// <returns>A new string representing the incremented password, with forbidden letters ('i', 'o', 'l') skipped as needed.</returns>
    private static string IncrementPassword(string password)
    {
        char[] chars = password.ToCharArray();
        for (int i = chars.Length - 1; i >= 0; i--)
        {
            if (chars[i] == 'z')
            {
                chars[i] = 'a';
            }
            else
            {
                chars[i]++;
                // Skip forbidden letters
                if (chars[i] == 'i' || chars[i] == 'o' || chars[i] == 'l')
                {
                    chars[i]++;
                }

                break;
            }
        }

        return new string(chars);
    }

    /// <summary>
    /// Determines whether the specified password contains an increasing straight of three consecutive letters.
    /// </summary>
    /// <param name="password">The password string to evaluate. Cannot be null.</param>
    /// <returns>true if the password contains at least one sequence of three increasing consecutive letters; otherwise, false.</returns>
    private static bool HasIncreasingStraight(string password)
    {
        for (int i = 0; i < password.Length - 2; i++)
        {
            char c1 = password[i];
            char c2 = password[i + 1];
            char c3 = password[i + 2];

            if (c2 - c1 == 1 && c3 - c2 == 1)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Determines whether the specified password does not contain any forbidden letters.
    /// </summary>
    /// <param name="password">The password string to check for forbidden letters. Cannot be null.</param>
    /// <returns>true if the password does not contain the letters 'i', 'o', or 'l'; otherwise, false.</returns>
    private static bool NoForbiddenLetters(string password) => !password.Contains('i') && !password.Contains('o') && !password.Contains('l');

    /// <summary>
    /// Determines whether the specified password contains at least two different pairs of consecutive identical
    /// characters.
    /// </summary>
    /// <param name="password">The password string to evaluate. Cannot be null.</param>
    /// <returns>true if the password contains at least two different pairs of consecutive identical characters; otherwise,
    /// false.</returns>
    private static bool HasTwoDifferentPairs(string password)
    {
        HashSet<char> pairsFound = new();

        for (int i = 0; i < password.Length - 1; i++)
        {
            if (password[i] == password[i + 1])
            {
                pairsFound.Add(password[i]);

                // Skip next character
                i++;

                if (pairsFound.Count >= 2)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Determines whether the specified password meets all required validity criteria.
    /// </summary>
    /// <param name="password">The password string to validate. Cannot be null.</param>
    /// <returns>true if the password satisfies all validity rules; otherwise, false.</returns>
    private static bool IsValidPassword(string password) => HasIncreasingStraight(password) && NoForbiddenLetters(password) && HasTwoDifferentPairs(password);

    /// <summary>
    /// Replaces the first occurrence of the forbidden letters 'i', 'o', or 'l' in the specified password with the next
    /// allowed letter and resets all subsequent characters to 'a'.
    /// </summary>
    /// <param name="password">The password string to process. Cannot be null.</param>
    /// <returns>A new string with the first forbidden letter replaced and all following characters set to 'a'. If no forbidden
    /// letters are present, returns the original password.</returns>
    private static string SkipForbiddenLetters(string password)
    {
        char[] chars = password.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == 'i' || chars[i] == 'o' || chars[i] == 'l')
            {
                if (chars[i] == 'i')
                {
                    chars[i] = 'j';
                }
                else if (chars[i] == 'o')
                {
                    chars[i] = 'p';
                }
                else if (chars[i] == 'l')
                {
                    chars[i] = 'm';
                }

                for (int j = i + 1; j < chars.Length; j++)
                {
                    chars[j] = 'a';
                }

                break;
            }
        }

        return new string(chars);
    }
}
