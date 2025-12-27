# Day 2: I Was Told There Would Be No Math - Step by Step Solution

## Step 1: Understanding the Problem

The elves need to calculate wrapping paper and ribbon for rectangular box-shaped presents.

**Part 1 - Wrapping Paper:**
- Calculate surface area of box: `2*l*w + 2*w*h + 2*h*l`
- Add slack: area of smallest side
- Sum for all presents

**Part 2 - Ribbon:**
- Calculate smallest perimeter: `2*(smallest + second_smallest)`
- Add bow: volume of box `l*w*h`
- Sum for all presents

**Input Format:** Each line contains dimensions `LxWxH` (e.g., `2x3x4`)

---

## Step 2: Breaking Down Part 1 - Wrapping Paper

### Formula Components:
```
Surface Area = 2*l*w + 2*w*h + 2*h*l

Breaking it down:
- 2*l*w = Two faces (top and bottom)
- 2*w*h = Two faces (front and back)
- 2*h*l = Two faces (left and right)

Slack = Minimum(l*w, w*h, h*l)
```

### Visual Example: Box 2x3x4
```
        Top/Bottom: 2x3 = 6 each  → 2*6 = 12
        Front/Back: 3x4 = 12 each → 2*12 = 24
        Left/Right: 2x4 = 8 each  → 2*8 = 16
        
Surface Area = 12 + 24 + 16 = 52

Three sides:
  - 2*3 = 6  ← Smallest
  - 3*4 = 12
  - 2*4 = 8

Slack = 6

Total = 52 + 6 = 58 square feet
```

---

## Step 3: Implementing Part 1

### Step 3.1: Parse Input
```csharp
(int l, int w, int h) ParseDimensions(string line)
{
    string[] parts = line.Split('x');
    int l = int.Parse(parts[0]);
    int w = int.Parse(parts[1]);
    int h = int.Parse(parts[2]);
    return (l, w, h);
}
```

### Step 3.2: Calculate Wrapping Paper for One Box
```csharp
int CalculateWrappingPaper(int l, int w, int h)
{
    // Calculate three side areas
    int side1 = l * w;
    int side2 = w * h;
    int side3 = h * l;
    
    // Surface area = 2 * each side
    int surfaceArea = 2 * side1 + 2 * side2 + 2 * side3;
    
    // Slack = smallest side
    int slack = Math.Min(side1, Math.Min(side2, side3));
    
    return surfaceArea + slack;
}
```

### Trace Example: `2x3x4`
```
Input: l=2, w=3, h=4

Step 1: Calculate sides
  side1 = 2 * 3 = 6
  side2 = 3 * 4 = 12
  side3 = 4 * 2 = 8

Step 2: Calculate surface area
  surfaceArea = 2*6 + 2*12 + 2*8
              = 12 + 24 + 16
              = 52

Step 3: Find smallest side
  Math.Min(6, Math.Min(12, 8))
  = Math.Min(6, 8)
  = 6

Step 4: Add slack
  total = 52 + 6 = 58 ✓
```

---

## Step 4: Breaking Down Part 2 - Ribbon

### Formula Components:
```
Ribbon = Smallest Perimeter + Bow

Smallest Perimeter = 2 * (smallest dimension + second smallest dimension)

Bow = Volume = l * w * h
```

### Visual Example: Box 2x3x4
```
Dimensions: 2, 3, 4
Sort: [2, 3, 4]
       ↑  ↑
    smallest & second smallest

Perimeter = 2 * (2 + 3) = 2 * 5 = 10 feet

Bow = 2 * 3 * 4 = 24 feet

Total = 10 + 24 = 34 feet
```

---

## Step 5: Implementing Part 2

```csharp
int CalculateRibbon(int l, int w, int h)
{
    // Sort dimensions to find two smallest
    int[] dimensions = { l, w, h };
    Array.Sort(dimensions);
    
    // Smallest perimeter uses two smallest dimensions
    int perimeter = 2 * (dimensions[0] + dimensions[1]);
    
    // Bow is the volume
    int bow = l * w * h;
    
    return perimeter + bow;
}
```

### Alternative Without Sorting:
```csharp
int CalculateRibbon(int l, int w, int h)
{
    // Find maximum dimension
    int max = Math.Max(l, Math.Max(w, h));
    
    // Perimeter uses sum of all minus max (twice)
    int perimeter = 2 * (l + w + h - max);
    
    // Bow is volume
    int bow = l * w * h;
    
    return perimeter + bow;
}
```

### Trace Example: `2x3x4`
```
Input: l=2, w=3, h=4

Method 1 (Sorting):
  dimensions = [2, 3, 4]
  After sort: [2, 3, 4]
  
  perimeter = 2 * (2 + 3) = 10
  bow = 2 * 3 * 4 = 24
  total = 10 + 24 = 34 ✓

Method 2 (Without Sorting):
  max = Math.Max(2, Math.Max(3, 4)) = 4
  sum = 2 + 3 + 4 = 9
  perimeter = 2 * (9 - 4) = 2 * 5 = 10
  bow = 2 * 3 * 4 = 24
  total = 10 + 24 = 34 ✓
```

---

## Step 6: Complete Implementation

```csharp
public static class Day2
{
    public static string SolvePart1(string input)
    {
        int totalPaper = 0;
        
        foreach (string line in input.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            var (l, w, h) = ParseDimensions(line.Trim());
            totalPaper += CalculateWrappingPaper(l, w, h);
        }
        
        return totalPaper.ToString();
    }

    public static string SolvePart2(string input)
    {
        int totalRibbon = 0;
        
        foreach (string line in input.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            var (l, w, h) = ParseDimensions(line.Trim());
            totalRibbon += CalculateRibbon(l, w, h);
        }
        
        return totalRibbon.ToString();
    }

    private static (int l, int w, int h) ParseDimensions(string line)
    {
        string[] parts = line.Split('x');
        int l = int.Parse(parts[0]);
        int w = int.Parse(parts[1]);
        int h = int.Parse(parts[2]);
        return (l, w, h);
    }

    private static int CalculateWrappingPaper(int l, int w, int h)
    {
        int side1 = l * w;
        int side2 = w * h;
        int side3 = h * l;
        
        int surfaceArea = 2 * (side1 + side2 + side3);
        int slack = Math.Min(side1, Math.Min(side2, side3));
        
        return surfaceArea + slack;
    }

    private static int CalculateRibbon(int l, int w, int h)
    {
        // Sort to find two smallest dimensions
        int[] dimensions = { l, w, h };
        Array.Sort(dimensions);
        
        int perimeter = 2 * (dimensions[0] + dimensions[1]);
        int bow = l * w * h;
        
        return perimeter + bow;
    }
}
```

---

## Step 7: Testing with Examples

### Test Case 1: `2x3x4`

**Part 1 - Wrapping Paper:**
```
Dimensions: l=2, w=3, h=4

Sides:
  2*3 = 6
  3*4 = 12
  2*4 = 8

Surface Area:
  2*(6 + 12 + 8) = 2*26 = 52

Slack:
  min(6, 12, 8) = 6

Total: 52 + 6 = 58 ✓
```

**Part 2 - Ribbon:**
```
Sorted dimensions: [2, 3, 4]

Perimeter:
  2*(2 + 3) = 10

Bow:
  2*3*4 = 24

Total: 10 + 24 = 34 ✓
```

### Test Case 2: `1x1x10`

**Part 1 - Wrapping Paper:**
```
Dimensions: l=1, w=1, h=10

Sides:
  1*1 = 1
  1*10 = 10
  1*10 = 10

Surface Area:
  2*(1 + 10 + 10) = 2*21 = 42

Slack:
  min(1, 10, 10) = 1

Total: 42 + 1 = 43 ✓
```

**Part 2 - Ribbon:**
```
Sorted dimensions: [1, 1, 10]

Perimeter:
  2*(1 + 1) = 4

Bow:
  1*1*10 = 10

Total: 4 + 10 = 14 ✓
```

---

## Step 8: Visual Representation

### Box 2x3x4:
```
           4
     +-----------+
    /|          /|
 3 / |       3 / |
  /  |        /  |
 +-----------+   | 2
 |   |       |   |
 |   +-------|---+
 |  / 2      |  /
 | /         | / 
 |/          |/
 +-----------+
      4

Faces:
- Top/Bottom: 2×3 = 6
- Front/Back: 3×4 = 12
- Left/Right: 2×4 = 8
```

### Ribbon Wrapping (Perimeter):
```
Looking at 2x3 face (smallest perimeter):

  +-----+
  |     | 3
  |     |
  +-----+
     2

Perimeter = 2 + 3 + 2 + 3 = 10
Or: 2*(2+3) = 10
```

---

## Step 9: Edge Cases

### Edge Case 1: Cube (all sides equal)
```
Input: "5x5x5"

Part 1:
  All sides = 25
  Surface = 2*(25+25+25) = 150
  Slack = 25
  Total = 175

Part 2:
  Any two sides: 2*(5+5) = 20
  Volume: 5*5*5 = 125
  Total = 145
```

### Edge Case 2: Flat Box
```
Input: "1x1x1"

Part 1:
  All sides = 1
  Surface = 2*(1+1+1) = 6
  Slack = 1
  Total = 7

Part 2:
  Perimeter: 2*(1+1) = 4
  Volume: 1*1*1 = 1
  Total = 5
```

### Edge Case 3: Very Long Box
```
Input: "1x1x100"

Part 1:
  Sides: 1, 100, 100
  Surface = 2*(1+100+100) = 402
  Slack = 1
  Total = 403

Part 2:
  Smallest: [1, 1, 100]
  Perimeter: 2*(1+1) = 4
  Volume: 1*1*100 = 100
  Total = 104
```

---

## Step 10: Common Mistakes

### Mistake 1: Forgetting Slack
```csharp
// WRONG - Missing slack
return 2 * (side1 + side2 + side3);

// CORRECT - Includes slack
return 2 * (side1 + side2 + side3) + Math.Min(side1, Math.Min(side2, side3));
```

### Mistake 2: Wrong Perimeter Calculation
```csharp
// WRONG - Using all three dimensions
int perimeter = 2 * (l + w + h);

// CORRECT - Using only two smallest
int[] dims = { l, w, h };
Array.Sort(dims);
int perimeter = 2 * (dims[0] + dims[1]);
```

### Mistake 3: Not Handling Empty Lines
```csharp
// WRONG - May crash on empty lines
foreach (string line in input.Split('\n'))
{
    var (l, w, h) = ParseDimensions(line);
}

// CORRECT - Skip empty lines
foreach (string line in input.Split('\n'))
{
    if (string.IsNullOrWhiteSpace(line))
        continue;
    var (l, w, h) = ParseDimensions(line.Trim());
}
```

---

## Step 11: Optimization Tips

### Simplify Surface Area Calculation:
```csharp
// Instead of calculating each side separately
int surfaceArea = 2*side1 + 2*side2 + 2*side3;

// Use:
int surfaceArea = 2 * (side1 + side2 + side3);
```

### Use LINQ for Cleaner Code:
```csharp
// Part 1 with LINQ
public static string SolvePart1(string input)
{
    return input.Split('\n')
        .Where(line => !string.IsNullOrWhiteSpace(line))
        .Select(line => ParseDimensions(line.Trim()))
        .Select(dims => CalculateWrappingPaper(dims.l, dims.w, dims.h))
        .Sum()
        .ToString();
}
```

---

## Step 12: Complexity Analysis

### Time Complexity:
- **Per box:** O(1) - constant time calculations
- **Total:** O(n) where n = number of boxes

### Space Complexity:
- O(1) - only need variables for current box
- Sorting array of 3 elements is O(1)

---

## Step 13: Formula Summary

### Part 1: Wrapping Paper
```
Total = Surface Area + Slack

Surface Area = 2*l*w + 2*w*h + 2*h*l
             = 2*(l*w + w*h + h*l)

Slack = min(l*w, w*h, h*l)
```

### Part 2: Ribbon
```
Total = Perimeter + Bow

Perimeter = 2*(smallest + second_smallest)
          = 2*(sum_of_all - largest)

Bow = l * w * h (volume)
```

---

## Step 14: Summary

**Key Concepts:**
- ✅ Geometric calculations (surface area, volume)
- ✅ Finding minimum/maximum values
- ✅ Array sorting for dimension ordering
- ✅ Parsing structured input (`LxWxH` format)
- ✅ Accumulating sums across multiple inputs

**Algorithm Pattern:**
```
For each box:
  1. Parse dimensions
  2. Calculate required amount
  3. Add to running total
Return total
```

**Important Points:**
- Surface area needs ALL six faces (2 of each)
- Slack is the SMALLEST side area
- Perimeter uses TWO SMALLEST dimensions
- Bow is the volume (all three dimensions)

---

**Happy calculating! 🎄📦**
