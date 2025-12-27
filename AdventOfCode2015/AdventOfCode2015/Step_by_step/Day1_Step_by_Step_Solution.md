# Day 1: Not Quite Lisp - Step by Step Solution

## Step 1: Understanding the Problem

Santa needs to navigate floors in an apartment building using parentheses:
- `(` means go UP one floor
- `)` means go DOWN one floor
- Start at floor 0 (ground floor)

**Part 1:** Find the final floor after processing all instructions  
**Part 2:** Find the position (1-based) when Santa first enters the basement (floor -1)

### Examples:
```
"(())" → floor 0    (up, up, down, down)
"(((" → floor 3     (up, up, up)
"))(" → floor -1    (down, down, up)
```

---

## Step 2: Breaking Down the Problem

### Part 1: Simple Counter
- Initialize floor counter to 0
- For each character:
  - If `(` → add 1
  - If `)` → subtract 1
- Return final floor

### Part 2: Early Exit Pattern
- Initialize floor counter to 0
- For each character (track position starting from 1):
  - Update floor based on character
  - If floor becomes -1 → return current position
- Return -1 if never enters basement

---

## Step 3: Implementing Part 1 - Final Floor

```csharp
int CalculateFinalFloor(string instructions)
{
    int floor = 0;
    
    foreach (char c in instructions)
    {
        if (c == '(')
        {
            floor++;
        }
        else if (c == ')')
        {
            floor--;
        }
    }
    
    return floor;
}
```

### Trace Example: `"(())"`
```
Position:  1   2   3   4
Character: (   (   )   )
Floor:     1   2   1   0

Initial: floor = 0

Step 1: c = '(' → floor = 0 + 1 = 1
Step 2: c = '(' → floor = 1 + 1 = 2
Step 3: c = ')' → floor = 2 - 1 = 1
Step 4: c = ')' → floor = 1 - 1 = 0

Final floor: 0 ✓
```

### Trace Example: `"((("`
```
Position:  1   2   3
Character: (   (   (
Floor:     1   2   3

Step 1: c = '(' → floor = 0 + 1 = 1
Step 2: c = '(' → floor = 1 + 1 = 2
Step 3: c = '(' → floor = 2 + 1 = 3

Final floor: 3 ✓
```

---

## Step 4: Optimizing Part 1

### Using LINQ (C#):
```csharp
int CalculateFinalFloor(string instructions)
{
    return instructions.Count(c => c == '(') - instructions.Count(c => c == ')');
}
```

**How it works:**
- Count all `(` characters (upward moves)
- Count all `)` characters (downward moves)
- Subtract downs from ups = final floor

### Example: `"(())"`
```
Up count:   Count('(') = 2
Down count: Count(')') = 2
Result:     2 - 2 = 0 ✓
```

### Example: `"((("`
```
Up count:   Count('(') = 3
Down count: Count(')') = 0
Result:     3 - 0 = 3 ✓
```

---

## Step 5: Implementing Part 2 - First Basement Entry

```csharp
int FindFirstBasementPosition(string instructions)
{
    int floor = 0;
    
    for (int i = 0; i < instructions.Length; i++)
    {
        if (instructions[i] == '(')
        {
            floor++;
        }
        else if (instructions[i] == ')')
        {
            floor--;
        }
        
        // Check if we entered basement (floor -1)
        if (floor == -1)
        {
            return i + 1; // Return 1-based position
        }
    }
    
    return -1; // Never entered basement
}
```

### Important: 1-Based Indexing
- Array index starts at 0
- Problem asks for position starting at 1
- Return `i + 1` to convert to 1-based position

---

## Step 6: Trace Example for Part 2

### Example: `")"`
```
Position (1-based): 1
Index (0-based):    0
Character:          )
Floor:             -1 ← Basement!

Initial: floor = 0

Step 1 (i=0):
  - Character: ')'
  - floor = 0 - 1 = -1
  - floor == -1? YES!
  - Return i + 1 = 0 + 1 = 1 ✓

Answer: Position 1
```

### Example: `"()())"`
```
Position:  1   2   3   4   5
Index:     0   1   2   3   4
Character: (   )   (   )   )
Floor:     1   0   1   0  -1 ← Basement!

Initial: floor = 0

Step 1 (i=0): c='(' → floor = 1 → Check: 1 ≠ -1, continue
Step 2 (i=1): c=')' → floor = 0 → Check: 0 ≠ -1, continue
Step 3 (i=2): c='(' → floor = 1 → Check: 1 ≠ -1, continue
Step 4 (i=3): c=')' → floor = 0 → Check: 0 ≠ -1, continue
Step 5 (i=4): c=')' → floor = -1 → Check: -1 == -1, YES!
                      Return i + 1 = 4 + 1 = 5 ✓

Answer: Position 5
```

---

## Step 7: Complete Implementation

```csharp
public static class Day1
{
    public static string SolvePart1(string input)
    {
        int floor = CalculateFinalFloor(input.Trim());
        return floor.ToString();
    }

    public static string SolvePart2(string input)
    {
        int position = FindFirstBasementPosition(input.Trim());
        return position.ToString();
    }

    private static int CalculateFinalFloor(string instructions)
    {
        int floor = 0;
        
        foreach (char c in instructions)
        {
            if (c == '(')
                floor++;
            else if (c == ')')
                floor--;
        }
        
        return floor;
    }
    
    // Alternative optimized version for Part 1
    private static int CalculateFinalFloorOptimized(string instructions)
    {
        return instructions.Count(c => c == '(') - 
               instructions.Count(c => c == ')');
    }

    private static int FindFirstBasementPosition(string instructions)
    {
        int floor = 0;
        
        for (int i = 0; i < instructions.Length; i++)
        {
            if (instructions[i] == '(')
                floor++;
            else if (instructions[i] == ')')
                floor--;
            
            if (floor == -1)
                return i + 1; // 1-based position
        }
        
        return -1; // Never enters basement
    }
}
```

---

## Step 8: Testing with All Examples

### Part 1 Tests:
```csharp
// Test: "(())"
CalculateFinalFloor("(())") → 0 ✓

// Test: "()()"
CalculateFinalFloor("()()") → 0 ✓

// Test: "((("
CalculateFinalFloor("(((") → 3 ✓

// Test: "(()(()("
CalculateFinalFloor("(()(()(") → 3 ✓

// Test: "))((((("
CalculateFinalFloor("))(((((" ) → 3 ✓

// Test: "())"
CalculateFinalFloor("())") → -1 ✓

// Test: "))("
CalculateFinalFloor("))(" ) → -1 ✓

// Test: ")))"
CalculateFinalFloor(")))") → -3 ✓

// Test: ")())())"
CalculateFinalFloor(")())())") → -3 ✓
```

### Part 2 Tests:
```csharp
// Test: ")"
FindFirstBasementPosition(")") → 1 ✓

// Test: "()())"
FindFirstBasementPosition("()())") → 5 ✓
```

---

## Step 9: Edge Cases to Consider

### Part 1:
1. **Empty string** → floor 0
2. **All up** → positive floor
3. **All down** → negative floor
4. **Balanced** → floor 0
5. **Single character** → floor 1 or -1

### Part 2:
1. **First character is `)`** → return 1
2. **Never enters basement** → return -1
3. **Multiple basement entries** → return first only
4. **Stays in basement** → return first entry point

---

## Step 10: Complexity Analysis

### Part 1:
**Time Complexity:** O(n) where n = length of input
- Must process each character once

**Space Complexity:** O(1)
- Only need one integer variable

### Part 2:
**Time Complexity:** O(n) worst case
- May need to check all characters
- Best case: O(1) if first character is `)`

**Space Complexity:** O(1)
- Only need floor counter and position tracker

---

## Step 11: Common Mistakes to Avoid

### Mistake 1: Wrong Indexing
```csharp
// WRONG - Returns 0-based index
if (floor == -1)
    return i;

// CORRECT - Returns 1-based position
if (floor == -1)
    return i + 1;
```

### Mistake 2: Not Handling "Never Enters Basement"
```csharp
// WRONG - May cause issues if basement never entered
private static int FindFirstBasementPosition(string instructions)
{
    int floor = 0;
    for (int i = 0; i < instructions.Length; i++)
    {
        // ... update floor ...
        if (floor == -1)
            return i + 1;
    }
    // Missing: return statement for "never enters"
}

// CORRECT - Returns -1 if never enters
private static int FindFirstBasementPosition(string instructions)
{
    // ... same code ...
    return -1; // Explicitly handle "never enters"
}
```

### Mistake 3: Forgetting to Trim Input
```csharp
// WRONG - May have trailing whitespace
SolvePart1(input) → May include newlines/spaces

// CORRECT - Trim first
SolvePart1(input.Trim()) → Clean input
```

---

## Step 12: Visual Diagram

### Floor Navigation Example: `"(())"`
```
Start:   Floor 0
           |
         ( | Go UP
           |
         Floor 1
           |
         ( | Go UP
           |
         Floor 2
           |
         ) | Go DOWN
           |
         Floor 1
           |
         ) | Go DOWN
           |
End:     Floor 0
```

### Basement Entry Example: `"()())"`
```
Position: 1   2   3   4   5
Char:     (   )   (   )   )
          
Floor:    1 → 0 → 1 → 0 → -1 ← BASEMENT!
                             ↑
                         Position 5
```

---

## Step 13: Performance Optimization Tips

### For Part 1 - Use LINQ:
```csharp
// Simple and readable
int floor = instructions.Count(c => c == '(') - 
            instructions.Count(c => c == ')');
```

### For Part 2 - Early Exit:
```csharp
// Stop as soon as basement is found
if (floor == -1)
    return i + 1; // No need to continue
```

### For Both - String vs Char Array:
```csharp
// String indexing is fine for this problem
foreach (char c in instructions) // Efficient

// No need to convert to char array
// char[] chars = instructions.ToCharArray(); // Unnecessary
```

---

## Step 14: Summary

**Part 1 - Final Floor:**
- Count `(` and `)` characters
- Subtract downs from ups
- Result is final floor

**Part 2 - First Basement:**
- Process characters sequentially
- Track current floor
- Return position (1-based) when floor reaches -1

**Key Concepts:**
- ✅ Sequential processing
- ✅ Simple counter pattern
- ✅ Early exit optimization
- ✅ 0-based vs 1-based indexing
- ✅ Edge case handling

**Algorithm Pattern:**
```
Part 1: Aggregation (sum all movements)
Part 2: Search with early exit (find first occurrence)
```

---

**Happy coding! 🎄**
