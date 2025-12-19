# 🎁 Day 4 Visual Guide - MD5 Hash Mining

## 🔐 Understanding MD5 Hashing

### What is MD5?
```
MD5 (Message-Digest Algorithm 5)
├─ Input: Any string (any length)
├─ Output: 32-character hexadecimal string
└─ Property: Deterministic (same input → same output)

Example:
Input:  "hello"
Output: "5d41402abc4b2a76b9719d911017c592"
         └─ Always 32 hex digits ─┘
```

### Hexadecimal Overview
```
Hexadecimal digits: 0-9, a-f (16 possible values)
┌───┬───┬───┬───┬───┬───┬───┬───┐
│ 0 │ 1 │ 2 │ 3 │ 4 │ 5 │ 6 │ 7 │
├───┼───┼───┼───┼───┼───┼───┼───┤
│ 8 │ 9 │ a │ b │ c │ d │ e │ f │
└───┴───┴───┴───┴───┴───┴───┴───┘

Each hex digit represents 4 bits (half a byte)
```

---

## 🎯 The Challenge

### Part 1: Find 5 Leading Zeros
```
Goal: Find lowest number N where MD5(secretKey + N) starts with "00000"

Example with secret key "abcdef":
┌────────┬──────────────────────────────────┬─────────┐
│ Number │ MD5 Hash                         │ Match?  │
├────────┼──────────────────────────────────┼─────────┤
│ 1      │ "e80b5017098950fc58aad83c8c14978e" │ No ✗    │
│ 2      │ "6057f13c496ecf7fd777ceb9e79ae285" │ No ✗    │
│ ...    │ ...                              │ ...     │
│ 609042 │ "00000c51a8c8f01dc1b44b8c8c7f0000" │ No ✗    │
│ 609043 │ "000001dbbfa3a5c83a2d506429c7b00e" │ YES ✓   │
└────────┴──────────────────────────────────┴─────────┘
                ^^^^^
           Five zeros!

Answer: 609043
```

### Part 2: Find 6 Leading Zeros
```
Goal: Find lowest number N where MD5(secretKey + N) starts with "000000"

Same process, but looking for "000000" instead of "00000"
This is MUCH harder (16× more rare)!
```

---

## 📊 Step-by-Step Mining Process

### Example: Secret Key "abcdef"

**Attempt 1:**
```
Input:  "abcdef1"
MD5:    "e80b5017098950fc58aad83c8c14978e"
Check:  e ≠ 0
Result: ✗ FAIL
```

**Attempt 2:**
```
Input:  "abcdef2"
MD5:    "6057f13c496ecf7fd777ceb9e79ae285"
Check:  6 ≠ 0
Result: ✗ FAIL
```

**Attempt 3:**
```
Input:  "abcdef3"
MD5:    "17045223e6e7e3cc4a6d7dfb1c30c103"
Check:  1 ≠ 0
Result: ✗ FAIL
```

**... 609,040 more attempts ...**

**Attempt 609,043:**
```
Input:  "abcdef609043"
MD5:    "000001dbbfa3a5c83a2d506429c7b00e"
         ^^^^^
Check:  Starts with 00000 ✓
Result: ✓ SUCCESS!

Answer: 609043
```

---

## 🔢 Hash Prefix Probability

### Part 1: Five Zeros (`00000`)
```
Probability calculation:
- Each hex position has 16 possibilities (0-f)
- Need 5 specific positions to be '0'
- Probability per attempt: (1/16)^5 = 1/1,048,576

Expected attempts: ~1,048,576 (over 1 million!)

Visual rarity:
████████████████████████████████████ 1,000,000 attempts
█                                    ~1 success
```

### Part 2: Six Zeros (`000000`)
```
Probability calculation:
- Need 6 specific positions to be '0'
- Probability per attempt: (1/16)^6 = 1/16,777,216

Expected attempts: ~16,777,216 (over 16 million!)

16× harder than Part 1!

Visual rarity:
████████████████████████████████████ 16,000,000 attempts
█                                    ~1 success
```

---

## 💻 Algorithm Visualization

### Brute Force Search
```csharp
string secretKey = "abcdef";
int number = 1;

while (true)
{
    // 1. Concatenate
    string input = secretKey + number;
    //    "abcdef" + "1" = "abcdef1"
    
    // 2. Compute MD5 hash
    string hash = ComputeMD5(input);
    //    "e80b5017098950fc58aad83c8c14978e"
    
    // 3. Check prefix
    if (hash.StartsWith("00000"))
    {
        // Found it!
        return number;
    }
    
    // 4. Try next number
    number++;
}
```

### Execution Flow
```
Loop iteration diagram:

number = 1 ──→ Compute MD5 ──→ Check ──→ No ──┐
                                              │
number = 2 ←──────────────────────────────────┘
           ──→ Compute MD5 ──→ Check ──→ No ──┐
                                              │
number = 3 ←──────────────────────────────────┘
           ──→ Compute MD5 ──→ Check ──→ No ──┐
                                              │
     ...   ←──────────────────────────────────┘

number = 609043 ──→ Compute MD5 ──→ Check ──→ YES!
                                              │
                                          Return 609043
```

---

## 🎨 Visual Hash Examples

### Different Inputs, Different Hashes
```
┌──────────────┬────────────────────────────────────┐
│ Input        │ MD5 Hash                           │
├──────────────┼────────────────────────────────────┤
│ "abcdef1"    │ "e80b5017098950fc58aad83c8c14978e" │
│ "abcdef2"    │ "6057f13c496ecf7fd777ceb9e79ae285" │
│ "abcdef3"    │ "17045223e6e7e3cc4a6d7dfb1c30c103" │
│ "abcdef100"  │ "26b98dd801a3c1f5e96c75f162b96c66" │
│ "abcdef1000" │ "fa0c2998b8f1b6e2e3e8a3a5f5f5f5f5" │
└──────────────┴────────────────────────────────────┘

Notice: Small change in input → completely different hash!
```

### Hash with Leading Zeros
```
Hash: "000001dbbfa3a5c83a2d506429c7b00e"
       ^^^^^
       │││││
       │││││└─ Position 5: '0'
       ││││└── Position 4: '0'
       │││└─── Position 3: '0'
       ││└──── Position 2: '0'
       │└───── Position 1: '0'
       └────── Position 0: '0'

Requirement met: 5 leading zeros ✓
```

---

## 🔍 Detailed Examples

### Example 1: Secret Key "pqrstuv"

**Part 1 Search:**
```
Attempt 1:
Input:  "pqrstuv1"
Hash:   "24d2b85c3b88f9eb4f8e3b6b7a5c1d2e"
Check:  2 ≠ 0 → Continue

Attempt 2:
Input:  "pqrstuv2"
Hash:   "8f6e3b1c4d9a2e7f5c8b3a9d1e6f2c4b"
Check:  8 ≠ 0 → Continue

... Many attempts later ...

Attempt 1048970:
Input:  "pqrstuv1048970"
Hash:   "000006136ef2ff3b291c85725f17325c"
         ^^^^^
Check:  Starts with 00000 ✓
Answer: 1048970
```

### Example 2: Secret Key "bgvyzdsv" (Actual puzzle input)

**Part 1:**
```
Finding hash starting with "00000"...

Iterating through numbers:
1, 2, 3, ..., 254574, 254575 ✓

Answer: 254575
Hash: "00000d73e5f8cfff6b5fe8dc5f0b8d39"
       ^^^^^
```

**Part 2:**
```
Finding hash starting with "000000"...

Iterating through numbers:
1, 2, 3, ..., 1038736, 1038737 ✓

Answer: 1038737
Hash: "000000a8c64b36df56ad3c4dd68e5e63"
       ^^^^^^
```

---

## 🧮 MD5 Computation Details

### How MD5 Works (Simplified)
```
Input String
    ↓
Convert to bytes (UTF-8)
    ↓
Apply MD5 algorithm
    ↓
128-bit hash (16 bytes)
    ↓
Convert to hex string (32 characters)
    ↓
Output Hash
```

### Byte to Hex Conversion
```
Example: Byte value 255

Binary:     11111111
Hex:        FF (two hex digits per byte)

Hash byte array (16 bytes):
[0x00, 0x00, 0x00, 0x1d, 0xbb, ...]
  ↓     ↓     ↓     ↓     ↓
 "00" + "00" + "00" + "1d" + "bb" + ...
  ↓
Final hash string: "000001dbb..."
```

### Code Implementation
```csharp
using System.Security.Cryptography;
using System.Text;

string ComputeMD5(string input)
{
    // 1. Convert string to bytes
    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
    //    "abcdef1" → [97, 98, 99, 100, 101, 102, 49]
    
    // 2. Compute hash
    byte[] hashBytes = MD5.HashData(inputBytes);
    //    → [232, 11, 80, 23, ...]
    
    // 3. Convert each byte to 2-digit hex
    return string.Concat(
        hashBytes.Select(b => b.ToString("x2"))
    );
    //    232 → "e8", 11 → "0b", 80 → "50", ...
    //    Result: "e80b5017..."
}
```

---

## 📈 Performance Considerations

### Time Complexity
```
Best case:  O(1) - First number works (extremely rare!)
Average:    O(n) where n ≈ 1,048,576 for Part 1
                         n ≈ 16,777,216 for Part 2
Worst case: O(∞) - Theoretically no limit

Each iteration:
- String concatenation: O(k) where k = key length
- MD5 computation: O(k) 
- Prefix check: O(1)
```

### Optimization Techniques
```csharp
// ✓ GOOD: Efficient hex conversion
string hash = string.Concat(
    hashBytes.Select(b => b.ToString("x2"))
);

// ✗ BAD: String concatenation in loop
string hash = "";
foreach (byte b in hashBytes)
{
    hash += b.ToString("x2"); // Creates many temp strings
}

// ✓ BETTER: Early exit on prefix check
if (!hash.StartsWith("00000"))
    continue;
// No need to check rest of hash

// ✓ OPTIMIZATION: Reuse MD5 instance
using var md5 = MD5.Create();
while (true)
{
    var hash = md5.ComputeHash(...);
    // Reuses same MD5 object
}
```

---

## 🐛 Common Mistakes

### Mistake 1: Starting from 0
```csharp
// WRONG - Starts from 0
int number = 0;
while (true)
{
    string input = secretKey + number;
    // "abcdef0" - Problem says "no leading zeroes"
    ...
}

// CORRECT - Start from 1
int number = 1;
while (true)
{
    string input = secretKey + number;
    // "abcdef1" ✓
    ...
}
```

### Mistake 2: Case Sensitivity
```csharp
// WRONG - Checking uppercase
if (hash.StartsWith("0000A"))
    return number;

// MD5 hashes are lowercase!
// Actual: "00000..."

// CORRECT - Check lowercase
if (hash.StartsWith("00000"))
    return number;
```

### Mistake 3: Checking Wrong Length
```csharp
// WRONG - Checking anywhere in string
if (hash.Contains("00000"))
    return number;
// "abc00000def" would match!

// CORRECT - Check prefix only
if (hash.StartsWith("00000"))
    return number;
```

### Mistake 4: Off-by-One in Prefix Length
```csharp
// Part 1 needs 5 zeros
if (hash.StartsWith("0000")) // WRONG! Only 4 zeros
if (hash.StartsWith("00000")) // CORRECT! 5 zeros

// Part 2 needs 6 zeros
if (hash.StartsWith("00000")) // WRONG! Only 5 zeros
if (hash.StartsWith("000000")) // CORRECT! 6 zeros
```

---

## 🎯 Pattern Recognition

### Observing Hash Patterns
```
Number    Hash starts with
1         e8...  (no zeros)
2         60...  (no zeros)
10        26...  (no zeros)
100       f2...  (no zeros)
1000      8c...  (no zeros)
10000     08...  (one zero)
100000    02...  (one zero)

Pattern: Leading zeros are RARE
No predictable pattern - must brute force
```

### Probability Distribution
```
Leading zeros count:
┌───────┬─────────────┬─────────────┐
│ Count │ Probability │ Avg Attempt │
├───────┼─────────────┼─────────────┤
│   1   │ 1/16        │ ~16         │
│   2   │ 1/256       │ ~256        │
│   3   │ 1/4,096     │ ~4,096      │
│   4   │ 1/65,536    │ ~65,536     │
│   5   │ 1/1,048,576 │ ~1,048,576  │
│   6   │ 1/16,777,216│ ~16,777,216 │
└───────┴─────────────┴─────────────┘

Each additional zero makes it 16× rarer!
```

---

## 🔬 Testing Strategy

### Small Test Case
```csharp
// Test with known answer
string secretKey = "abcdef";
int result = SolvePart1(secretKey);
Console.WriteLine(result); // Should print 609043

// Verify the hash
string input = "abcdef609043";
string hash = ComputeMD5(input);
Console.WriteLine(hash); 
// Should print: "000001dbbfa3a5c83a2d506429c7b00e"
Console.WriteLine(hash.StartsWith("00000")); // True
```

### Incremental Testing
```csharp
// Test MD5 computation
Console.WriteLine(ComputeMD5("hello"));
// Expected: "5d41402abc4b2a76b9719d911017c592"

// Test small range
for (int i = 1; i <= 10; i++)
{
    string hash = ComputeMD5("test" + i);
    Console.WriteLine($"{i}: {hash}");
}
```

---

## 📝 Summary

**Part 1 Key Points:**
1. 🔑 Concatenate secret key + number
2. 🔐 Compute MD5 hash
3. ✓ Check if starts with "00000"
4. 🔢 Find LOWEST number that works

**Part 2 Key Points:**
1. Same as Part 1
2. But check for "000000" (6 zeros)
3. Takes ~16× longer than Part 1
4. Still uses brute force approach

**Technical Details:**
- 📊 MD5 always produces 32 hex characters
- 🎲 Hash output appears random
- 🔄 Must try numbers sequentially
- ⏱️ Computationally expensive

**Memory Aids:**
```
Part 1: "00000" = 5 zeros
Part 2: "000000" = 6 zeros

Formula: secretKey + number
Example: "abcdef" + "609043" = "abcdef609043"

Check: hash.StartsWith("00000")
```

---

**Happy mining! ⛏️💎**
