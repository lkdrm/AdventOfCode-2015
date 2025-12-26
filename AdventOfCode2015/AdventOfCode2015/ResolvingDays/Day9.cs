using AdventOfCode2015.Tools;

namespace AdventOfCode2015.ResolvingDays;

/// <summary>
/// Provides solutions for Day 9 of the coding challenge.
/// </summary>
public static class Day9
{
    private static readonly HashSet<string> _cities = new();
    private static readonly Dictionary<(string, string), int> _directions = new();

    /// <summary>
    /// Calculates the shortest possible total distance required to visit all cities exactly once, based on the provided
    /// input data.
    /// </summary>
    /// <param name="input">An array of strings containing the city connection and distance information. Each element should represent a
    /// route between two cities and the associated distance.</param>
    /// <returns>A string representation of the minimum total distance required to traverse all cities in a single route.</returns>
    public static string SolvePart1(string[] input)
    {
        TimerExtension.Start();
        foreach (var line in input)
        {
            ParseAttributeValues(line);
        }
        InitializeCityFromDirections();

        var cityList = _cities.ToList();
        var allPermutations = GetPermutations(cityList);

        int minDistance = int.MaxValue;

        foreach (var route in allPermutations)
        {
            var distance = CalculateRouteDistance(route);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }

        return minDistance.ToString();
    }

    /// <summary>
    /// Calculates the longest possible route distance between all cities based on the provided input data.
    /// </summary>
    /// <param name="input">An array of strings representing the input data for city connections and distances.</param>
    /// <returns>A string containing the maximum route distance found among all possible city permutations.</returns>
    public static string SolvePart2(string[] input)
    {
        TimerExtension.Start();
        
        var citiList = _cities.ToList();
        var allPermutations = GetPermutations(citiList);

        int maxDisatnce = int.MinValue;

        foreach (var route in allPermutations)
        {
            var distance = CalculateRouteDistance(route);
            if (distance > maxDisatnce)
            {
                maxDisatnce = distance;
            }
        }

        return maxDisatnce.ToString();
    }

    /// <summary>
    /// Parses an attribute assignment from the specified line and updates the corresponding value in the attribute
    /// collection.
    /// </summary>
    /// <param name="line">A string containing an attribute assignment in the format "key = value".</param>
    private static void ParseAttributeValues(string line)
    {
        var parts = line.Split(" = ");
        var cities = parts[0].Split(" to ");
        _directions[(cities[0], cities[1])] = int.Parse(parts[1]);
    }

    /// <summary>
    /// Initializes the collection of cities based on the available directions.
    /// </summary>
    private static void InitializeCityFromDirections()
    {
        foreach (var direction in _directions.Keys)
        {
            _cities.Add(direction.Item1);
            _cities.Add(direction.Item2);
        }
    }

    /// <summary>
    /// Generates all permutations of the given list of cities
    /// </summary>
    /// <param name="cities">The list of cities to permute.</param>
    /// <returns>An enumerable of all possible permutations.</returns>
    private static IEnumerable<List<string>> GetPermutations(List<string> cities)
    {
        if (cities.Count <= 1)
        {
            yield return cities;
            yield break;
        }

        for (int i = 0; i < cities.Count; i++)
        {
            var current = cities[i];
            var remaining = cities.Where((_, index) => index != i).ToList();

            foreach (var permutation in GetPermutations(remaining))
            {
                var result = new List<string> { current };
                result.AddRange(permutation);
                yield return result;
            }
        }
    }

    /// <summary>
    /// Calculates the total distance of a given route through all cities.
    /// </summary>
    /// <param name="routs">The ordered list of cities in the route.</param>
    /// <returns>The total distance of the route.</returns>
    private static int CalculateRouteDistance(List<string> routs)
    {
        int totalDistance = 0;

        for (int i = 0; i < routs.Count - 1; i++)
        {
            var city1 = routs[i];
            var city2 = routs[i + 1];
            if (_directions.ContainsKey((city1, city2)))
            {
                totalDistance += _directions[(city1, city2)];
            }
            else if (_directions.ContainsKey((city2, city1)))
            {
                totalDistance += _directions[(city2, city1)];
            }
        }
        return totalDistance;
    }
}
