# 🔐 Day 11 Visual Guide - Corporate Policy Password

## 🎯 Understanding Password Incrementing

### Base-26 Counting System
```
Like counting in base-10, but with letters a-z:

Decimal:  0  1  2  3  ... 9  10  11
Letters:  a  b  c  d  ... z  aa  ab

Key concept: z wraps to a (like 9 wraps to 0)
```

**Incrementing Rules:**
- Start from the **rightmost** letter
- Increase it by one step: `a→b`, `b→c`, etc.
- If it's `z`, wrap to `a` and carry left
- Repeat until no carry needed

---

## 📊 Increment Examples

### Example 1: Simple increment `xx` → `xy`
```
Input:  x x
           ↑
    Increment rightmost

x → y (no carry needed)

Result: x y
```

### Example 2: Single wrap `xz` → `ya`
```
Input:  x z
           ↑
    z wraps to a

Step 1: z → a (carry 1)
        x z
          ↑
        wraps!

Step 2: x + carry → y
        y a
        ↑
    carry applied

Result: y a
```

### Example 3: Multiple wraps `azz` → `baa`
```
Input:  a z z
             ↑
    Start rightmost

Step 1: z → a (carry 1)
        a z a
            ↑
        wrapped

Step 2: z + carry → a (carry 1)
        a a a
          ↑
        wrapped again

Step 3: a + carry → b
        b a a
        ↑
    final carry

Result: b a a

Visual flow:
azz → aza → aaa → baa
    ↑     ↑     ↑
  Right  Middle Left
```

### Example 4: All z's `zzz` → `aaaa`
```
Input:  z z z
             ↑

Step 1: z → a (carry)
        z z a
            ↑

Step 2: z + carry → a (carry)
        z a a
          ↑

Step 3: z + carry → a (carry)
        a a a
        ↑

Step 4: Need new digit!
        a a a a
        ↑
    adds new digit

Result: a a a a

Like: 999 + 1 = 1000
```

---

## 📋 Part 1: Password Requirements

### Three Rules (ALL must be met)
```
┌──────────────────────────────────────────────────────┐
│ 1. ✓ Contains increasing straight of 3+ letters     │
│    (abc, bcd, cde, ... xyz)                          │
│                                                       │
│ 2. ✓ Does NOT contain: i, o, or l                   │
│    (confusing letters)                               │
│                                                       │
│ 3. ✓ Contains at least 2 different non-overlapping  │
│    pairs (aa, bb, cc, etc.)                          │
└──────────────────────────────────────────────────────┘

All three → VALID ✓
Missing any → INVALID ✗
```

---

## 🔍 Rule 1: Increasing Straight

### What Counts as "Straight"
```
Valid straights (consecutive letters):
abc ✓   bcd ✓   cde ✓   def ✓
efg ✓   fgh ✓   ghi ✓   hij ✓
ijk ✓   jkl ✓   klm ✓   lmn ✓
mno ✓   nop ✓   opq ✓   pqr ✓
qrs ✓   rst ✓   stu ✓   tuv ✓
uvw ✓   vwx ✓   wxy ✓   xyz ✓

Invalid (not consecutive):
abd ✗ (skips c)
acd ✗ (skips b)
ace ✗ (skips b and d)
```

### Visual Check
```
Password: hijklmmn

Looking for straight:
h i j k l m m n
↑ ↑ ↑
h-i-j? 
  h(104) → i(105) → j(106)
  Difference: +1, +1 ✓
  THREE consecutive → VALID ✓

Also:
h i j k l m m n
    ↑ ↑ ↑
    j-k-l? Yes ✓

Multiple straights is fine!
```

### ASCII Value Method
```csharp
// Check if three letters form straight
char c1 = 'a', c2 = 'b', c3 = 'c';

c2 - c1 == 1  →  98 - 97 = 1 ✓
c3 - c2 == 1  →  99 - 98 = 1 ✓

Straight confirmed! ✓
```

---

## 🔍 Rule 2: Forbidden Letters

### Banned Characters
```
i → Can look like 1 or l
o → Can look like 0
l → Can look like 1 or I

These are NEVER allowed in password!
```

### Check Examples
```
Password: hijklmmn
         ↑  ↑
       Contains i and l → INVALID ✗

Password: abcdffaa
         No i, o, or l → VALID ✓

Password: ghijklmn
            ↑  ↑
         Contains i and l → INVALID ✗
         
Password: ghjaaacc
         No forbidden letters → VALID ✓
```

### Quick Skip Strategy
```
When incrementing, if you hit:
i → Skip to j
o → Skip to p  
l → Skip to m

Example: ghizzzz → ghjaaaa
                    ↑
            Skipped 'i'
```

---

## 🔍 Rule 3: Two Different Pairs

### What Counts as Valid Pairs
```
Valid pairs (same letter twice):
aa ✓   bb ✓   cc ✓   dd ✓
ee ✓   ff ✓   gg ✓   ... zz ✓

Requirements:
1. Must be DIFFERENT pairs (not aa and aa)
2. Must be NON-OVERLAPPING
```

### Example Checks

**Valid: `abbceffg`**
```
a b b c e f f g
  ↑↑       ↑↑
  bb       ff

Two different pairs: bb and ff ✓
```

**Invalid: `abbcegjk`**
```
a b b c e g j k
  ↑↑
Only one pair: bb ✗

Need at least TWO different pairs!
```

**Valid: `aabbccdd`**
```
a a b b c c d d
↑↑ ↑↑ ↑↑ ↑↑

Pairs: aa, bb, cc, dd
Using any two different ones is enough ✓
```

**Edge case: `aaa`**
```
a a a
↑↑ ↑↑
Can be seen as two 'aa' pairs

But they're the SAME pair type!
Need DIFFERENT letters ✗

Think of it as:
- One 'aa' pair
- Not a second different pair
```

**Valid: `aabaa`**
```
a a b a a
↑↑   ↑↑

Wait, both are 'aa' pairs!
Same letter → Only counts as ONE pair type ✗
```

**Valid: `aaabbb`**
```
a a a b b b
↑↑   ↑↑

Pair 1: aa (first two a's)
Pair 2: bb (first two b's)

Two DIFFERENT pairs ✓
```

---

## 📊 Complete Validation Examples

### Example 1: `hijklmmn` ✗ INVALID

**Rule 1: Increasing straight?**
```
h i j k l m m n
↑ ↑ ↑
hij → Straight found ✓
```

**Rule 2: No forbidden letters?**
```
h i j k l m m n
  ↑     ↑
Contains 'i' and 'l' → INVALID ✗
```

**Result:**
```
┌──────────────────────┬────────┐
│ Rule                 │ Status │
├──────────────────────┼────────┤
│ Increasing straight  │ ✓ PASS │
│ No i/o/l             │ ✗ FAIL │ ← Stops here
│ Two pairs            │ (skip) │
├──────────────────────┼────────┤
│ FINAL                │✗INVALID│
└──────────────────────┴────────┘
```

---

### Example 2: `abbceffg` ✗ INVALID

**Rule 1: Increasing straight?**
```
a b b c e f f g

Check all positions:
abc? a-b-c → 97-98-99 ✓ FOUND!

Straight found ✓
```

**Rule 2: No forbidden letters?**
```
a b b c e f f g

Checking for i, o, l:
No forbidden letters ✓
```

**Rule 3: Two different pairs?**
```
a b b c e f f g
  ↑↑       ↑↑
  bb       ff

Pairs found: bb, ff
Different letters ✓
```

**Wait, this should be VALID?**
```
Actually, let's recheck Rule 1:

a b b c e f f g
↑ ↑ ↑
abc? a(97), b(98), c(99)
     98-97=1 ✓, 99-98=1 ✓

Straight is there... but in problem description,
this is marked as INVALID for not having straight.

Ah! Let's check more carefully:
a b b c e f f g
↑ ↑ ↑
Position 0,1,2: a,b,b
a-b-b: a(97), b(98), b(98)
       98-97=1, 98-98=0 ✗

Not consecutive!

The 'bb' breaks the pattern!

Need: a b c (positions next to each other)
Have: a b b c (extra b breaks sequence)

Actually valid straight must be:
a(pos 0) - b(pos 1) - c(pos 3)
But they must be CONSECUTIVE positions!
```

**Correct Rule 1 Check:**
```
Must find THREE CONSECUTIVE positions
where each increases by 1:

Position: 0 1 2 3 4 5 6 7
Letter:   a b b c e f f g

Check pos 0,1,2: a,b,b → b=b ✗
Check pos 1,2,3: b,b,c → b=b ✗
Check pos 2,3,4: b,c,e → 99-98=1✓, 101-99=2✗
Check pos 3,4,5: c,e,f → 101-99=2✗
Check pos 4,5,6: e,f,f → f=f ✗
Check pos 5,6,7: f,f,g → f=f ✗

No valid straight! ✗
```

**Result:**
```
┌──────────────────────┬────────┐
│ Rule                 │ Status │
├──────────────────────┼────────┤
│ Increasing straight  │ ✗ FAIL │ ← Fails here
│ No i/o/l             │ (skip) │
│ Two pairs            │ (skip) │
├──────────────────────┼────────┤
│ FINAL                │✗INVALID│
└──────────────────────┴────────┘
```

---

### Example 3: `abbcegjk` ✗ INVALID

**Rule 1: Increasing straight?**
```
a b b c e g j k

Check consecutive positions:
Position 0,1,2: a,b,b → ✗
Position 1,2,3: b,b,c → ✗
Position 2,3,4: b,c,e → c-b=1✓, e-c=2✗
Position 3,4,5: c,e,g → ✗
Position 4,5,6: e,g,j → ✗
Position 5,6,7: g,j,k → j-g=3✗

No straight ✗
```

**Result:**
```
┌──────────────────────┬────────┐
│ Rule                 │ Status │
├──────────────────────┼────────┤
│ Increasing straight  │ ✗ FAIL │
│ No i/o/l             │ (skip) │
│ Two pairs            │ (skip) │
├──────────────────────┼────────┤
│ FINAL                │✗INVALID│
└──────────────────────┴────────┘

(Even though it has 'bb' pair, fails first rule)
```

---

### Example 4: `abcdffaa` ✓ VALID

**Rule 1: Increasing straight?**
```
a b c d f f a a
↑ ↑ ↑
Position 0,1,2: a,b,c
  b-a = 98-97 = 1 ✓
  c-b = 99-98 = 1 ✓

Straight found! ✓

Also:
a b c d f f a a
  ↑ ↑ ↑
Position 1,2,3: b,c,d ✓
```

**Rule 2: No forbidden letters?**
```
a b c d f f a a

No i, o, or l present ✓
```

**Rule 3: Two different pairs?**
```
a b c d f f a a
        ↑↑   ↑↑
        ff   aa

Two pairs: ff and aa
Different letters ✓
```

**Result:**
```
┌──────────────────────┬────────┐
│ Rule                 │ Status │
├──────────────────────┼────────┤
│ Increasing straight  │ ✓ PASS │
│ No i/o/l             │ ✓ PASS │
│ Two pairs            │ ✓ PASS │
├──────────────────────┼────────┤
│ FINAL                │ ✓ VALID│
└──────────────────────┴────────┘
```

---

## 🎯 Finding Next Valid Password

### Process Overview
```
1. Start with current password
2. Increment by 1
3. Check all three rules
4. If invalid → increment and repeat
5. If valid → done!
```

### Example: `abcdefgh` → ?

**Iteration 1: `abcdefgh` → `abcdefgi`**
```
Check rules:
1. Straight? Multiple (abc, bcd, cde, def, efg)✓
2. No i/o/l? Contains 'i' at end ✗

Invalid! Continue...
```

**Optimization: Skip forbidden letters**
```
When we hit 'i', jump to 'j':
abcdefgi → abcdefgj

But actually, we should increment:
abcdefgh + 1 = abcdefgi (has i)

Better: When incrementing, skip i/o/l automatically!
```

**Continue incrementing...**
```
(Many iterations later)

abcdffaa:
1. Straight? abc ✓
2. No i/o/l? None ✓
3. Two pairs? ff, aa ✓

VALID! ✓

Answer: abcdffaa
```

---

### Example: `ghijklmn` → ?

**Start: `ghijklmn`**
```
Contains 'i' and 'l' → Invalid

Skip optimization:
When we see 'i' at position 2, can jump ahead:
ghijklmn
  ↑
  
Skip everything with 'i' → jump to 'j'
ghjaaaaa (reset everything after 'i' to 'a')
```

**From `ghjaaaaa`:**
```
Check rules:
1. Straight? No consecutive ✗
Continue incrementing...
```

**Eventually reach: `ghjaabcc`**
```
g h j a a b c c

1. Straight? 
   Check all positions:
   - ghi? No 'i' skipped
   - hja? Not consecutive ✗
   - jab? Not consecutive ✗
   - abc? a-b=1✓, c-b=1✓ ✓ FOUND
   
2. No i/o/l? None ✓

3. Two pairs?
   g h j a a b c c
         ↑↑     ↑↑
         aa     cc
   Two different pairs ✓

VALID! ✓

Answer: ghjaabcc
```

---

## 💻 Algorithm Implementation

### Main Loop
```csharp
string password = "abcdefgh";

while (!IsValid(password))
{
    password = Increment(password);
}

// password now contains next valid password
```

### Increment Function
```csharp
string Increment(string password)
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
            
            // Skip forbidden letters
            if (chars[i] == 'i' || chars[i] == 'o' || chars[i] == 'l')
                chars[i]++;
            
            break;
        }
    }
    
    return new string(chars);
}

// Example trace:
// "abcdefgh" → 'h' becomes 'i' → skip to 'j'
// Result: "abcdefgj"
```

### Validation Function
```csharp
bool IsValid(string password)
{
    return HasStraight(password) 
        && NoForbidden(password) 
        && HasTwoPairs(password);
}
```

### Rule 1: Check Straight
```csharp
bool HasStraight(string password)
{
    for (int i = 0; i < password.Length - 2; i++)
    {
        char c1 = password[i];
        char c2 = password[i + 1];
        char c3 = password[i + 2];
        
        // Check if consecutive increasing
        if (c2 - c1 == 1 && c3 - c2 == 1)
            return true;
    }
    return false;
}

// Example: "abcdffaa"
// i=0: a,b,c → 98-97=1, 99-98=1 → true ✓
```

### Rule 2: Check Forbidden
```csharp
bool NoForbidden(string password)
{
    return !password.Contains('i') 
        && !password.Contains('o') 
        && !password.Contains('l');
}

// Example: "abcdffaa"
// No 'i', no 'o', no 'l' → true ✓
```

### Rule 3: Check Pairs
```csharp
bool HasTwoPairs(string password)
{
    List<char> pairs = new List<char>();
    
    for (int i = 0; i < password.Length - 1; i++)
    {
        if (password[i] == password[i + 1])
        {
            char pairChar = password[i];
            
            // Add if not already found
            if (!pairs.Contains(pairChar))
                pairs.Add(pairChar);
            
            // Skip the second character of pair
            i++;
            
            // Early exit if we found 2
            if (pairs.Count >= 2)
                return true;
        }
    }
    
    return false;
}

// Example: "abcdffaa"
// i=4: 'f'='f' → Add 'f', skip to i=6
// i=6: 'a'='a' → Add 'a', count=2 → true ✓
```

---

## 🔄 Step-by-Step Trace

### Finding Next After `abcdefgh`

```
Iteration: Password        Check Result
────────────────────────────────────────
0:         abcdefgh        [Initial]
1:         abcdefgi        Has 'i' ✗
2:         abcdefgj        No straight ✗
3:         abcdefgk        No straight ✗
...
(many iterations)
...
N:         abcdffaa        ✓✓✓ VALID!

Rules check for abcdffaa:
┌─────────────────┬────────┬──────────┐
│ Rule            │ Status │ Details  │
├─────────────────┼────────┼──────────┤
│ Has straight    │ ✓      │ abc, bcd │
│ No forbidden    │ ✓      │ None     │
│ Two pairs       │ ✓      │ ff, aa   │
└─────────────────┴────────┴──────────┘

Answer: abcdffaa
```

---

## 🎨 Visual Memory Aids

### Password Rules Mnemonic: "SFP"
```
S = Straight (3 consecutive)
F = Forbidden (no i, o, l)
P = Pairs (2 different)

All three needed!
```

### Incrementing Memory Aid
```
Think of odometer rolling over:

099 → 100
azz → baa
 ↑     ↑
Roll  Carry
```

### Forbidden Letters
```
i → Looks like 1 or l
o → Looks like 0
l → Looks like 1 or I

IOL = Input/Output/Line? 
Too confusing! ❌
```

---

## 🐛 Common Mistakes

### Mistake 1: Not Checking Consecutive Positions
```csharp
// WRONG - Checks if letters are in sequence anywhere
bool HasStraight(string pwd)
{
    return pwd.Contains("abc") || pwd.Contains("bcd") /*...*/;
}

// This only checks specific substrings!
// Won't find "xyz" or other valid straights

// CORRECT - Checks actual consecutive positions
bool HasStraight(string pwd)
{
    for (int i = 0; i < pwd.Length - 2; i++)
    {
        if (pwd[i+1] - pwd[i] == 1 && 
            pwd[i+2] - pwd[i+1] == 1)
            return true;
    }
    return false;
}
```

### Mistake 2: Counting Same Pair Twice
```csharp
// WRONG - 'aaa' would count as 2 pairs
bool HasTwoPairs(string pwd)
{
    int count = 0;
    for (int i = 0; i < pwd.Length - 1; i++)
    {
        if (pwd[i] == pwd[i + 1])
            count++;
    }
    return count >= 2;
}

// "aaa" → counts aa at i=0 and aa at i=1
// But both are the SAME letter! ✗

// CORRECT - Track unique pair letters
bool HasTwoPairs(string pwd)
{
    HashSet<char> pairs = new HashSet<char>();
    for (int i = 0; i < pwd.Length - 1; i++)
    {
        if (pwd[i] == pwd[i + 1])
        {
            pairs.Add(pwd[i]);
            i++; // Skip next character
        }
    }
    return pairs.Count >= 2;
}
```

### Mistake 3: Not Skipping After Finding Pair
```csharp
// WRONG - Can count overlapping
for (int i = 0; i < pwd.Length - 1; i++)
{
    if (pwd[i] == pwd[i + 1])
        pairs.Add(pwd[i]);
    // Missing: i++
}

// "aaa" would check:
//   i=0: aa ✓
//   i=1: aa ✓ (overlaps!)

// CORRECT - Skip the paired character
if (pwd[i] == pwd[i + 1])
{
    pairs.Add(pwd[i]);
    i++; // Skip next position
}
```

### Mistake 4: Not Handling 'z' Wrapping
```csharp
// WRONG - Doesn't handle end of alphabet
string Increment(string pwd)
{
    char[] chars = pwd.ToCharArray();
    chars[chars.Length - 1]++;
    return new string(chars);
}

// "abcdefgz" + 1 → "abcdefg{" (invalid char!)

// CORRECT - Handle wrapping
string Increment(string pwd)
{
    char[] chars = pwd.ToCharArray();
    for (int i = chars.Length - 1; i >= 0; i--)
    {
        if (chars[i] == 'z')
            chars[i] = 'a'; // Wrap and continue
        else
        {
            chars[i]++;
            break; // Stop carrying
        }
    }
    return new string(chars);
}
```

---

## 📈 Optimization Strategies

### Strategy 1: Skip Forbidden Letters on Increment
```csharp
// Instead of:
chars[i]++;

// Use:
chars[i]++;
if (chars[i] == 'i' || chars[i] == 'o' || chars[i] == 'l')
    chars[i]++; // Skip to next letter

// Saves many iterations!
```

### Strategy 2: Reset After Forbidden Letter
```csharp
// If we find 'i' at position 3:
// "abcixyz" → "abcjzzz" won't work

// Better: "abcixyz" → "abcjaaaa"
// Start fresh after fixing forbidden letter

if (password.Contains('i'))
{
    int pos = password.IndexOf('i');
    password = password.Substring(0, pos) + 'j' +
               new string('a', password.Length - pos - 1);
}
```

### Strategy 3: Early Exit on Validation
```csharp
// Check forbidden FIRST (fastest check)
bool IsValid(string pwd)
{
    if (!NoForbidden(pwd)) return false;  // Fastest
    if (!HasStraight(pwd)) return false;  // Medium
    if (!HasTwoPairs(pwd)) return false;  // Slowest
    return true;
}
```

---

## 📊 Complexity Analysis

### Time Complexity
```
Per validation:
- HasStraight: O(n) where n = password length
- NoForbidden: O(n)
- HasTwoPairs: O(n)
Total per check: O(n)

Finding next valid:
- Worst case: O(k * n) where k = attempts needed
- For 8-char password: k can be large
- Average case: Much better with optimizations
```

### Space Complexity
```
O(n) for storing password string
O(1) for validation (no extra structures except small HashSet)
```

---

## 🎯 Practice Problems

### Problem 1: Increment `abc` three times
```
abc → abd → abe → abf

Each increment adds 1 to rightmost letter.
No wrapping needed.
```

### Problem 2: Increment `xyz` once
```
xyz → xy[z+1]
    → xy[wrap to a, carry 1]
    → x[y+1]a
    → xza

Result: xza
```

### Problem 3: Is `abccddee` valid?
```
Check:
1. Straight? 
   abc? a-b=1✓, c-b=1✓ → Yes ✓
   
2. No i/o/l? 
   No forbidden letters ✓
   
3. Two pairs?
   cc, dd, ee → Yes (multiple) ✓

VALID ✓
```

### Problem 4: Is `abcdabcd` valid?
```
Check:
1. Straight?
   abc? Yes ✓
   bcd? Yes ✓
   
2. No i/o/l? None ✓

3. Two pairs?
   Looking for doubles...
   abcdabcd
   No consecutive same letters ✗

INVALID ✗
```

---

## 📝 Summary

**Password Incrementing:**
- Like counting: a→b→c→...→z→aa→ab→...
- Rightmost letter increases first
- z wraps to a with carry left

**Three Validation Rules:**
1. 🔤 **Straight**: 3 consecutive letters (abc, bcd, etc.)
2. 🚫 **Forbidden**: No i, o, or l
3. 🎯 **Pairs**: 2 different pairs (aa, bb, etc.)

**Key Algorithm:**
```
1. Start with current password
2. Increment
3. Validate (all 3 rules)
4. Repeat until valid
```

**Optimization Tips:**
- ⚡ Skip forbidden letters during increment
- ⚡ Check forbidden first (fastest validation)
- ⚡ Reset to 'a' after fixing forbidden letter

**Memory Aid:**
```
S-F-P: Straight, Forbidden, Pairs
All three required for valid password!
```

---

**Happy password cracking! 🔐🎄**
