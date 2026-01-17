# Day 15: Science for Hungry People - Step by Step Solution

## Step 1: Understanding the Problem

Today, we're perfecting a milk-dunking cookie recipe by finding the right balance of ingredients.

**Part 1:** Find the highest-scoring cookie recipe using exactly 100 teaspoons of ingredients.

**Part 2:** Find the highest-scoring recipe with exactly 500 calories.

### Input Format:
```
Name: capacity X, durability Y, flavor Z, texture W, calories C
```

Example:
```
Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
```

**Key Points:**
- Exactly **100 teaspoons** total across all ingredients
- Each ingredient has 5 properties: capacity, durability, flavor, texture, calories
- Score = capacity × durability × flavor × texture (negative values become 0)
- **Calories are NOT included in score calculation**
- Must use whole teaspoons (no fractions)

---

## Step 2: Analyzing the Example

### Example Input (2 ingredients):
```
Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
```

### Testing: 44 Butterscotch + 56 Cinnamon

**Total Check:**
```
44 + 56 = 100 ✓
```

**Capacity:**
```
44 × (-1) + 56 × 2 = -44 + 112 = 68
```

**Durability:**
```
44 × (-2) + 56 × 3 = -88 + 168 = 80
```

**Flavor:**
```
44 × 6 + 56 × (-2) = 264 - 112 = 152
```

**Texture:**
```
44 × 3 + 56 × (-1) = 132 - 56 = 76
```

**Calories (for Part 2):**
```
44 × 8 + 56 × 3 = 352 + 168 = 520
```

**Score Calculation:**
```
Score = capacity × durability × flavor × texture
      = 68 × 80 × 152 × 76
      = 62,842,880
```

**Note:** This happens to be the optimal score for this example!

---

## Step 3: Handling Negative Properties

### Important Rule: Negative totals become 0

**Example: 10 Butterscotch + 90 Cinnamon**

```
Capacity: 10×(-1) + 90×2 = -10 + 180 = 170
Durability: 10×(-2) + 90×3 = -20 + 270 = 250
Flavor: 10×6 + 90×(-2) = 60 - 180 = -120 → becomes 0
Texture: 10×3 + 90×(-1) = 30 - 90 = -60 → becomes 0

Score = 170 × 250 × 0 × 0 = 0
```

**Key Insight:** Any negative property makes the entire score 0 (multiplication by zero).

---

## Step 4: Data Structure Design

### Ingredient Class:

```csharp
public class Ingredient
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int Durability { get; set; }
    public int Flavor { get; set; }
    public int Texture { get; set; }
    public int Calories { get; set; }
}
```

### Recipe Representation:

```csharp
// Array where index = ingredient, value = teaspoons
int[] recipe = new int[ingredients.Count];

// Example: [44, 56] means:
// - 44 teaspoons of ingredient 0 (Butterscotch)
// - 56 teaspoons of ingredient 1 (Cinnamon)
```

---

## Step 5: Parsing Input

### Parsing Strategy:

Each line format: `Name: capacity X, durability Y, flavor Z, texture W, calories C`

```csharp
public static List<Ingredient> ParseInput(string[] lines)
{
    var ingredients = new List<Ingredient>();
    
    foreach (string line in lines)
    {
        // Split by colon and comma
        var parts = line.Split(new[] { ':', ',', ' ' }, 
                               StringSplitOptions.RemoveEmptyEntries);
        
        // Extract name and properties
        var ingredient = new Ingredient
        {
            Name = parts[0],
            Capacity = int.Parse(parts[2]),
            Durability = int.Parse(parts[4]),
            Flavor = int.Parse(parts[6]),
            Texture = int.Parse(parts[8]),
            Calories = int.Parse(parts[10])
        };
        
        ingredients.Add(ingredient);
    }
    
    return ingredients;
}
```

### Example Parsing:

**Input:** `"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8"`

```
Split by ':', ',', ' ':
["Butterscotch", "capacity", "-1", "durability", "-2", 
 "flavor", "6", "texture", "3", "calories", "8"]

Extract:
parts[0] = "Butterscotch"  → Name
parts[2] = "-1"            → Capacity
parts[4] = "-2"            → Durability
parts[6] = "6"             → Flavor
parts[8] = "3"             → Texture
parts[10] = "8"            → Calories
```

---

## Step 6: Calculating Score for a Recipe

### Algorithm:

```csharp
private static long CalculateScore(List<Ingredient> ingredients, int[] amounts)
{
    // Calculate total for each property
    int capacity = 0;
    int durability = 0;
    int flavor = 0;
    int texture = 0;
    
    for (int i = 0; i < ingredients.Count; i++)
    {
        capacity += ingredients[i].Capacity * amounts[i];
        durability += ingredients[i].Durability * amounts[i];
        flavor += ingredients[i].Flavor * amounts[i];
        texture += ingredients[i].Texture * amounts[i];
    }
    
    // Negative values become 0
    capacity = Math.Max(0, capacity);
    durability = Math.Max(0, durability);
    flavor = Math.Max(0, flavor);
    texture = Math.Max(0, texture);
    
    // Calculate score
    return (long)capacity * durability * flavor * texture;
}
```

### Example Trace: [44, 56]

```
Butterscotch: capacity=-1, durability=-2, flavor=6, texture=3
Cinnamon: capacity=2, durability=3, flavor=-2, texture=-1

Loop i=0 (Butterscotch, 44 teaspoons):
  capacity += (-1) × 44 = -44
  durability += (-2) × 44 = -88
  flavor += 6 × 44 = 264
  texture += 3 × 44 = 132

Loop i=1 (Cinnamon, 56 teaspoons):
  capacity += 2 × 56 = 112 → total: -44 + 112 = 68
  durability += 3 × 56 = 168 → total: -88 + 168 = 80
  flavor += (-2) × 56 = -112 → total: 264 - 112 = 152
  texture += (-1) × 56 = -56 → total: 132 - 56 = 76

Apply Math.Max(0, x):
  capacity = Math.Max(0, 68) = 68
  durability = Math.Max(0, 80) = 80
  flavor = Math.Max(0, 152) = 152
  texture = Math.Max(0, 76) = 76

Score = 68 × 80 × 152 × 76 = 62,842,880
```

---

## Step 7: Calculating Calories

For Part 2, we need to check if a recipe has exactly 500 calories.

```csharp
private static int CalculateCalories(List<Ingredient> ingredients, int[] amounts)
{
    int calories = 0;
    
    for (int i = 0; i < ingredients.Count; i++)
    {
        calories += ingredients[i].Calories * amounts[i];
    }
    
    return calories;
}
```

### Example: [44, 56]

```
Butterscotch: 44 × 8 = 352
Cinnamon: 56 × 3 = 168
Total: 352 + 168 = 520 calories
```

---

## Step 8: Generating All Valid Recipes

### The Challenge:

We need to find all ways to distribute 100 teaspoons across N ingredients.

**Mathematical Problem:** Find all non-negative integer solutions where:
```
amount[0] + amount[1] + ... + amount[N-1] = 100
```

### Stars and Bars Method:

This is a classic combinatorial problem solved with recursive generation.

```csharp
private static void GenerateRecipes(
    List<Ingredient> ingredients,
    int[] amounts,
    int ingredientIndex,
    int remaining,
    Action<int[]> callback)
{
    // Base case: last ingredient gets all remaining
    if (ingredientIndex == ingredients.Count - 1)
    {
        amounts[ingredientIndex] = remaining;
        callback(amounts);
        return;
    }
    
    // Try all possible amounts for current ingredient
    for (int amount = 0; amount <= remaining; amount++)
    {
        amounts[ingredientIndex] = amount;
        GenerateRecipes(ingredients, amounts, ingredientIndex + 1, 
                        remaining - amount, callback);
    }
}
```

### How It Works (2 ingredients):

```
GenerateRecipes(index=0, remaining=100)
├─ amount=0: [0, ?] → recurse(index=1, remaining=100)
│  └─ Base case: [0, 100] ✓
│
├─ amount=1: [1, ?] → recurse(index=1, remaining=99)
│  └─ Base case: [1, 99] ✓
│
├─ amount=2: [2, ?] → recurse(index=1, remaining=98)
│  └─ Base case: [2, 98] ✓
│
...
│
└─ amount=100: [100, ?] → recurse(index=1, remaining=0)
   └─ Base case: [100, 0] ✓

Total: 101 combinations (0-100 for first ingredient)
```

### For N Ingredients:

Number of combinations = C(100 + N - 1, N - 1)

**Examples:**
- 2 ingredients: C(101, 1) = 101
- 3 ingredients: C(102, 2) = 5,151
- 4 ingredients: C(103, 3) = 176,851

---

## Step 9: Part 1 Complete Solution

### Algorithm:

```csharp
public static long SolvePart1(string[] input)
{
    var ingredients = ParseInput(input);
    long maxScore = 0;
    int[] amounts = new int[ingredients.Count];
    
    // Generate all valid recipes
    GenerateRecipes(ingredients, amounts, 0, 100, recipe =>
    {
        // Calculate score for this recipe
        long score = CalculateScore(ingredients, recipe);
        
        // Track maximum
        if (score > maxScore)
        {
            maxScore = score;
        }
    });
    
    return maxScore;
}

private static void GenerateRecipes(
    List<Ingredient> ingredients,
    int[] amounts,
    int ingredientIndex,
    int remaining,
    Action<int[]> callback)
{
    if (ingredientIndex == ingredients.Count - 1)
    {
        amounts[ingredientIndex] = remaining;
        callback(amounts);
        return;
    }
    
    for (int amount = 0; amount <= remaining; amount++)
    {
        amounts[ingredientIndex] = amount;
        GenerateRecipes(ingredients, amounts, ingredientIndex + 1, 
                        remaining - amount, callback);
    }
}

private static long CalculateScore(List<Ingredient> ingredients, int[] amounts)
{
    int capacity = 0, durability = 0, flavor = 0, texture = 0;
    
    for (int i = 0; i < ingredients.Count; i++)
    {
        capacity += ingredients[i].Capacity * amounts[i];
        durability += ingredients[i].Durability * amounts[i];
        flavor += ingredients[i].Flavor * amounts[i];
        texture += ingredients[i].Texture * amounts[i];
    }
    
    capacity = Math.Max(0, capacity);
    durability = Math.Max(0, durability);
    flavor = Math.Max(0, flavor);
    texture = Math.Max(0, texture);
    
    return (long)capacity * durability * flavor * texture;
}
```

---

## Step 10: Part 1 Example Execution

### Input (2 ingredients):
```
Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
```

### Processing:

```
Try recipe [0, 100]:
  capacity = 0×(-1) + 100×2 = 200
  durability = 0×(-2) + 100×3 = 300
  flavor = 0×6 + 100×(-2) = -200 → 0
  texture = 0×3 + 100×(-1) = -100 → 0
  score = 200 × 300 × 0 × 0 = 0

Try recipe [1, 99]:
  capacity = 1×(-1) + 99×2 = 197
  durability = 1×(-2) + 99×3 = 295
  flavor = 1×6 + 99×(-2) = -192 → 0
  texture = 1×3 + 99×(-1) = -96 → 0
  score = 197 × 295 × 0 × 0 = 0

...

Try recipe [44, 56]:
  capacity = 44×(-1) + 56×2 = 68
  durability = 44×(-2) + 56×3 = 80
  flavor = 44×6 + 56×(-2) = 152
  texture = 44×3 + 56×(-1) = 76
  score = 68 × 80 × 152 × 76 = 62,842,880 ← NEW MAX!

...

Try recipe [100, 0]:
  capacity = 100×(-1) + 0×2 = -100 → 0
  durability = 100×(-2) + 0×3 = -200 → 0
  flavor = 100×6 + 0×(-2) = 600
  texture = 100×3 + 0×(-1) = 300
  score = 0 × 0 × 600 × 300 = 0

Result: maxScore = 62,842,880
```

---

## Step 11: Part 2 - Adding Calorie Constraint

### New Requirement:

Find the highest-scoring recipe with **exactly 500 calories**.

### Modified Algorithm:

```csharp
public static long SolvePart2(string[] input)
{
    var ingredients = ParseInput(input);
    long maxScore = 0;
    int[] amounts = new int[ingredients.Count];
    
    // Generate all valid recipes
    GenerateRecipes(ingredients, amounts, 0, 100, recipe =>
    {
        // Check calorie constraint
        int calories = CalculateCalories(ingredients, recipe);
        if (calories != 500)
        {
            return; // Skip this recipe
        }
        
        // Calculate score for this recipe
        long score = CalculateScore(ingredients, recipe);
        
        // Track maximum
        if (score > maxScore)
        {
            maxScore = score;
        }
    });
    
    return maxScore;
}

private static int CalculateCalories(List<Ingredient> ingredients, int[] amounts)
{
    int calories = 0;
    for (int i = 0; i < ingredients.Count; i++)
    {
        calories += ingredients[i].Calories * amounts[i];
    }
    return calories;
}
```

---

## Step 12: Part 2 Example Trace

### Recipe [44, 56]:

```
Check calories:
  44 × 8 + 56 × 3 = 352 + 168 = 520
  520 ≠ 500 → SKIP

This recipe is not valid for Part 2!
```

### Finding Valid Recipe:

We need to find amounts where:
```
Butterscotch × 8 + Cinnamon × 3 = 500
AND
Butterscotch + Cinnamon = 100
```

**Solving:**
```
Let B = Butterscotch, C = Cinnamon
B + C = 100 → C = 100 - B
8B + 3C = 500
8B + 3(100 - B) = 500
8B + 300 - 3B = 500
5B = 200
B = 40

Therefore: B = 40, C = 60
```

### Recipe [40, 60]:

```
Calories check:
  40 × 8 + 60 × 3 = 320 + 180 = 500 ✓

Calculate score:
  capacity = 40×(-1) + 60×2 = -40 + 120 = 80
  durability = 40×(-2) + 60×3 = -80 + 180 = 100
  flavor = 40×6 + 60×(-2) = 240 - 120 = 120
  texture = 40×3 + 60×(-1) = 120 - 60 = 60
  
  score = 80 × 100 × 120 × 60 = 57,600,000

This is the best 500-calorie recipe!
```

---

## Step 13: Optimization Considerations

### Time Complexity:

**Recipe Generation:**
```
O(C(100 + N - 1, N - 1)) where N = number of ingredients

For N=4: C(103, 3) = 176,851 recipes
```

**Score Calculation per Recipe:**
```
O(N) - linear in number of ingredients
```

**Total:**
```
O(N × C(100 + N - 1, N - 1))

For 4 ingredients: ~700,000 operations
Very manageable!
```

### Space Complexity:

```
O(N) for recursion depth and arrays
```

### Why This Approach Works:

- Brute force is feasible (< 200k combinations for 4 ingredients)
- No need for complex optimization
- Straightforward and correct

---

## Step 14: Complete Implementation

```csharp
using System;
using System.Collections.Generic;

namespace AdventOfCode2015.ResolvingDays;

public static class Day15
{
    private class Ingredient
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Durability { get; set; }
        public int Flavor { get; set; }
        public int Texture { get; set; }
        public int Calories { get; set; }
    }
    
    // Part 1: Find highest-scoring recipe
    public static long SolvePart1(string[] input)
    {
        var ingredients = ParseInput(input);
        long maxScore = 0;
        int[] amounts = new int[ingredients.Count];
        
        GenerateRecipes(ingredients, amounts, 0, 100, recipe =>
        {
            long score = CalculateScore(ingredients, recipe);
            if (score > maxScore)
            {
                maxScore = score;
            }
        });
        
        return maxScore;
    }
    
    // Part 2: Find highest-scoring recipe with exactly 500 calories
    public static long SolvePart2(string[] input)
    {
        var ingredients = ParseInput(input);
        long maxScore = 0;
        int[] amounts = new int[ingredients.Count];
        
        GenerateRecipes(ingredients, amounts, 0, 100, recipe =>
        {
            int calories = CalculateCalories(ingredients, recipe);
            if (calories != 500)
            {
                return;
            }
            
            long score = CalculateScore(ingredients, recipe);
            if (score > maxScore)
            {
                maxScore = score;
            }
        });
        
        return maxScore;
    }
    
    // Parse input lines into ingredient list
    private static List<Ingredient> ParseInput(string[] lines)
    {
        var ingredients = new List<Ingredient>();
        
        foreach (string line in lines)
        {
            var parts = line.Split(new[] { ':', ',', ' ' }, 
                                   StringSplitOptions.RemoveEmptyEntries);
            
            ingredients.Add(new Ingredient
            {
                Name = parts[0],
                Capacity = int.Parse(parts[2]),
                Durability = int.Parse(parts[4]),
                Flavor = int.Parse(parts[6]),
                Texture = int.Parse(parts[8]),
                Calories = int.Parse(parts[10])
            });
        }
        
        return ingredients;
    }
    
    // Generate all valid recipes (amounts sum to 100)
    private static void GenerateRecipes(
        List<Ingredient> ingredients,
        int[] amounts,
        int ingredientIndex,
        int remaining,
        Action<int[]> callback)
    {
        // Last ingredient gets all remaining
        if (ingredientIndex == ingredients.Count - 1)
        {
            amounts[ingredientIndex] = remaining;
            callback(amounts);
            return;
        }
        
        // Try all amounts for current ingredient
        for (int amount = 0; amount <= remaining; amount++)
        {
            amounts[ingredientIndex] = amount;
            GenerateRecipes(ingredients, amounts, ingredientIndex + 1, 
                            remaining - amount, callback);
        }
    }
    
    // Calculate score for a recipe
    private static long CalculateScore(List<Ingredient> ingredients, int[] amounts)
    {
        int capacity = 0, durability = 0, flavor = 0, texture = 0;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            capacity += ingredients[i].Capacity * amounts[i];
            durability += ingredients[i].Durability * amounts[i];
            flavor += ingredients[i].Flavor * amounts[i];
            texture += ingredients[i].Texture * amounts[i];
        }
        
        // Negative values become 0
        capacity = Math.Max(0, capacity);
        durability = Math.Max(0, durability);
        flavor = Math.Max(0, flavor);
        texture = Math.Max(0, texture);
        
        return (long)capacity * durability * flavor * texture;
    }
    
    // Calculate calories for a recipe
    private static int CalculateCalories(List<Ingredient> ingredients, int[] amounts)
    {
        int calories = 0;
        for (int i = 0; i < ingredients.Count; i++)
        {
            calories += ingredients[i].Calories * amounts[i];
        }
        return calories;
    }
}
```

---

## Step 15: Common Mistakes to Avoid

### Mistake 1: Including Calories in Score
```csharp
// WRONG - Calories are not part of score
score = capacity × durability × flavor × texture × calories;

// CORRECT - Only 4 properties
score = capacity × durability × flavor × texture;
```

### Mistake 2: Not Handling Negatives
```csharp
// WRONG - Allows negative totals
int capacity = CalculateProperty(...);
score = capacity × durability × flavor × texture;

// CORRECT - Clamp to 0
capacity = Math.Max(0, capacity);
```

### Mistake 3: Integer Overflow
```csharp
// WRONG - May overflow for large values
int score = capacity * durability * flavor * texture;

// CORRECT - Use long for score
long score = (long)capacity * durability * flavor * texture;
```

### Mistake 4: Not Summing to 100
```csharp
// WRONG - Allows any total
for (int i = 0; i <= 100; i++)
    for (int j = 0; j <= 100; j++)
        // Not guaranteed to sum to 100!

// CORRECT - Use recursive generation
GenerateRecipes(...); // Ensures sum = 100
```

### Mistake 5: Part 2 Comparison
```csharp
// WRONG - Looking for any 500+ calories
if (calories >= 500)

// CORRECT - Exactly 500 calories
if (calories == 500)
```

---

## Step 16: Testing Strategy

### Test Case 1: Example from Problem
```
Input: Butterscotch and Cinnamon
Part 1 Expected: 62,842,880
Part 2 Expected: 57,600,000 (with 500 calories)
```

### Test Case 2: Single Ingredient
```
Input: One ingredient with all positive properties
Expected: 100^4 × (product of properties)
Verify: No distribution needed
```

### Test Case 3: All Negative Properties
```
Input: Ingredients with all negative values
Expected: 0 (all properties become 0)
```

### Test Case 4: Zero Properties
```
Input: Ingredient with 0 in one property
Expected: 0 (multiplication by zero)
```

---

## Step 17: Summary

**Part 1 Algorithm:**
```
1. Parse ingredients (5 properties each)
2. Generate all recipes summing to 100
3. For each recipe:
   - Calculate property totals
   - Clamp negatives to 0
   - Multiply: capacity × durability × flavor × texture
4. Return maximum score
```

**Part 2 Algorithm:**
```
1. Same as Part 1
2. Add filter: only consider recipes with 500 calories
3. Return maximum score among valid recipes
```

**Key Points:**
- ✅ Exactly 100 teaspoons total
- ✅ 5 properties per ingredient
- ✅ Score uses 4 properties (not calories)
- ✅ Negative totals become 0
- ✅ Brute force works (< 200k combinations)
- ✅ Use long for score (prevent overflow)

**Complexity:**
- ⏱️ Time: O(N × C(100 + N - 1, N - 1))
- 💾 Space: O(N)

**Memory Aid: "RECIPE"**
```
R - Recipe must sum to 100
E - Exactly 500 calories (Part 2)
C - Clamp negatives to zero
I - Ingredients have 5 properties
P - Product of 4 properties (not calories)
E - Enumerate all combinations
```

---

**Happy cookie baking! 🍪🎄**
