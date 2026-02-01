namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 16 of the coding challenge.
/// </summary>
public static class Day16
{
    private static readonly Dictionary<int, Dictionary<string, int>> _auntsTickerTapes = [];
    private static readonly Dictionary<string, int> _requiredTapes = new()
    {
        { "children", 3 }, { "cats", 7 }, { "samoyeds", 2 },
        { "pomeranians", 3 }, { "akitas", 0 }, { "vizslas", 0 },
        { "goldfish", 5 }, { "trees", 3 }, { "cars", 2 }, { "perfumes", 1 }
    };

    /// <summary>
    /// Determines the number of the aunt that matches the provided ticker tape information according to Part 1
    /// criteria.
    /// </summary>
    /// <param name="input">An array of strings, each representing an aunt's ticker tape data in the expected input format.</param>
    /// <returns>A string containing the number of the matching aunt. The value represents the 1-based index of the aunt that
    /// matches all specified properties.</returns>
    public static string SolvePart1(string[] input)
    {
        int aunt = 1;
        foreach (var item in input)
        {
            _auntsTickerTapes.Add(aunt, ParseInput(item));
            aunt++;
        }
        int result = FindMatchingAuntPart1();
        return result.ToString();
    }

    /// <summary>
    /// Solves part 2 of the puzzle using the specified input data and returns the result as a string.
    /// </summary>
    /// <param name="input">An array of strings representing the puzzle input lines to be processed.</param>
    /// <returns>A string containing the solution to part 2 of the puzzle.</returns>
    public static string SolvePart2(string[] input) => FindMatchingAuntPart2().ToString();

    /// <summary>
    /// Finds the identifier of the first aunt whose ticker tape information matches exactly three properties from the
    /// required set.
    /// </summary>
    /// <returns>The identifier of the first matching aunt found</returns>
    private static int FindMatchingAuntPart1()
    {
        int result = 0;
        foreach (var aunt in _auntsTickerTapes)
        {
            int matches = 0;
            foreach (var item in aunt.Value)
            {
                if (_requiredTapes[item.Key] == item.Value)
                {
                    matches++;
                }
                if (matches == 3)
                {
                    result = aunt.Key;
                }
            }
        }
        return result;
    }

    /// <summary>
    /// Finds the identifier of the aunt whose attributes match the criteria for part 2 of the analysis.
    /// </summary>
    /// <returns>The identifier of the matching aunt. Returns 0 if no matching aunt is found.</returns>
    private static int FindMatchingAuntPart2()
    {
        int result = 0;
        foreach (var aunt in _auntsTickerTapes)
        {
            if (MatchesAtrributes(aunt.Value))
            {
                result = aunt.Key;
            }
        }
        return result;
    }

    /// <summary>
    /// Determines whether all attributes in the specified collection match the required criteria.
    /// </summary>
    /// <param name="aunt">A dictionary containing attribute names and their corresponding values to be compared against the required
    /// criteria. Cannot be null.</param>
    /// <returns>true if all attributes in the collection satisfy the required conditions; otherwise, false.</returns>
    private static bool MatchesAtrributes(Dictionary<string, int> aunt)
    {
        foreach (var (attribute, value) in aunt)
        {
            if (!CompareAttribute(attribute, value, _requiredTapes[attribute]))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Determines whether the specified attribute value matches the expected value according to attribute-specific
    /// comparison rules.
    /// </summary>
    /// <param name="attribute">The name of the attribute to compare. Supported values include "cats", "trees", "pomeranians", "goldfish", and
    /// others.</param>
    /// <param name="aunt">The value of the attribute to evaluate.</param>
    /// <param name="expectedValue">The expected value to compare against.</param>
    /// <returns>true if the attribute value matches the expected value based on the comparison rules for the specified
    /// attribute; otherwise, false.</returns>
    private static bool CompareAttribute(string attribute, int aunt, int expectedValue) =>
        attribute switch
        {
            "cats" => aunt > expectedValue,
            "trees" => aunt > expectedValue,
            "pomeranians" => aunt < expectedValue,
            "goldfish" => aunt < expectedValue,
            _ => aunt == expectedValue
        };

    /// <summary>
    /// Parses a delimited input string and returns a dictionary mapping keys to integer values.
    /// </summary>
    /// <param name="input">A string containing key-value pairs separated by colons and commas. The expected format is
    /// "label:key1,value1,key2,value2,key3,value3".</param>
    /// <returns>A dictionary containing three entries, where each key is a trimmed string and each value is the corresponding
    /// integer parsed from the input.</returns>
    private static Dictionary<string, int> ParseInput(string input)
    {
        var splitString = input.Split(':', ',');
        return new Dictionary<string, int> { { splitString[1].Trim(), int.Parse(splitString[2]) }, { splitString[3].Trim(), int.Parse(splitString[4]) }, { splitString[5].Trim(), int.Parse(splitString[6]) } };
    }
}