# 📚 Advent of Code 2015 - Visual Guides Collection

Welcome to the comprehensive visual guide collection for Advent of Code 2015! Each guide provides detailed, step-by-step visualizations to help you understand the problems and solutions.

---

## 📖 Available Visual Guides

### 🏢 [Day 1: Not Quite Lisp](AdventOfCode2015/AdventOfCode2015/Tasks/Day1/Day1_Visual_Guide.md)
**Problem:** Navigate floors using `(` and `)` characters
- Understanding floor navigation with parentheses
- Finding when Santa enters the basement
- Step-by-step examples with visual floor diagrams
- Algorithm traces and optimization tips

**Key Concepts:**
- Sequential processing
- Counting algorithms
- Early exit patterns
- 1-based position indexing

---

### 📦 [Day 2: I Was Told There Would Be No Math](AdventOfCode2015/AdventOfCode2015/Tasks/Day2/Day2_Visual_Guide.md)
**Problem:** Calculate wrapping paper and ribbon for presents
- Surface area calculations for rectangular boxes
- Finding minimum face area (slack)
- Perimeter and volume calculations for ribbon
- Formula breakdowns with visual box diagrams

**Key Concepts:**
- Geometric calculations
- Min/max operations
- Array sorting
- Multiple dimensional analysis

---

### 🏠 [Day 3: Perfectly Spherical Houses in a Vacuum](AdventOfCode2015/AdventOfCode2015/Tasks/Day3/Day3_Visual_Guide.md)
**Problem:** Track Santa's delivery route on an infinite grid
- Coordinate system and direction mapping
- Using HashSet for tracking unique positions
- Santa + Robo-Santa alternating turns
- Grid visualizations and path tracing

**Key Concepts:**
- 2D grid navigation
- HashSet for uniqueness
- Position tracking
- Turn alternation logic

---

### 🎁 [Day 4: The Ideal Stocking Stuffer](AdventOfCode2015/AdventOfCode2015/Tasks/Day4/Day4_Visual_Guide.md)
**Problem:** Mine AdventCoins by finding MD5 hashes with leading zeros
- MD5 hash fundamentals
- Brute force search algorithm
- Probability calculations for hash patterns
- Performance optimization techniques

**Key Concepts:**
- Cryptographic hashing
- Brute force algorithms
- String manipulation
- Computational complexity

---

### 🎄 [Day 5: Doesn't He Have Intern-Elves For This?](AdventOfCode2015/AdventOfCode2015/Tasks/Day5/Day5_Visual_Guide.md)
**Problem:** Determine if strings are "naughty" or "nice"
- Multiple rule validation (Part 1 & Part 2)
- Pattern matching and substring searching
- Non-overlapping pair detection
- Character sequence analysis

**Key Concepts:**
- String pattern matching
- Multiple condition checking
- Overlapping vs non-overlapping patterns
- Boolean logic

---

### 💡 [Day 6: Probably a Fire Hazard](AdventOfCode2015/AdventOfCode2015/Tasks/Day6/Day6_Visual_Guide.md)
**Problem:** Control a 1000×1000 grid of lights
- 2D array manipulation
- Rectangle operations with coordinates
- State management (on/off/toggle)
- Brightness calculations

**Key Concepts:**
- 2D arrays
- Nested loops
- Range operations
- State mutations

---

### 🔌 [Day 7: Some Assembly Required](AdventOfCode2015/AdventOfCode2015/Tasks/Day7/Day7_Visual_Guide.md)
**Problem:** Simulate a circuit with bitwise logic gates
- 16-bit signal processing
- Bitwise operations (AND, OR, NOT, LSHIFT, RSHIFT)
- Dependency resolution with recursion
- Memoization for performance optimization

**Key Concepts:**
- Bitwise operations
- Recursive computation
- Memoization/caching
- Dependency resolution
- Circuit simulation

---

## 🎯 How to Use These Guides

### For Beginners
1. **Read the problem description** first (linked in each guide)
2. **Study the examples** with visual diagrams
3. **Follow the step-by-step traces** to understand the logic
4. **Review common mistakes** to avoid pitfalls
5. **Try implementing** your own solution

### For Experienced Developers
1. **Quick reference tables** for formula lookups
2. **Complexity analysis** for optimization insights
3. **Edge case examples** for thorough testing
4. **Implementation tips** for clean code

### Visual Guide Structure
Each guide includes:
- 📋 **Problem Overview** - Core concepts explained
- 📊 **Step-by-Step Examples** - Detailed walkthroughs
- 💻 **Code Traces** - Algorithm execution visualization
- 🎨 **Visual Diagrams** - Grid/graph/tree representations
- 🐛 **Common Mistakes** - What to avoid
- 📈 **Complexity Analysis** - Performance considerations
- 📝 **Summary** - Key takeaways and memory aids

---

## 🗂️ Guide Organization by Topic

### Data Structures
- **Arrays/Strings**: Days 1, 2, 5
- **2D Arrays**: Day 6
- **HashSet**: Day 3
- **Dictionary**: Day 7
- **Coordinate Systems**: Day 3

### Algorithms
- **Sequential Processing**: Days 1, 5
- **Grid Navigation**: Days 3, 6
- **Brute Force Search**: Day 4
- **Pattern Matching**: Day 5
- **Range Operations**: Day 6
- **Recursion with Memoization**: Day 7
- **Dependency Resolution**: Day 7

### Problem Types
- **Simulation**: Days 3, 6, 7
- **Parsing**: Days 1, 2, 5, 6, 7
- **Validation**: Day 5
- **Optimization**: Days 4, 7
- **State Management**: Days 6, 7

---

## 📊 Difficulty Progression

### Easy (★☆☆)
- **Day 1**: Simple counting with basic conditions
- **Day 2**: Straightforward math with formulas

### Medium (★★☆)
- **Day 3**: Grid navigation with set tracking
- **Day 5**: Multiple pattern matching rules
- **Day 7**: Circuit simulation with recursion

### Challenging (★★★)
- **Day 4**: Computational search with hashing
- **Day 6**: Large-scale array manipulation

---

## 🎓 Learning Paths

### Path 1: Sequential Processing
```
Day 1 (Basics) → Day 5 (Patterns) → Day 4 (Search)
```
Learn to process input character-by-character with increasing complexity.

### Path 2: Geometric Problems
```
Day 2 (Boxes) → Day 3 (Grid) → Day 6 (Large Grid)
```
Master spatial reasoning and coordinate systems.

### Path 3: Data Structure Usage
```
Day 1 (Variables) → Day 3 (HashSet) → Day 6 (2D Array) → Day 7 (Dictionary)
```
Understand when and how to use different data structures.

### Path 4: Advanced Algorithms
```
Day 4 (Brute Force) → Day 7 (Recursion + Memoization)
```
Learn optimization techniques for complex problems.

---

## 🔧 Technical Requirements

### Languages
These guides use **C#** for code examples, but concepts apply to:
- C# (.NET 6+)
- Java
- Python
- JavaScript/TypeScript
- Any modern programming language

### Concepts Covered
- ✓ Variables and basic types
- ✓ Loops (for, foreach, while)
- ✓ Conditionals (if, switch)
- ✓ Arrays and collections
- ✓ String manipulation
- ✓ Hash functions
- ✓ Record types
- ✓ LINQ (C# specific)
- ✓ Recursion
- ✓ Memoization
- ✓ Bitwise operations

---

## 📁 File Structure

```
AdventOfCode2015/
├── Tasks/
│   ├── Day1/
│   │   ├── Day1_Visual_Guide.md
│   │   ├── Day 1 Not Quite Lisp.md
│   │   └── Day1.txt
│   ├── Day2/
│   │   ├── Day2_Visual_Guide.md
│   │   ├── Day 2 I Was Told There Would Be No Math.md
│   │   └── Day2.txt
│   ├── Day3/
│   │   ├── Day3_Visual_Guide.md
│   │   ├── Day 3 Perfectly Spherical Houses in a Vacuum.md
│   │   └── Day3.txt
│   ├── Day4/
│   │   ├── Day4_Visual_Guide.md
│   │   ├── Day 4 The Ideal Stocking Stuffer.md
│   │   └── Day4.txt
│   ├── Day5/
│   │   ├── Day5_Visual_Guide.md
│   │   ├── Day 5 Doesn't He Have Intern Elves For This.md
│   │   └── Day5.txt
│   ├── Day6/
│   │   ├── Day6_Visual_Guide.md
│   │   ├── Day 6 Probably a Fire Hazard.md
│   │   └── Day6.txt
│   ├── Day7/
│   │   ├── Day7_Visual_Guide.md
│   │   ├── Day 7 Some Assembly Required.md
│   │   └── Day7.txt
├── ResolvingDays/
│    ├── Day1.cs
│    ├── Day2.cs
│    ├── Day3.cs
│    ├── Day4.cs
│    ├── Day5.cs
│    ├── Day6.cs
│    └── Day7.cs
└── README.md (this file)
```

---

## 🎨 Visual Guide Features

### Diagram Types Used
1. **ASCII Art Grids** - For spatial problems
2. **Flow Diagrams** - For algorithm logic
3. **State Transitions** - For tracking changes
4. **Tables** - For comparing values
5. **Step Traces** - For execution walkthrough
6. **Annotated Code** - For implementation details

### Color Coding (in text)
- ✓ **Success/Pass** - Green checkmark
- ✗ **Failure/Error** - Red X
- 🎯 **Important Point** - Target emoji
- 💡 **Tip** - Light bulb emoji
- 🐛 **Bug/Mistake** - Bug emoji
- 📊 **Data/Stats** - Chart emoji

---

## 💪 Practice Exercises

Each visual guide includes:
1. **Worked Examples** - Full solutions shown
2. **Practice Problems** - Similar challenges
3. **Edge Cases** - Unusual inputs to consider
4. **Test Cases** - Sample inputs with expected outputs

---

## 🔗 Additional Resources

### Official Problem Statements
- [Advent of Code 2015](https://adventofcode.com/2015)

### Community
- [Advent of Code Subreddit](https://www.reddit.com/r/adventofcode/)
- [GitHub Discussions](https://github.com/topics/advent-of-code)

### Learning Resources
- [Algorithms Visualization](https://visualgo.net/)
- [Big-O Cheat Sheet](https://www.bigocheatsheet.com/)
- [C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)

---

## 📜 Version History

- **v1.1** - Added Day 7: Some Assembly Required
  - Bitwise circuit simulation guide
  - Recursion and memoization techniques
  - Comprehensive dependency resolution examples
- **v1.0** - Initial release with Days 1-6 visual guides
  - Comprehensive examples and visualizations
  - Cross-referenced with actual solutions
  - Tested against real puzzle inputs

---

## 🎁 Special Thanks

These visual guides were created to help developers of all skill levels:
- **Beginners**: Learn fundamentals through practical problems
- **Intermediates**: Deepen understanding with detailed traces
- **Experts**: Reference complex patterns and optimizations

Happy coding and enjoy Advent of Code! 🎄✨

🚀**Start with Day 1 and work your way through! Each guide builds on previous concepts.** 🚀