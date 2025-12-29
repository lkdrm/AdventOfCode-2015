using System.Text.Json;
using System.Text.RegularExpressions;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 12 of the coding challenge.
/// </summary>
public static class Day12
{
    private const string NumberPattern = @"-?\d+";

    /// <summary>
    /// Extracts all numbers from the specified input string and returns their sum as a string.
    /// </summary>
    /// <param name="input">The input string to search for numeric values. Cannot be null.</param>
    /// <returns>A string representation of the sum of all numbers found in the input. Returns "0" if no numbers are present.</returns>
    public static string SolvePart1(string input) => new Regex(NumberPattern).Matches(input).Select(m => int.Parse(m.Value)).Sum().ToString();

    /// <summary>
    /// Calculates the sum of all numbers in the provided JSON input, excluding any objects that contain a property with
    /// the value "red".
    /// </summary>
    /// <param name="input">A JSON-formatted string containing the data to be processed. Cannot be null or empty.</param>
    /// <returns>A string representation of the total sum of all qualifying numbers found in the input JSON.</returns>
    public static string SolvePart2(string input) => SumNumbers(JsonDocument.Parse(input).RootElement, true).ToString();

    /// <summary>
    /// Calculates the sum of all numeric values contained within the specified JSON element, optionally excluding
    /// objects that contain a property with the value "red".
    /// </summary>
    /// <param name="jsonElement">The JSON element to process. Can be a number, array, or object containing nested elements.</param>
    /// <param name="filterRed">If <see langword="true"/>, objects with any property value equal to "red" are excluded from the sum; otherwise,
    /// all objects are included.</param>
    /// <returns>The total sum of all numeric values found in the JSON element and its descendants. Returns 0 if no numeric
    /// values are present.</returns>
    private static int SumNumbers(JsonElement jsonElement, bool filterRed)
        => jsonElement.ValueKind switch
        {
            JsonValueKind.Number => jsonElement.GetInt32(),
            JsonValueKind.Array => jsonElement.EnumerateArray().Select(element => SumNumbers(element, filterRed)).Sum(),
            JsonValueKind.Object when filterRed && HasRedValue(jsonElement) => 0,
            JsonValueKind.Object => jsonElement.EnumerateObject().Select(element => SumNumbers(element.Value, filterRed)).Sum(),
            _ => 0,
        };

    /// <summary>
    /// Determines whether any property of the specified JSON object has a string value equal to "red".
    /// </summary>
    /// <param name="jsonElement">The JSON object to inspect for properties with the value "red". Must be a JSON object; other value kinds are
    /// ignored.</param>
    /// <returns>true if at least one property of the JSON object has a string value equal to "red"; otherwise, false.</returns>
    private static bool HasRedValue(JsonElement jsonElement)
    {
        foreach (JsonProperty property in jsonElement.EnumerateObject())
        {
            if (property.Value.ValueKind == JsonValueKind.String && property.Value.GetString() == "red")
            {
                return true;
            }
        }
        return false;
    }
}

