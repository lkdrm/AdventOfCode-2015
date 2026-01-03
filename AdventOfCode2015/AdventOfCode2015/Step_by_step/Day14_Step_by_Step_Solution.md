# Day 14: Reindeer Olympics - Step by Step Solution

## Step 1: Understanding the Problem

Santa is organizing Reindeer Olympics! We need to determine which reindeer can travel the farthest distance in exactly **2503 seconds**.

**Part 1:** Find the winning reindeer based on total distance traveled.

**Part 2:** Award points each second to the reindeer currently in the lead. Find the winning reindeer based on points.

### Input Format:
```
Name can fly X km/s for Y seconds, but then must rest for Z seconds.
```

Example:
```
Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.
```

**Key Points:**
- Reindeer alternate between **flying** and **resting**
- Flying speed is constant (top speed always)
- Resting means **no movement** (distance stays the same)
- Each state lasts for complete seconds only
- Race duration: **2503 seconds** total

---

## Step 2: Analyzing the Example

### Example Input (2 reindeer):
```
Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.
```

### Comet's Cycle:
```
Speed: 14 km/s
Fly time: 10 seconds
Rest time: 127 seconds
Cycle duration: 10 + 127 = 137 seconds

One complete cycle:
- Seconds 1-10: Flying (14 km/s) → 140 km
- Seconds 11-137: Resting (0 km/s) → 0 km
Total distance per cycle: 140 km
```

### Dancer's Cycle:
```
Speed: 16 km/s
Fly time: 11 seconds
Rest time: 162 seconds
Cycle duration: 11 + 162 = 173 seconds

One complete cycle:
- Seconds 1-11: Flying (16 km/s) → 176 km
- Seconds 12-173: Resting (0 km/s) → 0 km
Total distance per cycle: 176 km
```

### Timeline Trace (First 200 seconds):
```
Second 1-10: Comet flying → 140 km, Dancer flying → 160 km
Second 11: Comet resting → 140 km, Dancer flying → 176 km
Second 12-137: Both resting
Second 138-147: Comet flying → 280 km, Dancer resting → 176 km
Second 148-173: Comet resting → 280 km, Dancer resting → 176 km
Second 174-183: Comet resting → 280 km, Dancer flying → 352 km
...
```

### After 1000 seconds:
```
Comet: 1120 km (winner at 1000s)
Dancer: 1056 km
```

**Why Comet wins at 1000s:**
- Shorter cycle (137s) allows more complete cycles
- Gets more flying opportunities despite lower speed

---

## Step 3: Mathematical Approach

Instead of simulating second-by-second, we can calculate directly:

### Formula Components:

**Total cycle time:**
```
cycle_time = fly_time + rest_time
```

**Complete cycles in T seconds:**
```
complete_cycles = T ÷ cycle_time (integer division)
```

**Remaining seconds:**
```
remaining = T % cycle_time (modulo)
```

**Distance from complete cycles:**
```
distance_complete = complete_cycles × fly_time × speed
```

**Distance from remaining seconds:**
```
flying_in_remaining = min(remaining, fly_time)
distance_remaining = flying_in_remaining × speed
```

**Total distance:**
```
total_distance = distance_complete + distance_remaining
```

---

## Step 4: Part 1 Algorithm - Direct Calculation

### Implementation Strategy:

```csharp
public static int CalculateDistance(int speed, int flyTime, int restTime, int totalTime)
{
    int cycleTime = flyTime + restTime;
    
    // Complete cycles
    int completeCycles = totalTime / cycleTime;
    int distanceFromCycles = completeCycles * flyTime * speed;
    
    // Remaining seconds
    int remaining = totalTime % cycleTime;
    int flyingInRemaining = Math.Min(remaining, flyTime);
    int distanceFromRemaining = flyingInRemaining * speed;
    
    return distanceFromCycles + distanceFromRemaining;
}
```

### Example Trace: Comet at 1000 seconds

**Given:**
```
speed = 14 km/s
flyTime = 10 seconds
restTime = 127 seconds
totalTime = 1000 seconds
```

**Calculation:**
```
cycleTime = 10 + 127 = 137 seconds

completeCycles = 1000 ÷ 137 = 7 (integer division)
distanceFromCycles = 7 × 10 × 14 = 980 km

remaining = 1000 % 137 = 41 seconds
flyingInRemaining = min(41, 10) = 10 seconds
distanceFromRemaining = 10 × 14 = 140 km

total = 980 + 140 = 1120 km
```

**Verification:**
```
7 complete cycles: 7 × 137 = 959 seconds used
Remaining: 1000 - 959 = 41 seconds

In those 41 seconds:
- First 10 seconds: Flying (Comet's fly time)
- Next 31 seconds: Resting

Distance: 10 × 14 = 140 km
Total: 980 + 140 = 1120 km ✓
```

---

## Step 5: Parsing Input

### Input Line Format:
```
Pattern: "Name can fly X km/s for Y seconds, but then must rest for Z seconds."

Example: "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds."
```

### Parsing Strategy:

**Using Regular Expressions:**
```csharp
var match = Regex.Matches(line, @"(\d+)");
// Captures all numbers in order: [14, 10, 127]

numbers[0] → speed (14)
numbers[1] → fly time (10)
numbers[2] → rest time (127)

var nameParts = line.Split(' ');
name = nameParts[0]; // "Comet"
```

### Complete Parser:

```csharp
public static (string name, int speed, int flyTime, int restTime) ParseReindeer(string line)
{
    // Extract numbers
    var numbers = Regex.Matches(line, @"(\d+)")
                       .Select(m => int.Parse(m.Value))
                       .ToList();
    
    int speed = numbers[0];
    int flyTime = numbers[1];
    int restTime = numbers[2];
    
    // Extract name
    string name = line.Split(' ')[0];
    
    return (name, speed, flyTime, restTime);
}
```

### Example Parsing:

**Input:** `"Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."`

```
Step 1: Extract numbers with regex
matches = [16, 11, 162]

Step 2: Assign to variables
speed = 16
flyTime = 11
restTime = 162

Step 3: Extract name
split = ["Dancer", "can", "fly", ...]
name = "Dancer"

Result: (name: "Dancer", speed: 16, flyTime: 11, restTime: 162)
```

---

## Step 6: Part 1 Complete Solution

### Algorithm:

```csharp
public static string SolvePart1(string[] input)
{
    int maxDistance = 0;
    const int totalTime = 2503;
    
    foreach (string line in input)
    {
        // Parse reindeer stats
        var (name, speed, flyTime, restTime) = ParseReindeer(line);
        
        // Calculate distance
        int distance = CalculateDistance(speed, flyTime, restTime, totalTime);
        
        // Track maximum
        if (distance > maxDistance)
            maxDistance = distance;
    }
    
    return maxDistance.ToString();
}

private static int CalculateDistance(int speed, int flyTime, int restTime, int totalTime)
{
    int cycleTime = flyTime + restTime;
    
    int completeCycles = totalTime / cycleTime;
    int distanceFromCycles = completeCycles * flyTime * speed;
    
    int remaining = totalTime % cycleTime;
    int flyingInRemaining = Math.Min(remaining, flyTime);
    int distanceFromRemaining = flyingInRemaining * speed;
    
    return distanceFromCycles + distanceFromRemaining;
}
```

### Example Execution (2 reindeer, 1000 seconds):

```
Input:
Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.

Processing Comet:
  (name: "Comet", speed: 14, flyTime: 10, restTime: 127)
  distance = CalculateDistance(14, 10, 127, 1000)
           = 1120 km
  maxDistance = 1120

Processing Dancer:
  (name: "Dancer", speed: 16, flyTime: 11, restTime: 162)
  distance = CalculateDistance(16, 11, 162, 1000)
           = 1056 km
  maxDistance = 1120 (unchanged)

Result: 1120
```

---

## Step 7: Understanding Part 2

### New Scoring System:

Instead of distance-based winner, Santa awards **points each second**:
- At the end of each second, check which reindeer is **in the lead**
- Award **1 point** to the leader(s)
- If multiple reindeer are tied for first, **all tied reindeer** get 1 point
- After 2503 seconds, the reindeer with the **most points** wins

### Why This Changes Everything:

**Part 1 Strategy:** Maximize total distance over 2503 seconds
**Part 2 Strategy:** Be in the lead as often as possible

**Key Difference:**
- A reindeer with consistent speed might accumulate more points
- A reindeer with burst speed might win on distance but fewer points
- Early lead matters (accumulate points throughout the race)

---

## Step 8: Part 2 Simulation Approach

### Why We Need Simulation:

Unlike Part 1, we **cannot use a direct formula** because:
- Points depend on relative positions at **each second**
- Leaders change dynamically throughout the race
- Tied positions award points to multiple reindeer

**We must simulate second-by-second!**

### Simulation Algorithm:

```
1. Parse all reindeer stats
2. Initialize: distance = 0, points = 0, time_in_cycle = 0 for each reindeer
3. For each second from 1 to 2503:
   a. Update each reindeer's state (flying or resting)
   b. Update each reindeer's distance
   c. Find the maximum distance (current leader)
   d. Award 1 point to all reindeer at maximum distance
4. Return the maximum points
```

---

## Step 9: Part 2 Data Structure

### Reindeer State Class:

```csharp
public class ReindeerState
{
    public string Name { get; set; }
    public int Speed { get; set; }
    public int FlyTime { get; set; }
    public int RestTime { get; set; }
    
    // Current state
    public int Distance { get; set; }
    public int Points { get; set; }
    public int TimeInCycle { get; set; } // 0 to (FlyTime + RestTime - 1)
    
    public bool IsFlying()
    {
        return TimeInCycle < FlyTime;
    }
    
    public void AdvanceSecond()
    {
        // Update distance if flying
        if (IsFlying())
        {
            Distance += Speed;
        }
        
        // Advance cycle time
        TimeInCycle++;
        int cycleLength = FlyTime + RestTime;
        if (TimeInCycle >= cycleLength)
        {
            TimeInCycle = 0; // Reset cycle
        }
    }
}
```

### Example State Trace (Comet):

```
Initial state:
Distance: 0, Points: 0, TimeInCycle: 0
IsFlying: true (0 < 10)

Second 1:
  AdvanceSecond():
    IsFlying() → true (0 < 10)
    Distance += 14 → 14 km
    TimeInCycle++ → 1
  Result: Distance: 14, TimeInCycle: 1

Second 2-10:
  (Similar process)
  Result after second 10: Distance: 140, TimeInCycle: 10

Second 11:
  IsFlying() → false (10 >= 10)
  Distance stays 140
  TimeInCycle++ → 11
  Result: Distance: 140, TimeInCycle: 11

Second 138:
  TimeInCycle reaches 137 (10 + 127)
  Reset: TimeInCycle → 0
  Cycle restarts!
```

---

## Step 10: Part 2 Complete Implementation

### Main Solution:

```csharp
public static string SolvePart2(string[] input)
{
    const int totalTime = 2503;
    
    // Parse all reindeer
    var reindeer = new List<ReindeerState>();
    foreach (string line in input)
    {
        var (name, speed, flyTime, restTime) = ParseReindeer(line);
        reindeer.Add(new ReindeerState
        {
            Name = name,
            Speed = speed,
            FlyTime = flyTime,
            RestTime = restTime,
            Distance = 0,
            Points = 0,
            TimeInCycle = 0
        });
    }
    
    // Simulate each second
    for (int second = 1; second <= totalTime; second++)
    {
        // Update all reindeer
        foreach (var deer in reindeer)
        {
            deer.AdvanceSecond();
        }
        
        // Find leader(s)
        int maxDistance = reindeer.Max(r => r.Distance);
        
        // Award points
        foreach (var deer in reindeer)
        {
            if (deer.Distance == maxDistance)
            {
                deer.Points++;
            }
        }
    }
    
    // Return maximum points
    int maxPoints = reindeer.Max(r => r.Points);
    return maxPoints.ToString();
}
```

---

## Step 11: Part 2 Example Trace

### Setup (2 reindeer at 1000 seconds):
```
Comet: speed=14, fly=10, rest=127
Dancer: speed=16, fly=11, rest=162
```

### Detailed Trace (First 150 seconds):

```
Second 1:
  Comet: flying, distance = 14 km
  Dancer: flying, distance = 16 km
  Leader: Dancer (16 km)
  Points: Comet: 0, Dancer: 1

Second 2-10:
  Comet: flying, distance = 140 km
  Dancer: flying, distance = 160 km
  Leader: Dancer
  Points: Comet: 0, Dancer: 10

Second 11:
  Comet: resting, distance = 140 km
  Dancer: flying, distance = 176 km
  Leader: Dancer
  Points: Comet: 0, Dancer: 11

Second 12-137:
  Both resting
  Leader: Dancer (176 km)
  Points: Comet: 0, Dancer: 137

Second 138:
  Comet: flying (new cycle!), distance = 154 km
  Dancer: resting, distance = 176 km
  Leader: Dancer
  Points: Comet: 0, Dancer: 138

Second 139:
  Comet: flying, distance = 168 km
  Dancer: resting, distance = 176 km
  Leader: Dancer
  Points: Comet: 0, Dancer: 139

Second 140:
  Comet: flying, distance = 182 km
  Dancer: resting, distance = 176 km
  Leader: Comet! (Takes the lead)
  Points: Comet: 1, Dancer: 139

Second 141-147:
  Comet: flying, distance = 280 km
  Dancer: resting, distance = 176 km
  Leader: Comet
  Points: Comet: 8, Dancer: 139

Second 148-173:
  Comet: resting, distance = 280 km
  Dancer: resting, distance = 176 km
  Leader: Comet
  Points: Comet: 34, Dancer: 139
```

### After 1000 seconds:
```
Comet: Distance = 1120 km, Points = 312
Dancer: Distance = 1056 km, Points = 689

Winner by distance: Comet (1120 km)
Winner by points: Dancer (689 points) ✓
```

**Key Insight:**
- Dancer gains early lead and holds it for many seconds
- Accumulates points faster in the beginning
- Even though Comet wins on distance, Dancer has more points!

---

## Step 12: Optimization Considerations

### Time Complexity:

**Part 1 (Direct Calculation):**
```
O(N) where N = number of reindeer
- Parse each reindeer: O(N)
- Calculate distance: O(1) per reindeer
- Find maximum: O(N)

Total: O(N)
Very efficient!
```

**Part 2 (Simulation):**
```
O(N × T) where N = number of reindeer, T = total time
- For each second (T iterations):
  - Update each reindeer: O(N)
  - Find max distance: O(N)
  - Award points: O(N)

Total: O(N × T)
For N=9, T=2503: 9 × 2503 = 22,527 operations
Still very fast!
```

### Space Complexity:

```
O(N) for storing reindeer states
- Each reindeer: name, stats, current state
- List of N reindeer

Very efficient!
```

---

## Step 13: Common Mistakes to Avoid

### Mistake 1: Off-by-One in Cycle
```csharp
// WRONG - Checking at wrong point in cycle
if (TimeInCycle <= FlyTime) // ✗
    Distance += Speed;

// CORRECT - Flying during [0, FlyTime)
if (TimeInCycle < FlyTime) // ✓
    Distance += Speed;

Example: FlyTime = 10
Should fly at TimeInCycle: 0,1,2,3,4,5,6,7,8,9
Should rest at TimeInCycle: 10,11,...
```

### Mistake 2: Not Resetting Cycle
```csharp
// WRONG - Cycle never resets
TimeInCycle++;

// CORRECT - Reset when reaching cycle end
TimeInCycle++;
if (TimeInCycle >= FlyTime + RestTime)
    TimeInCycle = 0;
```

### Mistake 3: Wrong Remaining Distance Calculation
```csharp
// WRONG - May count rest time as flying
int flyingInRemaining = remaining; // ✗

// CORRECT - Cap at actual fly time
int flyingInRemaining = Math.Min(remaining, flyTime); // ✓

Example: remaining = 41, flyTime = 10
Should only count 10 seconds of flying, not 41!
```

### Mistake 4: Part 2 Points Before Update
```csharp
// WRONG - Awarding points before updating distance
foreach (var deer in reindeer)
    if (deer.Distance == maxDistance) deer.Points++;
foreach (var deer in reindeer)
    deer.AdvanceSecond();

// CORRECT - Update distance first, then award points
foreach (var deer in reindeer)
    deer.AdvanceSecond();
int maxDistance = reindeer.Max(r => r.Distance);
foreach (var deer in reindeer)
    if (deer.Distance == maxDistance) deer.Points++;
```

### Mistake 5: Part 2 Using Part 1 Approach
```csharp
// WRONG - Part 2 needs simulation, not formula
return CalculateDistance(...); // ✗

// CORRECT - Simulate second by second
for (int second = 1; second <= totalTime; second++)
{
    // Update states and award points
}
```

---

## Step 14: Testing Strategy

### Test Case 1: Example from Problem
```
Input: Comet and Dancer
Total time: 1000 seconds

Part 1 Expected: 1120 (Comet)
Part 2 Expected: 689 (Dancer)

Verify: Direct calculation matches simulation for Part 1
```

### Test Case 2: Single Reindeer
```
Input: One reindeer only
Expected: Always wins (all points go to this reindeer)
Verify: Points equal to total time
```

### Test Case 3: Identical Reindeer
```
Input: Two reindeer with same stats
Expected: Tied every second
Verify: Both get same points (equal to total time)
```

### Test Case 4: Short Cycle vs Long Cycle
```
Reindeer A: fly 10s, rest 10s (cycle: 20s)
Reindeer B: fly 5s, rest 50s (cycle: 55s)

Test: Which accumulates more points?
Verify: Consistent leader wins despite cycles
```

### Test Case 5: Edge - Cycle Exactly Divides Time
```
Input: Reindeer with cycle that divides 2503 evenly
Expected: No partial cycle at end
Verify: Remaining = 0 in calculation
```

---

## Step 15: Complete Implementation

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2015.ResolvingDays;

public static class Day14
{
    private const int TotalTime = 2503;
    
    // Part 1: Find reindeer with maximum distance
    public static string SolvePart1(string[] input)
    {
        int maxDistance = 0;
        
        foreach (string line in input)
        {
            var (name, speed, flyTime, restTime) = ParseReindeer(line);
            int distance = CalculateDistance(speed, flyTime, restTime, TotalTime);
            
            if (distance > maxDistance)
                maxDistance = distance;
        }
        
        return maxDistance.ToString();
    }
    
    // Part 2: Find reindeer with maximum points
    public static string SolvePart2(string[] input)
    {
        var reindeer = new List<ReindeerState>();
        
        // Parse all reindeer
        foreach (string line in input)
        {
            var (name, speed, flyTime, restTime) = ParseReindeer(line);
            reindeer.Add(new ReindeerState
            {
                Name = name,
                Speed = speed,
                FlyTime = flyTime,
                RestTime = restTime,
                Distance = 0,
                Points = 0,
                TimeInCycle = 0
            });
        }
        
        // Simulate each second
        for (int second = 1; second <= TotalTime; second++)
        {
            // Update all reindeer positions
            foreach (var deer in reindeer)
            {
                deer.AdvanceSecond();
            }
            
            // Find current leader(s)
            int maxDistance = reindeer.Max(r => r.Distance);
            
            // Award points to leader(s)
            foreach (var deer in reindeer)
            {
                if (deer.Distance == maxDistance)
                {
                    deer.Points++;
                }
            }
        }
        
        // Return maximum points
        int maxPoints = reindeer.Max(r => r.Points);
        return maxPoints.ToString();
    }
    
    // Calculate distance using mathematical formula
    private static int CalculateDistance(int speed, int flyTime, int restTime, int totalTime)
    {
        int cycleTime = flyTime + restTime;
        
        // Complete cycles
        int completeCycles = totalTime / cycleTime;
        int distanceFromCycles = completeCycles * flyTime * speed;
        
        // Remaining time (partial cycle)
        int remaining = totalTime % cycleTime;
        int flyingInRemaining = Math.Min(remaining, flyTime);
        int distanceFromRemaining = flyingInRemaining * speed;
        
        return distanceFromCycles + distanceFromRemaining;
    }
    
    // Parse input line to extract reindeer stats
    private static (string name, int speed, int flyTime, int restTime) ParseReindeer(string line)
    {
        var numbers = Regex.Matches(line, @"(\d+)")
                           .Select(m => int.Parse(m.Value))
                           .ToList();
        
        int speed = numbers[0];
        int flyTime = numbers[1];
        int restTime = numbers[2];
        
        string name = line.Split(' ')[0];
        
        return (name, speed, flyTime, restTime);
    }
    
    // Reindeer state for Part 2 simulation
    private class ReindeerState
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int FlyTime { get; set; }
        public int RestTime { get; set; }
        public int Distance { get; set; }
        public int Points { get; set; }
        public int TimeInCycle { get; set; }
        
        public bool IsFlying()
        {
            return TimeInCycle < FlyTime;
        }
        
        public void AdvanceSecond()
        {
            if (IsFlying())
            {
                Distance += Speed;
            }
            
            TimeInCycle++;
            int cycleLength = FlyTime + RestTime;
            if (TimeInCycle >= cycleLength)
            {
                TimeInCycle = 0;
            }
        }
    }
}
```

---

## Step 16: Summary

**Part 1 - Maximum Distance:**
- 🧮 Use mathematical formula (no simulation needed)
- 🔄 Calculate: complete cycles + remaining time
- ⚡ O(N) time complexity - very fast!
- 🎯 Find reindeer with greatest total distance

**Part 2 - Maximum Points:**
- 🕐 Simulate second-by-second
- 📊 Track each reindeer's distance and points
- 👑 Award points to leader(s) each second
- 🏆 Find reindeer with most points

**Key Algorithm Parts:**
```
1. Parse input → extract (name, speed, flyTime, restTime)
2. Part 1: Calculate distance using formula
3. Part 2: Simulate with state tracking
4. Return maximum (distance or points)
```

**Formula (Part 1):**
```
completeCycles = totalTime ÷ (flyTime + restTime)
remaining = totalTime % (flyTime + restTime)
flyingSeconds = completeCycles × flyTime + min(remaining, flyTime)
distance = flyingSeconds × speed
```

**Simulation (Part 2):**
```
For each second:
  1. Update each reindeer (move if flying)
  2. Find leader(s) by maximum distance
  3. Award 1 point to all leaders
Return reindeer with most points
```

**Complexity:**
- ⏱️ Part 1: O(N) - Direct calculation
- ⏱️ Part 2: O(N × T) - Simulation (still fast!)
- 💾 Space: O(N) - Store reindeer states

**Common Pitfalls:**
- ❌ Off-by-one in cycle boundaries
- ❌ Forgetting to reset cycle counter
- ❌ Not capping remaining time at fly time
- ❌ Awarding points before updating distance
- ❌ Using formula for Part 2 (need simulation!)

**Memory Aid: "FCRS"**
```
F - Fly/Rest cycles (alternating states)
C - Calculate complete cycles first
R - Remaining time needs special handling
S - Simulate for Part 2 (no shortcuts!)
```

---

**Happy reindeer racing! 🦌🎄**
