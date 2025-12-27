# Day 11: Corporate Policy - Step by Step Solution

## Step 1: Understanding the Problem

Santa needs a new password that follows these rules:
1. Must be exactly 8 lowercase letters
2. Must include one increasing straight of at least 3 letters (abc, bcd, cde, etc.)
3. May NOT contain letters: i, o, or l
4. Must contain at least 2 different, non-overlapping pairs of letters (aa, bb, zz, etc.)

**Input:** Current password (8 lowercase letters)  
**Output:** Next valid password

---

## Step 2: Understanding Password Incrementing

Passwords increment like counting in base-26 (a-z instead of 0-9).

### Incrementing Rules:
1. Start from the rightmost letter
2. Increase it by one: a→b, b→c, ..., y→z
3. If it's 'z', wrap to 'a' and carry to the left
4. Continue until no carry is needed

### Examples:
```
xx → xy          (simple increment)
xz → ya          (wrap: z→a, carry: x→y)
azz → baa        (multiple wraps)
zzz → aaaa       (add new digit)
```

---

## Step 3: Implementing Password Increment Function

```csharp
string IncrementPassword(string password)
{
    char[] chars = password.ToCharArray();
    
    // Start from rightmost position
    for (int i = chars.Length - 1; i >= 0; i--)
    {
        if (chars[i] == 'z')
        {
            // Wrap to 'a' and continue carry
            chars[i] = 'a';
        }
        else
        {
            // Increment and stop
            chars[i]++;
            
            // Skip forbidden letters (i, o, l)
            if (chars[i] == 'i' || chars[i] == 'o' || chars[i] == 'l')
            {
                chars[i]++; // Skip to next letter
            }
            
            break;
        }
    }
    
    return new string(chars);
}
```

### Test the function:
```
IncrementPassword("abcdefgh") → "abcdefgi" → skip 'i' → "abcdefgj"
IncrementPassword("abcdefgz") → "abcdefha"
IncrementPassword("abcdezzz") → "abcdfaaa"
```

---

## Step 4: Implementing Rule 1 - Has Increasing Straight

Check if password contains 3 consecutive letters in sequence.

```csharp
bool HasIncreasingStraight(string password)
{
    // Check each position for 3 consecutive letters
    for (int i = 0; i < password.Length - 2; i++)
    {
        char c1 = password[i];
        char c2 = password[i + 1];
        char c3 = password[i + 2];
        
        // Check if they form a straight (each differs by 1)
        if (c2 - c1 == 1 && c3 - c2 == 1)
        {
            return true;
        }
    }
    
    return false;
}
```

### Examples:
```
"abcdefgh" → Position 0-2: abc → 'b'-'a'=1, 'c'-'b'=1 → TRUE ✓
"abbceffg" → No 3 consecutive letters in sequence → FALSE ✗
"hijklmmn" → Position 0-2: hij → TRUE ✓
```

---

## Step 5: Implementing Rule 2 - No Forbidden Letters

Check that password does NOT contain i, o, or l.

```csharp
bool NoForbiddenLetters(string password)
{
    return !password.Contains('i') && 
           !password.Contains('o') && 
           !password.Contains('l');
}
```

### Examples:
```
"hijklmmn" → Contains 'i' and 'l' → FALSE ✗
"abcdefgh" → No forbidden letters → TRUE ✓
"ghijklmn" → Contains 'i' and 'l' → FALSE ✗
```

---

## Step 6: Implementing Rule 3 - Two Different Pairs

Check that password contains at least 2 different, non-overlapping pairs.

```csharp
bool HasTwoDifferentPairs(string password)
{
    HashSet<char> pairsFound = new HashSet<char>();
    
    for (int i = 0; i < password.Length - 1; i++)
    {
        if (password[i] == password[i + 1])
        {
            // Found a pair
            pairsFound.Add(password[i]);
            
            // Skip the next character to avoid overlapping
            i++;
            
            // Early exit if we found 2 different pairs
            if (pairsFound.Count >= 2)
            {
                return true;
            }
        }
    }
    
    return false;
}
```

### Why use HashSet?
- Automatically tracks unique pair letters
- "aaa" → finds 'aa' at position 0, but HashSet only stores 'a' once
- Need 2 DIFFERENT letters in the set

### Examples:
```
"abcdffaa" → Pairs: ff, aa → 2 different → TRUE ✓
"abbceffg" → Pairs: bb, ff → 2 different → TRUE ✓
"abbcegjk" → Pairs: bb only → FALSE ✗
"aaa" → Pairs: aa → Only 1 unique letter → FALSE ✗
```

---

## Step 7: Complete Validation Function

Combine all three rules:

```csharp
bool IsValidPassword(string password)
{
    // Check all three requirements
    return HasIncreasingStraight(password) &&
           NoForbiddenLetters(password) &&
           HasTwoDifferentPairs(password);
}
```

### Test with examples:
```
IsValidPassword("hijklmmn"):
  - HasIncreasingStraight? YES (hij) ✓
  - NoForbiddenLetters? NO (has i, l) ✗
  → INVALID

IsValidPassword("abbceffg"):
  - HasIncreasingStraight? NO ✗
  → INVALID

IsValidPassword("abcdffaa"):
  - HasIncreasingStraight? YES (abc, bcd) ✓
  - NoForbiddenLetters? YES ✓
  - HasTwoDifferentPairs? YES (ff, aa) ✓
  → VALID
```

---

## Step 8: Finding Next Valid Password

Main algorithm:

```csharp
string FindNextValidPassword(string currentPassword)
{
    string password = currentPassword;
    
    // Keep incrementing until we find a valid password
    do
    {
        password = IncrementPassword(password);
    }
    while (!IsValidPassword(password));
    
    return password;
}
```

### Optimization: Skip passwords with forbidden letters

```csharp
string FindNextValidPassword(string currentPassword)
{
    string password = currentPassword;
    
    // Keep incrementing until we find a valid password
    do
    {
        password = IncrementPassword(password);
        
        // If password contains forbidden letter, skip ahead
        if (password.Contains('i') || password.Contains('o') || password.Contains('l'))
        {
            // Find first forbidden letter and skip to next valid letter
            password = SkipForbiddenLetters(password);
        }
    }
    while (!IsValidPassword(password));
    
    return password;
}

string SkipForbiddenLetters(string password)
{
    char[] chars = password.ToCharArray();
    
    for (int i = 0; i < chars.Length; i++)
    {
        if (chars[i] == 'i' || chars[i] == 'o' || chars[i] == 'l')
        {
            // Increment the forbidden letter
            if (chars[i] == 'i') chars[i] = 'j';
            else if (chars[i] == 'o') chars[i] = 'p';
            else if (chars[i] == 'l') chars[i] = 'm';
            
            // Reset all letters to the right to 'a'
            for (int j = i + 1; j < chars.Length; j++)
            {
                chars[j] = 'a';
            }
            
            break;
        }
    }
    
    return new string(chars);
}
```

---

## Step 9: Complete Implementation

```csharp
public static class Day11
{
    public static string SolvePart1(string input)
    {
        return FindNextValidPassword(input.Trim());
    }

    public static string SolvePart2(string input)
    {
        // Part 2: Find the next valid password after the one from Part 1
        string firstPassword = FindNextValidPassword(input.Trim());
        return FindNextValidPassword(firstPassword);
    }

    private static string FindNextValidPassword(string currentPassword)
    {
        string password = currentPassword;
        
        do
        {
            password = IncrementPassword(password);
        }
        while (!IsValidPassword(password));
        
        return password;
    }

    private static string IncrementPassword(string password)
    {
        char[] chars = password.ToCharArray();
        
        for (int i = chars.Length - 1; i >= 0; i--)
        {
            if (chars[i] == 'z')
            {
                chars[i] = 'a';
            }
            else
            {
                chars[i]++;
                
                // Skip forbidden letters
                if (chars[i] == 'i' || chars[i] == 'o' || chars[i] == 'l')
                {
                    chars[i]++;
                }
                
                break;
            }
        }
        
        return new string(chars);
    }

    private static bool IsValidPassword(string password)
    {
        return HasIncreasingStraight(password) &&
               NoForbiddenLetters(password) &&
               HasTwoDifferentPairs(password);
    }

    private static bool HasIncreasingStraight(string password)
    {
        for (int i = 0; i < password.Length - 2; i++)
        {
            if (password[i + 1] - password[i] == 1 && 
                password[i + 2] - password[i + 1] == 1)
            {
                return true;
            }
        }
        return false;
    }

    private static bool NoForbiddenLetters(string password)
    {
        return !password.Contains('i') && 
               !password.Contains('o') && 
               !password.Contains('l');
    }

    private static bool HasTwoDifferentPairs(string password)
    {
        HashSet<char> pairs = new HashSet<char>();
        
        for (int i = 0; i < password.Length - 1; i++)
        {
            if (password[i] == password[i + 1])
            {
                pairs.Add(password[i]);
                i++; // Skip next character
                
                if (pairs.Count >= 2)
                {
                    return true;
                }
            }
        }
        
        return false;
    }
}
```

---

## Step 10: Testing with Examples

### Example 1: `abcdefgh`
```
Iteration 1: "abcdefgh" → "abcdefgi" → skip 'i' → "abcdefgj"
  Valid? HasStraight(abc)✓, NoForbidden✓, TwoPairs(no)✗ → NO

Iteration 2: "abcdefgj" → "abcdefgk"
  Valid? HasStraight(abc)✓, NoForbidden✓, TwoPairs(no)✗ → NO

... (many iterations) ...

Iteration N: "abcdffaa"
  Valid? 
    - HasStraight? "abc" at position 0-2 ✓
    - NoForbidden? No i, o, l ✓
    - TwoPairs? "ff" and "aa" ✓
  → YES!

Answer: "abcdffaa"
```

### Example 2: `ghijklmn`
```
Start: "ghijklmn" (contains 'i' and 'l')

Skip optimization: Jump to "ghjaaaaa"

Continue incrementing...

Eventually reach: "ghjaabcc"
  Valid?
    - HasStraight? "abc" at position 3-5 ✓
    - NoForbidden? No i, o, l ✓
    - TwoPairs? "aa" and "cc" ✓
  → YES!

Answer: "ghjaabcc"
```

---

## Step 11: Performance Considerations

### Time Complexity:
- Per validation: O(n) where n = password length (8)
- Incrementing: O(n) worst case
- Finding next valid: O(k × n) where k = number of attempts

### Optimization Strategies:
1. **Skip forbidden letters immediately** during increment
2. **Check forbidden letters first** (fastest validation)
3. **Early exit** when two pairs are found
4. **Reset to 'a'** after fixing forbidden letter positions

### Typical Performance:
- Most passwords can be found in < 100,000 iterations
- With optimizations: Much faster (skip large ranges)

---

## Step 12: Summary

**Algorithm Flow:**
```
1. Start with current password
2. Increment password by 1 (with forbidden letter skipping)
3. Validate:
   a. Has 3-letter increasing straight?
   b. No forbidden letters (i, o, l)?
   c. Has 2 different pairs?
4. If valid → DONE
   If invalid → Go to step 2
```

**Key Points:**
- ✅ Increment like base-26 counting (rightmost first)
- ✅ Skip forbidden letters during increment (optimization)
- ✅ Use ASCII values to check consecutive letters
- ✅ Use HashSet to track unique pair letters
- ✅ Skip next character after finding a pair (non-overlapping)

**Part 2:**
Simply run the same algorithm on the result from Part 1 to get the next valid password after that.

---

**Happy coding! 🎄🔐**
