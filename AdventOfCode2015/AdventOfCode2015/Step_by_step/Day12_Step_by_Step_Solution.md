# Day 12: JSAbacusFramework.io - Step by Step Solution

## Step 1: Understanding the Problem

Santa's Accounting-Elves need help with a JSON document containing various data types.

**Part 1:** Find all numbers in the JSON and sum them together.

**Part 2:** Same as Part 1, but ignore any object (not array) that contains the string "red" as a value.

### Input Format:
- A single line of JSON text
- Contains arrays, objects, numbers, and strings

### JSON Elements:
```json
Arrays:   [1, 2, 3]
Objects:  {"a": 1, "b": 2}
Numbers:  42, -17, 0
Strings:  "hello", "red"
```

**Key Rule for Part 2:**
- If an **object** has ANY property with value "red", skip the entire object
- **Arrays** with "red" are NOT filtered

---

## Step 2: Analyzing Examples

### Example 1: `[1,2,3]`
```
Simple array of numbers
Sum: 1 + 2 + 3 = 6
```

### Example 2: `{"a":2,"b":4}`
```
Object with two properties
Property values: 2, 4
Sum: 2 + 4 = 6
```

### Example 3: `[[[3]]]`
```
Deeply nested array
Still contains number: 3
Sum: 3
```

### Example 4: `{"a":{"b":4},"c":-1}`
```
Nested object with negative number
Numbers: 4, -1
Sum: 4 + (-1) = 3
```

### Example 5: `{"a":[-1,1]}`
```
Object containing array
Numbers: -1, 1
Sum: -1 + 1 = 0
```

### Example 6: `[]` and `{}`
```
Empty structures
Sum: 0 (no numbers)
```

---

## Step 3: Part 1 - Simple Regex Approach

For Part 1, we can use a simple regex to find all numbers.

### Regex Pattern: `-?\d+`
```
-?    Optional minus sign
\d+   One or more digits

Matches:
  42   ✓
  -17  ✓
  0    ✓
  123  ✓
```

### Implementation:

```csharp
public static int SolvePart1(string input)
{
    // Find all numbers using regex
    var matches = Regex.Matches(input, @"-?\d+");
    
    // Parse and sum all numbers
    int sum = 0;
    foreach (Match match in matches)
    {
        sum += int.Parse(match.Value);
    }
    
    return sum;
}
```

### How it Works:

**Input:** `[1,2,3]`
```
Regex finds: "1", "2", "3"
Parse to int: 1, 2, 3
Sum: 1 + 2 + 3 = 6
```

**Input:** `{"a":10,"b":-5}`
```
Regex finds: "10", "-5"
Parse to int: 10, -5
Sum: 10 + (-5) = 5
```

### Using LINQ (Alternative):

```csharp
public static int SolvePart1(string input)
{
    return Regex.Matches(input, @"-?\d+")
                .Sum(m => int.Parse(m.Value));
}
```

**Pros of Regex Approach:**
- ✓ Very simple
- ✓ Fast
- ✓ Works perfectly for Part 1

**Cons:**
- ✗ Cannot handle Part 2 (no structure awareness)

---

## Step 4: Understanding Part 2 Requirements

Part 2 adds a filtering rule:
- Ignore objects where ANY value is the string "red"
- Arrays are NOT affected by this rule

### Example: Object with "red"
```json
{"a":1,"b":"red","c":3}
```
**Analysis:**
- This is an object
- Property "b" has value "red"
- Skip entire object → Sum: 0

### Example: Array with "red"
```json
[1,"red",5]
```
**Analysis:**
- This is an array
- Arrays ignore the "red" rule
- Count all numbers → Sum: 1 + 5 = 6

### Example: Nested structures
```json
[1,{"c":"red","b":2},3]
```
**Analysis:**
- Outer structure is array (counts ✓)
- Element 1: number 1 → count it
- Element 2: object with "red" → skip object (2 ignored)
- Element 3: number 3 → count it
- Sum: 1 + 3 = 4

---

## Step 5: JSON Parsing Approach

For Part 2, we need to properly parse and traverse JSON structure.

### Using System.Text.Json:

```csharp
using System.Text.Json;

public static int SolvePart2(string input)
{
    // Parse JSON into structured document
    JsonDocument doc = JsonDocument.Parse(input);
    
    // Sum numbers with "red" filtering
    return SumNumbers(doc.RootElement, filterRed: true);
}
```

---

## Step 6: Implementing Recursive Traversal

The core algorithm is recursive - visit each node and sum numbers.

### Basic Structure:

```csharp
private static int SumNumbers(JsonElement element, bool filterRed)
{
    // Handle different JSON types
    switch (element.ValueKind)
    {
        case JsonValueKind.Number:
            // It's a number - return its value
            return element.GetInt32();
            
        case JsonValueKind.Array:
            // It's an array - sum all elements
            return SumArray(element, filterRed);
            
        case JsonValueKind.Object:
            // It's an object - check for "red" first
            return SumObject(element, filterRed);
            
        default:
            // String, boolean, null, etc. - not numbers
            return 0;
    }
}
```

---

## Step 7: Handling Arrays

Arrays are straightforward - recursively sum all elements.

```csharp
private static int SumArray(JsonElement array, bool filterRed)
{
    int sum = 0;
    
    // Iterate through each element
    foreach (JsonElement element in array.EnumerateArray())
    {
        // Recursively sum this element
        sum += SumNumbers(element, filterRed);
    }
    
    return sum;
}
```

### Example Trace: `[1, [2, 3], 4]`

```
SumArray([1, [2, 3], 4])
│
├─ Element 0: 1
│  └─ SumNumbers(1) → 1
│
├─ Element 1: [2, 3]
│  └─ SumNumbers([2,3])
│     └─ SumArray([2, 3])
│        ├─ Element 0: 2 → 2
│        └─ Element 1: 3 → 3
│        └─ Return: 5
│
└─ Element 2: 4
   └─ SumNumbers(4) → 4

Total: 1 + 5 + 4 = 10
```

---

## Step 8: Handling Objects

Objects require checking for "red" before summing.

```csharp
private static int SumObject(JsonElement obj, bool filterRed)
{
    // Part 2: Check if object should be filtered
    if (filterRed && HasRedValue(obj))
    {
        return 0; // Skip entire object
    }
    
    int sum = 0;
    
    // Iterate through all properties
    foreach (JsonProperty property in obj.EnumerateObject())
    {
        // Recursively sum property values
        sum += SumNumbers(property.Value, filterRed);
    }
    
    return sum;
}
```

---

## Step 9: Checking for "red" Value

Check if any property value is the string "red".

```csharp
private static bool HasRedValue(JsonElement obj)
{
    // Check all properties in the object
    foreach (JsonProperty property in obj.EnumerateObject())
    {
        // Check if value is string "red"
        if (property.Value.ValueKind == JsonValueKind.String 
            && property.Value.GetString() == "red")
        {
            return true; // Found "red" - skip this object
        }
    }
    
    return false; // No "red" found
}
```

### Important Notes:
- Only checks direct values (not nested)
- Only checks VALUES, not property names
- String must be exactly "red" (case-sensitive)

### Examples:

**Object: `{"a":"red","b":5}`**
```
Check properties:
  "a": "red" → ValueKind.String → GetString() == "red" → TRUE
  
Return: true (skip object)
```

**Object: `{"red":5}`**
```
Check properties:
  "red": 5 → ValueKind.Number → not a string → FALSE
  
Note: "red" is the KEY, not the VALUE
Return: false (count numbers)
```

**Object: `{"a":"blue","b":10}`**
```
Check properties:
  "a": "blue" → ValueKind.String → "blue" != "red" → FALSE
  "b": 10 → ValueKind.Number → not a string → FALSE
  
Return: false (no "red" found)
```

---

## Step 10: Complete Implementation

Putting it all together:

```csharp
using System.Text.Json;
using System.Text.RegularExpressions;

public static class Day12
{
    // Part 1: Simple regex approach
    public static int SolvePart1(string input)
    {
        // Find all numbers (positive and negative)
        var matches = Regex.Matches(input, @"-?\d+");
        
        // Sum all found numbers
        return matches.Sum(m => int.Parse(m.Value));
    }

    // Part 2: JSON parsing with "red" filtering
    public static int SolvePart2(string input)
    {
        // Parse JSON document
        using JsonDocument doc = JsonDocument.Parse(input);
        
        // Sum numbers, filtering objects with "red"
        return SumNumbers(doc.RootElement, filterRed: true);
    }

    // Recursive function to sum numbers
    private static int SumNumbers(JsonElement element, bool filterRed)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Number:
                return element.GetInt32();
                
            case JsonValueKind.Array:
                return SumArray(element, filterRed);
                
            case JsonValueKind.Object:
                return SumObject(element, filterRed);
                
            default:
                return 0;
        }
    }

    // Sum all elements in an array
    private static int SumArray(JsonElement array, bool filterRed)
    {
        int sum = 0;
        foreach (JsonElement element in array.EnumerateArray())
        {
            sum += SumNumbers(element, filterRed);
        }
        return sum;
    }

    // Sum all property values in an object
    private static int SumObject(JsonElement obj, bool filterRed)
    {
        // Check if object should be filtered
        if (filterRed && HasRedValue(obj))
        {
            return 0;
        }
        
        int sum = 0;
        foreach (JsonProperty property in obj.EnumerateObject())
        {
            sum += SumNumbers(property.Value, filterRed);
        }
        return sum;
    }

    // Check if object has "red" as any value
    private static bool HasRedValue(JsonElement obj)
    {
        foreach (JsonProperty property in obj.EnumerateObject())
        {
            if (property.Value.ValueKind == JsonValueKind.String 
                && property.Value.GetString() == "red")
            {
                return true;
            }
        }
        return false;
    }
}
```

---

## Step 11: Detailed Trace Example

### Input: `{"a":10,"b":["red",20],"c":{"d":"red","e":30}}`

**Part 1 (no filtering):**
```
Regex finds: 10, 20, 30
Sum: 10 + 20 + 30 = 60
```

**Part 2 (with filtering):**
```
SumNumbers(root_object)
│
├─ Check HasRedValue(root) → false (no direct "red")
│
├─ Property "a": 10
│  └─ SumNumbers(10) → 10
│
├─ Property "b": ["red", 20]
│  └─ SumNumbers(array)
│     └─ SumArray(["red", 20])
│        ├─ Element "red"
│        │  └─ SumNumbers("red") → 0 (not a number)
│        └─ Element 20
│           └─ SumNumbers(20) → 20
│        └─ Return: 20
│
└─ Property "c": {"d":"red","e":30}
   └─ SumNumbers(object)
      └─ SumObject({"d":"red","e":30})
         ├─ Check HasRedValue → true (has "red")
         └─ Return: 0 (skip entire object!)

Total: 10 + 20 + 0 = 30
```

**Note:** The 30 inside object "c" is NOT counted because that object contains "red".

---

## Step 12: Testing with Examples

### Test Case 1: `[1,2,3]`
```
Part 1: 1 + 2 + 3 = 6
Part 2: 1 + 2 + 3 = 6 (no filtering needed)
```

### Test Case 2: `[1,{"c":"red","b":2},3]`
```
Part 1: 1 + 2 + 3 = 6
Part 2: 
  - Array element 1 → 1
  - Object {"c":"red","b":2} → has "red" → skip (0)
  - Array element 3 → 3
  - Total: 1 + 0 + 3 = 4
```

### Test Case 3: `{"d":"red","e":4,"f":5}`
```
Part 1: 4 + 5 = 9
Part 2: Object has "red" → skip entire object → 0
```

### Test Case 4: `[1,"red",5]`
```
Part 1: 1 + 5 = 6
Part 2: 1 + 5 = 6 (array, not filtered)
```

---

## Step 13: Common Mistakes to Avoid

### Mistake 1: Filtering Arrays
```csharp
// WRONG - Don't filter arrays
if (element.ValueKind == JsonValueKind.Array && HasRedValue(element))
    return 0;

// CORRECT - Only filter objects
if (element.ValueKind == JsonValueKind.Object && filterRed && HasRedValue(obj))
    return 0;
```

### Mistake 2: Checking Property Names
```csharp
// WRONG - Checking property name instead of value
if (property.Name == "red")
    return true;

// CORRECT - Check property value
if (property.Value.GetString() == "red")
    return true;
```

### Mistake 3: Partial Object Filtering
```csharp
// WRONG - Only skipping the "red" property
if (property.Value.GetString() == "red")
    continue; // Skip this property only

// CORRECT - Skip entire object
if (HasRedValue(obj))
    return 0; // Skip all properties
```

### Mistake 4: Not Checking ValueKind
```csharp
// WRONG - Will throw if value is not a string
if (property.Value.GetString() == "red")
    return true;

// CORRECT - Check type first
if (property.Value.ValueKind == JsonValueKind.String 
    && property.Value.GetString() == "red")
    return true;
```

---

## Step 14: Performance Considerations

### Time Complexity:

**Part 1 (Regex):**
```
O(n) where n = length of input string
- Single pass through string
- Constant time per number found
```

**Part 2 (JSON Parsing):**
```
O(m) where m = number of JSON elements
- Must visit each element once
- Constant work per element
```

### Space Complexity:

**Part 1:**
```
O(k) where k = number of numbers
- Stores regex matches
```

**Part 2:**
```
O(d) where d = maximum nesting depth
- Recursive call stack
- Plus O(m) for parsed JSON structure
```

### Optimization: Early Exit

```csharp
private static bool HasRedValue(JsonElement obj)
{
    foreach (JsonProperty property in obj.EnumerateObject())
    {
        if (property.Value.ValueKind == JsonValueKind.String 
            && property.Value.GetString() == "red")
        {
            return true; // Exit as soon as "red" found
        }
    }
    return false;
}
```

---

## Step 15: Alternative Implementation (LINQ)

More concise using LINQ:

```csharp
public static int SolvePart1(string input)
{
    return Regex.Matches(input, @"-?\d+")
                .Sum(m => int.Parse(m.Value));
}

public static int SolvePart2(string input)
{
    using JsonDocument doc = JsonDocument.Parse(input);
    return SumNumbers(doc.RootElement, true);
}

private static int SumNumbers(JsonElement element, bool filterRed)
{
    return element.ValueKind switch
    {
        JsonValueKind.Number => element.GetInt32(),
        
        JsonValueKind.Array => element.EnumerateArray()
            .Sum(e => SumNumbers(e, filterRed)),
        
        JsonValueKind.Object when filterRed && HasRedValue(element) => 0,
        
        JsonValueKind.Object => element.EnumerateObject()
            .Sum(p => SumNumbers(p.Value, filterRed)),
        
        _ => 0
    };
}

private static bool HasRedValue(JsonElement obj)
{
    return obj.EnumerateObject()
        .Any(p => p.Value.ValueKind == JsonValueKind.String 
                  && p.Value.GetString() == "red");
}
```

**Pros:**
- ✓ More concise
- ✓ Functional style

**Cons:**
- ✗ May be less readable for beginners
- ✗ Creates more enumerators (slightly slower)

---

## Step 16: Edge Cases

### Edge Case 1: Deeply Nested
```json
[[[[[[10]]]]]]
```
**Expected:** 10 (depth doesn't matter)

### Edge Case 2: Many "red" Objects
```json
{"a":"red","b":{"c":"red","d":10}}
```
**Expected:** 0 (both objects filtered)

### Edge Case 3: "red" in Array Inside Object
```json
{"a":["red",5],"b":10}
```
**Expected:** 15 (array not filtered, 5 + 10)

### Edge Case 4: Zero and Negative
```json
{"a":0,"b":-100,"c":100}
```
**Expected Part 1:** 0 (0 + -100 + 100)
**Expected Part 2:** 0 (same, no "red")

### Edge Case 5: Empty Structures
```json
{"a":[],"b":{},"c":[]}
```
**Expected:** 0 (no numbers)

---

## Step 17: Summary

**Part 1 Algorithm:**
```
1. Use regex to find all numbers: -?\d+
2. Parse each match as integer
3. Sum all integers
4. Return total
```

**Part 2 Algorithm:**
```
1. Parse JSON into structured format
2. Recursively traverse:
   - Number → return value
   - Array → sum all elements
   - Object → check for "red":
     * If "red" found → return 0
     * Otherwise → sum all property values
   - Other → return 0
3. Return total sum
```

**Key Points:**
- ✅ Part 1: Simple regex works perfectly
- ✅ Part 2: Need proper JSON parsing
- ✅ Only objects are filtered, not arrays
- ✅ "red" must be a VALUE, not a key
- ✅ Recursive traversal handles nesting
- ✅ Early exit on "red" detection

**Memory Aid: "JSON RULES"**
```
J - JSON structure matters (Part 2)
S - Sum all numbers (Part 1)
O - Objects can be filtered
N - Numbers are our target
R - Red means skip (objects only)
U - Use recursion for traversal
L - Look at values, not keys
E - Early exit on "red" found
S - Strings are checked for "red"
```

---

**Happy JSON parsing! 📊🎄**
