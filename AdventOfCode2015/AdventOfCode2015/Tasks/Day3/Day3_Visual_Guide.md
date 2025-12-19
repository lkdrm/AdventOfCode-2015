# 🏠 Day 3 Visual Guide - Infinite Grid Delivery

## 🗺️ Understanding the Grid System

### Coordinate System
```
        North (^)
           ↑
           │
West (<) ──┼── East (>)
           │
           ↓
        South (v)

Cartesian Plane:
         Y
         ↑
    ... -2 -1  0  1  2 ...
         │
─────────┼─────────────── X
         │
         0

Starting Point: (0, 0)
```

### Direction Mapping
```
┌────────┬────────┬────────┬────────┐
│ Symbol │ Name   │ X      │ Y      │
├────────┼────────┼────────┼────────┤
│   ^    │ North  │ +0     │ +1     │
│   v    │ South  │ +0     │ -1     │
│   >    │ East   │ +1     │ +0     │
│   <    │ West   │ -1     │ +0     │
└────────┴────────┴────────┴────────┘
```

---

## 📊 Part 1: Step-by-Step Examples

### Example 1: `>`
```
Instruction: > (go East)

Grid visualization:
    -1  0  1
   ┌───┬───┬───┐
 1 │   │   │   │
   ├───┼───┼───┤
 0 │   │🎅→│🎁 │  Santa moves right
   ├───┼───┼───┤
-1 │   │   │   │
   └───┴───┴───┘

Step-by-step:
Position 0: (0,0) 🎅 ← Start, deliver present #1
After '>': (1,0) 🎁 ← Deliver present #2

Visited houses: {(0,0), (1,0)}
Answer: 2 houses
```

### Example 2: `^>v<`
```
Instructions: ^ > v < (square pattern)

Timeline:
Step 0 - Start:     (0, 0)  🎅
Step 1 - ^:         (0, 1)  ↑
Step 2 - >:         (1, 1)  →
Step 3 - v:         (1, 0)  ↓
Step 4 - <:         (0, 0)  ← (back to start!)

Grid after all moves:
    -1  0  1  2
   ┌───┬───┬───┬───┐
 2 │   │   │   │   │
   ├───┼───┼───┼───┤
 1 │   │🎁②│🎁③│   │  ② and ③ delivered
   ├───┼───┼───┼───┤
 0 │   │🎅①│🎁④│   │  ① start, ④ delivered
   ├───┼───┼───┼───┤
-1 │   │   │   │   │
   └───┴───┴───┴───┘

Path visualization:
    0 ────→ 1
    ↑       ↓
    │       │
    └───────┘

Visited: {(0,0), (0,1), (1,1), (1,0)}
(0,0) visited TWICE but only counts ONCE!
Answer: 4 houses
```

### Example 3: `^v^v^v^v^v`
```
Instructions: Alternating North-South

Move-by-move:
Start:  (0, 0)  🎅
^:      (0, 1)  ↑
v:      (0, 0)  ↓ (back to start)
^:      (0, 1)  ↑ (revisit)
v:      (0, 0)  ↓ (revisit)
^:      (0, 1)  ↑ (revisit)
v:      (0, 0)  ↓ (revisit)
^:      (0, 1)  ↑ (revisit)
v:      (0, 0)  ↓ (revisit)
^:      (0, 1)  ↑ (revisit)

Grid view:
      0
   ┌─────┐
 2 │     │
   ├─────┤
 1 │ 🎁② │  ← Visited 5 times
   ├─────┤
 0 │ 🎅① │  ← Visited 5 times
   ├─────┤
-1 │     │
   └─────┘

Just oscillating between two houses!

Visited: {(0,0), (0,1)}
Answer: 2 houses (only unique locations count)
```

### Example 4: `>><< ` (Longer East-West)
```
Instructions: >> << (go East twice, then West twice)

Timeline:
Start:  (0, 0)  🎅
>:      (1, 0)  →
>:      (2, 0)  →
<:      (1, 0)  ← (revisit)
<:      (0, 0)  ← (back to start)

Grid visualization:
      0   1   2
   ┌───┬───┬───┐
 0 │🎅①│🎁②│🎁③│
   └───┴───┴───┘

Path: 0→1→2→1→0
       ①──②──③
         ↺──↺

Visited: {(0,0), (1,0), (2,0)}
Answer: 3 houses
```

---

## 🤖 Part 2: Santa & Robo-Santa

### Understanding Turn Alternation
```
Input string:  ^  v  ^  >  <  v
Index:         0  1  2  3  4  5
Turn:          S  R  S  R  S  R

S = Santa's turn
R = Robo-Santa's turn

Pattern: Even indices (0,2,4,...) → Santa
         Odd indices  (1,3,5,...) → Robo-Santa
```

---

### Example 1: `^v`
```
Input: ^ v

Move breakdown:
┌─────┬──────┬────────┬──────────────┬──────────────┐
│ Idx │ Char │ Who?   │ From         │ To           │
├─────┼──────┼────────┼──────────────┼──────────────┤
│  -  │  -   │ Both   │ (0,0) START  │ (0,0) START  │
│  0  │  ^   │ Santa  │ (0,0)        │ (0,1)        │
│  1  │  v   │ Robo   │ (0,0)        │ (0,-1)       │
└─────┴──────┴────────┴──────────────┴──────────────┘

Grid visualization:
       0
   ┌──────┐
 1 │  🎅  │  ← Santa went North
   ├──────┤
 0 │  ⭐  │  ← Both started here (2 presents!)
   ├──────┤
-1 │  🤖  │  ← Robo-Santa went South
   └──────┘

Visited houses:
- (0, 0): START (both deliver)
- (0, 1): Santa
- (0,-1): Robo-Santa

Answer: 3 houses
```

### Example 2: `^>v<`
```
Input: ^ > v <

Move breakdown:
┌─────┬──────┬────────┬──────────┬──────────┐
│ Idx │ Char │ Who?   │ Position │ Delivers │
├─────┼──────┼────────┼──────────┼──────────┤
│  -  │  -   │ Both   │ (0,0)    │ 2x here  │
│  0  │  ^   │ Santa  │ (0,1)    │ 🎅       │
│  1  │  >   │ Robo   │ (1,0)    │ 🤖       │
│  2  │  v   │ Santa  │ (0,0)    │ (again)  │
│  3  │  <   │ Robo   │ (0,0)    │ (again)  │
└─────┴──────┴────────┴──────────┴──────────┘

Santa's path:
(0,0) → (0,1) → (0,0)
  ①      ②      ③

Robo-Santa's path:
(0,0) → (1,0) → (0,0)
  ①      ②      ③

Grid visualization:
    0   1
  ┌───┬───┐
1 │🎅②│   │  ← Santa went up
  ├───┼───┤
0 │⭐①│🤖②│  ← Both started, Robo went right
  │③④ │   │  ← Both returned here
  └───┴───┘

Unique houses visited: {(0,0), (0,1), (1,0)}
Answer: 3 houses
```

### Example 3: `^v^v^v^v^v`
```
Input: ^v^v^v^v^v (10 moves)

Turn assignment:
Index: 0  1  2  3  4  5  6  7  8  9
Move:  ^  v  ^  v  ^  v  ^  v  ^  v
Who:   S  R  S  R  S  R  S  R  S  R

Santa's moves (even indices):
Start: (0, 0)
   ^:  (0, 1)
   ^:  (0, 2)
   ^:  (0, 3)
   ^:  (0, 4)
   ^:  (0, 5)

Robo-Santa's moves (odd indices):
Start: (0, 0)
   v:  (0, -1)
   v:  (0, -2)
   v:  (0, -3)
   v:  (0, -4)
   v:  (0, -5)

Grid visualization:
       0
   ┌──────┐
 5 │  🎅  │
 4 │  🎅  │
 3 │  🎅  │
 2 │  🎅  │
 1 │  🎅  │  Santa goes North
   ├──────┤
 0 │  ⭐  │  Both start here
   ├──────┤
-1 │  🤖  │
-2 │  🤖  │
-3 │  🤖  │  Robo-Santa goes South
-4 │  🤖  │
-5 │  🤖  │
   └──────┘

Visited houses:
Santa:      {(0,0), (0,1), (0,2), (0,3), (0,4), (0,5)} = 6
Robo-Santa: {(0,0), (0,-1), (0,-2), (0,-3), (0,-4), (0,-5)} = 6
Combined:   11 unique houses (they share (0,0))

Answer: 11 houses
```

---

## 💻 Implementation Details

### Using HashSet for Uniqueness
```csharp
// HashSet automatically handles duplicates
HashSet<Position> visited = new();

// Adding same position multiple times
visited.Add((0, 0)); // Returns true (added)
visited.Add((1, 0)); // Returns true (added)
visited.Add((0, 0)); // Returns false (already exists)

// Final count ignores duplicates
Console.WriteLine(visited.Count); // 2, not 3!
```

### Position Record Struct
```csharp
record struct Position(int X, int Y);

// Why record struct?
// 1. Value equality (compares by content)
Position p1 = new(0, 0);
Position p2 = new(0, 0);
Console.WriteLine(p1 == p2); // True!

// 2. Works perfectly with HashSet
HashSet<Position> set = new();
set.Add(new(0, 0));
set.Add(new(0, 0)); // Recognized as duplicate
```

---

## 🔄 Detailed Algorithm Traces

### Part 1: Single Santa
```csharp
string input = "^>v<";
Position current = new(0, 0);
HashSet<Position> visited = new() { current };

Processing each character:
┌─────┬──────┬─────────────┬──────────────┬───────┐
│ Step│ Char │ Current     │ New Position │ Count │
├─────┼──────┼─────────────┼──────────────┼───────┤
│ 0   │ -    │ (0,0) START │ -            │ 1     │
│ 1   │ ^    │ (0,0)       │ (0,1)        │ 2     │
│ 2   │ >    │ (0,1)       │ (1,1)        │ 3     │
│ 3   │ v    │ (1,1)       │ (1,0)        │ 4     │
│ 4   │ <    │ (1,0)       │ (0,0)        │ 4*    │
└─────┴──────┴─────────────┴──────────────┴───────┘
* Count stays 4 because (0,0) already visited

Final answer: visited.Count = 4
```

### Part 2: Santa + Robo-Santa
```csharp
string input = "^v^>";
Position santa = new(0, 0);
Position robo = new(0, 0);
HashSet<Position> visited = new() { new(0, 0) };

Processing:
┌─────┬──────┬────────┬────────────┬────────────┬───────┐
│ Idx │ Char │ Turn   │ Santa      │ Robo       │ Count │
├─────┼──────┼────────┼────────────┼────────────┼───────┤
│  -  │  -   │ START  │ (0,0)      │ (0,0)      │ 1     │
│  0  │  ^   │ Even   │ (0,1) ✓    │ (0,0)      │ 2     │
│  1  │  v   │ Odd    │ (0,1)      │ (0,-1) ✓   │ 3     │
│  2  │  ^   │ Even   │ (0,2) ✓    │ (0,-1)     │ 4     │
│  3  │  >   │ Odd    │ (0,2)      │ (1,-1) ✓   │ 5     │
└─────┴──────┴────────┴────────────┴────────────┴───────┘
✓ = New house added

Final answer: visited.Count = 5
```

---

## 🎨 Complex Pattern Example

### Input: `^>v<>^<v` (Figure-8 pattern)

**Part 1: Solo Santa**
```
Trace:
(0,0) → (0,1) → (1,1) → (1,0) → (0,0) →
(1,0) → (1,1) → (0,1) → (0,0)

Grid:
    0   1
  ┌───┬───┐
1 │ 🎁│🎁 │  Visited (0,1) and (1,1)
  ├───┼───┤
0 │ 🎅│🎁 │  Visited (0,0) and (1,0)
  └───┴───┘

Unique: {(0,0), (0,1), (1,1), (1,0)} = 4 houses
```

**Part 2: Santa + Robo-Santa**
```
Santa takes:   ^  v  >  v  (indices 0,2,4,6)
Robo takes:     >  <  ^  <  (indices 1,3,5,7)

Santa's path:
(0,0) → (0,1) → (0,0) → (1,0) → (1,-1)
  ①      ②      ③      ④       ⑤

Robo's path:
(0,0) → (1,0) → (0,0) → (0,1) → (-1,1)
  ①      ②      ③      ④       ⑤

Combined grid:
   -1  0   1
  ┌───┬───┬───┐
1 │🤖⑤│🎁 │   │  Robo visits (-1,1), both visit (0,1)
  ├───┼───┼───┤
0 │   │⭐ │🎁 │  Both visit (0,0) and (1,0)
  ├───┼───┼───┤
-1│   │   │🎅⑤│  Santa visits (1,-1)
  └───┴───┴───┘

Unique houses:
Santa:     {(0,0), (0,1), (1,0), (1,-1)} = 4
Robo:      {(0,0), (1,0), (0,1), (-1,1)} = 4
Combined:  {(0,0), (0,1), (1,0), (1,-1), (-1,1)} = 5

Answer: 5 houses
```

---

## 🐛 Common Mistakes

### Mistake 1: Not Starting at Origin
```csharp
// WRONG - Forgetting initial delivery
Position current = new(0, 0);
HashSet<Position> visited = new(); // Empty!
foreach (char c in input)
{
    Move(ref current, c);
    visited.Add(current);
}
// Missing the starting house!

// CORRECT - Start includes first delivery
Position current = new(0, 0);
HashSet<Position> visited = new() { current };
foreach (char c in input)
{
    Move(ref current, c);
    visited.Add(current);
}
```

### Mistake 2: Wrong Turn Assignment
```csharp
// WRONG - Using modulo incorrectly
for (int i = 0; i < input.Length; i++)
{
    if (i % 2 == 1) // Odd means Santa?
        MoveSanta(input[i]);
    else
        MoveRobo(input[i]);
}
// This swaps Santa and Robo!

// CORRECT - Even indices for Santa
for (int i = 0; i < input.Length; i++)
{
    if (i % 2 == 0) // Even = Santa
        MoveSanta(input[i]);
    else // Odd = Robo
        MoveRobo(input[i]);
}
```

### Mistake 3: Reference vs Value Types
```csharp
// WRONG - Using class (reference type)
class Position 
{
    public int X, Y;
}
// Two new(0,0) are different objects!

// CORRECT - Using record struct (value type)
record struct Position(int X, int Y);
// Two new(0,0) are considered equal!
```

---

## 📈 Complexity Analysis

### Part 1
```
Time:  O(n) where n = length of input
Space: O(k) where k = unique houses visited
       Worst case: O(n) if all houses unique
       Best case:  O(1) if staying in one spot
```

### Part 2
```
Time:  O(n) where n = length of input
Space: O(k) where k = unique houses visited
       Worst case: O(n) if Santa and Robo never overlap
       Best case:  O(n/2) if they follow same path
```

---

## 🎯 Quick Reference

### Direction Vectors
```
    North (^): (0, +1)
    South (v): (0, -1)
    East  (>): (+1, 0)
    West  (<): (-1, 0)
```

### Key Insights
```
Part 1:
✓ One person delivering
✓ Process sequentially
✓ Track unique positions

Part 2:
✓ Two people delivering
✓ Alternate between them
✓ Both start at (0,0)
✓ Share the same visited set
```

### Memory Aids
```
🎅 Santa = Even indices (0, 2, 4, ...)
🤖 Robo  = Odd indices  (1, 3, 5, ...)

^  v  ^  v
S  R  S  R
```

---

## 📝 Summary

**Part 1 Key Points:**
1. 🎅 Single delivery person
2. 📍 Track each position visited
3. 🏠 Count unique houses (use HashSet)
4. ⭐ Don't forget starting position

**Part 2 Key Points:**
1. 🎅🤖 Two delivery people
2. 🔄 Alternate turns (even/odd)
3. 🏠 Both share same visited set
4. ⭐ Both start and deliver at (0,0)

**Data Structure Choice:**
- 📦 **Position**: record struct for value equality
- 🗂️ **Visited**: HashSet for automatic deduplication
- 🧮 **Count**: visited.Count gives answer

---

**Happy delivering! 🎁🎄**
