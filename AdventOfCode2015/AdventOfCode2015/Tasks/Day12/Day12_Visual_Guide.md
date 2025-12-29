# 📊 Day 12 Visual Guide - JSAbacusFramework.io

## 🎯 Understanding JSON Number Extraction

### What is JSON?
```
JSON = JavaScript Object Notation
A text format for storing structured data

Basic Elements:
- Arrays: [1, 2, 3]
- Objects: {"a": 1, "b": 2}
- Numbers: 42, -17, 3.14
- Strings: "hello"
- Booleans: true, false
- Null: null
```

**Our Task:**
Find ALL numbers in a JSON document and add them together.

---

## 📋 Part 1: Simple Number Extraction

### Rule: Extract ALL Numbers
```
Numbers can appear:
✓ In arrays: [1, 2, 3]
✓ In objects: {"a": 10, "b": 20}
✓ Nested deeply: {"x": [1, {"y": 2}]}
✓ Negative: [-5, -10]

Strings with numbers are NOT counted:
✗ "123" → This is a string, not a number!
```

---

## 📊 Example 1: Simple Array `[1,2,3]`

```
Input: [1,2,3]

Visualization:
[  1  ,  2  ,  3  ]
   ↑     ↑     ↑
   1  +  2  +  3  = 6

Result: 6
```

---

## 📊 Example 2: Simple Object `{"a":2,"b":4}`

```
Input: {"a":2,"b":4}

Visualization:
{
  "a": 2    ← Number: 2
  "b": 4    ← Number: 4
}

Keys ("a", "b") are strings → ignore
Values (2, 4) are numbers → count

Sum: 2 + 4 = 6

Result: 6
```

---

## 📊 Example 3: Nested Structures `[[[3]]]`

```
Input: [[[3]]]

Visualization:
[               ← Array level 1
  [             ← Array level 2
    [           ← Array level 3
      3         ← Number found!
    ]
  ]
]

Depth doesn't matter, still finds: 3

Result: 3
```

---

## 📊 Example 4: Complex Nesting `{"a":{"b":4},"c":-1}`

```
Input: {"a":{"b":4},"c":-1}

Visualization:
{
  "a": {        ← Object (nested)
    "b": 4      ← Number: 4
  }
  "c": -1       ← Number: -1 (negative counts!)
}

Numbers found: 4, -1
Sum: 4 + (-1) = 3

Result: 3
```

---

## 📊 Example 5: Mixed Array `{"a":[-1,1]}`

```
Input: {"a":[-1,1]}

Visualization:
{
  "a": [        ← Array value
    -1          ← Number: -1
    1           ← Number: 1
  ]
}

Sum: -1 + 1 = 0

Result: 0
```

---

## 📊 Example 6: Array with Object `[-1,{"a":1}]`

```
Input: [-1,{"a":1}]

Visualization:
[
  -1            ← Number: -1
  {             ← Object
    "a": 1      ← Number: 1
  }
]

Sum: -1 + 1 = 0

Result: 0
```

---

## 📊 Example 7: Empty Structures `[]` and `{}`

```
Input: []
Numbers found: (none)
Result: 0

Input: {}
Numbers found: (none)
Result: 0

Empty structures contain no numbers → sum is 0
```

---

## 🔍 Part 1 Algorithm: Regex Approach

### Strategy: Find All Numbers Using Pattern
```
Pattern to match:
- Optional minus sign: -?
- One or more digits: \d+

Regex: -?\d+

This matches:
✓ 42
✓ -17
✓ 0
✓ 123

But not:
✗ "42" (we'll match the digits anyway in JSON text)
✗ 3.14 (if we only want integers)
```

### Implementation Flow
```csharp
Step 1: Find all number patterns in JSON string
Step 2: Parse each match as integer
Step 3: Sum all integers

Example:
Input: "[1,2,3]"

Regex finds: "1", "2", "3"
Parse to int: 1, 2, 3
Sum: 1 + 2 + 3 = 6
```

---

## 📊 Regex Matching Visualization

### Example: `{"a":10,"b":-5,"c":"text"}`

```
Original string:
{ " a " : 1 0 , " b " : - 5 , " c " : " t e x t " }

Regex pattern -?\d+ matches:
          ↓↓          ↓↓↓
{ " a " : 1 0 , " b " : - 5 , " c " : " t e x t " }

Matches found:
- "10" at position 7-8
- "-5" at position 15-16

Note: "text" is a string, not matched

Parse and sum: 10 + (-5) = 5
```

---

## 🔴 Part 2: Ignore Objects with "red"

### New Rule
```
If ANY property VALUE in an object is the string "red",
ignore that ENTIRE object (including all its numbers).

Important:
✓ Only ignore OBJECTS with "red"
✗ Do NOT ignore ARRAYS with "red"
✓ Nested objects are checked separately
```

---

## 📊 Part 2 Example 1: Object with "red" value

### Input: `{"a":5,"b":"red","c":10}`

```
Object contains "red" as a value:
{
  "a": 5        ← Would be counted
  "b": "red"    ← RED FLAG! 🚫
  "c": 10       ← Would be counted
}

BUT: "red" appears → ignore ENTIRE object
Numbers ignored: 5, 10

Result: 0
```

---

## 📊 Part 2 Example 2: Array with "red" (still counts!)

### Input: `[1,"red",5]`

```
This is an ARRAY, not an object:
[
  1             ← Count it ✓
  "red"         ← This is in an array, not object
  5             ← Count it ✓
]

Arrays are NOT filtered by "red" rule!

Sum: 1 + 5 = 6

Result: 6
```

---

## 📊 Part 2 Example 3: Nested objects with "red"

### Input: `{"a":{"b":"red","c":5},"d":10}`

```
Structure:
{
  "a": {            ← Inner object
    "b": "red"      ← RED in inner object! 🚫
    "c": 5          ← Ignored (inner object has red)
  }
  "d": 10           ← This is in outer object ✓
}

Inner object has "red" → ignore inner object
But outer object doesn't have "red" at its level

Numbers counted: 10 (only from outer level)

Result: 10
```

---

## 📊 Part 2 Example 4: "red" in different positions

### Case A: `{"color":"red","value":5}`
```
{
  "color": "red"    ← Value is "red" 🚫
  "value": 5        ← Ignored
}

Object has "red" → entire object ignored
Result: 0
```

### Case B: `{"red":5}`
```
{
  "red": 5          ← "red" is a KEY, not value
}

"red" is the property NAME, not VALUE
Property value is 5 (a number)
Rule only checks VALUES → count the 5 ✓

Result: 5
```

### Case C: `{"a":"redish"}`
```
{
  "a": "redish"     ← Value is "redish", not "red"
}

Value must be EXACTLY "red"
"redish" ≠ "red" → doesn't trigger rule

If there were numbers, they'd count.
Result: 0 (no numbers anyway)
```

---

## 🔍 Part 2 Algorithm: JSON Parsing

### Why Regex Won't Work for Part 2
```
Need to:
1. Understand JSON structure
2. Know if we're in object vs array
3. Check all values in each object
4. Conditionally skip entire objects

Regex can't do this → need proper JSON parser!
```

### Parsing Strategy
```csharp
1. Parse JSON into structured object (JToken/JsonElement)
2. Recursively traverse the structure
3. For each node:
   - If it's a number → add to sum
   - If it's an array → recurse into elements
   - If it's an object:
     a. Check all values for "red"
     b. If "red" found → skip entire object
     c. Otherwise → recurse into properties
4. Return total sum
```

---

## 💻 Recursive Traversal Visualization

### Example: `{"a":[1,2],"b":{"c":"red","d":3},"e":4}`

```
Root Object {a, b, e}
│
├─ Check for "red" at this level? No
├─ Property "a": [1,2]
│  │
│  └─ Array [1, 2]
│     ├─ Element 1 → Sum += 1
│     └─ Element 2 → Sum += 2
│
├─ Property "b": {c, d}
│  │
│  └─ Object {c, d}
│     ├─ Check for "red"? YES → SKIP! 🚫
│     └─ (Don't recurse into this object)
│
└─ Property "e": 4 → Sum += 4

Total: 1 + 2 + 4 = 7
(3 was ignored because parent object had "red")
```

---

## 📊 Step-by-Step Trace: Complex Example

### Input: `{"a":10,"b":["red",20],"c":{"d":"red","e":30}}`

```
Step 1: Root object
  Check values: 10, [array], {object}
  Contains "red"? NO (it's in nested structures)
  ✓ Process this object

Step 2: Property "a" = 10
  Is number? YES
  ✓ Sum += 10 (total: 10)

Step 3: Property "b" = ["red", 20]
  Is array? YES
  Arrays ignore "red" rule
  ✓ Recurse into array

Step 4: Array element "red"
  Is number? NO
  Skip (it's a string)

Step 5: Array element 20
  Is number? YES
  ✓ Sum += 20 (total: 30)

Step 6: Property "c" = {"d":"red","e":30}
  Is object? YES
  Check this object's values: "red", 30
  Contains "red"? YES! 🚫
  ✗ Skip entire object (30 is NOT counted)

Final sum: 10 + 20 = 30
```

---

## 🎨 JSON Structure Types

### Type Detection
```csharp
JsonElement element;

// Check element type:
element.ValueKind == JsonValueKind.Number
element.ValueKind == JsonValueKind.Array
element.ValueKind == JsonValueKind.Object
element.ValueKind == JsonValueKind.String
```

### Handling Each Type
```
Number:
  → Add to sum

Array:
  → Recurse into each element
  → No "red" filtering

Object:
  → Check all values for "red"
  → If "red" found: return 0
  → Otherwise: recurse into values

String:
  → Check if it's "red" (for filtering)
  → Otherwise ignore
```

---

## 📊 Comparison: Part 1 vs Part 2

### Example: `[1,{"c":"red","b":2},3]`

**Part 1: Count all numbers**
```
Array [1, {object}, 3]
  ├─ 1 → count ✓
  ├─ Object {c, b}
  │  └─ 2 → count ✓
  └─ 3 → count ✓

Total: 1 + 2 + 3 = 6
```

**Part 2: Ignore objects with "red"**
```
Array [1, {object}, 3]
  ├─ 1 → count ✓
  ├─ Object {c, b}
  │  ├─ Check for "red"? YES! 🚫
  │  └─ Skip object (2 ignored)
  └─ 3 → count ✓

Total: 1 + 3 = 4
```

**Difference: 6 - 4 = 2**
(The 2 inside the object with "red")

---

## 🔍 Edge Cases to Consider

### Edge Case 1: "red" as a key vs value
```json
{"red": 10}        → Count 10 (key is "red", value is 10)
{10: "red"}        → Skip object (value is "red")
                     Note: JSON keys must be strings
```

### Edge Case 2: Nested "red"
```json
{"a": {"b": "red"}, "c": 5}

Inner object has "red" → skip inner object only
Outer object doesn't have "red" → count outer numbers

Result: Count 5 ✓
```

### Edge Case 3: Multiple "red" values
```json
{"a": "red", "b": "red", "c": 10}

Object has "red" (multiple times)
Still skip entire object

Result: 0
```

### Edge Case 4: "red" in array inside object
```json
{"a": ["red", 5], "b": 10}

Array contains "red" → BUT arrays don't filter
Object values: [array], 10
Object doesn't have "red" string directly

Result: Count 5 + 10 = 15 ✓
```

### Edge Case 5: Empty after filtering
```json
{"a": {"b": "red"}}

Inner object skipped
Outer object has no numbers
Result: 0
```

---

## 💻 Implementation Approaches

### Approach 1: Regex (Part 1 Only)
```csharp
// Simple and fast for Part 1
var matches = Regex.Matches(json, @"-?\d+");
int sum = matches.Sum(m => int.Parse(m.Value));
```

**Pros:**
- ✓ Very simple
- ✓ Fast
- ✓ Works for Part 1

**Cons:**
- ✗ Can't handle Part 2
- ✗ No structure awareness
- ✗ Might match numbers in strings (usually OK for AoC)

---

### Approach 2: JSON Parsing (Both Parts)
```csharp
// Use System.Text.Json
JsonDocument doc = JsonDocument.Parse(json);
int sum = SumNumbers(doc.RootElement, filterRed: false);

int SumNumbers(JsonElement element, bool filterRed)
{
    switch (element.ValueKind)
    {
        case JsonValueKind.Number:
            return element.GetInt32();
            
        case JsonValueKind.Array:
            return element.EnumerateArray()
                .Sum(e => SumNumbers(e, filterRed));
                
        case JsonValueKind.Object:
            if (filterRed && HasRedValue(element))
                return 0;
            return element.EnumerateObject()
                .Sum(p => SumNumbers(p.Value, filterRed));
                
        default:
            return 0;
    }
}

bool HasRedValue(JsonElement obj)
{
    return obj.EnumerateObject()
        .Any(p => p.Value.ValueKind == JsonValueKind.String 
                  && p.Value.GetString() == "red");
}
```

**Pros:**
- ✓ Works for both parts
- ✓ Proper structure handling
- ✓ Clean recursive solution

**Cons:**
- ✗ More complex than regex
- ✗ Slightly slower (usually fine)

---

## 📊 Complexity Analysis

### Time Complexity

**Regex Approach (Part 1):**
```
O(n) where n = length of JSON string
- Single pass through string
- Constant time per match
```

**JSON Parsing Approach:**
```
O(n) where n = total elements in JSON
- Must visit every node once
- Constant work per node
```

### Space Complexity

**Regex Approach:**
```
O(m) where m = number of matches
- Stores all matches
```

**JSON Parsing Approach:**
```
O(d) where d = depth of nesting
- Recursive call stack
- Plus O(n) for parsed JSON structure
```

---

## 🐛 Common Mistakes

### Mistake 1: Counting String Numbers
```csharp
// WRONG - Might count "42" as a number
var matches = Regex.Matches(json, @"\d+");
// This finds digits anywhere, even in strings

// CORRECT - Actually parse JSON
// Or be careful with regex context
```

### Mistake 2: Filtering Arrays in Part 2
```csharp
// WRONG - Checking arrays for "red"
if (element.ValueKind == JsonValueKind.Array)
{
    if (HasRedValue(element)) return 0; // ✗ WRONG!
}

// CORRECT - Only filter objects
if (element.ValueKind == JsonValueKind.Object)
{
    if (HasRedValue(element)) return 0; // ✓ CORRECT
}
```

### Mistake 3: Partial Object Filtering
```csharp
// WRONG - Only skipping the "red" value
if (value.ValueKind == JsonValueKind.String 
    && value.GetString() == "red")
    continue; // Skip only this property

// CORRECT - Skip entire object
if (HasRedValue(element))
    return 0; // Skip all properties
```

### Mistake 4: Checking Keys Instead of Values
```csharp
// WRONG - Checking property names
if (property.Name == "red") // ✗ WRONG

// CORRECT - Checking property values
if (property.Value.GetString() == "red") // ✓ CORRECT
```

---

## 🎯 Testing Strategy

### Test Cases for Part 1
```
Input               Expected    Reason
[1,2,3]            6           Simple array
{"a":2,"b":4}      6           Simple object
[[[3]]]            3           Deep nesting
{"a":{"b":4},"c":-1}  3        Nested + negative
{"a":[-1,1]}       0           Canceling numbers
[-1,{"a":1}]       0           Mixed
[]                 0           Empty array
{}                 0           Empty object
```

### Test Cases for Part 2
```
Input                           Expected    Reason
[1,2,3]                        6           No red
[1,"red",5]                    6           Red in array (OK)
{"d":"red","e":4,"f":5}        0           Red in object
{"a":1,"b":"red"}              0           Red → skip object
{"a":[1,2],"b":"red"}          0           Red in object
[1,{"c":"red","b":2},3]        4           Skip inner object
{"a":1,"b":{"c":"red"}}        1           Red in nested only
```

---

## 🎨 Memory Aids

### Part 1: "Sum ALL the numbers!"
```
🔢 Find every number
➕ Add them all together
✅ Simple!
```

### Part 2: "Red means STOP for objects!"
```
🔴 See "red" in object values?
🚫 Stop! Skip that object
📊 But arrays don't care about red
✅ Count everything else
```

### Recursion Pattern
```
Each node:
  Number? → Return it
  Array? → Sum children
  Object? → Check red, then sum children
  Other? → Return 0
```

---

## 📈 Optimization Tips

### Tip 1: Early Exit on "red" Check
```csharp
// Check for "red" before recursing
bool HasRedValue(JsonElement obj)
{
    foreach (var prop in obj.EnumerateObject())
    {
        if (prop.Value.ValueKind == JsonValueKind.String 
            && prop.Value.GetString() == "red")
            return true; // Early exit
    }
    return false;
}
```

### Tip 2: Use LINQ Carefully
```csharp
// LINQ is clean but creates enumerators
return obj.EnumerateObject()
    .Sum(p => SumNumbers(p.Value, filterRed));

// Manual loop might be slightly faster
int sum = 0;
foreach (var prop in obj.EnumerateObject())
    sum += SumNumbers(prop.Value, filterRed);
return sum;
```

### Tip 3: Cache "red" Check Results
```csharp
// If same objects appear multiple times
Dictionary<JsonElement, bool> redCache = new();

bool HasRedValueCached(JsonElement obj)
{
    if (redCache.TryGetValue(obj, out bool result))
        return result;
    
    bool hasRed = HasRedValue(obj);
    redCache[obj] = hasRed;
    return hasRed;
}
```

---

## 📝 Summary

**Part 1 - Extract All Numbers:**
- 🔢 Find every number in JSON
- ➕ Sum them all together
- 📊 Use regex or JSON parser

**Part 2 - Filter Objects with "red":**
- 🔴 Check each object for "red" values
- 🚫 Skip entire object if "red" found
- ✅ Arrays are NOT filtered
- 🔄 Use recursive JSON traversal

**Key Concepts:**
- JSON structure: objects vs arrays
- Recursive tree traversal
- Conditional filtering
- Pattern matching (regex vs parsing)

**Algorithm Pattern:**
```
1. Parse JSON structure
2. Recursively traverse
3. Handle each type appropriately
4. Sum numbers, skip filtered objects
5. Return total
```

**Memory Aid: "JSONFIND"**
```
J - JSON parsing
S - Sum numbers
O - Objects can be skipped
N - Nested structures
F - Filter "red" (Part 2)
I - Ignore strings
N - Numbers are our target
D - Depth-first traversal
```

---

**Happy JSON parsing! 📊🎄**
