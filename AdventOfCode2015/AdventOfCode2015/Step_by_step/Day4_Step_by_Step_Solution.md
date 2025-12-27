# Day 4: The Ideal Stocking Stuffer - Step by Step Solution

## Step 1: Understanding the Problem

Mine AdventCoins by finding MD5 hashes with leading zeros.

**Part 1:** Find the lowest positive number that produces an MD5 hash starting with five zeros  
**Part 2:** Find the lowest positive number that produces an MD5 hash starting with six zeros

**Input:** A secret key (e.g., "abcdef")  
**Hash Format:** MD5(secret_key + number) → hexadecimal string

---

## Step 2: Understanding MD5 Hashing

### MD5 Basics:
- Cryptographic hash function
- Input: any string
- Output: 128-bit hash (32 hexadecimal characters)
- Same input always produces same output

### Example:
```
MD5("abcdef609043") = "000001dbbfa3a5c83a2d506429c7b00e"
                       ^^^^^
                       5 leading zeros!
```

---

## Step 3: Algorithm

```
1. Start with number = 1
2. Create input: secret_key + number
3. Calculate MD5 hash
4. Check if hash starts with required zeros
5. If yes → return number
   If no → increment number and repeat
```

---

## Step 4: Implementation

```csharp
using System.Security.Cryptography;
using System.Text;

public static class Day4
{
    public static string SolvePart1(string input)
    {
        string secretKey = input.Trim();
        int result = FindHashWithLeadingZeros(secretKey, 5);
        return result.ToString();
    }

    public static string SolvePart2(string input)
    {
        string secretKey = input.Trim();
        int result = FindHashWithLeadingZeros(secretKey, 6);
        return result.ToString();
    }

    private static int FindHashWithLeadingZeros(string secretKey, int zeroCount)
    {
        using (MD5 md5 = MD5.Create())
        {
            int number = 1;
            string prefix = new string('0', zeroCount);
            
            while (true)
            {
                string input = secretKey + number;
                string hash = GetMD5Hash(md5, input);
                
                if (hash.StartsWith(prefix))
                {
                    return number;
                }
                
                number++;
            }
        }
    }

    private static string GetMD5Hash(MD5 md5, string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        
        // Convert to hexadecimal string
        StringBuilder sb = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            sb.Append(b.ToString("x2")); // lowercase hex
        }
        
        return sb.ToString();
    }
}
```

---

## Step 5: Trace Example

### Example: Secret Key = "abcdef"

```
Attempt 1: "abcdef1"
  Hash: "8aa7f04a..." → No leading zeros ✗

Attempt 2: "abcdef2"
  Hash: "ab56b4d9..." → No leading zeros ✗

... (many attempts) ...

Attempt 609043: "abcdef609043"
  Hash: "000001dbbfa3a5c83a2d506429c7b00e"
         ^^^^^
  Starts with 5 zeros! ✓

Answer: 609043
```

---

## Step 6: Optimization - Early Exit Check

```csharp
private static int FindHashWithLeadingZeros(string secretKey, int zeroCount)
{
    using (MD5 md5 = MD5.Create())
    {
        int number = 1;
        
        while (true)
        {
            string input = secretKey + number;
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            
            // Check leading zeros in byte form for efficiency
            if (HasLeadingZeros(hashBytes, zeroCount))
            {
                return number;
            }
            
            number++;
        }
    }
}

private static bool HasLeadingZeros(byte[] hashBytes, int zeroCount)
{
    int bytesToCheck = zeroCount / 2;
    
    // Check full zero bytes
    for (int i = 0; i < bytesToCheck; i++)
    {
        if (hashBytes[i] != 0)
            return false;
    }
    
    // Check partial byte if odd number of zeros
    if (zeroCount % 2 == 1)
    {
        // Upper nibble should be 0
        if ((hashBytes[bytesToCheck] & 0xF0) != 0)
            return false;
    }
    
    return true;
}
```

---

## Step 7: Complexity Analysis

### Time Complexity:
- O(n × m) where:
  - n = number to find (unknown, depends on input)
  - m = time to compute MD5 hash (constant)
- Expected: millions of iterations

### Space Complexity:
- O(1) - only store current hash

### Performance Notes:
- This is a brute force approach
- No way to predict the answer
- Must try every number sequentially
- Part 2 takes ~16x longer than Part 1 (probability-based)

---

## Step 8: Why Can't We Optimize Further?

### Properties of Cryptographic Hashes:
1. **One-way function** - Can't reverse from hash to input
2. **Unpredictable** - Can't predict output from input
3. **Avalanche effect** - Small change in input drastically changes output

```
MD5("abcdef609043") = "000001dbb..."
MD5("abcdef609044") = "7e6fdf..."  ← Completely different!
```

---

## Step 9: Testing

### Test Part 1 Examples:
```csharp
// Example 1: "abcdef"
FindHashWithLeadingZeros("abcdef", 5) → 609043 ✓

// Example 2: "pqrstuv"
FindHashWithLeadingZeros("pqrstuv", 5) → 1048970 ✓
```

---

## Step 10: Common Mistakes

### Mistake 1: Starting from 0
```csharp
// WRONG - Should start from 1
int number = 0;

// CORRECT - Start from 1
int number = 1;
```

### Mistake 2: Case-Sensitive Hash Comparison
```csharp
// Be consistent with lowercase
sb.Append(b.ToString("x2")); // lowercase
// or
sb.Append(b.ToString("X2")); // uppercase
```

### Mistake 3: Not Reusing MD5 Instance
```csharp
// INEFFICIENT - Creates new MD5 each time
MD5.Create().ComputeHash(bytes);

// BETTER - Reuse instance
using (MD5 md5 = MD5.Create())
{
    // Use md5 multiple times
}
```

---

## Step 11: Summary

**Key Concepts:**
- ✅ Cryptographic hashing (MD5)
- ✅ Brute force search
- ✅ String to byte conversion
- ✅ Hexadecimal representation
- ✅ Pattern matching in hash output

**Algorithm:**
```
Try numbers 1, 2, 3, ... until hash starts with required zeros
```

**Happy mining! 🎄⛏️**
