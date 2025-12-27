# Day 5: Doesn't He Have Intern-Elves For This? - Step by Step Solution

## Step 1: Understanding the Problem

Determine if strings are "nice" or "naughty" based on different rules.

**Part 1 Rules (ALL must be true):**
1. Contains at least 3 vowels (a, e, i, o, u)
2. Contains at least one letter that appears twice in a row (aa, bb, cc, etc.)
3. Does NOT contain: "ab", "cd", "pq", or "xy"

**Part 2 Rules (ALL must be true):**
1. Contains a pair of any two letters that appears at least twice without overlapping
2. Contains at least one letter which repeats with exactly one letter between them

---

## Step 2: Implementing Part 1 - Rule 1 (Vowel Count)

```csharp
bool HasThreeVowels(string text)
{
    int vowelCount = 0;
    
    foreach (char c in text)
    {
        if ("aeiou".Contains(c))
        {
            vowelCount++;
            if (vowelCount >= 3)
                return true;
        }
    }
    
    return false;
}
```

### Examples:
```
"aei" → 3 vowels ✓
"xazegov" → a, e, o = 3 vowels ✓
"aeiouaeiouaeiou" → many vowels ✓
```

---

## Step 3: Implementing Part 1 - Rule 2 (Double Letters)

```csharp
bool HasDoubleLetter(string text)
{
    for (int i = 0; i < text.Length - 1; i++)
    {
        if (text[i] == text[i + 1])
        {
            return true;
        }
    }
    
    return false;
}
```

### Examples:
```
"xx" → true ✓
"abcdde" → "dd" found ✓
"aabbccdd" → many doubles ✓
```

---

## Step 4: Implementing Part 1 - Rule 3 (Forbidden Substrings)

```csharp
bool HasNoForbiddenStrings(string text)
{
    string[] forbidden = { "ab", "cd", "pq", "xy" };
    
    foreach (string substr in forbidden)
    {
        if (text.Contains(substr))
        {
            return false;
        }
    }
    
    return true;
}
```

### Examples:
```
"haegwjzuvuyypxyu" → contains "xy" ✗
"abcd" → contains "ab" and "cd" ✗
"hello" → no forbidden strings ✓
```

---

## Step 5: Complete Part 1 Validation

```csharp
bool IsNicePart1(string text)
{
    return HasThreeVowels(text) &&
           HasDoubleLetter(text) &&
           HasNoForbiddenStrings(text);
}
```

### Test Examples:
```
"ugknbfddgicrmopn"
  - Vowels: u, o (2) → ✗ Wait, let's recount: u, o, i = 3 ✓
  - Double: "dd" ✓
  - Forbidden: none ✓
  → NICE ✓

"aaa"
  - Vowels: 3 a's ✓
  - Double: "aa" ✓
  - Forbidden: none ✓
  → NICE ✓

"jchzalrnumimnmhp"
  - Vowels: a, u, i, i = 4 ✓
  - Double: none ✗
  → NAUGHTY ✗

"haegwjzuvuyypxyu"
  - Vowels: many ✓
  - Double: "yy" ✓
  - Forbidden: "xy" ✗
  → NAUGHTY ✗

"dvszwmarrgswjxmb"
  - Vowels: only 1 ✗
  → NAUGHTY ✗
```

---

## Step 6: Implementing Part 2 - Rule 1 (Non-overlapping Pairs)

```csharp
bool HasNonOverlappingPair(string text)
{
    for (int i = 0; i < text.Length - 1; i++)
    {
        string pair = text.Substring(i, 2);
        
        // Search for this pair after position i+1
        int nextOccurrence = text.IndexOf(pair, i + 2);
        
        if (nextOccurrence != -1)
        {
            return true;
        }
    }
    
    return false;
}
```

### Why i + 2?
```
Position: 0 1 2 3 4
String:   a a a b c

Pair at 0-1: "aa"
Look from position 2 onwards
Find "aa" at position 1-2? No, that overlaps!
Must start search at position i + 2
```

### Examples:
```
"xyxy" → "xy" appears at 0-1 and 2-3 ✓
"aabcdefgaa" → "aa" appears at 0-1 and 8-9 ✓
"aaa" → "aa" at 0-1 and "aa" at 1-2 → OVERLAPS ✗
```

---

## Step 7: Implementing Part 2 - Rule 2 (Repeat with One Between)

```csharp
bool HasRepeatWithOneBetween(string text)
{
    for (int i = 0; i < text.Length - 2; i++)
    {
        if (text[i] == text[i + 2])
        {
            return true;
        }
    }
    
    return false;
}
```

### Pattern: `xyx`
```
"aba" → a_a (b in between) ✓
"aaa" → a_a (a in between) ✓
"abcdefeghi" → e_e (f in between) ✓
```

---

## Step 8: Complete Part 2 Validation

```csharp
bool IsNicePart2(string text)
{
    return HasNonOverlappingPair(text) &&
           HasRepeatWithOneBetween(text);
}
```

### Test Examples:
```
"qjhvhtzxzqqjkmpb"
  - Non-overlapping: "qj" appears twice ✓
  - Repeat with one between: "zxz" ✓
  → NICE ✓

"xxyxx"
  - Non-overlapping: "xx" appears twice ✓
  - Repeat with one between: "xyx" ✓
  → NICE ✓

"uurcxstgmygtbstg"
  - Non-overlapping: "tg" appears twice ✓
  - Repeat with one between: none ✗
  → NAUGHTY ✗

"ieodomkazucvgmuy"
  - Non-overlapping: none ✗
  → NAUGHTY ✗
```

---

## Step 9: Complete Implementation

```csharp
public static class Day5
{
    public static string SolvePart1(string input)
    {
        int niceCount = 0;
        
        foreach (string line in input.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            if (IsNicePart1(line.Trim()))
                niceCount++;
        }
        
        return niceCount.ToString();
    }

    public static string SolvePart2(string input)
    {
        int niceCount = 0;
        
        foreach (string line in input.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            if (IsNicePart2(line.Trim()))
                niceCount++;
        }
        
        return niceCount.ToString();
    }

    private static bool IsNicePart1(string text)
    {
        return HasThreeVowels(text) &&
               HasDoubleLetter(text) &&
               HasNoForbiddenStrings(text);
    }

    private static bool IsNicePart2(string text)
    {
        return HasNonOverlappingPair(text) &&
               HasRepeatWithOneBetween(text);
    }

    private static bool HasThreeVowels(string text)
    {
        return text.Count(c => "aeiou".Contains(c)) >= 3;
    }

    private static bool HasDoubleLetter(string text)
    {
        for (int i = 0; i < text.Length - 1; i++)
        {
            if (text[i] == text[i + 1])
                return true;
        }
        return false;
    }

    private static bool HasNoForbiddenStrings(string text)
    {
        return !text.Contains("ab") &&
               !text.Contains("cd") &&
               !text.Contains("pq") &&
               !text.Contains("xy");
    }

    private static bool HasNonOverlappingPair(string text)
    {
        for (int i = 0; i < text.Length - 1; i++)
        {
            string pair = text.Substring(i, 2);
            if (text.IndexOf(pair, i + 2) != -1)
                return true;
        }
        return false;
    }

    private static bool HasRepeatWithOneBetween(string text)
    {
        for (int i = 0; i < text.Length - 2; i++)
        {
            if (text[i] == text[i + 2])
                return true;
        }
        return false;
    }
}
```

---

## Step 10: Common Mistakes

### Mistake 1: Overlapping Pairs
```csharp
// WRONG - Allows overlapping
if (text.IndexOf(pair, i + 1) != -1)

// CORRECT - Prevents overlapping
if (text.IndexOf(pair, i + 2) != -1)
```

### Mistake 2: Checking All Substrings
```csharp
// INEFFICIENT - Checks every substring
for (int i = 0; i < text.Length; i++)
    for (int j = i + 1; j < text.Length; j++)
        // check all substrings

// EFFICIENT - Only check pairs
for (int i = 0; i < text.Length - 1; i++)
    // check only 2-character substrings
```

---

## Step 11: Complexity Analysis

### Time Complexity:
- HasThreeVowels: O(n)
- HasDoubleLetter: O(n)
- HasNoForbiddenStrings: O(n)
- HasNonOverlappingPair: O(n²) worst case
- HasRepeatWithOneBetween: O(n)

### Space Complexity:
- O(1) for all checks

---

## Step 12: Summary

**Part 1 Rules:**
- ✅ At least 3 vowels
- ✅ At least one double letter
- ✅ No forbidden strings (ab, cd, pq, xy)

**Part 2 Rules:**
- ✅ Non-overlapping pair appears twice
- ✅ Letter repeats with one letter between

**Key Concepts:**
- Multiple condition checking
- Pattern matching
- Substring searching
- Overlapping vs non-overlapping

**Happy string checking! 🎄📝**
