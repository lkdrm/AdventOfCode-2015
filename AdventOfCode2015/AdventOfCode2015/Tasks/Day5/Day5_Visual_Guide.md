# 🎄 Day 5 Visual Guide - Naughty or Nice Strings

## 📋 Part 1: Original Rules

### Three Requirements (ALL must be met)
```
┌─────────────────────────────────────────────────┐
│ 1. ✓ At least 3 vowels (a, e, i, o, u)         │
│ 2. ✓ At least one double letter (xx, aa, etc.) │
│ 3. ✓ NO forbidden substrings (ab, cd, pq, xy)  │
└─────────────────────────────────────────────────┘

All three → NICE ✓
Missing any → NAUGHTY ✗
```

---

## 📊 Part 1: Step-by-Step Examples

### Example 1: `ugknbfddgicrmopn` ✓ NICE

**Step 1: Check vowels**
```
u g k n b f d d g i c r m o p n
↑             ↑       ↑
vowel       vowel   vowel

Vowels found: u, i, o
Count: 3 ✓ (requirement met)
```

**Step 2: Check double letters**
```
u g k n b f d d g i c r m o p n
            ↑ ↑
         Same letter!

Double letter: dd ✓ (requirement met)
```

**Step 3: Check forbidden substrings**
```
Checking for: ab, cd, pq, xy

ugknbfddgicrmopn
- Contains "ab"? No ✓
- Contains "cd"? No ✓
- Contains "pq"? No ✓
- Contains "xy"? No ✓

No forbidden substrings ✓ (requirement met)
```

**Result:**
```
┌──────────────────┬────────┐
│ Rule             │ Status │
├──────────────────┼────────┤
│ 3+ vowels        │ ✓ PASS │
│ Double letter    │ ✓ PASS │
│ No forbidden     │ ✓ PASS │
├──────────────────┼────────┤
│ FINAL            │ ✓ NICE │
└──────────────────┴────────┘
```

---

### Example 2: `aaa` ✓ NICE

**Step 1: Check vowels**
```
a a a
↑ ↑ ↑
All vowels!

Count: 3 ✓
```

**Step 2: Check double letters**
```
a a a
↑ ↑
aa ✓

Also:
a a a
  ↑ ↑
  aa ✓

Multiple doubles (both count) ✓
```

**Step 3: Check forbidden**
```
"aaa" doesn't contain: ab, cd, pq, or xy ✓
```

**Result:**
```
Special case: Same letters satisfy multiple rules!
- Vowel count: 3 ✓
- Double letter: Yes ✓
- No forbidden: Yes ✓

NICE ✓
```

---

### Example 3: `jchzalrnumimnmhp` ✗ NAUGHTY

**Step 1: Check vowels**
```
j c h z a l r n u m i m n m h p
        ↑       ↑ ↑
      vowels

Vowels: a, u, i
Count: 3 ✓
```

**Step 2: Check double letters**
```
j c h z a l r n u m i m n m h p
  
Looking for consecutive identical letters:
j≠c, c≠h, h≠z, z≠a, a≠l, l≠r, r≠n,
n≠u, u≠m, m≠i, i≠m, m≠n, n≠m, m≠h, h≠p

No double letters found ✗ FAIL
```

**Result:**
```
┌──────────────────┬────────┐
│ Rule             │ Status │
├──────────────────┼────────┤
│ 3+ vowels        │ ✓ PASS │
│ Double letter    │ ✗ FAIL │ ← Breaks here
│ No forbidden     │ (skip) │
├──────────────────┼────────┤
│ FINAL            │✗NAUGHTY│
└──────────────────┴────────┘

Fails because no double letter!
```

---

### Example 4: `haegwjzuvuyypxyu` ✗ NAUGHTY

**Step 1: Check vowels**
```
h a e g w j z u v u y y p x y u
  ↑ ↑       ↑   ↑           ↑
         vowels

Vowels: a, e, u, u, u
Count: 5 ✓
```

**Step 2: Check double letters**
```
h a e g w j z u v u y y p x y u
                    ↑ ↑
                   yy ✓

Double found: yy ✓
```

**Step 3: Check forbidden**
```
Checking: ab, cd, pq, xy

h a e g w j z u v u y y p x y u
                          ↑ ↑
                          xy ✗

Contains forbidden substring "xy" ✗ FAIL
```

**Result:**
```
┌──────────────────┬────────┐
│ Rule             │ Status │
├──────────────────┼────────┤
│ 3+ vowels        │ ✓ PASS │
│ Double letter    │ ✓ PASS │
│ No forbidden     │ ✗ FAIL │ ← Breaks here
├──────────────────┼────────┤
│ FINAL            │✗NAUGHTY│
└──────────────────┴────────┘
```

---

### Example 5: `dvszwmarrgswjxmb` ✗ NAUGHTY

**Step 1: Check vowels**
```
d v s z w m a r r g s w j x m b
            ↑
          Only one vowel!

Vowels: a
Count: 1 ✗ FAIL (need at least 3)
```

**Result:**
```
┌──────────────────┬────────┐
│ Rule             │ Status │
├──────────────────┼────────┤
│ 3+ vowels        │ ✗ FAIL │ ← Breaks here
│ Double letter    │ (skip) │
│ No forbidden     │ (skip) │
├──────────────────┼────────┤
│ FINAL            │✗NAUGHTY│
└──────────────────┴────────┘

Fails on first check!
```

---

## 🎯 Part 2: New Rules

### Two Requirements (BOTH must be met)
```
┌────────────────────────────────────────────────┐
│ 1. ✓ Pair appears twice (non-overlapping)     │
│ 2. ✓ Letter repeats with one between (xyx)    │
└────────────────────────────────────────────────┘

Both → NICE ✓
Missing either → NAUGHTY ✗
```

---

## 📊 Part 2: Step-by-Step Examples

### Example 1: `qjhvhtzxzqqjkmpb` ✓ NICE

**Step 1: Check for pair appearing twice**
```
String: qjhvhtzxzqqjkmpb

Looking for pairs:
Position 0-1: "qj" → Search rest: qjhvhtzxzqqjkmpb
                                            ↑↑
                     Found at position 10-11!

Pair "qj" appears twice ✓

Visual:
q j h v h t z x z q q j k m p b
↑↑                     ↑↑
First "qj"          Second "qj"
```

**Step 2: Check for letter with one between**
```
String: qjhvhtzxzqqjkmpb

Checking positions:
Position 0 vs 2: q ≠ h
Position 1 vs 3: j ≠ v
Position 2 vs 4: h ≠ h... wait!
Position 5 vs 7: z = x? No
Position 6 vs 8: x = z? No
Position 7 vs 9: z = q? No
Position 8 vs 10: z ≠ q

Wait, let's check more carefully:
q j h v h t z x z q q j k m p b
    ↑   ↑
    h   h
Position 2 = h, Position 4 = h ✓

Pattern: h v h ✓
```

**Result:**
```
┌──────────────────────────┬────────┐
│ Rule                     │ Status │
├──────────────────────────┼────────┤
│ Pair appears twice       │ ✓ PASS │
│ Letter repeats (xyx)     │ ✓ PASS │
├──────────────────────────┼────────┤
│ FINAL                    │ ✓ NICE │
└──────────────────────────┴────────┘
```

---

### Example 2: `xxyxx` ✓ NICE

**Step 1: Check for pair appearing twice**
```
String: xxyxx

Try pair "xx":
Position 0-1: "xx" → Look from position 2: yxx
                                            ↑↑
                     Found at position 3-4!

Visual:
x x y x x
↑↑     ↑↑
First Second
"xx"  "xx"

Pair "xx" appears twice ✓
```

**Step 2: Check for letter with one between**
```
String: xxyxx

Check all positions:
Position 0 vs 2: x = y? No
Position 1 vs 3: x = x? Yes! ✓

Pattern found: x y x ✓

Visual:
x x y x x
  ↑ ↑ ↑
  xyx pattern
```

**Result:**
```
Special case: Overlapping rules!
- "xx" pair appears twice ✓
- "xyx" pattern exists ✓

Both requirements met → NICE ✓
```

---

### Example 3: `uurcxstgmygtbstg` ✗ NAUGHTY

**Step 1: Check for pair appearing twice**
```
String: uurcxstgmygtbstg

Try "uu": uurcxstgmygtbstg → Not found again
Try "ur": uurcxstgmygtbstg → Not found again
Try "rc": uurcxstgmygtbstg → Not found again
...
Try "tg": uurcxstgmygtbstg
              ↑↑       ↑↑
         Position 6    Position 13

Pair "tg" appears twice ✓

Visual:
u u r c x s t g m y g t b s t g
            ↑↑             ↑↑
         First tg       Second tg
```

**Step 2: Check for letter with one between**
```
String: uurcxstgmygtbstg

Checking all positions:
Position 0 vs 2: u = r? No
Position 1 vs 3: u = c? No
Position 2 vs 4: r = x? No
Position 3 vs 5: c = s? No
Position 4 vs 6: x = t? No
Position 5 vs 7: s = g? No
Position 6 vs 8: t = m? No
Position 7 vs 9: g = y? No
Position 8 vs 10: m = g? No
Position 9 vs 11: y = t? No
Position 10 vs 12: g = b? No
Position 11 vs 13: t = s? No
Position 12 vs 14: b = t? No
Position 13 vs 15: s = g? No

No xyx pattern found ✗
```

**Result:**
```
┌──────────────────────────┬────────┐
│ Rule                     │ Status │
├──────────────────────────┼────────┤
│ Pair appears twice       │ ✓ PASS │
│ Letter repeats (xyx)     │ ✗ FAIL │
├──────────────────────────┼────────┤
│ FINAL                    │✗NAUGHTY│
└──────────────────────────┴────────┘
```

---

### Example 4: `ieodomkazucvgmuy` ✗ NAUGHTY

**Step 1: Check for pair appearing twice**
```
String: ieodomkazucvgmuy

Try all possible pairs:
"ie": ieodomkazucvgmuy → Not found again
"eo": ieodomkazucvgmuy → Not found again
"od": ieodomkazucvgmuy → Not found again
"do": ieodomkazucvgmuy → Not found again
... (trying all)

No pair appears twice ✗
```

**Step 2: Check for letter with one between**
```
String: ieodomkazucvgmuy

Position 2 vs 4: o = o? Yes! ✓

Visual:
i e o d o m k a z u c v g m u y
    ↑   ↑
    o d o

Pattern: odo ✓
```

**Result:**
```
┌──────────────────────────┬────────┐
│ Rule                     │ Status │
├──────────────────────────┼────────┤
│ Pair appears twice       │ ✗ FAIL │
│ Letter repeats (xyx)     │ ✓ PASS │
├──────────────────────────┼────────┤
│ FINAL                    │✗NAUGHTY│
└──────────────────────────┴────────┘
```

---

## 🔍 Edge Cases Explained

### Part 1: Overlapping Requirements

**Case: `aaa`**
```
a a a
↑ ↑ ↑
All three count as vowels ✓

a a a
↑↑ ↑↑
Both pairs are doubles ✓

Overlapping is OK!
```

**Case: `aaab`**
```
Vowels: a, a, a (count: 3) ✓
Double: aa ✓
Forbidden: No ✓

NICE ✓
```

---

### Part 2: Non-Overlapping Pairs

**WRONG: `aaa`**
```
String: aaa

Looking for pair "aa":
Position 0-1: "aa" ─┐
                    │ Overlaps!
Position 1-2: "aa" ─┘

These overlap at position 1, so they DON'T count as two!

Result: ✗ Pair doesn't appear twice separately
```

**Visualization:**
```
a a a
↑↑─↑↑  ← Overlap at middle 'a'

Need:
a a . . a a  ← Separate occurrences
↑↑       ↑↑
```

**CORRECT: `aaaa`**
```
String: aaaa

Position 0-1: "aa" ─┐
                    │ No overlap
Position 2-3: "aa" ─┘

a a a a
↑↑   ↑↑
Separate pairs ✓
```

---

### Part 2: xyx Pattern

**Pattern Explained:**
```
x y x
↑   ↑
Same letter with exactly one between

Examples that work:
aba → a_a ✓
xyx → x_x ✓
pop → p_p ✓
aaa → a_a ✓ (same letter is fine)

Examples that DON'T work:
abc → a≠c ✗
abcd → a≠d ✗
aa → (no letter between) ✗
```

**Code check:**
```csharp
// For each position i
if (string[i] == string[i + 2])
{
    // Found xyx pattern!
    // string[i+1] is the "one between"
}

Example: "pop"
Position 0: p == p? (comparing p and p at position 2)
            p o p
            ↑   ↑
            Yes! ✓
```

---

## 💻 Algorithm Visualizations

### Part 1: Checking Process
```csharp
string input = "ugknbfddgicrmopn";

// Check 1: Count vowels
int vowelCount = 0;
foreach (char c in input)
{
    if ("aeiou".Contains(c))
        vowelCount++;
}
// vowelCount = 3 ✓

// Check 2: Find double letter
bool hasDouble = false;
for (int i = 0; i < input.Length - 1; i++)
{
    if (input[i] == input[i + 1])
    {
        hasDouble = true;
        break;
    }
}
// hasDouble = true (found "dd") ✓

// Check 3: Check forbidden
string[] forbidden = {"ab", "cd", "pq", "xy"};
bool hasForbidden = forbidden.Any(s => input.Contains(s));
// hasForbidden = false ✓

// Result
bool isNice = (vowelCount >= 3) && hasDouble && !hasForbidden;
// true && true && true = true ✓
```

---

### Part 2: Checking Process
```csharp
string input = "xxyxx";

// Check 1: Pair appears twice
bool hasPair = false;
for (int i = 0; i < input.Length - 1; i++)
{
    string pair = input.Substring(i, 2);
    // i=0: pair="xx"
    
    // Look for same pair starting from i+2
    if (input.IndexOf(pair, i + 2) >= 0)
    {
        hasPair = true;
        break;
    }
    // Found "xx" again at position 3 ✓
}
// hasPair = true ✓

// Check 2: Letter repeats with one between
bool hasRepeat = false;
for (int i = 0; i < input.Length - 2; i++)
{
    if (input[i] == input[i + 2])
    {
        hasRepeat = true;
        break;
    }
    // i=1: 'x' == 'x' at position 3 ✓
}
// hasRepeat = true ✓

// Result
bool isNice = hasPair && hasRepeat;
// true && true = true ✓
```

---

## 🐛 Common Mistakes

### Mistake 1: Counting Vowel Letters Instead of Occurrences
```csharp
// WRONG - Only counts unique vowels
string vowels = "aeiou";
int count = input.Count(c => vowels.Contains(c));
// "aaa" → counts as 1 unique vowel ✗

// CORRECT - Counts all vowel occurrences
int count = input.Count(c => "aeiou".Contains(c));
// "aaa" → counts as 3 vowels ✓
```

### Mistake 2: Overlapping Pairs
```csharp
// WRONG - Allows overlapping
for (int i = 0; i < input.Length - 1; i++)
{
    string pair = input.Substring(i, 2);
    if (input.IndexOf(pair, i + 1) >= 0) // Should be i+2!
        return true;
}

// "aaa" → finds "aa" at i=0, then again at i=1
// But these overlap! ✗

// CORRECT - No overlapping
if (input.IndexOf(pair, i + 2) >= 0) // Start from i+2
```

### Mistake 3: Wrong Index for xyx Check
```csharp
// WRONG - Checks adjacent characters
if (input[i] == input[i + 1])
    return true;
// This checks for doubles, not xyx ✗

// CORRECT - Checks with one between
if (input[i] == input[i + 2])
    return true;
// Skips the middle character ✓
```

---

## 📊 Quick Reference Tables

### Part 1 Rules
```
┌────────────────┬──────────────┬─────────────────┐
│ Rule           │ Check        │ Example         │
├────────────────┼──────────────┼─────────────────┤
│ 3+ vowels      │ Count aeiou  │ "aei" = 3 ✓     │
│ Double letter  │ xx pattern   │ "aa", "bb" ✓    │
│ No forbidden   │ !contains    │ No ab/cd/pq/xy  │
└────────────────┴──────────────┴─────────────────┘
```

### Part 2 Rules
```
┌────────────────┬──────────────┬─────────────────┐
│ Rule           │ Check        │ Example         │
├────────────────┼──────────────┼─────────────────┤
│ Pair twice     │ Non-overlap  │ "xyxy" ✓        │
│ Letter repeats │ xyx pattern  │ "aba", "pop" ✓  │
└────────────────┴──────────────┴─────────────────┘
```

---

## 🎯 Decision Flowcharts

### Part 1 Flowchart
```
Start
  ↓
Count vowels
  ├─ < 3? → NAUGHTY ✗
  └─ ≥ 3? 
      ↓
  Find double letter
      ├─ No? → NAUGHTY ✗
      └─ Yes?
          ↓
  Check forbidden
          ├─ Found? → NAUGHTY ✗
          └─ None? → NICE ✓
```

### Part 2 Flowchart
```
Start
  ↓
Find pair twice (non-overlapping)
  ├─ No? → NAUGHTY ✗
  └─ Yes?
      ↓
  Find xyx pattern
      ├─ No? → NAUGHTY ✗
      └─ Yes? → NICE ✓
```

---

## 📝 Summary

**Part 1 - Three Rules:**
1. 🎯 **3+ vowels** (a, e, i, o, u count)
2. 🎯 **Double letter** (any letter twice in a row)
3. 🎯 **No forbidden** (no ab, cd, pq, xy)
4. All three required for NICE

**Part 2 - Two Rules:**
1. 🎯 **Pair appears twice** (non-overlapping)
2. 🎯 **xyx pattern** (letter, one between, same letter)
3. Both required for NICE

**Key Differences:**
```
Part 1: Focus on simple patterns
Part 2: Focus on non-overlapping repeated patterns
```

**Memory Aids:**
```
Part 1: 3-2-0
- 3 vowels minimum
- 2 same letters (double)
- 0 forbidden substrings

Part 2: 2-1-2
- 2 letters (pair)
- 1 letter between (xyx)
- 2 times (appears twice)
```

---

**Happy string checking! 🎄📝**
