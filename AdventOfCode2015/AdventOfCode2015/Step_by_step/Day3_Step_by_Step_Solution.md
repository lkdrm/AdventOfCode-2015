# Day 3: Perfectly Spherical Houses in a Vacuum - Step by Step Solution

## Step 1: Understanding the Problem

Santa delivers presents following directional instructions on an infinite 2D grid.

**Directions:**
- `^` = North (up)
- `v` = South (down)
- `>` = East (right)
- `<` = West (left)

**Part 1:** How many houses receive at least one present (Santa alone)?  
**Part 2:** How many houses receive at least one present (Santa + Robo-Santa alternating)?

**Starting position:** (0, 0) - this house gets a present at the start

---

## Step 2: Setting Up Coordinates

### Coordinate System:
```
        North (^)
           y+1
            |
West (<) ---+--- East (>)
   x-1      |      x+1
            |
         South (v)
           y-1

Starting point: (0, 0)
```

### Direction Mapping:
```csharp
(int dx, int dy) GetDirection(char move)
{
    return move switch
    {
        '^' => (0, 1),   // North: y increases
        'v' => (0, -1),  // South: y decreases
        '>' => (1, 0),   // East: x increases
        '<' => (-1, 0),  // West: x decreases
        _ => (0, 0)
    };
}
```

---

## Step 3: Implementing Part 1 - Santa Alone

### Algorithm:
1. Start at (0, 0) and mark it as visited
2. For each direction:
   - Update position
   - Mark new position as visited
3. Return count of unique positions

```csharp
int CountHousesVisited(string directions)
{
    HashSet<(int, int)> visited = new HashSet<(int, int)>();
    int x = 0, y = 0;
    
    // Starting house receives a present
    visited.Add((x, y));
    
    foreach (char move in directions)
    {
        // Update position based on direction
        var (dx, dy) = GetDirection(move);
        x += dx;
        y += dy;
        
        // Mark house as visited
        visited.Add((x, y));
    }
    
    return visited.Count;
}
```

### Why HashSet?
- Automatically handles uniqueness
- Adding duplicate coordinates doesn't increase count
- O(1) average time for add and contains operations

---

## Step 4: Trace Example Part 1

### Example: `">"`
```
Start: (0,0) ← Present delivered
       visited = {(0,0)}

Move 1: '>' (East)
  x = 0 + 1 = 1
  y = 0 + 0 = 0
  Position: (1,0) ← Present delivered
  visited = {(0,0), (1,0)}

Result: 2 houses ✓
```

### Example: `"^>v<"`
```
Start: (0,0)
  visited = {(0,0)}

Move 1: '^' (North)
  Position: (0,1)
  visited = {(0,0), (0,1)}

Move 2: '>' (East)
  Position: (1,1)
  visited = {(0,0), (0,1), (1,1)}

Move 3: 'v' (South)
  Position: (1,0)
  visited = {(0,0), (0,1), (1,1), (1,0)}

Move 4: '<' (West)
  Position: (0,0) ← Back to start!
  visited = {(0,0), (0,1), (1,1), (1,0)}
          (no change, already visited)

Result: 4 houses ✓

Grid visualization:
  (0,1)---(1,1)
    |       |
  (0,0)---(1,0)
  
Forms a square!
```

### Example: `"^v^v^v^v^v"`
```
Start: (0,0)

Move 1: '^' → (0,1)
Move 2: 'v' → (0,0) ← back to start
Move 3: '^' → (0,1) ← already visited
Move 4: 'v' → (0,0) ← already visited
... (repeats)

visited = {(0,0), (0,1)}

Result: 2 houses ✓

Santa keeps going back and forth!
```

---

## Step 5: Implementing Part 2 - Santa + Robo-Santa

### Algorithm:
1. Both start at (0, 0)
2. Take turns following directions:
   - Santa: indices 0, 2, 4, 6, ... (even)
   - Robo-Santa: indices 1, 3, 5, 7, ... (odd)
3. Count unique houses visited by either

```csharp
int CountHousesWithRoboSanta(string directions)
{
    HashSet<(int, int)> visited = new HashSet<(int, int)>();
    
    int santaX = 0, santaY = 0;
    int roboX = 0, roboY = 0;
    
    // Starting house receives presents from both
    visited.Add((0, 0));
    
    for (int i = 0; i < directions.Length; i++)
    {
        char move = directions[i];
        var (dx, dy) = GetDirection(move);
        
        if (i % 2 == 0)
        {
            // Santa's turn (even indices)
            santaX += dx;
            santaY += dy;
            visited.Add((santaX, santaY));
        }
        else
        {
            // Robo-Santa's turn (odd indices)
            roboX += dx;
            roboY += dy;
            visited.Add((roboX, roboY));
        }
    }
    
    return visited.Count;
}
```

---

## Step 6: Trace Example Part 2

### Example: `"^v"`
```
Start: Both at (0,0)
  visited = {(0,0)}

Index 0: '^' - Santa's turn
  Santa: (0,0) → (0,1)
  visited = {(0,0), (0,1)}

Index 1: 'v' - Robo-Santa's turn
  Robo: (0,0) → (0,-1)
  visited = {(0,0), (0,1), (0,-1)}

Result: 3 houses ✓

Grid:
    (0,1)  ← Santa
      |
    (0,0)  ← Starting point
      |
   (0,-1)  ← Robo-Santa
```

### Example: `"^>v<"`
```
Start: Both at (0,0)
  visited = {(0,0)}

Index 0: '^' - Santa
  Santa: (0,1)
  visited = {(0,0), (0,1)}

Index 1: '>' - Robo-Santa
  Robo: (1,0)
  visited = {(0,0), (0,1), (1,0)}

Index 2: 'v' - Santa
  Santa: (0,1) → (0,0) ← back to start
  visited = {(0,0), (0,1), (1,0)}

Index 3: '<' - Robo-Santa
  Robo: (1,0) → (0,0) ← back to start
  visited = {(0,0), (0,1), (1,0)}

Result: 3 houses ✓

Grid:
    (0,1)
      |
    (0,0)---(1,0)
    
Only 3 unique houses!
```

---

## Step 7: Complete Implementation

```csharp
public static class Day3
{
    public static string SolvePart1(string input)
    {
        int houses = CountHousesVisited(input.Trim());
        return houses.ToString();
    }

    public static string SolvePart2(string input)
    {
        int houses = CountHousesWithRoboSanta(input.Trim());
        return houses.ToString();
    }

    private static int CountHousesVisited(string directions)
    {
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        int x = 0, y = 0;
        
        visited.Add((x, y));
        
        foreach (char move in directions)
        {
            var (dx, dy) = GetDirection(move);
            x += dx;
            y += dy;
            visited.Add((x, y));
        }
        
        return visited.Count;
    }

    private static int CountHousesWithRoboSanta(string directions)
    {
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        int santaX = 0, santaY = 0;
        int roboX = 0, roboY = 0;
        
        visited.Add((0, 0));
        
        for (int i = 0; i < directions.Length; i++)
        {
            var (dx, dy) = GetDirection(directions[i]);
            
            if (i % 2 == 0)
            {
                santaX += dx;
                santaY += dy;
                visited.Add((santaX, santaY));
            }
            else
            {
                roboX += dx;
                roboY += dy;
                visited.Add((roboX, roboY));
            }
        }
        
        return visited.Count;
    }

    private static (int dx, int dy) GetDirection(char move)
    {
        return move switch
        {
            '^' => (0, 1),
            'v' => (0, -1),
            '>' => (1, 0),
            '<' => (-1, 0),
            _ => (0, 0)
        };
    }
}
```

---

## Step 8: Edge Cases

### Case 1: Empty String
```
Input: ""
Part 1: 1 house (starting position)
Part 2: 1 house (starting position)
```

### Case 2: Single Direction
```
Input: "^"
Part 1: 2 houses (start + one move)
Part 2: 2 houses (Santa moves, Robo stays)
```

### Case 3: Revisiting Many Times
```
Input: "^^^^vvvv"
Santa goes up 4, then down 4
Visits: (0,0), (0,1), (0,2), (0,3), (0,4), back to (0,0)
Result: 5 unique houses
```

---

## Step 9: Common Mistakes

### Mistake 1: Forgetting Starting House
```csharp
// WRONG - Doesn't count starting position
HashSet<(int, int)> visited = new HashSet<(int, int)>();
foreach (char move in directions)
{
    // ... move and add ...
}

// CORRECT - Add starting position first
visited.Add((0, 0));
```

### Mistake 2: Wrong Turn Assignment
```csharp
// WRONG - Both might move on same turn
if (i % 2 == 0)
    santaMove();
if (i % 2 == 1)  // Should be 'else'
    roboMove();

// CORRECT - Use else for alternation
if (i % 2 == 0)
    santaMove();
else
    roboMove();
```

---

## Step 10: Complexity Analysis

### Time Complexity:
- O(n) where n = number of moves
- Each move is O(1) operation
- HashSet add is O(1) average

### Space Complexity:
- O(k) where k = unique houses visited
- Worst case: O(n) if all moves visit new houses
- Best case: O(1) if always revisiting same house

---

## Step 11: Summary

**Key Concepts:**
- ✅ 2D coordinate system
- ✅ HashSet for tracking unique positions
- ✅ Direction mapping with tuples
- ✅ Alternating turns (even/odd indices)

**Algorithm Pattern:**
```
Part 1: Single agent tracking
Part 2: Multiple agent tracking with turn alternation
```

**Happy delivering! 🎄🎁**
