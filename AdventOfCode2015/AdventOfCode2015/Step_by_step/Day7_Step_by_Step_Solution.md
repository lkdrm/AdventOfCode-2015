# Day 7: Some Assembly Required - Step by Step Solution

## Step 1: Understanding the Problem

Simulate a circuit with bitwise logic gates that produce 16-bit signals.

**Operations:**
- `123 -> x` - Direct assignment
- `x AND y -> z` - Bitwise AND
- `x OR y -> z` - Bitwise OR
- `NOT x -> y` - Bitwise NOT (complement)
- `x LSHIFT 2 -> y` - Left shift
- `x RSHIFT 3 -> z` - Right shift

**Goal:** Find the signal ultimately provided to wire `a`

---

## Step 2: Understanding 16-Bit Signals

```
16-bit range: 0 to 65,535 (2^16 - 1)
Binary: 0000000000000000 to 1111111111111111
```

### Bitwise Operations:
```
AND: 123 AND 456
  123: 0000000001111011
  456: 0000000111001000
  AND: 0000000001001000 = 72

OR:  123 OR 456
  123: 0000000001111011
  456: 0000000111001000
  OR:  0000000111111011 = 507

NOT: NOT 123
  123: 0000000001111011
  NOT: 1111111110000100 = 65412

LSHIFT: 123 LSHIFT 2
  123:     0000000001111011
  << 2:    0000000111101100 = 492

RSHIFT: 456 RSHIFT 2
  456:     0000000111001000
  >> 2:    0000000001110010 = 114
```

---

## Step 3: Algorithm - Recursive Evaluation with Memoization

```
1. Parse all instructions into a dictionary
2. To get value of wire:
   a. If already computed → return cached value
   b. If instruction needs other wires → recursively get their values
   c. Compute result
   d. Cache result
   e. Return result
```

---

## Step 4: Data Structure

```csharp
class Circuit
{
    private Dictionary<string, string> instructions = new();
    private Dictionary<string, ushort> cache = new();
    
    public void AddInstruction(string line)
    {
        string[] parts = line.Split(" -> ");
        instructions[parts[1]] = parts[0];
    }
    
    public ushort GetValue(string wire)
    {
        // If already computed, return cached value
        if (cache.ContainsKey(wire))
            return cache[wire];
        
        // If it's a number, return it directly
        if (ushort.TryParse(wire, out ushort value))
            return value;
        
        // Get instruction for this wire
        string instruction = instructions[wire];
        ushort result;
        
        // Parse and evaluate instruction
        if (instruction.Contains("AND"))
        {
            string[] parts = instruction.Split(" AND ");
            result = (ushort)(GetValue(parts[0]) & GetValue(parts[1]));
        }
        else if (instruction.Contains("OR"))
        {
            string[] parts = instruction.Split(" OR ");
            result = (ushort)(GetValue(parts[0]) | GetValue(parts[1]));
        }
        else if (instruction.Contains("LSHIFT"))
        {
            string[] parts = instruction.Split(" LSHIFT ");
            result = (ushort)(GetValue(parts[0]) << int.Parse(parts[1]));
        }
        else if (instruction.Contains("RSHIFT"))
        {
            string[] parts = instruction.Split(" RSHIFT ");
            result = (ushort)(GetValue(parts[0]) >> int.Parse(parts[1]));
        }
        else if (instruction.StartsWith("NOT"))
        {
            string operand = instruction.Substring(4);
            result = (ushort)(~GetValue(operand));
        }
        else
        {
            // Direct assignment
            result = GetValue(instruction);
        }
        
        // Cache and return
        cache[wire] = result;
        return result;
    }
}
```

---

## Step 5: Example Trace

```
Instructions:
123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i

Evaluate wire 'd':
1. Need: x AND y
2. Get x: "123" → direct → 123
3. Get y: "456" → direct → 456
4. Calculate: 123 & 456 = 72
5. Cache: d = 72
6. Return 72

Evaluate wire 'h':
1. Need: NOT x
2. Get x: cached → 123
3. Calculate: ~123 = 65412 (16-bit)
4. Cache: h = 65412
5. Return 65412
```

---

## Step 6: Complete Implementation

```csharp
public static class Day7
{
    public static string SolvePart1(string input)
    {
        Circuit circuit = new Circuit();
        
        foreach (string line in input.Split('\n'))
        {
            if (!string.IsNullOrWhiteSpace(line))
                circuit.AddInstruction(line.Trim());
        }
        
        ushort result = circuit.GetValue("a");
        return result.ToString();
    }

    public static string SolvePart2(string input)
    {
        Circuit circuit = new Circuit();
        
        foreach (string line in input.Split('\n'))
        {
            if (!string.IsNullOrWhiteSpace(line))
                circuit.AddInstruction(line.Trim());
        }
        
        // Get initial value of 'a'
        ushort aValue = circuit.GetValue("a");
        
        // Reset and override 'b' with value from 'a'
        circuit = new Circuit();
        foreach (string line in input.Split('\n'))
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                string trimmed = line.Trim();
                // Override instruction for wire 'b'
                if (trimmed.EndsWith("-> b"))
                    circuit.AddInstruction($"{aValue} -> b");
                else
                    circuit.AddInstruction(trimmed);
            }
        }
        
        ushort result = circuit.GetValue("a");
        return result.ToString();
    }

    private class Circuit
    {
        private Dictionary<string, string> instructions = new();
        private Dictionary<string, ushort> cache = new();
        
        public void AddInstruction(string line)
        {
            string[] parts = line.Split(" -> ");
            instructions[parts[1]] = parts[0];
        }
        
        public ushort GetValue(string wire)
        {
            if (cache.ContainsKey(wire))
                return cache[wire];
            
            if (ushort.TryParse(wire, out ushort value))
                return value;
            
            string instruction = instructions[wire];
            ushort result;
            
            if (instruction.Contains("AND"))
            {
                string[] parts = instruction.Split(" AND ");
                result = (ushort)(GetValue(parts[0]) & GetValue(parts[1]));
            }
            else if (instruction.Contains("OR"))
            {
                string[] parts = instruction.Split(" OR ");
                result = (ushort)(GetValue(parts[0]) | GetValue(parts[1]));
            }
            else if (instruction.Contains("LSHIFT"))
            {
                string[] parts = instruction.Split(" LSHIFT ");
                result = (ushort)(GetValue(parts[0]) << int.Parse(parts[1]));
            }
            else if (instruction.Contains("RSHIFT"))
            {
                string[] parts = instruction.Split(" RSHIFT ");
                result = (ushort)(GetValue(parts[0]) >> int.Parse(parts[1]));
            }
            else if (instruction.StartsWith("NOT"))
            {
                string operand = instruction.Substring(4);
                result = (ushort)(~GetValue(operand));
            }
            else
            {
                result = GetValue(instruction);
            }
            
            cache[wire] = result;
            return result;
        }
    }
}
```

---

## Step 7: Why Memoization?

Without caching:
```
To get 'z':
  Need 'y'
    Need 'x'
      Need 'w' ... compute w
    Need 'x' again ... recompute w! ✗
```

With caching:
```
To get 'z':
  Need 'y'
    Need 'x'
      Need 'w' ... compute w, cache it
    Need 'x' again ... use cached value ✓
```

---

## Step 8: Common Mistakes

### Mistake 1: Not Using ushort
```csharp
// WRONG - int allows > 65535
int result = ~value;

// CORRECT - ushort enforces 16-bit
ushort result = (ushort)(~value);
```

### Mistake 2: Not Handling Direct Numbers
```csharp
// WRONG - Tries to look up "123" as wire
return instructions["123"];

// CORRECT - Parse numbers directly
if (ushort.TryParse(wire, out ushort value))
    return value;
```

---

## Step 9: Complexity Analysis

### Time Complexity:
- Without memoization: O(2^n) - exponential
- With memoization: O(n) - each wire computed once

### Space Complexity:
- O(n) for instructions and cache

---

## Step 10: Summary

**Key Concepts:**
- ✅ Bitwise operations
- ✅ Recursive evaluation
- ✅ Memoization/caching
- ✅ Dependency resolution
- ✅ 16-bit unsigned integers

**Happy circuit simulating! 🎄🔌**
