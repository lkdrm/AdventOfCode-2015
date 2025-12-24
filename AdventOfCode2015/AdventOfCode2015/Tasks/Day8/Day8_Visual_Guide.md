# 📝 Day 8 Visual Guide - Matchsticks (String Escaping)

## 🎯 Problem Overview

Santa needs to calculate the difference between:
1. **Code representation** - How many characters in the string literal (including quotes)
2. **In-memory value** - How many characters the string actually contains

**Key Insight:** Escape sequences in code take up MORE characters than they represent!

**Goal:** Find the total difference for all strings in the input file.

---

## 📏 Understanding the Difference

### Core Concept
```
Code Characters:   What you TYPE in source code
Memory Characters: What the STRING CONTAINS

Example:
Code:   "abc"      ← 5 characters typed
Memory: abc        ← 3 characters in string
Difference: 5 - 3 = 2
```

---

## 🔤 Escape Sequences Explained

### 1️ The Empty String
```
Code:     ""
          ↑↑
          Two quote characters

Memory:   (nothing)

Code Length:   2
Memory Length: 0
Difference:    2
```

**Visual:**
```
Type: " + " = 2 characters

Store: [empty] = 0 characters
```

---

### 2️ Regular String (No Escapes)
```
Code:     "abc"
          ↑   ↑
          Quotes + 3 letters = 5 chars

Memory:   abc
          ↑↑↑
          Just 3 letters

Code Length:   5
Memory Length: 3
Difference:    5 - 3 = 2
```

**Visual:**
```
Type: " + a + b + c + " = 5 characters

Store: a + b + c = 3 characters

The quotes "wrap" the string but don't get stored!
```

---

### 3️ Escaped Quote: `\"`
```
Code:     "aaa\"aaa"
          ↑   ↑↑    ↑
          Opening quote
              Backslash + quote (escaped)
                   Closing quote

Memory:   aaa"aaa
          ↑↑↑↑↑↑↑
          7 characters (3 a's, quote, 3 a's)

Code Length:   10 (quote + 3 a's + backslash + quote + 3 a's + quote)
Memory Length: 7  (3 a's + quote + 3 a's)
Difference:    10 - 7 = 3
```

**Step-by-step breakdown:**
```
Position: 1 2 3 4 5 6 7 8 9 10
Code:     " a a a \ " a a a "
          ↑           ↑
          Don't count these quotes
                  ↑ ↑
              These 2 chars → 1 char in memory

Memory representation:
Position: 1 2 3 4 5 6 7
Memory:   a a a " a a a
              ↑
          A single quote character
```

**The Magic:**
```
In code:  \"   (2 characters)
          ↑↑
          Backslash tells us: "Next char is special"

In memory: "   (1 character)
           ↑
           Just the quote itself
```

---

### 4️ Escaped Backslash: `\\`
```
Code:     "a\\b"
          ↑ ↑↑ ↑
          " a \ \ b "
          
Memory:   a\b
          ↑↑↑
          3 characters

Code Length:   5 (" + a + \ + \ + b + ")
Memory Length: 3 (a + \ + b)
Difference:    5 - 3 = 2
```

**Visual:**
```
Type: " + a + \ + \ + b + " = 5 chars
            ↑   ↑
        These two represent ONE backslash

Store: a + \ + b = 3 chars
           ↑
       Single backslash
```

---

### 5️ Hexadecimal Escape: `\xNN`
```
Code:     "\x27"
          ↑↑↑↑↑↑
          " \ x 2 7 "

Memory:   '
          ↑
          1 character (apostrophe, ASCII 39 = 0x27)

Code Length:   6 (" + \ + x + 2 + 7 + ")
Memory Length: 1 (single apostrophe)
Difference:    6 - 1 = 5
```

**How it works:**
```
\x27 means:
  \x  → "The next 2 digits are hexadecimal"
  27  → Hexadecimal number 27
  
Convert: 0x27 = 2×16 + 7 = 39 (decimal)
ASCII 39 = ' (apostrophe)

So: \x27 → '
    4 chars in code → 1 char in memory
```

**Common hex escapes:**
```
Code      Memory    ASCII   Description
────────  ────────  ─────   ──────────────
\x00      (null)    0       Null character
\x09      (tab)     9       Tab
\x0a      (newline) 10      Line feed
\x20      (space)   32      Space
\x27      '         39      Apostrophe
\x41      A         65      Capital A
\x61      a         97      Lowercase a
```

---

## 📊 Complete Example Walkthrough

### Given Input
```
""
"abc"
"aaa\"aaa"
"\x27"
```

### String 1: `""`
```
┌─────────────────────────────────┐
│ Code:   ""                      │
│         ↑↑                      │
│ Count:  2                       │
└─────────────────────────────────┘

┌─────────────────────────────────┐
│ Memory: (empty)                 │
│ Count:  0                       │
└─────────────────────────────────┘

Difference: 2 - 0 = 2
```

---

### String 2: `"abc"`
```
┌─────────────────────────────────┐
│ Code:   "abc"                   │
│         ↑   ↑                   │
│ Chars:  " a b c "               │
│ Count:  5                       │
└─────────────────────────────────┘

┌─────────────────────────────────┐
│ Memory: abc                     │
│ Chars:  a b c                   │
│ Count:  3                       │
└─────────────────────────────────┘

Difference: 5 - 3 = 2
```

---

### String 3: `"aaa\"aaa"`
```
┌─────────────────────────────────┐
│ Code:   "aaa\"aaa"              │
│         ↑   ↑↑    ↑             │
│ Chars:  " a a a \ " a a a "     │
│ Count:  10                      │
└─────────────────────────────────┘

┌─────────────────────────────────┐
│ Memory: aaa"aaa                 │
│ Chars:  a a a " a a a           │
│ Count:  7                       │
└─────────────────────────────────┘

Parsing step-by-step:
  "       → Start of string (don't count)
  a       → Regular char (count: 1)
  a       → Regular char (count: 2)
  a       → Regular char (count: 3)
  \       → Escape character...
  "       → ...escaped quote (count: 4)
  a       → Regular char (count: 5)
  a       → Regular char (count: 6)
  a       → Regular char (count: 7)
  "       → End of string (don't count)

Difference: 10 - 7 = 3
```

---

### String 4: `"\x27"`
```
┌─────────────────────────────────┐
│ Code:   "\x27"                  │
│         ↑↑↑↑↑↑                  │
│ Chars:  " \ x 2 7 "             │
│ Count:  6                       │
└─────────────────────────────────┘

┌─────────────────────────────────┐
│ Memory: '                       │
│ Chars:  '                       │
│ Count:  1                       │
└─────────────────────────────────┘

Parsing:
  "       → Start of string (don't count)
  \       → Escape character...
  x       → ...hex escape...
  2       → ...first hex digit...
  7       → ...second hex digit
            (Together: 0x27 = 39 = ')
            (count: 1)
  "       → End of string (don't count)

Difference: 6 - 1 = 5
```

---

### Summary Calculation
```
┌────────────┬──────────┬──────────┬────────────┐
│ String     │ Code Len │ Mem Len  │ Difference │
├────────────┼──────────┼──────────┼────────────┤
│ ""         │    2     │    0     │     2      │
│ "abc"      │    5     │    3     │     2      │
│ "aaa\"aaa" │   10     │    7     │     3      │
│ "\x27"     │    6     │    1     │     5      │
├────────────┼──────────┼──────────┼────────────┤
│ TOTALS:    │   23     │   11     │    12      │
└────────────┴──────────┴──────────┴────────────┘

Answer: 23 - 11 = 12
```

---

## 💻 Algorithm - Code Length

### Simple Approach
```csharp
int codeLength = line.Length;
```

**That's it!** The code length is just the number of characters in the line.

**Example:**
```csharp
string line = "\"abc\"";  // This is how C# stores "abc"
int codeLength = line.Length;  // Returns 5

// Character by character:
line[0] = '"'  (quote)
line[1] = 'a'
line[2] = 'b'
line[3] = 'c'
line[4] = '"'  (quote)
Total: 5
```

---

## 💻 Algorithm - Memory Length

### Step-by-Step Parsing

```csharp
int memoryLength = 0;
int i = 1;  // Start after opening quote
int end = line.Length - 1;  // Stop before closing quote

while (i < end)
{
    if (line[i] == '\\')  // Escape sequence!
    {
        if (line[i+1] == 'x')  // Hex escape \xNN
        {
            memoryLength++;
            i += 4;  // Skip \, x, and 2 hex digits
        }
        else  // \\ or \"
        {
            memoryLength++;
            i += 2;  // Skip \ and next char
        }
    }
    else  // Regular character
    {
        memoryLength++;
        i++;
    }
}
```

---

## 🔍 Detailed Parsing Example

### Input: `"ab\\cd\"ef\x01gh"`

**Visual trace:**
```
Position: 0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16
Chars:    "  a  b  \  \  c  d  \  "  e  f  \  x  0  1  g  h  "
          ↑                                                  ↑
       Start                                               End
       (skip)                                             (skip)
```

**Parsing steps:**

```
i=1: line[1]='a' → Regular char
     memoryLength=1, i=2

i=2: line[2]='b' → Regular char
     memoryLength=2, i=3

i=3: line[3]='\' → Escape!
     line[4]='\' → Escaped backslash
     memoryLength=3, i=5  (skip both)

i=5: line[5]='c' → Regular char
     memoryLength=4, i=6

i=6: line[6]='d' → Regular char
     memoryLength=5, i=7

i=7: line[7]='\' → Escape!
     line[8]='"' → Escaped quote
     memoryLength=6, i=9  (skip both)

i=9: line[9]='e' → Regular char
     memoryLength=7, i=10

i=10: line[10]='f' → Regular char
      memoryLength=8, i=11

i=11: line[11]='\' → Escape!
      line[12]='x' → Hex escape!
      line[13]='0', line[14]='1' → 0x01
      memoryLength=9, i=15  (skip all 4)

i=15: line[15]='g' → Regular char
      memoryLength=10, i=16

i=16: line[16]='h' → Regular char
      memoryLength=11, i=17

i=17: End reached (closing quote)
```

**Result:**
```
Code Length:   18 characters
Memory Length: 11 characters
Difference:    18 - 11 = 7

In memory, the string is:
ab\cd"ef[0x01]gh
↑ ↑  ↑  ↑     ↑↑
Regular chars
  Escaped backslash
     Escaped quote
        Hex char (ASCII 1)
```

---

## 🔄 Character-by-Character Flow Diagram

```
Start with line: "abc\"de"

                 ┌─────────────┐
                 │  i = 1      │  (Skip opening ")
                 └──────┬──────┘
                        │
                   ┌────▼────┐
                   │ line[i] │
                   └────┬────┘
                        │
          ┌─────────────┴─────────────┐
          │                           │
     ┌────▼─────┐               ┌─────▼──────┐
     │ Is '\\'? │───No───────►  │  Regular   │
     └────┬─────┘               │  Character │
          │                     └─────┬──────┘
         Yes                          │
          │                           │
    ┌─────▼──────┐                    │
    │ line[i+1]? │                    │
    └─────┬──────┘                    │
          │                           │
    ┌─────┴─────┐                     │
    │           │                     │
┌───▼───┐  ┌────▼────┐                │
│ 'x'?  │  │ Other   │                │
│ (Hex) │  │ (\\ \") │                │
└───┬───┘  └────┬────┘                │
    │           │                     │
    │      ┌────▼────┐                │
    │      │ memory++│◄───────────────┘
    │      │ i += 2  │
    │      └─────────┘
    │
┌───▼────┐
│memory++│
│i += 4  │
└────────┘
```

---

## 🐛 Common Mistakes

### Mistake 1: Counting the Quotes in Memory
```csharp
// WRONG:
int memoryLength = line.Length;  // Includes quotes!

// Example: "abc"
// This gives 5, but memory length is 3!

// CORRECT:
int memoryLength = CalculateMemoryLength(line);
// Parse escape sequences and skip quotes
```

---

### Mistake 2: Not Handling Hex Escapes
```csharp
// WRONG:
if (line[i] == '\\')
{
    memoryLength++;
    i += 2;  // Always skip 2 chars
}

// This fails for "\x27" - we skip 2, but need to skip 4!

// CORRECT:
if (line[i] == '\\')
{
    if (line[i+1] == 'x')
    {
        i += 4;  // Skip \xNN
    }
    else
    {
        i += 2;  // Skip \c
    }
    memoryLength++;
}
```

---

### Mistake 3: Off-by-One Errors
```csharp
// WRONG:
int i = 0;  // Starts at opening quote!
int end = line.Length;  // Includes closing quote!

// This would try to parse the quotes themselves

// CORRECT:
int i = 1;  // Skip opening quote
int end = line.Length - 1;  // Skip closing quote
```

---

### Mistake 4: Not Skipping Enough Characters
```csharp
// Input: "a\\b"
//         ↑↑↑↑

// WRONG:
if (line[i] == '\\')
{
    memoryLength++;
    i++;  // Only skip the backslash
}
// Next iteration: i points to the SECOND backslash
// This counts it as another escape!

// CORRECT:
if (line[i] == '\\')
{
    memoryLength++;
    i += 2;  // Skip BOTH the \ and the next char
}
```

---

## 📊 Visual Summary Table

### Escape Sequence Reference

```
┌────────────┬──────────────┬────────────┬────────────┐
│  In Code   │  In Memory   │ Code Chars │ Mem Chars  │
├────────────┼──────────────┼────────────┼────────────┤
│ ""         │ (empty)      │     2      │     0      │
│ "a"        │ a            │     3      │     1      │
│ "\\"       │ \            │     4      │     1      │
│ "\""       │ "            │     4      │     1      │
│ "\x41"     │ A            │     6      │     1      │
│ "a\\b"     │ a\b          │     5      │     3      │
│ "a\"b"     │ a"b          │     5      │     3      │
│ "\x27"     │ '            │     6      │     1      │
│ "ab\x0acd" │ ab(newline)cd│    10      │     5      │
└────────────┴──────────────┴────────────┴────────────┘
```

### Counting Pattern

```
Every string:
  Code:   +2 for quotes (always)
  Memory: +0 for quotes (never counted)

Regular chars:
  Code:   +1 per character
  Memory: +1 per character

\\ or \":
  Code:   +2 (backslash + char)
  Memory: +1 (just one char)

\xNN:
  Code:   +4 (backslash + x + 2 digits)
  Memory: +1 (just one char)
```

---

## 🎓 Part 2 Teaser

While Part 1 asks about **decoding** (code → memory), Part 2 typically involves **encoding** (memory → code).

### Encoding Example
```
Original code:  "abc"
                ↓
To encode, we need to escape special chars:
  " → \"
  \ → \\

Encoded: "\\"abc\\""
         ↑↑   ↑↑
         Escaped quotes
```

**The formula reverses!**
- Part 1: Code length - Memory length
- Part 2: Encoded length - Code length

---

## 💡 Visual Memory Aid

### The Three Representations

```
┌─────────────────────────────────────────┐
│ What You Type (Source Code)             │
│                                         │
│     "abc\"def"                          │
│                                         │
└─────────────────┬───────────────────────┘
                  │
                  │ Parse escape sequences
                  │
                  ▼
┌─────────────────────────────────────────┐
│ What's In Memory (Runtime String)       │
│                                         │
│     abc"def                             │
│                                         │
└─────────────────┬───────────────────────┘
                  │
                  │ Display/Use
                  │
                  ▼
┌─────────────────────────────────────────┐
│ What You See (Output)                   │
│                                         │
│     abc"def                             │
│                                         │
└─────────────────────────────────────────┘
```

---

## 🧮 Quick Reference for Calculations

### For Each Line:

1️⃣ **Code Length** = `line.Length` (easy!)

2️⃣ **Memory Length** = Count characters, skipping:
   - Opening quote (position 0)
   - Closing quote (last position)
   - Escape backslashes (`\`)
   - Hex escape sequences (`\xNN` counts as 1)

3️⃣ **Difference** = Code Length - Memory Length

4️⃣ **Total** = Sum all differences

---

## 🔧 Pseudocode

```python
function CalculateMemoryLength(line):
    memory = 0
    i = 1  # Skip opening quote
    
    while i < line.length - 1:  # Stop before closing quote
        if line[i] == '\':
            if line[i+1] == 'x':  # Hex escape
                memory += 1
                i += 4  # Skip \xNN
            else:  # \\ or \"
                memory += 1
                i += 2  # Skip \c
        else:  # Regular character
            memory += 1
            i += 1
    
    return memory

function SolvePart1(input):
    totalCodeLength = 0
    totalMemoryLength = 0
    
    for line in input:
        codeLength = line.length
        memoryLength = CalculateMemoryLength(line)
        
        totalCodeLength += codeLength
        totalMemoryLength += memoryLength
    
    return totalCodeLength - totalMemoryLength
```

---

## 📝 Practice Problem

### Test Your Understanding

Calculate the difference for this string:
```
"xy\\z\x41b\"c"
```

<details>
<summary>💡 Step-by-Step Solution</summary>

```
Code: "xy\\z\x41b\"c"

Position: 0  1  2  3  4  5  6  7  8  9 10 11 12 13
Chars:    "  x  y  \  \  z  \  x  4  1  b  \  "  c  "

Code Length: 15 characters

Memory parsing:
i=1:  'x' → regular (memory: 1)
i=2:  'y' → regular (memory: 2)
i=3:  '\' → escape!
i=4:  '\' → escaped backslash (memory: 3, skip to i=5)
i=5:  'z' → regular (memory: 4)
i=6:  '\' → escape!
i=7:  'x' → hex escape!
i=8,9: '41' → 0x41 = 'A' (memory: 5, skip to i=10)
i=10: 'b' → regular (memory: 6)
i=11: '\' → escape!
i=12: '"' → escaped quote (memory: 7, skip to i=13)
i=13: 'c' → regular (memory: 8)

Memory: xy\zAb"c
        ↑↑↑↑↑↑↑↑

Memory Length: 8 characters
Difference: 15 - 8 = 7
```

</details>

---

## 🎯 Summary Checklist

**Before you code:**
- [ ] Understand the three escape types: `\\`, `\"`, `\xNN`
- [ ] Know that quotes are counted in code but not in memory
- [ ] Remember hex escapes are 4 chars in code, 1 in memory

**While coding:**
- [ ] Code length is just `line.Length`
- [ ] Memory length requires parsing escape sequences
- [ ] Start parsing at index 1 (skip opening quote)
- [ ] End parsing at `length - 1` (skip closing quote)
- [ ] Increment `i` correctly for each escape type

**Testing:**
- [ ] Test with the provided examples
- [ ] Verify: `"" → 2-0=2`, `"abc" → 5-3=2`, etc.
- [ ] Check edge cases: strings with multiple escapes

---

**Remember:** Every escape sequence makes the code LONGER than the memory! 📏✨

The key insight: **Characters in code ≠ Characters in memory** when escapes are involved! 🔤
