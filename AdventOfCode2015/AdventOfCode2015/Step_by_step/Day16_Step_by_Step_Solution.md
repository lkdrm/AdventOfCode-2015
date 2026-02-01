# Day 16: Aunt Sue - Step by Step Solution

## Step 1: Understanding the Problem

You received a wonderful gift from one of your 500 Aunts named "Sue". To send a thank you card to the right person, you need to identify which Aunt Sue sent it based on the analysis from your "My First Crime Scene Analysis Machine" (MFCSAM).

**Part 1:** Find the Sue whose remembered attributes match the MFCSAM output exactly.

**Part 2:** Some attributes use range comparisons instead of exact matches.

### MFCSAM Output (Target Profile):
```
children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1
```

### Input Format:
```
Sue N: attribute1: value1, attribute2: value2, attribute3: value3
```

Example:
```
Sue 1: goldfish: 9, cars: 0, samoyeds: 9
Sue 2: perfumes: 5, trees: 8, goldfish: 8
Sue 3: pomeranians: 4, goldfish: 10, trees: 2
```

**Key Points:**
- Each Sue has a unique number (1-500)
- Each Sue lists exactly **3 attributes** (out of 10 possible)
- **Missing attributes ≠ zero** - they're simply unknown
- We only check attributes that are remembered
- Only one Sue will match all criteria

---

## Step 2: Analyzing the Example

### MFCSAM Target Profile:
```
children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1
```

### Testing Candidate: Sue 213

**Her Attributes:**
```
Sue 213: goldfish: 5, trees: 3, cars: 2
```

**Part 1 Matching (Exact):**
```
Attribute 1: goldfish
  Sue has: 5
  Target:  5
  Match? 5 == 5 → YES ✓

Attribute 2: trees
  Sue has: 3
  Target:  3
  Match? 3 == 3 → YES ✓

Attribute 3: cars
  Sue has: 2
  Target:  2
  Match? 2 == 2 → YES ✓

Attributes NOT listed (unknown):
  children, cats, samoyeds, pomeranians, akitas, vizslas, perfumes
  → We don't check these (unknown is OK)

Result: ALL remembered attributes match → Sue 213 is the answer!
```

---

## Step 3: Understanding Part 2 Rules

### The MFCSAM Retroencabulator Revelation

Part 2 reveals that the MFCSAM uses different comparison modes:

**Exact Match (==):**
- children
- samoyeds
- akitas
- vizslas
- cars
- perfumes

**Greater Than (>):**
- cats: Sue must have MORE than the reading
- trees: Sue must have MORE than the reading

**Fewer Than (<):**
- pomeranians: Sue must have FEWER than the reading
- goldfish: Sue must have FEWER than the reading

### Why Part 1 Answer Changes

**Sue 213 in Part 1:**
```
goldfish: 5, trees: 3, cars: 2
All exact matches → VALID ✓
```

**Sue 213 in Part 2:**
```
Check goldfish: Sue has 5, Target is 5
  Rule: Sue must have < 5
  Is 5 < 5? NO ✗
  
INVALID! Sue 213 is not the answer for Part 2.
```

---

## Step 4: Data Structure Design

### Target Profile Storage:

```csharp
// Dictionary to store MFCSAM output
Dictionary<string, int> targetProfile = new()
{
    ["children"] = 3,
    ["cats"] = 7,
    ["samoyeds"] = 2,
    ["pomeranians"] = 3,
    ["akitas"] = 0,
    ["vizslas"] = 0,
    ["goldfish"] = 5,
    ["trees"] = 3,
    ["cars"] = 2,
    ["perfumes"] = 1
};
```

### Aunt Sue Representation:

```csharp
public class AuntSue
{
    public int Number { get; set; }
    public Dictionary<string, int> Attributes { get; set; } = new();
}

// Example:
AuntSue sue213 = new()
{
    Number = 213,
    Attributes = new()
    {
        ["goldfish"] = 5,
        ["trees"] = 3,
        ["cars"] = 2
    }
};
```

---

## Step 5: Parsing Input

### Parsing MFCSAM Output (Target Profile):

The MFCSAM output is typically hardcoded since it's the same for everyone:

```csharp
private static Dictionary<string, int> GetTargetProfile()
{
    return new Dictionary<string, int>
    {
        ["children"] = 3,
        ["cats"] = 7,
        ["samoyeds"] = 2,
        ["pomeranians"] = 3,
        ["akitas"] = 0,
        ["vizslas"] = 0,
        ["goldfish"] = 5,
        ["trees"] = 3,
        ["cars"] = 2,
        ["perfumes"] = 1
    };
}
```

### Parsing Aunt Sue Records:

```csharp
private static List<AuntSue> ParseInput(string[] lines)
{
    var sues = new List<AuntSue>();
    
    foreach (string line in lines)
    {
        // Example: "Sue 213: goldfish: 5, trees: 3, cars: 2"
        
        // Extract Sue number
        int colonIndex = line.IndexOf(':');
        int sueNumber = int.Parse(line.Substring(4, colonIndex - 4));
        
        // Parse attributes
        var attributes = new Dictionary<string, int>();
        string attributePart = line.Substring(colonIndex + 2);
        string[] pairs = attributePart.Split(", ");
        
        foreach (string pair in pairs)
        {
            string[] parts = pair.Split(": ");
            string attribute = parts[0];
            int value = int.Parse(parts[1]);
            attributes[attribute] = value;
        }
        
        sues.Add(new AuntSue
        {
            Number = sueNumber,
            Attributes = attributes
        });
    }
    
    return sues;
}
```

### Example Parsing Trace:

**Input:** `"Sue 213: goldfish: 5, trees: 3, cars: 2"`

```
Step 1: Extract Sue number
  Find colon at index 7
  Substring(4, 3) = "213"
  sueNumber = 213

Step 2: Extract attribute part
  Substring(9) = "goldfish: 5, trees: 3, cars: 2"

Step 3: Split by ", "
  ["goldfish: 5", "trees: 3", "cars: 2"]

Step 4: Parse each pair
  "goldfish: 5" → attribute="goldfish", value=5
  "trees: 3"    → attribute="trees", value=3
  "cars: 2"     → attribute="cars", value=2

Result:
  AuntSue {
    Number = 213,
    Attributes = {
      "goldfish": 5,
      "trees": 3,
      "cars": 2
    }
  }
```

---

## Step 6: Part 1 Matching Logic

### Algorithm:

```csharp
private static bool MatchesPart1(AuntSue sue, Dictionary<string, int> target)
{
    // Check each attribute Sue remembers
    foreach (var (attribute, value) in sue.Attributes)
    {
        // Must match exactly
        if (target[attribute] != value)
        {
            return false; // Mismatch found
        }
    }
    
    // All remembered attributes match
    return true;
}
```

### Example Trace: Sue 213

```
Target Profile:
  goldfish: 5, trees: 3, cars: 2, ...

Sue 213:
  goldfish: 5, trees: 3, cars: 2

Iteration 1: attribute="goldfish", value=5
  Check: target["goldfish"] == 5?
  5 == 5? YES → Continue

Iteration 2: attribute="trees", value=3
  Check: target["trees"] == 3?
  3 == 3? YES → Continue

Iteration 3: attribute="cars", value=2
  Check: target["cars"] == 2?
  2 == 2? YES → Continue

All checks passed → return true
```

---

## Step 7: Part 1 Complete Solution

```csharp
public static int SolvePart1(string[] input)
{
    var target = GetTargetProfile();
    var sues = ParseInput(input);
    
    foreach (var sue in sues)
    {
        if (MatchesPart1(sue, target))
        {
            return sue.Number;
        }
    }
    
    return -1; // Not found
}

private static bool MatchesPart1(AuntSue sue, Dictionary<string, int> target)
{
    foreach (var (attribute, value) in sue.Attributes)
    {
        if (target[attribute] != value)
        {
            return false;
        }
    }
    return true;
}
```

### Execution Flow:

```
Initialize:
  target = {children: 3, cats: 7, ...}
  sues = [Sue 1, Sue 2, ..., Sue 500]

Loop through sues:
  Sue 1:
    Check attributes → No match
  Sue 2:
    Check attributes → No match
  ...
  Sue 213:
    Check goldfish: 5 == 5 ✓
    Check trees: 3 == 3 ✓
    Check cars: 2 == 2 ✓
    → All match! Return 213

Result: 213
```

---

## Step 8: Part 2 Matching Logic

### Comparison Rules:

```csharp
private static bool CompareAttribute(string attribute, int sueValue, int targetValue)
{
    return attribute switch
    {
        "cats" => sueValue > targetValue,      // Greater than
        "trees" => sueValue > targetValue,     // Greater than
        "pomeranians" => sueValue < targetValue, // Fewer than
        "goldfish" => sueValue < targetValue,    // Fewer than
        _ => sueValue == targetValue              // Exact match
    };
}
```

### Updated Matching Function:

```csharp
private static bool MatchesPart2(AuntSue sue, Dictionary<string, int> target)
{
    foreach (var (attribute, value) in sue.Attributes)
    {
        if (!CompareAttribute(attribute, value, target[attribute]))
        {
            return false; // Mismatch found
        }
    }
    return true;
}
```

---

## Step 9: Part 2 Example Trace

### Testing Sue 213

**Sue 213:**
```
goldfish: 5, trees: 3, cars: 2
```

**Part 2 Checks:**
```
Attribute 1: goldfish
  Sue has: 5
  Target:  5
  Rule: < (fewer than)
  Is 5 < 5? NO ✗
  
  MISMATCH! Return false.
```

Sue 213 does NOT match in Part 2.

### Testing Sue 40 (Hypothetical)

**Sue 40:**
```
akitas: 0, trees: 5, cars: 2
```

**Part 2 Checks:**
```
Attribute 1: akitas
  Sue has: 0
  Target:  0
  Rule: == (exact)
  Is 0 == 0? YES ✓

Attribute 2: trees
  Sue has: 5
  Target:  3
  Rule: > (greater than)
  Is 5 > 3? YES ✓

Attribute 3: cars
  Sue has: 2
  Target:  2
  Rule: == (exact)
  Is 2 == 2? YES ✓

All checks passed → return true
```

Sue 40 matches in Part 2!

---

## Step 10: Part 2 Complete Solution

```csharp
public static int SolvePart2(string[] input)
{
    var target = GetTargetProfile();
    var sues = ParseInput(input);
    
    foreach (var sue in sues)
    {
        if (MatchesPart2(sue, target))
        {
            return sue.Number;
        }
    }
    
    return -1; // Not found
}

private static bool MatchesPart2(AuntSue sue, Dictionary<string, int> target)
{
    foreach (var (attribute, value) in sue.Attributes)
    {
        if (!CompareAttribute(attribute, value, target[attribute]))
        {
            return false;
        }
    }
    return true;
}

private static bool CompareAttribute(string attribute, int sueValue, int targetValue)
{
    return attribute switch
    {
        "cats" => sueValue > targetValue,
        "trees" => sueValue > targetValue,
        "pomeranians" => sueValue < targetValue,
        "goldfish" => sueValue < targetValue,
        _ => sueValue == targetValue
    };
}
```

---

## Step 11: Complete Implementation

```csharp
using System;
using System.Collections.Generic;

namespace AdventOfCode2015.ResolvingDays;

public static class Day16
{
    private class AuntSue
    {
        public int Number { get; set; }
        public Dictionary<string, int> Attributes { get; set; } = new();
    }
    
    // Target profile from MFCSAM
    private static Dictionary<string, int> GetTargetProfile()
    {
        return new Dictionary<string, int>
        {
            ["children"] = 3,
            ["cats"] = 7,
            ["samoyeds"] = 2,
            ["pomeranians"] = 3,
            ["akitas"] = 0,
            ["vizslas"] = 0,
            ["goldfish"] = 5,
            ["trees"] = 3,
            ["cars"] = 2,
            ["perfumes"] = 1
        };
    }
    
    // Part 1: Find Sue with exact matches
    public static int SolvePart1(string[] input)
    {
        var target = GetTargetProfile();
        var sues = ParseInput(input);
        
        foreach (var sue in sues)
        {
            if (MatchesPart1(sue, target))
            {
                return sue.Number;
            }
        }
        
        return -1; // Not found
    }
    
    // Part 2: Find Sue with range-based matches
    public static int SolvePart2(string[] input)
    {
        var target = GetTargetProfile();
        var sues = ParseInput(input);
        
        foreach (var sue in sues)
        {
            if (MatchesPart2(sue, target))
            {
                return sue.Number;
            }
        }
        
        return -1; // Not found
    }
    
    // Parse input lines into Aunt Sue objects
    private static List<AuntSue> ParseInput(string[] lines)
    {
        var sues = new List<AuntSue>();
        
        foreach (string line in lines)
        {
            // Example: "Sue 213: goldfish: 5, trees: 3, cars: 2"
            
            // Extract Sue number
            int colonIndex = line.IndexOf(':');
            int sueNumber = int.Parse(line.Substring(4, colonIndex - 4));
            
            // Parse attributes
            var attributes = new Dictionary<string, int>();
            string attributePart = line.Substring(colonIndex + 2);
            string[] pairs = attributePart.Split(", ");
            
            foreach (string pair in pairs)
            {
                string[] parts = pair.Split(": ");
                string attribute = parts[0];
                int value = int.Parse(parts[1]);
                attributes[attribute] = value;
            }
            
            sues.Add(new AuntSue
            {
                Number = sueNumber,
                Attributes = attributes
            });
        }
        
        return sues;
    }
    
    // Part 1: Check if all remembered attributes match exactly
    private static bool MatchesPart1(AuntSue sue, Dictionary<string, int> target)
    {
        foreach (var (attribute, value) in sue.Attributes)
        {
            if (target[attribute] != value)
            {
                return false; // Mismatch
            }
        }
        return true; // All match
    }
    
    // Part 2: Check if all remembered attributes match with range rules
    private static bool MatchesPart2(AuntSue sue, Dictionary<string, int> target)
    {
        foreach (var (attribute, value) in sue.Attributes)
        {
            if (!CompareAttribute(attribute, value, target[attribute]))
            {
                return false; // Mismatch
            }
        }
        return true; // All match
    }
    
    // Part 2: Compare attribute using appropriate rule
    private static bool CompareAttribute(string attribute, int sueValue, int targetValue)
    {
        return attribute switch
        {
            "cats" => sueValue > targetValue,      // Greater than
            "trees" => sueValue > targetValue,     // Greater than
            "pomeranians" => sueValue < targetValue, // Fewer than
            "goldfish" => sueValue < targetValue,    // Fewer than
            _ => sueValue == targetValue              // Exact match
        };
    }
}
```

---

## Step 12: Alternative Parsing with Regex

### Using Regular Expressions:

```csharp
using System.Text.RegularExpressions;

private static List<AuntSue> ParseInputWithRegex(string[] lines)
{
    var sues = new List<AuntSue>();
    
    // Pattern: Sue (\d+): (\w+): (\d+), (\w+): (\d+), (\w+): (\d+)
    var regex = new Regex(@"Sue (\d+): (\w+): (\d+), (\w+): (\d+), (\w+): (\d+)");
    
    foreach (string line in lines)
    {
        var match = regex.Match(line);
        if (match.Success)
        {
            int sueNumber = int.Parse(match.Groups[1].Value);
            
            var attributes = new Dictionary<string, int>
            {
                [match.Groups[2].Value] = int.Parse(match.Groups[3].Value),
                [match.Groups[4].Value] = int.Parse(match.Groups[5].Value),
                [match.Groups[6].Value] = int.Parse(match.Groups[7].Value)
            };
            
            sues.Add(new AuntSue
            {
                Number = sueNumber,
                Attributes = attributes
            });
        }
    }
    
    return sues;
}
```

---

## Step 13: Optimization Considerations

### Early Exit:

The foreach loop in both `MatchesPart1` and `MatchesPart2` already implements early exit - as soon as a mismatch is found, it returns false.

### Single Pass:

Both solutions process the list of Sues once, finding the first match. Since there's guaranteed to be exactly one matching Sue, we can stop as soon as we find it.

### Time Complexity:

```
Parsing: O(N × K) where N = 500 Sues, K = 3 attributes each
       = O(1500) = O(1)

Matching: O(N × K) = O(1500) = O(1)

Total: O(1) - constant time (since N is fixed at 500)
```

### Space Complexity:

```
Storing all Sues: O(N × K) = O(1500) = O(1)

We could optimize to O(1) by processing one Sue at a time
without storing all of them:
```

```csharp
public static int SolvePart1Optimized(string[] input)
{
    var target = GetTargetProfile();
    
    foreach (string line in input)
    {
        var sue = ParseSingleSue(line);
        if (MatchesPart1(sue, target))
        {
            return sue.Number;
        }
    }
    
    return -1;
}
```

---

## Step 14: Common Mistakes to Avoid

### Mistake 1: Treating Unknown as Zero
```csharp
// WRONG - Don't assume missing attributes are zero ✗
if (!sue.Attributes.ContainsKey("cats"))
{
    if (target["cats"] != 0) // Wrong assumption!
        return false;
}

// CORRECT - Only check remembered attributes ✓
if (sue.Attributes.ContainsKey("cats"))
{
    if (target["cats"] != sue.Attributes["cats"])
        return false;
}
```

### Mistake 2: Wrong Part 2 Comparison Direction
```csharp
// WRONG - Backwards comparison ✗
case "cats": return sueValue < targetValue; // Should be >

// CORRECT - Cats are greater than ✓
case "cats": return sueValue > targetValue;
```

### Mistake 3: Including Boundary in Range
```csharp
// WRONG - Using <= or >= ✗
case "goldfish": return sueValue <= targetValue; // Should be <

// CORRECT - Strictly less than ✓
case "goldfish": return sueValue < targetValue;
```

### Mistake 4: Returning First Attribute Match
```csharp
// WRONG - Returns too early ✗
foreach (var (attr, val) in sue.Attributes)
{
    if (target[attr] == val)
        return true; // Don't return on first match!
}

// CORRECT - Check all attributes ✓
foreach (var (attr, val) in sue.Attributes)
{
    if (target[attr] != val)
        return false; // Only return false on mismatch
}
return true; // All matched
```

---

## Step 15: Testing Strategy

### Test Case 1: Simple Exact Match (Part 1)
```
Input:
  Sue 1: cats: 7, trees: 3, cars: 2
  
Target:
  cats: 7, trees: 3, cars: 2, ...
  
Expected: Sue 1 matches (all exact)
```

### Test Case 2: Partial Match with Unknowns
```
Input:
  Sue 2: goldfish: 5, trees: 3
  
Target:
  goldfish: 5, trees: 3, ...
  
Expected: Sue 2 matches (unknowns are OK)
```

### Test Case 3: One Attribute Mismatch
```
Input:
  Sue 3: cats: 8, trees: 3, cars: 2
  
Target:
  cats: 7, trees: 3, cars: 2, ...
  
Expected: Sue 3 doesn't match (cats mismatch)
```

### Test Case 4: Part 2 Greater Than
```
Input:
  Sue 4: cats: 8, trees: 4, cars: 2
  
Target:
  cats: 7, trees: 3, cars: 2
  
Rules: cats > 7, trees > 3, cars == 2
  
Expected: Sue 4 matches (8 > 7, 4 > 3, 2 == 2)
```

### Test Case 5: Part 2 Fewer Than
```
Input:
  Sue 5: goldfish: 4, pomeranians: 2, cars: 2
  
Target:
  goldfish: 5, pomeranians: 3, cars: 2
  
Rules: goldfish < 5, pomeranians < 3, cars == 2
  
Expected: Sue 5 matches (4 < 5, 2 < 3, 2 == 2)
```

### Test Case 6: Part 2 Boundary Value
```
Input:
  Sue 6: goldfish: 5, trees: 3, cars: 2
  
Target:
  goldfish: 5, trees: 3, cars: 2
  
Rules: goldfish < 5, trees > 3, cars == 2
  
Expected: Sue 6 doesn't match (5 < 5 is false, 3 > 3 is false)
```

---

## Step 16: Summary

**Part 1 Algorithm:**
```
1. Parse MFCSAM target profile (hardcoded)
2. Parse all Aunt Sue records
3. For each Sue:
   a. Check each remembered attribute
   b. If any doesn't match exactly → skip
   c. If all match → return Sue number
4. There will be exactly one matching Sue
```

**Part 2 Algorithm:**
```
1. Same as Part 1
2. Change comparison rules:
   - cats, trees: sue_value > target_value
   - pomeranians, goldfish: sue_value < target_value
   - all others: sue_value == target_value
3. Return Sue number that passes all checks
```

**Key Points:**
- ✅ 500 Aunt Sues to check
- ✅ Each Sue lists 3 attributes (out of 10)
- ✅ Missing attributes = unknown (not zero!)
- ✅ Only check remembered attributes
- ✅ Part 1: Exact matches only (==)
- ✅ Part 2: Special rules for 4 attributes (>, <)
- ✅ Guaranteed exactly one matching Sue

**Complexity:**
- ⏱️ Time: O(N × K) = O(500 × 3) = O(1)
- 💾 Space: O(N × K) = O(1500) = O(1)

**Memory Aid: "MFCSAM"**
```
M - Match remembered attributes only
F - Fewer than (pomeranians, goldfish in Part 2)
C - Cats (greater than in Part 2)
S - Sue number (1-500)
A - Attributes (3 per Sue, 10 possible)
M - More than (cats, trees in Part 2)
```

---

**Happy Aunt Sue hunting! 🔍🎁**
