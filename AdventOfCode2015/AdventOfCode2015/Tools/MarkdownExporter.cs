namespace AdventOfCode2015.Tools;

/// <summary>
/// Provides the functionality to export Advent of Code results to a Markdown file.
/// </summary>
public static class MarkdownExporter
{
    /// <summary>
    /// Writes all provided Advent of Code results to a Markdown file in formatted table for each day.
    /// </summary>
    /// <param name="filePath">The path to the Markdown file where the results will be saved.</param>
    /// <param name="result">A collection of tuples containing the day number, puzzle title and result for Part1 and Part2.</param>
    public static void SaveResultToMarkdown(string filePath, IEnumerable<(int day, string title, string part1Result, string part2Result)> result)
    {
        List<string> lines = ["# Advent of Code 2015", ""];

        foreach (var (day, title, part1, part2) in result)
        {
            lines.Add($"## Day {day}: {title}");
            lines.Add("");
            lines.Add($"| Part   | Result  |");
            lines.Add($"|--------|---------|");
            lines.Add($"  Part 1 | `{part1}`  ");
            lines.Add($"  Part 2 | `{part2}`  ");
            lines.Add($"|--------|-------- |");
            lines.Add("");
        }
        File.WriteAllLines(filePath, lines);
    }
}
