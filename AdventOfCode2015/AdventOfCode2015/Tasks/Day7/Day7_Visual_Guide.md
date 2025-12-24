# 🔌 Day 7 Visual Guide - Bitwise Circuit Simulation

## 🎯 Problem Overview

Santa's kit contains wires and bitwise logic gates. Each wire:
- Has a **lowercase letter identifier** (e.g., `x`, `y`, `ab`, `lx`)
- Carries a **16-bit signal** (values 0 to 65,535)
- Gets signal from ONE source but can output to MULTIPLE destinations
- Gates produce no signal until ALL inputs are ready

**Goal:** Find the signal value on wire `a`

---

## 🔢 Understanding 16-bit Signals

### Number Range
```
Minimum: 0                    = 0000000000000000 (binary)
Maximum: 65,535 (2^16 - 1)    = 1111111111111111 (binary)
Example: 123                  = 0000000001111011 (binary)
Example: 456                  = 0000000111001000 (binary)
```

### Why 16 bits?
```
Each bit position represents a power of 2:

Position: 15 14 13 12 11 10  9  8  7  6  5  4  3  2  1  0
Power:    2¹⁵ ...                          2² 2¹ 2⁰
Value:    32768 16384 8192 4096 ... 16  8  4  2  1

Example: 123 in binary
  Bit positions with 1: 6, 5, 4, 3, 1, 0
  64 + 32 + 16 + 8 + 2 + 1 = 123
```

---

## 🚪 Gate Types and Operations

### 1️⃣ Direct Value Assignment
```
Syntax: VALUE -> wire
Example: 123 -> x
```

**Visual:**
```
     ┌─────┐
 123 │     │
────►│  x  │
     └──┬──┘
        │
      x = 123
```

### 2️⃣ Wire Copy
```
Syntax: source_wire -> target_wire
Example: lx -> a
```

**Visual:**
```
     ┌─────┐      ┌─────┐
     │ lx  │      │  a  │
     └──┬──┘      └─────┘
        │            ▲
        └────────────┘
   a gets the value from lx
```

### 3️⃣ AND Gate
```
Syntax: wire1 AND wire2 -> target
Example: x AND y -> d
```

**Visual:**
```
Input x: 123  = 0000000001111011
Input y: 456  = 0000000111001000
                ─────────────────  (AND each bit)
Output d: 72  = 0000000001001000
                     │  │  │  
                     └──┴──┴── Only 1 AND 1 = 1

     ┌─────┐
  x  │     │
────►│ AND │
  y  │     │
────►└──┬──┘
        │
      d = 72
```

**Truth Table:**
```
x | y | AND
──┼───┼────
0 | 0 | 0
0 | 1 | 0
1 | 0 | 0
1 | 1 | 1   ← Only true when BOTH are 1
```

### 4️⃣ OR Gate
```
Syntax: wire1 OR wire2 -> target
Example: x OR y -> e
```

**Visual:**
```
Input x: 123  = 0000000001111011
Input y: 456  = 0000000111001000
                ─────────────────  (OR each bit)
Output e: 507 = 0000000111111011
                     │││││││││
                     └┴┴┴┴┴┴┴┴── 1 OR anything = 1

     ┌─────┐
  x  │     │
────►│ OR  │
  y  │     │
────►└──┬──┘
        │
      e = 507
```

**Truth Table:**
```
x | y | OR
──┼───┼───
0 | 0 | 0
0 | 1 | 1
1 | 0 | 1
1 | 1 | 1   ← True when ANY is 1
```

### 5️⃣ NOT Gate (Bitwise Complement)
```
Syntax: NOT wire -> target
Example: NOT x -> h
```

**Visual:**
```
Input x: 123    = 0000000001111011
                  ─────────────────  (Flip each bit)
Output h: 65412 = 1111111110000100

   ┌─────┐
x  │     │
──►│ NOT │
   └──┬──┘
      │
    h = 65412

Note: In 16-bit, NOT is calculated as: 65535 - value
      65535 - 123 = 65412
```

### 6️⃣ LSHIFT (Left Shift)
```
Syntax: wire LSHIFT amount -> target
Example: x LSHIFT 2 -> f
```

**Visual:**
```
Input x: 123 = 0000000001111011
               << Shift left by 2
               0000000111101100 = 492
               ──►──►
               New bits filled with 0

Bit movement:
Original: ...00001111011
Shift 2:  ...00111101100
              ▲▲
              New zeros added

Effect: Multiplies by 2^amount
        123 × 2² = 123 × 4 = 492
```

**Step-by-step:**
```
Step 0: 0000000001111011 = 123
        ↓
Step 1: 0000000011110110 = 246  (×2)
        ↓
Step 2: 0000000111101100 = 492  (×2)
```

### 7️⃣ RSHIFT (Right Shift)
```
Syntax: wire RSHIFT amount -> target
Example: y RSHIFT 2 -> g
```

**Visual:**
```
Input y: 456 = 0000000111001000
               >> Shift right by 2
               0000000001110010 = 114
                         ◄─◄─
               Bits fall off the right

Bit movement:
Original: 0000000111001000
Shift 2:  0000000001110010
                        ▼▼
                        Bits discarded

Effect: Integer division by 2^amount
        456 ÷ 2² = 456 ÷ 4 = 114
```

---

## 📋 Complete Example Walkthrough

### Given Circuit
```
123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i
```

### Execution Order (with Dependencies)

```
Step 1 & 2: Initialize constants
┌─────────────┐
│ 123 -> x    │  No dependencies → Execute immediately
│ 456 -> y    │  No dependencies → Execute immediately
└─────────────┘
Result: x = 123, y = 456

Step 3: x AND y -> d
┌─────────────────────────────┐
│ Requires: x = 123 ✓         │
│           y = 456 ✓         │
│ Both ready → Execute        │
└─────────────────────────────┘
  0000000001111011  (x = 123)
& 0000000111001000  (y = 456)
  ────────────────
  0000000001001000  (d = 72)

Step 4: x OR y -> e
┌─────────────────────────────┐
│ Requires: x = 123 ✓         │
│           y = 456 ✓         │
│ Both ready → Execute        │
└─────────────────────────────┘
  0000000001111011  (x = 123)
| 0000000111001000  (y = 456)
  ────────────────
  0000000111111011  (e = 507)

Step 5: x LSHIFT 2 -> f
┌─────────────────────────────┐
│ Requires: x = 123 ✓         │
│ Ready → Execute             │
└─────────────────────────────┘
  0000000001111011  (x = 123)
  << 2
  0000000111101100  (f = 492)

Step 6: y RSHIFT 2 -> g
┌─────────────────────────────┐
│ Requires: y = 456 ✓         │
│ Ready → Execute             │
└─────────────────────────────┘
  0000000111001000  (y = 456)
  >> 2
  0000000001110010  (g = 114)

Step 7: NOT x -> h
┌─────────────────────────────┐
│ Requires: x = 123 ✓         │
│ Ready → Execute             │
└─────────────────────────────┘
  0000000001111011  (x = 123)
  ~  (flip all bits)
  1111111110000100  (h = 65412)

Step 8: NOT y -> i
┌─────────────────────────────┐
│ Requires: y = 456 ✓         │
│ Ready → Execute             │
└─────────────────────────────┘
  0000000111001000  (y = 456)
  ~  (flip all bits)
  1111111000110111  (i = 65079)
```

### Final State
```
┌──────┬────────┐
│ Wire │ Signal │
├──────┼────────┤
│  x   │  123   │
│  y   │  456   │
│  d   │   72   │ ← x AND y
│  e   │  507   │ ← x OR y
│  f   │  492   │ ← x LSHIFT 2
│  g   │  114   │ ← y RSHIFT 2
│  h   │ 65412  │ ← NOT x
│  i   │ 65079  │ ← NOT y
└──────┴────────┘
```

---

## 🔄 Dependency Resolution

### Complex Dependency Example

From actual input: Finding signal on wire `a`
```
lx -> a          ← We want this!
  ↑
  Depends on lx
  
lw OR lv -> lx   ← Need to compute lx first
  ↑    ↑
  │    └─ Depends on lv
  └────── Depends on lw
  
lc LSHIFT 1 -> lw   ← Need to compute lw
  ↑
  Depends on lc
  
lb OR la -> lc   ← Need to compute lc
  ↑    ↑
  │    └─ Depends on la
  └────── Depends on lb
  
... and so on ...
```

### Dependency Tree Visualization
```
                    a
                    │
                   lx
                    │
            ┌───────┴───────┐
           lw              lv
            │               │
           lc         (more deps...)
            │
        ┌───┴───┐
       lb      la
        │       │
      (...)   (...)
```

**This is why we need MEMOIZATION!**
- Without caching: Recalculate same wires multiple times
- With caching: Calculate once, retrieve from cache

---

## 💾 Memoization Strategy

### Problem Without Memoization
```
Suppose wire 'x' is used in 100 different calculations.
Without cache: Recalculate x value 100 times!

Example:
  a AND x -> b    ← Calculate x
  c OR x -> d     ← Calculate x AGAIN
  x LSHIFT 1 -> e ← Calculate x AGAIN
  ... 97 more times ...
```

### Solution: Caching
```csharp
Dictionary<string, ushort> cache = new();

ushort GetSignal(string wire)
{
    // Check cache first!
    if (cache.ContainsKey(wire))
        return cache[wire];  // ← Return cached value
    
    // Calculate only if not cached
    ushort result = CalculateSignal(wire);
    
    // Store in cache
    cache[wire] = result;
    
    return result;
}
```

### Cache Hit Visualization
```
First call: GetSignal("x")
  ┌─────────────────────┐
  │ Cache: {}           │
  │ Not found!          │
  │ Calculate: x = 123  │
  │ Store: {"x": 123}   │
  └─────────────────────┘

Second call: GetSignal("x")
  ┌─────────────────────┐
  │ Cache: {"x": 123}   │
  │ Found! Return 123   │ ← No calculation needed!
  │ (Instant retrieval) │
  └─────────────────────┘
```

---

## 🧮 Parsing Instructions

### Instruction Patterns

#### Pattern 1: Direct Value
```
Input: "123 -> x"

Parse:
  ┌──────┐
  │ 123  │ → Value (ushort)
  └───┬──┘
      │
  ┌───▼──┐
  │  x   │ → Target wire
  └──────┘

Code:
if (ushort.TryParse(input, out ushort value))
    return value;
```

#### Pattern 2: Binary Operation
```
Input: "x AND y -> z"

Parse:
  ┌───┐   ┌───────┐   ┌───┐   ┌───┐   ┌───┐
  │ x │   │  AND  │   │ y │   │->│   │ z │
  └─┬─┘   └───┬───┘   └─┬─┘   └───┘   └─┬─┘
    │         │         │                │
    │         │         │                │
  Left    Operator   Right           Target
  wire               wire             wire

Code structure:
var parts = instruction.Split(' ');
string left = parts[0];
string operator = parts[1];
string right = parts[2];
string target = parts[4];
```

#### Pattern 3: NOT Operation
```
Input: "NOT x -> y"

Parse:
  ┌─────┐   ┌───┐   ┌───┐   ┌───┐
  │ NOT │   │ x │   │->│   │ y │
  └──┬──┘   └─┬─┘   └───┘   └─┬─┘
     │        │                │
  Operator  Wire           Target

Code structure:
var parts = instruction.Split(' ');
string operator = parts[0];  // "NOT"
string wire = parts[1];      // "x"
string target = parts[3];    // "y"
```

#### Pattern 4: Shift Operation
```
Input: "x LSHIFT 2 -> y"

Parse:
  ┌───┐   ┌────────┐   ┌───┐   ┌───┐   ┌───┐
  │ x │   │ LSHIFT │   │ 2 │   │->│   │ y │
  └─┬─┘   └────┬───┘   └─┬─┘   └───┘   └─┬─┘
    │          │         │                │
   Wire    Operator    Amount          Target

Code structure:
var parts = instruction.Split(' ');
string wire = parts[0];
string operator = parts[1];  // "LSHIFT" or "RSHIFT"
int amount = int.Parse(parts[2]);
string target = parts[4];
```

---

## 🔧 Algorithm Implementation

### Step-by-Step Algorithm

```
┌─────────────────────────────────────────┐
│ STEP 1: Parse all instructions         │
├─────────────────────────────────────────┤
│ Read input lines                        │
│ Store in Dictionary<string, string>     │
│   Key: target wire name                 │
│   Value: instruction string             │
│                                         │
│ Example:                                │
│   {"x": "123"}                          │
│   {"d": "x AND y"}                      │
│   {"a": "lx"}                           │
└─────────────────────────────────────────┘
          ↓
┌─────────────────────────────────────────┐
│ STEP 2: Create cache dictionary        │
├─────────────────────────────────────────┤
│ Dictionary<string, ushort> cache = {}   │
└─────────────────────────────────────────┘
          ↓
┌─────────────────────────────────────────┐
│ STEP 3: Call GetSignal("a")            │
├─────────────────────────────────────────┤
│ This triggers recursive resolution      │
│ of all dependencies                     │
└─────────────────────────────────────────┘
          ↓
┌─────────────────────────────────────────┐
│ STEP 4: Return final result            │
└─────────────────────────────────────────┘
```

### GetSignal Pseudocode
```python
function GetSignal(wireOrValue):
    # Case 1: Is it a number?
    if wireOrValue is numeric:
        return numeric_value
    
    # Case 2: Already computed?
    if wireOrValue in cache:
        return cache[wireOrValue]
    
    # Case 3: Compute it!
    instruction = circuit[wireOrValue]
    
    if instruction is "VALUE":
        result = VALUE
    
    elif instruction is "WIRE -> target":
        result = GetSignal(WIRE)  # Recursive!
    
    elif instruction is "A AND B -> target":
        left = GetSignal(A)       # Recursive!
        right = GetSignal(B)      # Recursive!
        result = left & right
    
    elif instruction is "A OR B -> target":
        left = GetSignal(A)
        right = GetSignal(B)
        result = left | right
    
    elif instruction is "NOT A -> target":
        value = GetSignal(A)
        result = ~value & 0xFFFF  # Keep 16-bit
    
    elif instruction is "A LSHIFT N -> target":
        value = GetSignal(A)
        result = value << N
    
    elif instruction is "A RSHIFT N -> target":
        value = GetSignal(A)
        result = value >> N
    
    # Cache the result
    cache[wireOrValue] = result
    return result
```

---

## 🐛 Common Pitfalls

### Pitfall 1: NOT Overflow
```csharp
// WRONG: Gives negative numbers!
ushort value = 123;
int result = ~value;
// Result: -124 (32-bit NOT)

// CORRECT: Keep only 16 bits
ushort value = 123;
ushort result = (ushort)(~value & 0xFFFF);
// Result: 65412

Alternative (even simpler):
ushort result = (ushort)~value;  // Cast truncates to 16 bits
```

**Visual explanation:**
```
32-bit NOT:
  00000000 00000000 00000000 01111011  (123)
  ~
  11111111 11111111 11111111 10000100  (-124 in signed)

16-bit NOT (what we want):
  0000000001111011  (123)
  ~
  1111111110000100  (65412)
  
Mask with 0xFFFF:
  11111111 11111111 11111111 10000100
& 00000000 00000000 11111111 11111111  (0xFFFF)
  ────────────────────────────────────
  00000000 00000000 11111111 10000100  (65412)
```

### Pitfall 2: Infinite Loop
```csharp
// If circuit has circular dependencies:
a -> b
b -> c
c -> a  ← Loop!

// GetSignal("a") would cause:
GetSignal("a")
  → GetSignal("b")
    → GetSignal("c")
      → GetSignal("a")  ← Back to start!
        → ... infinite recursion
```

**Solution:** The puzzle input is guaranteed to have no cycles!

### Pitfall 3: Forgetting to Cache
```csharp
// WRONG: Recalculates every time
ushort GetSignal(string wire)
{
    // Parse and calculate
    return result;  // Not cached!
}

// This wire might be calculated THOUSANDS of times!
```

**Performance comparison:**
```
Without cache: 
  Wire 'x' used 1000 times
  → 1000 calculations
  → Time: 1000 × T

With cache:
  Wire 'x' used 1000 times
  → 1 calculation + 999 cache hits
  → Time: T + (999 × 0.001T)
  → ~1000× faster!
```

### Pitfall 4: Number vs Wire Name
```csharp
// Input: "1 AND cx -> cy"
//         ↑ This is a NUMBER, not wire "1"!

// WRONG: Try to look up wire "1"
GetSignal("1")  // No such wire!

// CORRECT: Check if it's a number first
if (ushort.TryParse("1", out ushort value))
    return value;  // Return 1 directly
```

---

## 📊 Real Input Analysis

### Sample Instructions from Day7.txt

```
14146 -> b                # Large initial value
0 -> c                    # Zero initialization
1 AND cx -> cy            # Number in operation
NOT lk -> ll              # Simple NOT
af AND ah -> ai           # Two wires
x RSHIFT 5 -> aa          # Shift operation
lx -> a                   # Wire 'a' (our goal!)
```

### Dependency Count Example
```
To find 'a', you need 'lx'
To find 'lx', you need 'lw' and 'lv'
To find 'lw', you need 'lc'
To find 'lc', you need 'lb' and 'la'
... and so on

Total dependencies: 339 wires!
Without caching: Could require millions of calculations
With caching: Exactly 339 calculations
```

---

## 🎓 Part 2 Insight

Typically, Part 2 of Day 7 involves:

```
Part 1: Find signal on wire 'a'
        Result: [some value X]

Part 2: Override wire 'b' with value X
        Clear cache
        Recalculate signal on wire 'a'
        Result: [new value Y]
```

**Code pattern:**
```csharp
// Part 1
string part1Result = SolvePart1(input);

// Part 2
cache.Clear();  // Important: Clear cache!
circuit["b"] = part1Result;  // Override 'b'
string part2Result = GetSignal("a");
```

---

## 💡 Visual Memory Aid

### The Circuit as a Tree
```
Goal: Find 'a'

            [a]
             │
          ┌──┴──┐
        [lx]    ...
          │
      ┌───┴───┐
    [lw]     [lv]
      │        │
    [lc]     [...] 
      │
  ┌───┴───┐
 [lb]    [la]
  │       │
 ...     ...

Each node computes ONCE
Results cached
Reused by other branches
```

### Bitwise Operations Quick Reference
```
┌──────────┬─────────┬─────────────────┐
│ Operation│ Symbol  │ When result = 1 │
├──────────┼─────────┼─────────────────┤
│   AND    │    &    │ BOTH bits = 1   │
│   OR     │    |    │ ANY bit = 1     │
│   NOT    │    ~    │ Flip the bit    │
│  LSHIFT  │   <<    │ Move bits left  │
│  RSHIFT  │   >>    │ Move bits right │
└──────────┴─────────┴─────────────────┘
```

---

## 🧪 Test Your Understanding

### Question 1
```
Given: x = 5, y = 3
What is: x AND y?

x = 5 = 0000000000000101
y = 3 = 0000000000000011
        ────────────────
        0000000000000001 = ?
```

<details>
<summary>Answer</summary>

```
Result: 1

Only the rightmost bit is 1 in both numbers:
  0101
& 0011
  ────
  0001
```
</details>

### Question 2
```
Given: x = 8
What is: x LSHIFT 1?
```

<details>
<summary>Answer</summary>

```
Result: 16

x = 8 = 0000000000001000
Shift left by 1:
        0000000000010000 = 16

Effect: 8 × 2 = 16
```
</details>

### Question 3
```
Given: x = 10
What is: NOT x (in 16-bit)?
```

<details>
<summary>Answer</summary>

```
Result: 65525

x = 10 = 0000000000001010
~x     = 1111111111110101 = 65525

Quick calculation: 65535 - 10 = 65525
```
</details>

---

## 📝 Summary Checklist

**Before you code:**
- [ ] Understand the 5 instruction types
- [ ] Know how 16-bit signals work
- [ ] Understand dependency resolution
- [ ] Plan your caching strategy

**While coding:**
- [ ] Parse instructions into a dictionary
- [ ] Implement GetSignal() with recursion
- [ ] Always check cache first
- [ ] Handle both numbers and wire names
- [ ] Mask NOT operation to 16 bits
- [ ] Cache every computed value

**Testing:**
- [ ] Test with the example circuit first
- [ ] Verify each gate type works correctly
- [ ] Check that caching improves performance
- [ ] Ensure no infinite loops

---

**Remember:** This is a dependency resolution problem disguised as a circuit simulation! 🔌⚡

The key insight: **Memoization + Recursion = Elegant Solution** ✨
