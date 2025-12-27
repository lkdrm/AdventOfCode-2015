# Day 8: Matchsticks - Step by Step Solution

## Step 1: Understanding the Problem

Calculate the difference between code representation and in-memory representation of strings.

**Part 1:** Code length - Memory length  
**Part 2:** Encoded length - Code length

**Escape sequences:**
- `\\` → Single backslash `\`
- `\"` → Single quote `"`
- `\xNN` → Single character (hex code)

---

## Step 2: Understanding String Representations

### Example: `"abc"`
```
Code:   "abc"     → 5 characters
Memory: abc       → 3 characters
Difference: 5 - 3 = 2
```

### Example: `"aaa\"aaa"`
```
Code:   "aaa\"aaa"  → 10 characters
Memory: aaa"aaa     → 7 characters
                     (\" becomes ")
Difference: 10 - 7 = 3
```

### Example: `"\x27"`
```
Code:   "\x27"    → 6 characters
Memory: '         → 1 character
                   (\x27 is apostrophe)
Difference: 6 - 1 = 5
```

---

## Step 3: Algorithm for Part 1

```csharp
int CalculateMemoryLength(string code)
{
    int length = 0;
    int i = 1; // Skip opening quote
    
    while (i < code.Length - 1) // Skip closing quote
    {
        if (code[i] == '\\')
        {
            if (code[i + 1] == '\\' || code[i + 1] == '"')
            {
                // \\ or \" → one character
                length++;
                i += 2;
            }
            else if (code[i + 1] == 'x')
            {
                // \xNN → one character
                length++;
                i += 4; // Skip \xNN
            }
        }
        else
        {
            // Regular character
            length++;
            i++;
        }
    }
    
    return length;
}
```

---

## Step 4: Trace Example Part 1

### Input: `"abc"`
```
Code: " a b c "
      0 1 2 3 4

i=1: 'a' → regular → length=1, i=2
i=2: 'b' → regular → length=2, i=3
i=3: 'c' → regular → length=3, i=4
i=4: '"' → stop (closing quote)

Memory length: 3
Code length: 5
Difference: 2 ✓
```

### Input: `"aaa\"aaa"`
```
Code: " a a a \ " a a a "
      0 1 2 3 4 5 6 7 8 9

i=1: 'a' → regular → length=1, i=2
i=2: 'a' → regular → length=2, i=3
i=3: 'a' → regular → length=3, i=4
i=4: '\' → check next
  i+1: '"' → \" escape
  → length=4, i=6
i=6: 'a' → regular → length=5, i=7
i=7: 'a' → regular → length=6, i=8
i=8: 'a' → regular → length=7, i=9
i=9: '"' → stop

Memory length: 7
Code length: 10
Difference: 3 ✓
```

### Input: `"\x27"`
```
Code: " \ x 2 7 "
      0 1 2 3 4 5

i=1: '\' → check next
  i+1: 'x' → \xNN escape
  → length=1, i=5
i=5: '"' → stop

Memory length: 1
Code length: 6
Difference: 5 ✓
```

---

## Step 5: Algorithm for Part 2 (Encoding)

```csharp
int CalculateEncodedLength(string code)
{
    int length = 2; // Opening and closing quotes
    
    foreach (char c in code)
    {
        if (c == '"' || c == '\\')
        {
            // Need to escape: " → \" or \ → \\
            length += 2;
        }
        else
        {
            // Regular character
            length++;
        }
    }
    
    return length;
}
```

---

## Step 6: Trace Example Part 2

### Input: `"abc"`
```
Original: "abc"

Encode each character:
  " → \"    (2 chars)
  a → a     (1 char)
  b → b     (1 char)
  c → c     (1 char)
  " → \"    (2 chars)

Encoded: "\"abc\""
Length: 9
Original: 5
Difference: 4 ✓
```

### Input: `"aaa\"aaa"`
```
Original: "aaa\"aaa"

Encode:
  " → \"
  a → a
  a → a
  a → a
  \ → \\
  " → \"
  a → a
  a → a
  a → a
  " → \"

Encoded: "\"aaa\\\"aaa\""
Length: 16
Original: 10
Difference: 6 ✓
```

---

## Step 7: Complete Implementation

```csharp
public static class Day8
{
    public static string SolvePart1(string input)
    {
        int totalCode = 0;
        int totalMemory = 0;
        
        foreach (string line in input.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            
            string code = line.Trim();
            totalCode += code.Length;
            totalMemory += CalculateMemoryLength(code);
        }
        
        return (totalCode - totalMemory).ToString();
    }

    public static string SolvePart2(string input)
    {
        int totalCode = 0;
        int totalEncoded = 0;
        
        foreach (string line in input.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            
            string code = line.Trim();
            totalCode += code.Length;
            totalEncoded += CalculateEncodedLength(code);
        }
        
        return (totalEncoded - totalCode).ToString();
    }

    private static int CalculateMemoryLength(string code)
    {
        int length = 0;
        int i = 1; // Skip opening quote
        
        while (i < code.Length - 1)
        {
            if (code[i] == '\\')
            {
                if (code[i + 1] == '\\' || code[i + 1] == '"')
                {
                    length++;
                    i += 2;
                }
                else if (code[i + 1] == 'x')
                {
                    length++;
                    i += 4;
                }
                else
                {
                    length++;
                    i++;
                }
            }
            else
            {
                length++;
                i++;
            }
        }
        
        return length;
    }

    private static int CalculateEncodedLength(string code)
    {
        int length = 2; // Surrounding quotes
        
        foreach (char c in code)
        {
            if (c == '"' || c == '\\')
                length += 2;
            else
                length++;
        }
        
        return length;
    }
}
```

---

## Step 8: Common Mistakes

### Mistake 1: Not Skipping Quotes
```csharp
// WRONG - Includes quotes in count
for (int i = 0; i < code.Length; i++)

// CORRECT - Skip opening and closing quotes
for (int i = 1; i < code.Length - 1; i++)
```

### Mistake 2: Wrong Hex Escape Length
```csharp
// WRONG - \xNN is 4 characters
i += 2;

// CORRECT - Skip all 4 characters
i += 4; // \, x, N, N
```

---

## Step 9: Summary

**Part 1:**
- Parse escape sequences
- Count actual characters in memory
- Compare with code length

**Part 2:**
- Encode by escaping special characters
- Count encoded length
- Compare with original

**Key escape sequences:**
- `\\` → `\`
- `\"` → `"`
- `\xNN` → single char

**Happy parsing! 🎄📝**
