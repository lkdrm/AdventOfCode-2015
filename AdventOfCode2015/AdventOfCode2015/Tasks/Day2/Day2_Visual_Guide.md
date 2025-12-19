# 📦 Day 2 Visual Guide - Wrapping Paper Math

## 📐 Understanding Box Dimensions

### Box Anatomy
```
         ┌──────────┐
        /|         /|
       / |        / |
      /  |       /  |    Height (h)
     ┌───────────┐  |      ↕
     |   |       |  |
     |   └───────|──┘
     |  /        | /
     | /         |/      Width (w)
     └───────────┘      ←────→
        Length (l)
       ←────────→
```

**Format:** `l x w x h`
- **Length (l)** = First dimension
- **Width (w)** = Second dimension  
- **Height (h)** = Third dimension

---

## 📏 Surface Area Calculation

### The Six Faces
```
A rectangular box has 6 faces:

        Top (l×w)
        ┌─────┐
       /│     /│
  Left/ │    / │Right   ← Front (l×h)
  (w×h)│ │   (w×h)
      │ └───│──┘
      │ Bot │  ← Back (l×h)
      └─────┘
       (l×w)
```

### Formula Breakdown
```
Surface Area = 2×l×w + 2×w×h + 2×h×l

Why multiply by 2?
Each face has an OPPOSITE face:
- Top & Bottom      (l×w each)
- Left & Right      (w×h each)
- Front & Back      (l×h each)
```

---

## 📊 Part 1: Step-by-Step Examples

### Example 1: `2x3x4`

**Step 1: Identify dimensions**
```
Input: "2x3x4"
l = 2, w = 3, h = 4
```

**Step 2: Calculate each face area**
```
Face dimensions:
┌─────────────┬──────┬───────┐
│ Face Pair   │ Calc │ Area  │
├─────────────┼──────┼───────┤
│ Top/Bottom  │ 2×3  │ 6     │
│ Left/Right  │ 3×4  │ 12    │
│ Front/Back  │ 2×4  │ 8     │
└─────────────┴──────┴───────┘
```

**Step 3: Calculate total surface area**
```
Surface = 2×(l×w) + 2×(w×h) + 2×(h×l)
        = 2×6 + 2×12 + 2×8
        = 12 + 24 + 16
        = 52 square feet
```

**Step 4: Find smallest side (slack)**
```
Compare all sides:
- l×w = 2×3 = 6   ← SMALLEST
- w×h = 3×4 = 12
- h×l = 4×2 = 8

Slack = 6 square feet
```

**Step 5: Total wrapping paper**
```
Total = Surface Area + Slack
      = 52 + 6
      = 58 square feet ✓
```

**Visual representation:**
```
      3
   ┌─────┐
  /│  6  /│ 
2/ │12  /4│12
 │ └────│──┘
 │  8   │
 └──────┘
   2
   
Smallest area (6) gets extra paper!
```

---

### Example 2: `1x1x10`

**Step 1: Parse dimensions**
```
Input: "1x1x10"
l = 1, w = 1, h = 10

Visual (very tall box):
   1
   ┌┐
   ││ ↑
   ││ │ 10
   ││ │
   ││ ↓
   └┘
   1
```

**Step 2: Calculate face areas**
```
┌─────────────┬──────┬───────┐
│ Face Pair   │ Calc │ Area  │
├─────────────┼──────┼───────┤
│ Top/Bottom  │ 1×1  │ 1     │← SMALLEST
│ Left/Right  │ 1×10 │ 10    │
│ Front/Back  │ 1×10 │ 10    │
└─────────────┴──────┴───────┘
```

**Step 3: Surface area**
```
Surface = 2×1 + 2×10 + 2×10
        = 2 + 20 + 20
        = 42 square feet
```

**Step 4: Slack**
```
Smallest side = 1×1 = 1 square foot
```

**Step 5: Total**
```
Total = 42 + 1 = 43 square feet ✓
```

---

### Example 3: `5x5x5` (Perfect Cube)

**Special case: All sides equal**
```
Input: "5x5x5"

Visual:
     5
   ┌───┐
  /│   /│
5/ │  / │5
 │ └──│──┘
 │    │
 └────┘
   5

All faces are identical: 5×5 = 25

Surface = 2×25 + 2×25 + 2×25
        = 150 square feet

Slack = 25 (any face, all equal)

Total = 150 + 25 = 175 square feet
```

---

## 🎀 Part 2: Ribbon Calculation

### Understanding Ribbon Requirements

**Two components:**
1. **Wrap ribbon** = Perimeter of smallest face
2. **Bow ribbon** = Volume of box

```
Total Ribbon = Wrap + Bow
```

---

### Example 1: `2x3x4`

**Step 1: Sort dimensions to find smallest perimeter**
```
Dimensions: 2, 3, 4
Sorted: [2, 3, 4]
         ↑  ↑
    Smallest two
```

**Step 2: Calculate wrap ribbon (perimeter)**
```
Perimeter of smallest face = 2×side₁ + 2×side₂
                           = 2×2 + 2×3
                           = 4 + 6
                           = 10 feet

Visual of smallest face (2×3):
   3
  ┌───┐
 2│   │2  Total perimeter: 2+3+2+3 = 10
  └───┘
   3
```

**Step 3: Calculate bow ribbon (volume)**
```
Volume = l × w × h
       = 2 × 3 × 4
       = 24 cubic feet
```

**Step 4: Total ribbon**
```
Total = Wrap + Bow
      = 10 + 24
      = 34 feet ✓
```

---

### Example 2: `1x1x10`

**Step 1: Sort dimensions**
```
Dimensions: 1, 1, 10
Sorted: [1, 1, 10]
         ↑  ↑
    Two smallest
```

**Step 2: Wrap ribbon**
```
Perimeter = 2×1 + 2×1
          = 2 + 2
          = 4 feet

Smallest face (1×1):
  1
 ┌─┐
1│ │1
 └─┘
  1
Perimeter: 1+1+1+1 = 4
```

**Step 3: Bow ribbon**
```
Volume = 1 × 1 × 10
       = 10 cubic feet
```

**Step 4: Total**
```
Total = 4 + 10 = 14 feet ✓
```

---

### Example 3: `10x10x10` (Large Cube)

**Step 1: All dimensions equal**
```
Sorted: [10, 10, 10]
         ↑   ↑
```

**Step 2: Wrap ribbon**
```
Perimeter = 2×10 + 2×10
          = 20 + 20
          = 40 feet
```

**Step 3: Bow ribbon**
```
Volume = 10 × 10 × 10
       = 1000 cubic feet
```

**Step 4: Total**
```
Total = 40 + 1000 = 1040 feet
```

---

## 🎯 Side-by-Side Comparison

### Box: `2x3x4`
```
┌──────────────────┬──────────┬──────────┐
│ Requirement      │ Part 1   │ Part 2   │
├──────────────────┼──────────┼──────────┤
│ Surface Area     │ 52       │ -        │
│ Slack (smallest) │ 6        │ -        │
│ Wrap Perimeter   │ -        │ 10       │
│ Bow Volume       │ -        │ 24       │
├──────────────────┼──────────┼──────────┤
│ TOTAL            │ 58 sqft  │ 34 ft    │
└──────────────────┴──────────┴──────────┘
```

### Box: `1x1x10`
```
┌──────────────────┬──────────┬──────────┐
│ Requirement      │ Part 1   │ Part 2   │
├──────────────────┼──────────┼──────────┤
│ Surface Area     │ 42       │ -        │
│ Slack (smallest) │ 1        │ -        │
│ Wrap Perimeter   │ -        │ 4        │
│ Bow Volume       │ -        │ 10       │
├──────────────────┼──────────┼──────────┤
│ TOTAL            │ 43 sqft  │ 14 ft    │
└──────────────────┴──────────┴──────────┘
```

---

## 💻 Code Trace Example

### Input Line: `"2x3x4"`

**Parsing:**
```csharp
string line = "2x3x4";
string[] parts = line.Split('x');
// parts = ["2", "3", "4"]

int l = int.Parse(parts[0]); // 2
int w = int.Parse(parts[1]); // 3
int h = int.Parse(parts[2]); // 4
```

**Part 1 Calculation:**
```csharp
// Calculate surface area
int surfaceArea = 2 * (l*w + w*h + h*l);
              // = 2 * (2*3 + 3*4 + 4*2)
              // = 2 * (6 + 12 + 8)
              // = 2 * 26
              // = 52

// Find smallest side
int side1 = l * w; // 2 * 3 = 6
int side2 = w * h; // 3 * 4 = 12
int side3 = h * l; // 4 * 2 = 8

int slack = Math.Min(Math.Min(side1, side2), side3);
         // = Math.Min(Math.Min(6, 12), 8)
         // = Math.Min(6, 8)
         // = 6

int total = surfaceArea + slack;
         // = 52 + 6
         // = 58
```

**Part 2 Calculation:**
```csharp
// Sort to find two smallest dimensions
int[] sorted = [l, w, h];
Array.Sort(sorted);
// sorted = [2, 3, 4]

// Wrap ribbon (perimeter of smallest face)
int wrap = 2 * sorted[0] + 2 * sorted[1];
        // = 2 * 2 + 2 * 3
        // = 4 + 6
        // = 10

// Bow ribbon (volume)
int bow = l * w * h;
       // = 2 * 3 * 4
       // = 24

int total = wrap + bow;
         // = 10 + 24
         // = 34
```

---

## 🧮 Formula Reference Card

### Part 1: Wrapping Paper
```
┌────────────────────────────────────────┐
│ Surface Area = 2lw + 2wh + 2hl        │
│ Slack = min(lw, wh, hl)               │
│ Total = Surface Area + Slack          │
└────────────────────────────────────────┘

Example: 2x3x4
  Surface = 2(6) + 2(12) + 2(8) = 52
  Slack = min(6, 12, 8) = 6
  Total = 52 + 6 = 58
```

### Part 2: Ribbon
```
┌────────────────────────────────────────┐
│ Wrap = 2 × (smallest + second)        │
│ Bow = l × w × h                       │
│ Total = Wrap + Bow                    │
└────────────────────────────────────────┘

Example: 2x3x4
  Sorted: [2, 3, 4]
  Wrap = 2(2) + 2(3) = 10
  Bow = 2 × 3 × 4 = 24
  Total = 10 + 24 = 34
```

---

## 🐛 Common Mistakes

### Mistake 1: Wrong Slack Calculation
```csharp
// WRONG - Forgot to find minimum
int slack = l * w; // Just picks one arbitrarily

// CORRECT - Find actual minimum
int slack = Math.Min(l*w, Math.Min(w*h, h*l));
```

### Mistake 2: Wrong Perimeter Formula
```csharp
// WRONG - Only one of each side
int wrap = smallest[0] + smallest[1];

// CORRECT - Perimeter needs all 4 sides
int wrap = 2 * smallest[0] + 2 * smallest[1];

Visual why:
   3
  ┌───┐
 2│   │2  ← Need both 2's
  └───┘
   3     ← Need both 3's
Total: 2+3+2+3 = 10 ✓
```

### Mistake 3: Not Sorting for Part 2
```csharp
// WRONG - Assumes input is sorted
int wrap = 2 * l + 2 * w; // What if h is smaller?

// CORRECT - Sort first
int[] sorted = [l, w, h];
Array.Sort(sorted);
int wrap = 2 * sorted[0] + 2 * sorted[1];
```

---

## 🔄 Processing Multiple Boxes

### Input Format
```
2x3x4
1x1x10
5x5x5
10x2x8
```

### Loop Processing
```csharp
int totalPaper = 0;
int totalRibbon = 0;

foreach (string line in input)
{
    // Parse: "2x3x4" → l=2, w=3, h=4
    string[] parts = line.Split('x');
    int l = int.Parse(parts[0]);
    int w = int.Parse(parts[1]);
    int h = int.Parse(parts[2]);
    
    // Part 1: Calculate paper for this box
    int area = 2*(l*w + w*h + h*l);
    int slack = Math.Min(l*w, Math.Min(w*h, h*l));
    totalPaper += area + slack;
    
    // Part 2: Calculate ribbon for this box
    int[] dims = [l, w, h];
    Array.Sort(dims);
    int wrap = 2*dims[0] + 2*dims[1];
    int bow = l * w * h;
    totalRibbon += wrap + bow;
}

Console.WriteLine($"Total paper: {totalPaper}");
Console.WriteLine($"Total ribbon: {totalRibbon}");
```

### Trace Example
```
Box 1: 2x3x4
  Paper:  58
  Ribbon: 34
  Running totals: Paper=58, Ribbon=34

Box 2: 1x1x10
  Paper:  43
  Ribbon: 14
  Running totals: Paper=101, Ribbon=48

Box 3: 5x5x5
  Paper:  175
  Ribbon: 90
  Running totals: Paper=276, Ribbon=138
```

---

## 📈 Visual Summary

### Part 1: Paper Calculation
```
        Calculate       Find           Add
       Surface Area → Smallest → Together
           ↓            Side        ↓
    2lw+2wh+2hl      min(lw,wh,hl)  Total
           ↓              ↓           ↓
          52       +      6      =   58
```

### Part 2: Ribbon Calculation
```
        Sort         Calculate    Calculate
      Dimensions →  Perimeter →   Volume  → Add
          ↓             ↓            ↓       ↓
      [2,3,4]      2(2)+2(3)    2×3×4    10+24
                       ↓            ↓       ↓
                      10      +    24   =  34
```

---

## 🎓 Practice Problems

### Problem 1: Calculate for `3x11x24`
```
Part 1:
  Surface = 2(3×11) + 2(11×24) + 2(24×3)
          = 2(33) + 2(264) + 2(72)
          = 66 + 528 + 144
          = 738
  Slack = min(33, 264, 72) = 33
  Total = 738 + 33 = 771 sqft

Part 2:
  Sorted: [3, 11, 24]
  Wrap = 2(3) + 2(11) = 6 + 22 = 28
  Bow = 3 × 11 × 24 = 792
  Total = 28 + 792 = 820 ft
```

### Problem 2: Calculate for `7x7x7`
```
Part 1:
  Surface = 2(49) + 2(49) + 2(49) = 294
  Slack = 49 (all sides equal)
  Total = 294 + 49 = 343 sqft

Part 2:
  Sorted: [7, 7, 7]
  Wrap = 2(7) + 2(7) = 28
  Bow = 7 × 7 × 7 = 343
  Total = 28 + 343 = 371 ft
```

---

## 📝 Key Takeaways

**Part 1 - Wrapping Paper:**
1. 📏 Calculate total surface area (all 6 faces)
2. 🔍 Find the smallest face area
3. ➕ Add them together

**Part 2 - Ribbon:**
1. 📊 Sort the three dimensions
2. 📐 Use two smallest for perimeter
3. 📦 Multiply all three for volume
4. ➕ Add perimeter + volume

**Memory Tricks:**
- 🎁 Paper covers the **surface** → Area formula
- 🎀 Ribbon goes **around** → Perimeter formula
- 🎀 Bow needs **filling** → Volume formula

---

**Happy wrapping! 📦🎄**
