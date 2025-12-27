# Day 6: Probably a Fire Hazard - Step by Step Solution

## Step 1: Understanding the Problem

Control a 1000×1000 grid of lights based on instructions.

**Part 1 - Boolean Lights:**
- `turn on x1,y1 through x2,y2` → Turn on all lights in rectangle
- `turn off x1,y1 through x2,y2` → Turn off all lights in rectangle
- `toggle x1,y1 through x2,y2` → Toggle all lights in rectangle
- Count how many lights are ON after all instructions

**Part 2 - Brightness Lights:**
- `turn on` → Increase brightness by 1
- `turn off` → Decrease brightness by 1 (minimum 0)
- `toggle` → Increase brightness by 2
- Calculate total brightness after all instructions

---

## Step 2: Setting Up the Grid

```csharp
// Part 1: Boolean grid
bool[,] grid = new bool[1000, 1000];

// Part 2: Brightness grid
int[,] brightness = new int[1000, 1000];
```

---

## Step 3: Parsing Instructions

```csharp
enum Command { TurnOn, TurnOff, Toggle }

(Command cmd, int x1, int y1, int x2, int y2) ParseInstruction(string line)
{
    Command cmd;
    string[] parts;
    
    if (line.StartsWith("turn on"))
    {
        cmd = Command.TurnOn;
        parts = line.Substring(8).Split(new[] { " through ", "," }, 
                                         StringSplitOptions.None);
    }
    else if (line.StartsWith("turn off"))
    {
        cmd = Command.TurnOff;
        parts = line.Substring(9).Split(new[] { " through ", "," }, 
                                         StringSplitOptions.None);
    }
    else // toggle
    {
        cmd = Command.Toggle;
        parts = line.Substring(7).Split(new[] { " through ", "," }, 
                                         StringSplitOptions.None);
    }
    
    int x1 = int.Parse(parts[0]);
    int y1 = int.Parse(parts[1]);
    int x2 = int.Parse(parts[2]);
    int y2 = int.Parse(parts[3]);
    
    return (cmd, x1, y1, x2, y2);
}
```

### Example:
```
"turn on 0,0 through 999,999"
→ (TurnOn, 0, 0, 999, 999)

"toggle 0,0 through 999,0"
→ (Toggle, 0, 0, 999, 0)

"turn off 499,499 through 500,500"
→ (TurnOff, 499, 499, 500, 500)
```

---

## Step 4: Implementing Part 1

```csharp
int SolvePart1(string input)
{
    bool[,] grid = new bool[1000, 1000];
    
    foreach (string line in input.Split('\n'))
    {
        if (string.IsNullOrWhiteSpace(line))
            continue;
            
        var (cmd, x1, y1, x2, y2) = ParseInstruction(line.Trim());
        
        for (int x = x1; x <= x2; x++)
        {
            for (int y = y1; y <= y2; y++)
            {
                switch (cmd)
                {
                    case Command.TurnOn:
                        grid[x, y] = true;
                        break;
                    case Command.TurnOff:
                        grid[x, y] = false;
                        break;
                    case Command.Toggle:
                        grid[x, y] = !grid[x, y];
                        break;
                }
            }
        }
    }
    
    // Count lights that are on
    int count = 0;
    for (int x = 0; x < 1000; x++)
    {
        for (int y = 0; y < 1000; y++)
        {
            if (grid[x, y])
                count++;
        }
    }
    
    return count;
}
```

---

## Step 5: Example Trace Part 1

### Example: "turn on 0,0 through 999,999"
```
Affects: All 1,000,000 lights
Action: Turn all ON
Result: 1,000,000 lights lit ✓
```

### Example: "toggle 0,0 through 999,0"
```
Starting from all OFF:
Affects: First row (1,000 lights)
Action: Toggle (OFF → ON)
Result: 1,000 lights lit ✓
```

### Example: "turn off 499,499 through 500,500"
```
Affects: 2×2 square = 4 lights
  (499,499), (499,500)
  (500,499), (500,500)
  
Starting from all ON:
Action: Turn OFF
Result: 1,000,000 - 4 = 999,996 lights lit ✓
```

---

## Step 6: Implementing Part 2

```csharp
int SolvePart2(string input)
{
    int[,] brightness = new int[1000, 1000];
    
    foreach (string line in input.Split('\n'))
    {
        if (string.IsNullOrWhiteSpace(line))
            continue;
            
        var (cmd, x1, y1, x2, y2) = ParseInstruction(line.Trim());
        
        for (int x = x1; x <= x2; x++)
        {
            for (int y = y1; y <= y2; y++)
            {
                switch (cmd)
                {
                    case Command.TurnOn:
                        brightness[x, y]++;
                        break;
                    case Command.TurnOff:
                        if (brightness[x, y] > 0)
                            brightness[x, y]--;
                        break;
                    case Command.Toggle:
                        brightness[x, y] += 2;
                        break;
                }
            }
        }
    }
    
    // Calculate total brightness
    int total = 0;
    for (int x = 0; x < 1000; x++)
    {
        for (int y = 0; y < 1000; y++)
        {
            total += brightness[x, y];
        }
    }
    
    return total;
}
```

---

## Step 7: Example Trace Part 2

### Example: "turn on 0,0 through 0,0"
```
Single light at (0,0)
Action: Increase by 1
Brightness: 0 → 1
Total: 1 ✓
```

### Example: "toggle 0,0 through 999,999"
```
All 1,000,000 lights
Action: Increase each by 2
Total brightness: 2,000,000 ✓
```

---

## Step 8: Complete Implementation

```csharp
public static class Day6
{
    private enum Command { TurnOn, TurnOff, Toggle }

    public static string SolvePart1(string input)
    {
        bool[,] grid = new bool[1000, 1000];
        
        foreach (string line in input.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            var (cmd, x1, y1, x2, y2) = ParseInstruction(line.Trim());
            ApplyCommandPart1(grid, cmd, x1, y1, x2, y2);
        }
        
        return CountLightsOn(grid).ToString();
    }

    public static string SolvePart2(string input)
    {
        int[,] brightness = new int[1000, 1000];
        
        foreach (string line in input.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            var (cmd, x1, y1, x2, y2) = ParseInstruction(line.Trim());
            ApplyCommandPart2(brightness, cmd, x1, y1, x2, y2);
        }
        
        return CalculateTotalBrightness(brightness).ToString();
    }

    private static void ApplyCommandPart1(bool[,] grid, Command cmd, 
                                          int x1, int y1, int x2, int y2)
    {
        for (int x = x1; x <= x2; x++)
        {
            for (int y = y1; y <= y2; y++)
            {
                grid[x, y] = cmd switch
                {
                    Command.TurnOn => true,
                    Command.TurnOff => false,
                    Command.Toggle => !grid[x, y],
                    _ => grid[x, y]
                };
            }
        }
    }

    private static void ApplyCommandPart2(int[,] brightness, Command cmd, 
                                          int x1, int y1, int x2, int y2)
    {
        for (int x = x1; x <= x2; x++)
        {
            for (int y = y1; y <= y2; y++)
            {
                switch (cmd)
                {
                    case Command.TurnOn:
                        brightness[x, y]++;
                        break;
                    case Command.TurnOff:
                        if (brightness[x, y] > 0)
                            brightness[x, y]--;
                        break;
                    case Command.Toggle:
                        brightness[x, y] += 2;
                        break;
                }
            }
        }
    }

    private static (Command, int, int, int, int) ParseInstruction(string line)
    {
        Command cmd;
        string[] parts;
        
        if (line.StartsWith("turn on"))
        {
            cmd = Command.TurnOn;
            line = line.Substring(8);
        }
        else if (line.StartsWith("turn off"))
        {
            cmd = Command.TurnOff;
            line = line.Substring(9);
        }
        else
        {
            cmd = Command.Toggle;
            line = line.Substring(7);
        }
        
        parts = line.Split(new[] { " through ", "," }, 
                           StringSplitOptions.RemoveEmptyEntries);
        
        return (cmd, int.Parse(parts[0]), int.Parse(parts[1]), 
                int.Parse(parts[2]), int.Parse(parts[3]));
    }

    private static int CountLightsOn(bool[,] grid)
    {
        int count = 0;
        for (int x = 0; x < 1000; x++)
            for (int y = 0; y < 1000; y++)
                if (grid[x, y]) count++;
        return count;
    }

    private static int CalculateTotalBrightness(int[,] brightness)
    {
        int total = 0;
        for (int x = 0; x < 1000; x++)
            for (int y = 0; y < 1000; y++)
                total += brightness[x, y];
        return total;
    }
}
```

---

## Step 9: Common Mistakes

### Mistake 1: Off-by-One in Range
```csharp
// WRONG - Misses last coordinate
for (int x = x1; x < x2; x++)

// CORRECT - Includes both endpoints
for (int x = x1; x <= x2; x++)
```

### Mistake 2: Brightness Going Negative
```csharp
// WRONG - Allows negative
brightness[x, y]--;

// CORRECT - Minimum is 0
if (brightness[x, y] > 0)
    brightness[x, y]--;
```

---

## Step 10: Complexity Analysis

### Time Complexity:
- Per instruction: O(w × h) where w, h = rectangle dimensions
- Worst case per instruction: O(1,000,000)
- Total: O(n × m) where n = instructions, m = lights affected

### Space Complexity:
- O(1,000,000) for the grid

---

## Step 11: Summary

**Key Concepts:**
- ✅ 2D array manipulation
- ✅ Rectangle range operations
- ✅ State management (boolean and integer)
- ✅ Instruction parsing
- ✅ Nested loops for grid traversal

**Happy lighting! 🎄💡**
