namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 13 of the coding challenge.
/// </summary>
public static class Day13
{
    private static readonly Dictionary<(string, string), int> _happiness = [];
    private static readonly HashSet<string> _people = [];
    private const string JustMe = "Me";

    /// <summary>
    /// Calculates the maximum total happiness achievable by arranging all people based on the provided input data.
    /// </summary>
    /// <param name="input">An array of strings representing the input data for each person's happiness preferences.</param>
    /// <returns>A string containing the highest possible total happiness value for any seating arrangement.</returns>
    public static string SolvePart1(string[] input)
    {
        ParseInput(input);
        var permutations = GeneratePermutations(_people.ToList());

        int maxHappines = int.MinValue;

        foreach (var line in permutations)
        {
            var arrangement = new List<string> { };
            arrangement.AddRange(line);

            int happiness = CalculateHappiness(arrangement, _happiness);

            if (happiness > maxHappines)
            {
                maxHappines = happiness;
            }
        }

        return maxHappines.ToString();
    }

    /// <summary>
    /// Calculates the maximum total happiness for all seating arrangements, including yourself, based on the provided
    /// input data.
    /// </summary>
    /// <param name="input">An array of strings representing the happiness relationships between individuals. Each entry should specify how
    /// much happiness one person gains or loses by sitting next to another.</param>
    /// <returns>A string representation of the highest possible total happiness achievable by seating all individuals, including
    /// yourself, according to the input relationships.</returns>
    public static string SolvePart2(string[] input)
    {
        ParseInput(input);

        foreach (var person in _people)
        {
            _happiness[(JustMe, person)] = 0;
            _happiness[(person, JustMe)] = 0;
        }
        _people.Add(JustMe);

        var permutations = GeneratePermutations(_people.ToList());

        int maxHappines = int.MinValue;

        foreach (var line in permutations)
        {
            var arrangement = new List<string> { };
            arrangement.AddRange(line);

            int happiness = CalculateHappiness(arrangement, _happiness);

            if (happiness > maxHappines)
            {
                maxHappines = happiness;
            }
        }

        return maxHappines.ToString();

    }

    /// <summary>
    /// Parses an array of input strings to extract and store happiness relationships and people identifiers.
    /// </summary>
    /// <param name="input">An array of strings, each representing a relationship statement to be parsed. Each string must be formatted with
    /// space-separated values describing the relationship.</param>
    private static void ParseInput(string[] input)
    {
        foreach (var line in input)
        {
            var splitString = line.Split(' ');
            _happiness[(splitString.First(), splitString.Last().TrimEnd('.'))] = splitString.Where(element => int.TryParse(element, out _)).Select(int.Parse).First() * (splitString[2] == "lose" ? -1 : 1);
            _people.Add(splitString.First());
            _people.Add(splitString.Last().TrimEnd('.'));
        }
    }

    /// <summary>
    /// Generates all possible permutations of the elements in the specified list.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the input list.</typeparam>
    /// <param name="items">The list of items to permute. Cannot be null.</param>
    /// <returns>A list of lists, where each inner list represents a unique permutation of the input items. If the input list is
    /// empty, returns a list containing a single empty list.</returns>
    private static List<List<T>> GeneratePermutations<T>(List<T> items)
    {
        var results = new List<List<T>>();
        Permute(items, 0, results);
        return results;
    }

    /// <summary>
    /// Generates all possible permutations of the elements in the specified list and adds them to the provided results
    /// collection.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the list to permute.</typeparam>
    /// <param name="peoples">The list of elements to permute. The method modifies this list during execution but restores its original order
    /// before returning.</param>
    /// <param name="start">The starting index from which to generate permutations. Must be within the bounds of the list.</param>
    /// <param name="results">The collection to which each generated permutation is added as a new list.</param>
    private static void Permute<T>(List<T> peoples, int start, List<List<T>> results)
    {
        if (start == peoples.Count - 1)
        {
            results.Add([.. peoples]);
            return;
        }

        for (int i = start; i < peoples.Count; i++)
        {
            // Swap
            (peoples[start], peoples[i]) = (peoples[i], peoples[start]);
            // Recurse
            Permute(peoples, start + 1, results);
            // Backtrack
            (peoples[start], peoples[i]) = (peoples[i], peoples[start]);
        }
    }

    /// <summary>
    /// Calculates the total happiness score for a circular arrangement of persons based on pairwise happiness values.
    /// </summary>
    /// <param name="persons">The ordered list of person names representing their seating arrangement. The arrangement is treated as circular,
    /// so the last person is considered adjacent to the first.</param>
    /// <param name="happiness">A dictionary mapping each pair of person names to an integer happiness value, representing the happiness impact
    /// between those two persons in both directions.</param>
    /// <returns>The total happiness score for the given arrangement, computed by summing the happiness values for all adjacent
    /// pairs in both directions.</returns>
    private static int CalculateHappiness(List<string> persons, Dictionary<(string, string), int> happiness)
    {
        int total = 0;
        int n = persons.Count;

        for (int i = 0; i < n; i++)
        {
            string current = persons[i];
            string next = persons[(i + 1) % n];// Wrap around for last person

            total += happiness[(current, next)];
            total += happiness[(next, current)];
        }
        return total;
    }
}
