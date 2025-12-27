# Day 10: Elves Look, Elves Say - Step by Step Solution

## Step 1: Understanding the Problem

Generate look-and-say sequences (also known as run-length encoding).

**Rule:** Look at the sequence and say what you see by counting consecutive digits.

**Part 1:** Apply the process 40 times, return final length  
**Part 2:** Apply the process 50 times, return final length

---

## Step 2: Understanding Look-and-Say

### Example: `1`
```
Look: "one 1"
Say:  "11"
```

### Example: `11`
```
Look: "two 1s"
Say:  "21"
```

### Example: `21`
```
Look: "one 2, then one 1"
Say:  "1211"
```

### Example: `1211`
```
Look: "one 1, one 2, two 1s"
Say:  "111221"
```

### Example: `111221`
```
Look: "three 1s, two 2s, one 1"
Say:  "312211"
```

---

## Step 3: Algorithm

```
1. Group consecutive identical digits
2. For each group:
   - Count how many
   - What digit it is
   - Append "count" + "digit" to result
3. Repeat n times
```

---

## Step 4: Implementation

```csharp
string LookAndSay(string input)
{
    StringBuilder result = new StringBuilder();
    int i = 0;
    
    while (i < input.Length)
    {
        char currentDigit = input[i];
        int count = 1;
        
        // Count consecutive identical digits
        while (i + count < input.Length && 
               input[i + count] == currentDigit)
        {
            count++;
        }
        
        // Append count and digit
        result.Append(count);
        result.Append(currentDigit);
        
        // Move to next group
        i += count;
    }
    
    return result.ToString();
}
```

---

## Step 5: Trace Example

### Input: `"1211"`
```
Position: 0 1 2 3
Input:    1 2 1 1

i=0:
  currentDigit = '1'
  count = 1 (only one '1')
  result = "11"
  i = 0 + 1 = 1

i=1:
  currentDigit = '2'
  count = 1 (only one '2')
  result = "1112"
  i = 1 + 1 = 2

i=2:
  currentDigit = '1'
  count = 1, 2 (two '1's at positions 2 and 3)
  result = "111221"
  i = 2 + 2 = 4

i=4: Exit (i >= input.Length)

Result: "111221" ✓
```

---

## Step 6: Apply Multiple Times

```csharp
string ApplyLookAndSay(string input, int iterations)
{
    string current = input;
    
    for (int i = 0; i < iterations; i++)
    {
        current = LookAndSay(current);
    }
    
    return current;
}
```

### Example: Starting with "1", apply 5 times
```
Start:      1
Iteration 1: 11        (one 1)
Iteration 2: 21        (two 1s)
Iteration 3: 1211      (one 2, one 1)
Iteration 4: 111221    (one 1, one 2, two 1s)
Iteration 5: 312211    (three 1s, two 2s, one 1)
```

---

## Step 7: Complete Implementation

```csharp
public static class Day10
{
    public static string SolvePart1(string input)
    {
        string result = ApplyLookAndSay(input.Trim(), 40);
        return result.Length.ToString();
    }

    public static string SolvePart2(string input)
    {
        string result = ApplyLookAndSay(input.Trim(), 50);
        return result.Length.ToString();
    }

    private static string ApplyLookAndSay(string input, int iterations)
    {
        string current = input;
        
        for (int i = 0; i < iterations; i++)
        {
            current = LookAndSay(current);
        }
        
        return current;
    }

    private static string LookAndSay(string input)
    {
        StringBuilder result = new StringBuilder();
        int i = 0;
        
        while (i < input.Length)
        {
            char currentDigit = input[i];
            int count = 1;
            
            // Count consecutive identical digits
            while (i + count < input.Length && 
                   input[i + count] == currentDigit)
            {
                count++;
            }
            
            // Append count and digit
            result.Append(count);
            result.Append(currentDigit);
            
            // Move to next group
            i += count;
        }
        
        return result.ToString();
    }
}
```

---

## Step 8: Growth Analysis

### Conway's Constant
The sequence grows by approximately **1.303577** each iteration.

```
Iteration 0: length = L
Iteration 1: length ≈ 1.3 × L
Iteration 2: length ≈ 1.3² × L
Iteration n: length ≈ 1.3ⁿ × L
```

### Example Growth:
```
Starting with "1111":

Iteration 0: 1111                  (4 chars)
Iteration 1: 41                    (2 chars)
Iteration 2: 1411                  (4 chars)
Iteration 3: 111411                (6 chars)
Iteration 4: 31141131              (8 chars)
Iteration 5: 13211411131131        (14 chars)
...
Iteration 40: ~360,000 characters
Iteration 50: ~4,900,000 characters
```

---

## Step 9: Optimization - StringBuilder

### Why StringBuilder?
```csharp
// INEFFICIENT - Creates new string each time
string result = "";
result += count.ToString();
result += digit.ToString();

// EFFICIENT - Builds string without reallocations
StringBuilder result = new StringBuilder();
result.Append(count);
result.Append(digit);
```

### Performance Difference:
- String concatenation: O(n²) due to immutability
- StringBuilder: O(n) amortized

---

## Step 10: Common Mistakes

### Mistake 1: Not Grouping Consecutive Digits
```csharp
// WRONG - Treats each digit separately
foreach (char c in input)
{
    result.Append("1");
    result.Append(c);
}

// CORRECT - Groups consecutive identical digits
while (i < input.Length)
{
    // Count consecutive digits
    while (i + count < input.Length && 
           input[i + count] == currentDigit)
        count++;
    // Append count + digit
}
```

### Mistake 2: Not Moving Index Correctly
```csharp
// WRONG - Only moves by 1
i++;

// CORRECT - Moves by count
i += count;
```

---

## Step 11: Edge Cases

### Single Digit
```
Input: "5"
Output: "15" (one 5)
```

### All Same Digits
```
Input: "1111"
Output: "41" (four 1s)
```

### Alternating Digits
```
Input: "1212"
Output: "11121112" (one 1, one 2, one 1, one 2)
```

---

## Step 12: Testing Examples

```csharp
// Test single transformation
LookAndSay("1") → "11" ✓
LookAndSay("11") → "21" ✓
LookAndSay("21") → "1211" ✓
LookAndSay("1211") → "111221" ✓
LookAndSay("111221") → "312211" ✓

// Test multiple iterations
ApplyLookAndSay("1", 5) → "312211" ✓
```

---

## Step 13: Complexity Analysis

### Time Complexity:
- Per iteration: O(n) where n = current length
- Total for k iterations: O(L × 1.3^k) where L = initial length

### Space Complexity:
- O(n) for StringBuilder
- Final result: O(L × 1.3^k)

---

## Step 14: Interesting Properties

### No digits > 3 appear
Starting from digits 1-3, only digits 1, 2, and 3 ever appear:
- Groups of 3 or fewer → "11", "21", "31"
- Never need to represent > 3

### Sequence never stabilizes
Unlike some sequences, this grows forever.

### Conway's Cosmological Theorem
For most starting sequences, after enough iterations, the sequence can be broken into 92 distinct "elements" that never interact.

---

## Step 15: Summary

**Key Concepts:**
- ✅ Run-length encoding
- ✅ Consecutive grouping
- ✅ StringBuilder optimization
- ✅ Exponential growth
- ✅ Iterative transformation

**Algorithm:**
```
1. Group consecutive identical digits
2. Count each group
3. Output "count + digit"
4. Repeat n times
```

**Happy looking and saying! 🎄🔢**
