# 💡 Day 6 Visual Guide - Step-by-Step Examples

## 📐 Understanding Coordinates

### Grid Layout
```
     0   1   2   3   4   5  ...  999  (X-axis →)
  ┌───┬───┬───┬───┬───┬───┬───┬───┐
0 │   │   │   │   │   │   │...│   │
  ├───┼───┼───┼───┼───┼───┼───┼───┤
1 │   │   │   │   │   │   │...│   │
  ├───┼───┼───┼───┼───┼───┼───┼───┤
2 │   │   │   │   │   │   │...│   │
  ├───┼───┼───┼───┼───┼───┼───┼───┤
3 │   │   │   │   │   │   │...│   │
  :                             :
999│   │   │   │   │   │   │...│   │
  └───┴───┴───┴───┴───┴───┴───┴───┘
(Y-axis ↓)
```

**Key Point:** The coordinate `(x, y)` means:
- `x` = column number (left to right)
- `y` = row number (top to bottom)
- But in code we use `array[y, x]` (row first, then column)

---

## 📏 Rectangle Size Calculation

### Formula
```
Width = x2 - x1 + 1
Height = y2 - y1 + 1
Total Lights = Width × Height
```

### Examples

#### Example 1: `0,0 through 2,2`
```
Coordinates: (0,0) to (2,2)
Width  = 2 - 0 + 1 = 3
Height = 2 - 0 + 1 = 3
Total  = 3 × 3 = 9 lights

Visual:
  0 1 2
0 ■ ■ ■
1 ■ ■ ■
2 ■ ■ ■
```

#### Example 2: `5,10 through 8,12`
```
Coordinates: (5,10) to (8,12)
Width  = 8 - 5 + 1 = 4
Height = 12 - 10 + 1 = 3
Total  = 4 × 3 = 12 lights

Visual:
    5 6 7 8
10  ■ ■ ■ ■
11  ■ ■ ■ ■
12  ■ ■ ■ ■
```

#### Example 3: `100,100 through 100,100`
```
Coordinates: (100,100) to (100,100)
Width  = 100 - 100 + 1 = 1
Height = 100 - 100 + 1 = 1
Total  = 1 × 1 = 1 light (single point!)
```

---

## 🔄 Command Execution Walkthrough

Let's trace through a **5×5 grid** with Part 1 rules:

### Initial State
```
All lights OFF (represented by .)

  0 1 2 3 4
0 . . . . .
1 . . . . .
2 . . . . .
3 . . . . .
4 . . . . .

Lights ON: 0
```

---

### Step 1: `turn on 1,1 through 3,3`

**Processing:**
```csharp
for (y = 1; y <= 3; y++)
    for (x = 1; x <= 3; x++)
        lights[y, x] = true;
```

**Result:**
```
  0 1 2 3 4
0 . . . . .
1 . ■ ■ ■ .
2 . ■ ■ ■ .
3 . ■ ■ ■ .
4 . . . . .

Lights ON: 9 (3×3 square)
```

---

### Step 2: `toggle 2,0 through 4,2`

**Processing:**
```csharp
for (y = 0; y <= 2; y++)
    for (x = 2; x <= 4; x++)
        lights[y, x] = !lights[y, x];
```

**Before toggle:**
```
  0 1 2 3 4
0 . . . . .     ← Rows 0-2 will be affected
1 . ■ ■ ■ .     ← Rows 0-2 will be affected
2 . ■ ■ ■ .     ← Rows 0-2 will be affected
3 . ■ ■ ■ .
4 . . . . .
      ↑ ↑ ↑
    Columns 2-4 affected
```

**After toggle:**
```
  0 1 2 3 4
0 . . ■ ■ ■     ← . → ■ (OFF → ON)
1 . ■ . . ■     ← ■ → . (ON → OFF)
2 . ■ . . ■     ← ■ → . (ON → OFF)
3 . ■ ■ ■ .     ← No change
4 . . . . .     ← No change

Changes:
- (2,0), (3,0), (4,0): OFF → ON  (+3)
- (2,1), (3,1): ON → OFF         (-2)
- (2,2), (3,2): ON → OFF         (-2)
- (4,1), (4,2): OFF → ON         (+2)

Lights ON: 9 - 4 + 5 = 10
```

---

### Step 3: `turn off 0,0 through 2,4`

**Processing:**
```csharp
for (y = 0; y <= 4; y++)
    for (x = 0; x <= 2; x++)
        lights[y, x] = false;
```

**Before:**
```
  0 1 2 3 4
0 . . ■ ■ ■
1 . ■ . . ■
2 . ■ . . ■
3 . ■ ■ ■ .
4 . . . . .
↑ ↑ ↑
Columns 0-2 will be turned OFF
```

**After:**
```
  0 1 2 3 4
0 . . . ■ ■     ← (2,0) was ON, now OFF
1 . . . . ■     ← (1,1) was ON, now OFF
2 . . . . ■     ← (1,2) was ON, now OFF
3 . . . ■ .     ← (1,3) was ON, now OFF
4 . . . . .     ← No change

Lights ON: 10 - 4 = 6
```

---

## 💡 Part 2: Brightness Example

Same commands, but with **brightness levels** instead:

### Initial State
```
All lights at brightness 0

  0 1 2 3 4
0 0 0 0 0 0
1 0 0 0 0 0
2 0 0 0 0 0
3 0 0 0 0 0
4 0 0 0 0 0

Total Brightness: 0
```

---

### Step 1: `turn on 1,1 through 3,3`
**Rule:** Increase brightness by 1

```
  0 1 2 3 4
0 0 0 0 0 0
1 0 1 1 1 0
2 0 1 1 1 0
3 0 1 1 1 0
4 0 0 0 0 0

Total Brightness: 9 (nine lights at 1 each)
```

---

### Step 2: `toggle 2,0 through 4,2`
**Rule:** Increase brightness by 2

```
  0 1 2 3 4
0 0 0 2 2 2     ← 0+2=2
1 0 1 3 3 2     ← Center: 1+2=3
2 0 1 3 3 2     ← Center: 1+2=3
3 0 1 1 1 0     ← No change
4 0 0 0 0 0     ← No change

Total Brightness:
  Previous: 9
  Added: (3×2) + (4×2) + (2×2) = 6 + 8 + 4 = 18
  New Total: 27
```

**Breakdown:**
- 3 lights: 0→2 (added 6)
- 4 lights: 1→3 (added 8)
- 2 lights: 0→2 (added 4)

---

### Step 3: `turn off 0,0 through 2,4`
**Rule:** Decrease brightness by 1 (min 0)

```
  0 1 2 3 4
0 0 0 1 2 2     ← 2-1=1
1 0 0 2 3 2     ← 1-1=0, 3-1=2
2 0 0 2 3 2     ← 1-1=0, 3-1=2
3 0 0 0 1 0     ← 1-1=0
4 0 0 0 0 0     ← Already 0

Total Brightness:
  1×4 + 2×4 + 3×2 + 2×2 = 4 + 8 + 6 + 4 = 22
```

---

## 💻 Code Trace Example

Let's trace the parsing and execution:

### Input Line
```
"turn on 887,9 through 959,629"
```

### Parsing Step
```csharp
var match = Regex.Match(instruction, @"(\d+),(\d+) through (\d+),(\d+)");

// Captures:
// Group[1] = "887"   → x1 = 887
// Group[2] = "9"     → y1 = 9
// Group[3] = "959"   → x2 = 959
// Group[4] = "629"   → y2 = 629

command = "turn on"  // From string prefix check
```

### Rectangle Size
```
Width  = 959 - 887 + 1 = 73
Height = 629 - 9 + 1 = 621
Total  = 73 × 621 = 45,333 lights affected!
```

### Execution Loop
```csharp
for (int y = 9; y <= 629; y++)        // 621 iterations
    for (int x = 887; x <= 959; x++)  // 73 iterations each
        lights[y, x] = true;           // Turn ON
        
// Total operations: 621 × 73 = 45,333
```

---

## 🗃️ Memory Layout Visualization

### How the 2D Array Stores Data
```csharp
bool[,] lights = new bool[1000, 1000];
```

**Conceptual layout:**
```
Memory address:  [0,0] [0,1] [0,2] ... [0,999] [1,0] [1,1] ...

Logical grid:
Row 0: lights[0,0] lights[0,1] ... lights[0,999]
Row 1: lights[1,0] lights[1,1] ... lights[1,999]
...
Row 999: lights[999,0] lights[999,1] ... lights[999,999]
```

**Access pattern matters for performance:**
```csharp
// GOOD - Sequential memory access (cache-friendly)
for (int y = 0; y < 1000; y++)
    for (int x = 0; x < 1000; x++)
        process(lights[y, x]);

// BAD - Strided memory access (cache-unfriendly)
for (int x = 0; x < 1000; x++)
    for (int y = 0; y < 1000; y++)
        process(lights[y, x]);
```

---

## 🐛 Common Bugs - Visual Examples

### Bug 1: Off-by-One Error
```csharp
// WRONG: Uses < instead of <=
for (int y = y1; y < y2; y++)  // Misses y2!
    for (int x = x1; x < x2; x++)  // Misses x2!
        lights[y, x] = true;
```

**Example:** `turn on 0,0 through 2,2`
```
Expected (9 lights):     Got (4 lights):
  0 1 2                    0 1 2
0 ■ ■ ■                  0 ■ ■ .
1 ■ ■ ■                  1 ■ ■ .
2 ■ ■ ■                  2 . . .
```

### Bug 2: Swapped Coordinates
```csharp
// WRONG: x and y swapped
lights[x, y] = true;  // Should be lights[y, x]
```

**Result:** Operations happen at wrong locations!

### Bug 3: Negative Brightness
```csharp
// WRONG: Brightness can go negative
lights[y, x] -= 1;

// If lights[y,x] was 0:
0 - 1 = -1  // Invalid brightness!

// CORRECT:
lights[y, x] = Math.Max(0, lights[y, x] - 1);
```

---

## 📝 Summary

**Key Points to Remember:**
1. 📍 Coordinates are **inclusive** (both start and end)
2. ✓ Use `<=` not `<` in loops
3. 📊 Array access is `[row, column]` or `[y, x]`
4. 📏 Rectangle size = `(x2-x1+1) × (y2-y1+1)`
5. 💡 Part 2 brightness can't go below 0

**Visual Reminder:**
```
turn on 2,1 through 4,3 means:

    0 1 2 3 4 5
  ┌───┬───┬───┬───┬───┬───┐
0 │   │   │   │   │   │   │
  ├───┼───┼───┼───┼───┼───┤
1 │   │   │ ■ │ ■ │ ■ │   │  ← Start y=1
  ├───┼───┼───┼───┼───┼───┤
2 │   │   │ ■ │ ■ │ ■ │   │
  ├───┼───┼───┼───┼───┼───┤
3 │   │   │ ■ │ ■ │ ■ │   │  ← End y=3
  ├───┼───┼───┼───┼───┼───┤
4 │   │   │   │   │   │   │
  └───┴───┴───┴───┴───┴───┘
            ↑       ↑
         x=2     x=4

3 columns × 3 rows = 9 lights
```

---

**Practice makes perfect! Try running the code and watch the lights change! 💡**
