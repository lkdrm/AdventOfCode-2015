# Day 13: Knights of the Dinner Table - Step by Step Solution

## Step 1: Understanding the Problem

Santa needs help arranging people around a circular dinner table to maximize happiness.

**Part 1:** Find the seating arrangement that maximizes total happiness.

**Part 2:** Add yourself to the seating arrangement (with neutral happiness values) and find the new optimal arrangement.

### Input Format:
```
Person1 would gain/lose X happiness units by sitting next to Person2.
```

Example:
```
Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Bob would gain 83 happiness units by sitting next to Alice.
```

**Key Points:**
- Relationships are **directional**: Alice→Bob ≠ Bob→Alice
- Table is **circular**: first person sits next to last person
- Each person has exactly **2 neighbors**
- Total happiness = sum of all neighbor pairs (counting both directions)

---

## Step 2: Analyzing the Example

### Example Input (4 people):
```
Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol.
```

### Optimal Arrangement:
```
    David
   ↙    ↘
Carol    Alice
   ↖    ↗
    Bob

Pairs:
1. Alice ↔ David: (-2) + (+46) = 44
2. Alice ↔ Bob:   (+54) + (+83) = 137
3. Bob ↔ Carol:   (-7) + (+60) = 53
4. Carol ↔ David: (+55) + (+41) = 96

Total: 44 + 137 + 53 + 96 = 330
```

**Why this is optimal:**
- Strong Alice-Bob connection (137)
- Good David-Carol connection (96)
- Avoids very negative pairs

---

## Step 3: Data Structure Design

### Happiness Dictionary
Store all relationships in a dictionary with tuple keys:

```csharp
Dictionary<(string, string), int> happiness = new();

// Example entries:
happiness[("Alice", "Bob")] = 54;
happiness[("Bob", "Alice")] = 83;
happiness[("Alice", "Carol")] = -79;
```

**Why tuple keys?**
- Fast O(1) lookup
- Natural representation of directed relationship
- Easy to access both directions

### People Set
Store unique person names:

```csharp
HashSet<string> people = new();

// Extract from input:
people.Add("Alice");
people.Add("Bob");
people.Add("Carol");
people.Add("David");
```

---

## Step 4: Parsing Input

### Parsing Strategy

Each line follows this pattern:
```
Position: 0      1     2    3     4         5     6      7      8      9      10
          Person would gain/lose value happiness units by    sitting next   to     Person2.
```

### Implementation:

```csharp
public static (Dictionary<(string, string), int>, HashSet<string>) ParseInput(string[] lines)
{
    var happiness = new Dictionary<(string, string), int>();
    var people = new HashSet<string>();
    
    foreach (var line in lines)
    {
        var parts = line.Split(' ');
        
        string person1 = parts[0];
        string gainOrLose = parts[2];
        int value = int.Parse(parts[3]);
        string person2 = parts[10].TrimEnd('.');
        
        // Handle "lose" by negating value
        if (gainOrLose == "lose")
            value = -value;
        
        // Store relationship
        happiness[(person1, person2)] = value;
        
        // Track unique people
        people.Add(person1);
        people.Add(person2);
    }
    
    return (happiness, people);
}
```

### Example Parsing:

**Input line:** `"Alice would gain 54 happiness units by sitting next to Bob."`

```
parts[0] = "Alice"
parts[2] = "gain"
parts[3] = "54"
parts[10] = "Bob."

After processing:
person1 = "Alice"
gainOrLose = "gain"
value = 54
person2 = "Bob"

Store: happiness[("Alice", "Bob")] = 54
```

**Input line:** `"Alice would lose 79 happiness units by sitting next to Carol."`

```
parts[2] = "lose"
parts[3] = "79"

After processing:
value = -79 (negated because "lose")

Store: happiness[("Alice", "Carol")] = -79
```

---

## Step 5: Generating Permutations

### Why Permutations?

We need to try all possible seating arrangements:
- For N people around a circular table: **(N-1)!** unique arrangements
- We fix the first person to avoid counting rotations as different

### Circular vs Linear Permutations:

**Linear (4 people):** 4! = 24 permutations
```
Different: [A,B,C,D] vs [B,C,D,A]
```

**Circular (4 people):** (4-1)! = 6 permutations
```
Same: [A,B,C,D] and [B,C,D,A] (just rotated)
Solution: Fix A at position 0, permute rest
```

### Recursive Permutation Generator:

```csharp
private static List<List<T>> GeneratePermutations<T>(List<T> items)
{
    var results = new List<List<T>>();
    Permute(items, 0, results);
    return results;
}

private static void Permute<T>(List<T> items, int start, List<List<T>> results)
{
    if (start == items.Count - 1)
    {
        results.Add(new List<T>(items));
        return;
    }
    
    for (int i = start; i < items.Count; i++)
    {
        // Swap
        (items[start], items[i]) = (items[i], items[start]);
        
        // Recurse
        Permute(items, start + 1, results);
        
        // Backtrack
        (items[start], items[i]) = (items[i], items[start]);
    }
}
```

### How It Works:

**Input:** [Bob, Carol, David] (Alice already fixed)

```
Step 1: Fix Bob at position 0
  Permute [Carol, David]
  → [Bob, Carol, David]
  → [Bob, David, Carol]

Step 2: Fix Carol at position 0
  Permute [Bob, David]
  → [Carol, Bob, David]
  → [Carol, David, Bob]

Step 3: Fix David at position 0
  Permute [Bob, Carol]
  → [David, Bob, Carol]
  → [David, Carol, Bob]

Result: 6 permutations (3! = 6)
```

---

## Step 6: Calculating Happiness for an Arrangement

### Algorithm:

For each person in the arrangement:
1. Find their two neighbors (left and right)
2. Add happiness in both directions
3. Handle wrap-around for circular table

### Implementation:

```csharp
private static int CalculateHappiness(
    List<string> arrangement, 
    Dictionary<(string, string), int> happiness)
{
    int total = 0;
    int n = arrangement.Count;
    
    for (int i = 0; i < n; i++)
    {
        string current = arrangement[i];
        string next = arrangement[(i + 1) % n]; // Wrap around for last person
        
        // Add both directions
        total += happiness[(current, next)];
        total += happiness[(next, current)];
    }
    
    return total;
}
```

### Example Trace:

**Arrangement:** [Alice, David, Carol, Bob]

```
Visual:
    Alice (0)
   ↙      ↘
Bob (3)    David (1)
   ↖      ↗
    Carol (2)

Iteration i=0: current=Alice, next=David
  happiness[("Alice", "David")] = -2
  happiness[("David", "Alice")] = 46
  total = 0 + (-2) + 46 = 44

Iteration i=1: current=David, next=Carol
  happiness[("David", "Carol")] = 41
  happiness[("Carol", "David")] = 55
  total = 44 + 41 + 55 = 140

Iteration i=2: current=Carol, next=Bob
  happiness[("Carol", "Bob")] = 60
  happiness[("Bob", "Carol")] = -7
  total = 140 + 60 + (-7) = 193

Iteration i=3: current=Bob, next=Alice (wraps: (3+1)%4 = 0)
  happiness[("Bob", "Alice")] = 83
  happiness[("Alice", "Bob")] = 54
  total = 193 + 83 + 54 = 330

Final total: 330
```

**Key Point:** The modulo operation `(i + 1) % n` handles the circular connection from the last person back to the first.

---

## Step 7: Part 1 - Find Maximum Happiness

### Complete Algorithm:

```csharp
public static int SolvePart1(string[] lines)
{
    // Parse input
    var (happiness, people) = ParseInput(lines);
    
    // Convert to list and fix first person
    var peopleList = people.ToList();
    var first = peopleList[0];
    var remaining = peopleList.Skip(1).ToList();
    
    // Generate permutations of remaining people
    var permutations = GeneratePermutations(remaining);
    
    int maxHappiness = int.MinValue;
    
    // Try each permutation
    foreach (var perm in permutations)
    {
        // Add fixed person at the start
        var arrangement = new List<string> { first };
        arrangement.AddRange(perm);
        
        // Calculate happiness
        int happiness = CalculateHappiness(arrangement, happiness);
        
        // Track maximum
        if (happiness > maxHappiness)
            maxHappiness = happiness;
    }
    
    return maxHappiness;
}
```

### Example Execution (4 people):

```
People: [Alice, Bob, Carol, David]
Fix: Alice
Remaining: [Bob, Carol, David]

Generate 6 permutations:

1. [Alice, Bob, Carol, David]   → Calculate: 325
2. [Alice, Bob, David, Carol]   → Calculate: 22
3. [Alice, Carol, Bob, David]   → Calculate: 270
4. [Alice, Carol, David, Bob]   → Calculate: 264
5. [Alice, David, Bob, Carol]   → Calculate: 286
6. [Alice, David, Carol, Bob]   → Calculate: 330 ← MAX!

Return: 330
```

---

## Step 8: Part 2 - Adding Yourself

### New Requirements:

Add yourself ("Me") to the seating arrangement:
- Your happiness toward anyone: **0** (neutral)
- Anyone's happiness toward you: **0** (neutral)

### Implementation:

```csharp
public static int SolvePart2(string[] lines)
{
    // Parse input
    var (happiness, people) = ParseInput(lines);
    
    // Add yourself with neutral relationships
    string me = "Me";
    foreach (var person in people)
    {
        happiness[(me, person)] = 0;
        happiness[(person, me)] = 0;
    }
    people.Add(me);
    
    // Now solve like Part 1
    var peopleList = people.ToList();
    var first = peopleList[0];
    var remaining = peopleList.Skip(1).ToList();
    
    var permutations = GeneratePermutations(remaining);
    
    int maxHappiness = int.MinValue;
    
    foreach (var perm in permutations)
    {
        var arrangement = new List<string> { first };
        arrangement.AddRange(perm);
        
        int currentHappiness = CalculateHappiness(arrangement, happiness);
        
        if (currentHappiness > maxHappiness)
            maxHappiness = currentHappiness;
    }
    
    return maxHappiness;
}
```

### Example with "Me":

**Original (Part 1):**
```
4 people: Alice, Bob, Carol, David
Permutations: 3! = 6
Max happiness: 330
```

**With "Me" (Part 2):**
```
5 people: Alice, Bob, Carol, David, Me
Permutations: 4! = 24

New relationships:
("Me", "Alice") = 0    ("Alice", "Me") = 0
("Me", "Bob") = 0      ("Bob", "Me") = 0
("Me", "Carol") = 0    ("Carol", "Me") = 0
("Me", "David") = 0    ("David", "Me") = 0

Best arrangement might be:
Alice - David - Carol - Bob - Me - (back to Alice)

Pairs involving "Me":
Bob ↔ Me:    0 + 0 = 0
Me ↔ Alice:  0 + 0 = 0

These replace two potentially positive pairs!

Max happiness: Usually lower than Part 1 (e.g., 286)
```

---

## Step 9: Complete Implementation

Putting it all together:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public static class Day13
{
    // Parse input lines into happiness dictionary and people set
    public static (Dictionary<(string, string), int>, HashSet<string>) ParseInput(string[] lines)
    {
        var happiness = new Dictionary<(string, string), int>();
        var people = new HashSet<string>();
        
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            
            string person1 = parts[0];
            string gainOrLose = parts[2];
            int value = int.Parse(parts[3]);
            string person2 = parts[10].TrimEnd('.');
            
            if (gainOrLose == "lose")
                value = -value;
            
            happiness[(person1, person2)] = value;
            people.Add(person1);
            people.Add(person2);
        }
        
        return (happiness, people);
    }
    
    // Generate all permutations of a list
    private static List<List<T>> GeneratePermutations<T>(List<T> items)
    {
        var results = new List<List<T>>();
        Permute(items, 0, results);
        return results;
    }
    
    private static void Permute<T>(List<T> items, int start, List<List<T>> results)
    {
        if (start == items.Count - 1)
        {
            results.Add(new List<T>(items));
            return;
        }
        
        for (int i = start; i < items.Count; i++)
        {
            (items[start], items[i]) = (items[i], items[start]);
            Permute(items, start + 1, results);
            (items[start], items[i]) = (items[i], items[start]);
        }
    }
    
    // Calculate total happiness for a seating arrangement
    private static int CalculateHappiness(
        List<string> arrangement, 
        Dictionary<(string, string), int> happiness)
    {
        int total = 0;
        int n = arrangement.Count;
        
        for (int i = 0; i < n; i++)
        {
            string current = arrangement[i];
            string next = arrangement[(i + 1) % n];
            
            total += happiness[(current, next)];
            total += happiness[(next, current)];
        }
        
        return total;
    }
    
    // Part 1: Find optimal seating arrangement
    public static int SolvePart1(string[] lines)
    {
        var (happiness, people) = ParseInput(lines);
        
        var peopleList = people.ToList();
        var first = peopleList[0];
        var remaining = peopleList.Skip(1).ToList();
        
        var permutations = GeneratePermutations(remaining);
        
        int maxHappiness = int.MinValue;
        
        foreach (var perm in permutations)
        {
            var arrangement = new List<string> { first };
            arrangement.AddRange(perm);
            
            int currentHappiness = CalculateHappiness(arrangement, happiness);
            maxHappiness = Math.Max(maxHappiness, currentHappiness);
        }
        
        return maxHappiness;
    }
    
    // Part 2: Add yourself with neutral relationships
    public static int SolvePart2(string[] lines)
    {
        var (happiness, people) = ParseInput(lines);
        
        // Add yourself
        string me = "Me";
        foreach (var person in people.ToList())
        {
            happiness[(me, person)] = 0;
            happiness[(person, me)] = 0;
        }
        people.Add(me);
        
        // Solve like Part 1
        var peopleList = people.ToList();
        var first = peopleList[0];
        var remaining = peopleList.Skip(1).ToList();
        
        var permutations = GeneratePermutations(remaining);
        
        int maxHappiness = int.MinValue;
        
        foreach (var perm in permutations)
        {
            var arrangement = new List<string> { first };
            arrangement.AddRange(perm);
            
            int currentHappiness = CalculateHappiness(arrangement, happiness);
            maxHappiness = Math.Max(maxHappiness, currentHappiness);
        }
        
        return maxHappiness;
    }
}
```

---

## Step 10: Detailed Example Trace

### Input (simplified 4 people):
```
Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol.
```

### Part 1 Execution:

**Step 1: Parse**
```
happiness[("Alice", "Bob")] = 54
happiness[("Alice", "Carol")] = -79
happiness[("Alice", "David")] = -2
happiness[("Bob", "Alice")] = 83
happiness[("Bob", "Carol")] = -7
happiness[("Bob", "David")] = -63
happiness[("Carol", "Alice")] = -62
happiness[("Carol", "Bob")] = 60
happiness[("Carol", "David")] = 55
happiness[("David", "Alice")] = 46
happiness[("David", "Bob")] = -7
happiness[("David", "Carol")] = 41

people = {Alice, Bob, Carol, David}
```

**Step 2: Generate Permutations**
```
Fix: Alice
Remaining: [Bob, Carol, David]

Permutations (3! = 6):
1. [Bob, Carol, David]   → [Alice, Bob, Carol, David]
2. [Bob, David, Carol]   → [Alice, Bob, David, Carol]
3. [Carol, Bob, David]   → [Alice, Carol, Bob, David]
4. [Carol, David, Bob]   → [Alice, Carol, David, Bob]
5. [David, Bob, Carol]   → [Alice, David, Bob, Carol]
6. [David, Carol, Bob]   → [Alice, David, Carol, Bob]
```

**Step 3: Calculate Each**

**Permutation 1: [Alice, Bob, Carol, David]**
```
Pairs:
i=0: Alice→Bob: 54, Bob→Alice: 83    → 137
i=1: Bob→Carol: -7, Carol→Bob: 60    → 53
i=2: Carol→David: 55, David→Carol: 41 → 96
i=3: David→Alice: 46, Alice→David: -2 → 44
Total: 137 + 53 + 96 + 44 = 330
```

**Permutation 2: [Alice, Bob, David, Carol]**
```
Pairs:
i=0: Alice→Bob: 54, Bob→Alice: 83      → 137
i=1: Bob→David: -63, David→Bob: -7     → -70
i=2: David→Carol: 41, Carol→David: 55  → 96
i=3: Carol→Alice: -79, Alice→Carol: -62 → -141
Total: 137 + (-70) + 96 + (-141) = 22
```

**Permutation 6: [Alice, David, Carol, Bob]**
```
Pairs:
i=0: Alice→David: -2, David→Alice: 46  → 44
i=1: David→Carol: 41, Carol→David: 55  → 96
i=2: Carol→Bob: 60, Bob→Carol: -7      → 53
i=3: Bob→Alice: 83, Alice→Bob: 54      → 137
Total: 44 + 96 + 53 + 137 = 330 ← MAXIMUM!
```

**Result:** Maximum happiness = **330**

---

## Step 11: Common Mistakes to Avoid

### Mistake 1: Forgetting Circular Connection
```csharp
// WRONG - Loop doesn't include last→first
for (int i = 0; i < n - 1; i++)
{
    string current = arrangement[i];
    string next = arrangement[i + 1];
    // ...
}

// CORRECT - Modulo wraps around
for (int i = 0; i < n; i++)
{
    string current = arrangement[i];
    string next = arrangement[(i + 1) % n]; // Wraps!
    // ...
}
```

### Mistake 2: One-Directional Happiness
```csharp
// WRONG - Only counts one direction
total += happiness[(current, next)];

// CORRECT - Count both directions
total += happiness[(current, next)];
total += happiness[(next, current)];
```

### Mistake 3: Not Negating "lose"
```csharp
// WRONG - All values positive
int value = int.Parse(parts[3]);

// CORRECT - Negate for "lose"
int value = int.Parse(parts[3]);
if (parts[2] == "lose")
    value = -value;
```

### Mistake 4: Counting Rotations
```csharp
// WRONG - Generates N! permutations (includes rotations)
var permutations = GeneratePermutations(people.ToList());

// CORRECT - Fix first person, generate (N-1)! permutations
var first = people.First();
var remaining = people.Skip(1).ToList();
var permutations = GeneratePermutations(remaining);
// Then prepend first to each
```

### Mistake 5: Part 2 Incomplete Relationships
```csharp
// WRONG - Only one direction
happiness[("Me", person)] = 0;

// CORRECT - Both directions
happiness[("Me", person)] = 0;
happiness[(person, "Me")] = 0;
```

---

## Step 12: Optimization Considerations

### Time Complexity:

**Parsing:**
```
O(M) where M = number of input lines
```

**Permutation Generation:**
```
O(N!) where N = number of people
Actually (N-1)! with fixed first person
```

**Happiness Calculation:**
```
O(N! × N)
- Each of (N-1)! permutations
- Each takes O(N) to calculate
```

**Total: O(N! × N)**

### Space Complexity:

```
O(N²) for happiness dictionary
O(N!) if storing all permutations
O(N) if generating on-the-fly

Practical: O(N²) with on-the-fly generation
```

### Performance for Different Sizes:

```
N=4:  3! = 6 permutations          (instant)
N=8:  7! = 5,040 permutations      (< 1ms)
N=10: 9! = 362,880 permutations    (< 100ms)
N=12: 11! = 39,916,800 permutations (~5 seconds)
```

### Why This Problem Is Hard:

- **NP-hard** (similar to Traveling Salesman Problem)
- No known polynomial-time algorithm
- Brute force is reasonable for small N (< 13)
- For large N, need heuristics or approximations

---

## Step 13: Testing Strategy

### Test Case 1: Example from Problem
```
Input: 4 people with sample relationships
Expected: 330
Verify: Specific arrangement found
```

### Test Case 2: All Positive Relationships
```
Input: Everyone likes everyone
Expected: Maximum value (all sums positive)
Verify: Any arrangement should work well
```

### Test Case 3: All Negative Relationships
```
Input: Everyone dislikes everyone
Expected: Least negative value
Verify: "Least bad" arrangement
```

### Test Case 4: Part 2 with Neutral
```
Input: Same as Part 1 + "Me" with 0 values
Expected: Lower than Part 1 (neutral breaks good pairs)
Verify: Correct neutral relationship handling
```

### Test Case 5: Edge Case - 2 People
```
Input: Just Alice and Bob
Only one arrangement: Alice - Bob - Alice
Verify: Correct circular calculation
```

---

## Step 14: Alternative Implementation (LINQ)

More concise version using LINQ:

```csharp
public static int SolvePart1(string[] lines)
{
    var (happiness, people) = ParseInput(lines);
    var peopleList = people.ToList();
    var first = peopleList[0];
    var remaining = peopleList.Skip(1).ToList();
    
    return GeneratePermutations(remaining)
        .Select(perm =>
        {
            var arrangement = new List<string> { first };
            arrangement.AddRange(perm);
            return CalculateHappiness(arrangement, happiness);
        })
        .Max();
}
```

**Pros:**
- ✓ More concise
- ✓ Functional style
- ✓ Clear intent (find maximum)

**Cons:**
- ✗ Slightly less readable for beginners
- ✗ May create more intermediate collections

---

## Step 15: Summary

**Part 1 Algorithm:**
```
1. Parse input → happiness dictionary + people set
2. Fix first person (avoid rotation duplicates)
3. Generate (N-1)! permutations of remaining
4. For each permutation:
   - Calculate total happiness
   - Count all neighbor pairs (both directions)
   - Include circular connection
5. Return maximum happiness
```

**Part 2 Algorithm:**
```
1. Same as Part 1
2. Add "Me" with all neutral (0) relationships
3. Now have N! permutations to check
4. Find maximum (usually lower than Part 1)
```

**Key Points:**
- ✅ Circular table → wrap around connection
- ✅ Bidirectional happiness → count both ways
- ✅ Fix first person → avoid rotation duplicates
- ✅ Brute force works → small N (< 13 people)
- ✅ NP-hard problem → no fast algorithm known

**Memory Aid: "CBPN"**
```
C - Circular (don't forget wrap-around!)
B - Bidirectional (both directions count!)
P - Permutations (try all arrangements)
N - Negate "lose" values
```

---

**Happy dinner planning! 🍽️🎄**
