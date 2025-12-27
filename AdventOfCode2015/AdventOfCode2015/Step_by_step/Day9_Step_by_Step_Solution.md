# Day 9: All in a Single Night - Step by Step Solution

## Step 1: Understanding the Problem

Find the shortest and longest route visiting all locations exactly once (Traveling Salesman Problem).

**Input format:** `LocationA to LocationB = distance`

**Part 1:** Find shortest route visiting all locations  
**Part 2:** Find longest route visiting all locations

---

## Step 2: Understanding TSP

### Example:
```
London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141

Possible routes (3! = 6):
1. London → Dublin → Belfast = 464 + 141 = 605
2. London → Belfast → Dublin = 518 + 141 = 659
3. Dublin → London → Belfast = 464 + 518 = 982
4. Dublin → Belfast → London = 141 + 518 = 659
5. Belfast → London → Dublin = 518 + 464 = 982
6. Belfast → Dublin → London = 141 + 464 = 605

Shortest: 605 (routes 1 or 6)
Longest: 982 (routes 3 or 5)
```

---

## Step 3: Algorithm - Generate All Permutations

```
1. Parse input to get locations and distances
2. Generate all permutations of locations
3. For each permutation:
   - Calculate total distance
   - Track minimum and maximum
4. Return min (Part 1) or max (Part 2)
```

---

## Step 4: Data Structure

```csharp
class RouteCalculator
{
    private Dictionary<string, Dictionary<string, int>> distances = new();
    private HashSet<string> locations = new();
    
    public void AddRoute(string from, string to, int distance)
    {
        locations.Add(from);
        locations.Add(to);
        
        if (!distances.ContainsKey(from))
            distances[from] = new Dictionary<string, int>();
        if (!distances.ContainsKey(to))
            distances[to] = new Dictionary<string, int>();
        
        distances[from][to] = distance;
        distances[to][from] = distance; // Bidirectional
    }
    
    public int GetDistance(string from, string to)
    {
        return distances[from][to];
    }
    
    public List<string> GetLocations()
    {
        return locations.ToList();
    }
}
```

---

## Step 5: Generating Permutations

```csharp
void GeneratePermutations<T>(List<T> items, int start, List<List<T>> result)
{
    if (start == items.Count - 1)
    {
        result.Add(new List<T>(items));
        return;
    }
    
    for (int i = start; i < items.Count; i++)
    {
        // Swap
        (items[start], items[i]) = (items[i], items[start]);
        
        // Recurse
        GeneratePermutations(items, start + 1, result);
        
        // Swap back
        (items[start], items[i]) = (items[i], items[start]);
    }
}
```

### Example: Permutations of [A, B, C]
```
Start with [A, B, C]

Level 0 (start=0):
  - Swap A with A: [A, B, C]
    → Recurse with start=1
  - Swap A with B: [B, A, C]
    → Recurse with start=1
  - Swap A with C: [C, B, A]
    → Recurse with start=1

Level 1 (start=1) for [A, B, C]:
  - Swap B with B: [A, B, C]
    → Recurse with start=2
  - Swap B with C: [A, C, B]
    → Recurse with start=2

Level 2 (start=2):
  - Base case: Add to result

Results:
[A, B, C]
[A, C, B]
[B, A, C]
[B, C, A]
[C, A, B]
[C, B, A]
```

---

## Step 6: Calculating Route Distance

```csharp
int CalculateRouteDistance(List<string> route, RouteCalculator calc)
{
    int totalDistance = 0;
    
    for (int i = 0; i < route.Count - 1; i++)
    {
        totalDistance += calc.GetDistance(route[i], route[i + 1]);
    }
    
    return totalDistance;
}
```

---

## Step 7: Complete Implementation

```csharp
public static class Day9
{
    public static string SolvePart1(string input)
    {
        var calc = ParseInput(input);
        int shortest = FindShortestRoute(calc);
        return shortest.ToString();
    }

    public static string SolvePart2(string input)
    {
        var calc = ParseInput(input);
        int longest = FindLongestRoute(calc);
        return longest.ToString();
    }

    private static RouteCalculator ParseInput(string input)
    {
        var calc = new RouteCalculator();
        
        foreach (string line in input.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            
            // Parse: "London to Dublin = 464"
            string[] parts = line.Split(new[] { " to ", " = " }, 
                                        StringSplitOptions.None);
            string from = parts[0].Trim();
            string to = parts[1].Trim();
            int distance = int.Parse(parts[2].Trim());
            
            calc.AddRoute(from, to, distance);
        }
        
        return calc;
    }

    private static int FindShortestRoute(RouteCalculator calc)
    {
        List<string> locations = calc.GetLocations();
        List<List<string>> permutations = new();
        GeneratePermutations(locations, 0, permutations);
        
        int minDistance = int.MaxValue;
        
        foreach (var route in permutations)
        {
            int distance = CalculateRouteDistance(route, calc);
            minDistance = Math.Min(minDistance, distance);
        }
        
        return minDistance;
    }

    private static int FindLongestRoute(RouteCalculator calc)
    {
        List<string> locations = calc.GetLocations();
        List<List<string>> permutations = new();
        GeneratePermutations(locations, 0, permutations);
        
        int maxDistance = 0;
        
        foreach (var route in permutations)
        {
            int distance = CalculateRouteDistance(route, calc);
            maxDistance = Math.Max(maxDistance, distance);
        }
        
        return maxDistance;
    }

    private static void GeneratePermutations<T>(List<T> items, int start, 
                                                 List<List<T>> result)
    {
        if (start == items.Count - 1)
        {
            result.Add(new List<T>(items));
            return;
        }
        
        for (int i = start; i < items.Count; i++)
        {
            (items[start], items[i]) = (items[i], items[start]);
            GeneratePermutations(items, start + 1, result);
            (items[start], items[i]) = (items[i], items[start]);
        }
    }

    private static int CalculateRouteDistance(List<string> route, 
                                              RouteCalculator calc)
    {
        int total = 0;
        for (int i = 0; i < route.Count - 1; i++)
        {
            total += calc.GetDistance(route[i], route[i + 1]);
        }
        return total;
    }

    private class RouteCalculator
    {
        private Dictionary<string, Dictionary<string, int>> distances = new();
        private HashSet<string> locations = new();
        
        public void AddRoute(string from, string to, int distance)
        {
            locations.Add(from);
            locations.Add(to);
            
            if (!distances.ContainsKey(from))
                distances[from] = new Dictionary<string, int>();
            if (!distances.ContainsKey(to))
                distances[to] = new Dictionary<string, int>();
            
            distances[from][to] = distance;
            distances[to][from] = distance;
        }
        
        public int GetDistance(string from, string to)
        {
            return distances[from][to];
        }
        
        public List<string> GetLocations()
        {
            return locations.ToList();
        }
    }
}
```

---

## Step 8: Complexity Analysis

### Time Complexity:
- Generating permutations: O(n!)
- Calculating each route: O(n)
- Total: O(n! × n)

### For 8 locations:
- 8! = 40,320 permutations
- Still manageable for brute force

### For 10 locations:
- 10! = 3,628,800 permutations
- Getting slow but still feasible

---

## Step 9: Why This is NP-Hard

- No known polynomial-time algorithm
- Brute force is acceptable for small inputs (< 20 locations)
- For larger inputs, need heuristics:
  - Nearest neighbor
  - Genetic algorithms
  - Branch and bound

---

## Step 10: Common Mistakes

### Mistake 1: Forgetting Bidirectional Edges
```csharp
// WRONG - Only one direction
distances[from][to] = distance;

// CORRECT - Both directions
distances[from][to] = distance;
distances[to][from] = distance;
```

### Mistake 2: Not Copying List in Permutation
```csharp
// WRONG - All permutations reference same list
result.Add(items);

// CORRECT - Create new copy
result.Add(new List<T>(items));
```

---

## Step 11: Summary

**Key Concepts:**
- ✅ Traveling Salesman Problem (TSP)
- ✅ Permutation generation
- ✅ Backtracking algorithm
- ✅ Graph distance calculation
- ✅ NP-hard problem solving

**Algorithm:**
```
Generate all permutations → Calculate distances → Find min/max
```

**Happy traveling! 🎄🗺️**
