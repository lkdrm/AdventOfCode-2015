# 🦌 Day 14 Visual Guide - Reindeer Olympics

## 🎯 Understanding the Reindeer Race

### The Racing Problem
```
Goal: Determine which reindeer travels the FARTHEST
      in exactly 2503 seconds

Key Points:
- Reindeer alternate between FLYING and RESTING
- Flying = constant top speed
- Resting = zero speed (no movement)
- Each phase lasts complete seconds only
```

**Our Tasks:**
- **Part 1:** Find the reindeer that travels the farthest distance
- **Part 2:** Award points each second to the leader; find highest points

---

## 📋 Part 1: Distance-Based Winner

### Input Format
```
Name can fly X km/s for Y seconds, but then must rest for Z seconds.

Examples:
Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.

Components:
- Speed: X km/s (constant during flight)
- Fly time: Y seconds (duration of flying phase)
- Rest time: Z seconds (duration of resting phase)
```

---

## 🔄 Understanding Flight Cycles

### Comet's Cycle Pattern
```
Speed: 14 km/s
Fly time: 10 seconds
Rest time: 127 seconds
Cycle length: 10 + 127 = 137 seconds

Visual Timeline:
Seconds 1-10:     🚀🚀🚀🚀🚀🚀🚀🚀🚀🚀 (Flying: 10s × 14 km/s = 140 km)
Seconds 11-137:   😴😴😴😴😴😴😴😴... (Resting: 127s × 0 km/s = 0 km)
Seconds 138-147:  🚀🚀🚀🚀🚀🚀🚀🚀🚀🚀 (Flying: 10s × 14 km/s = 140 km)
Seconds 148-274:  😴😴😴😴😴😴😴😴... (Resting: 127s)
...and so on

One complete cycle = 137 seconds
Distance per cycle = 140 km
```

### Dancer's Cycle Pattern
```
Speed: 16 km/s
Fly time: 11 seconds
Rest time: 162 seconds
Cycle length: 11 + 162 = 173 seconds

Visual Timeline:
Seconds 1-11:     🚀🚀🚀🚀🚀🚀🚀🚀🚀🚀🚀 (Flying: 11s × 16 km/s = 176 km)
Seconds 12-173:   😴😴😴😴😴😴😴😴😴... (Resting: 162s × 0 km/s = 0 km)
Seconds 174-184:  🚀🚀🚀🚀🚀🚀🚀🚀🚀🚀🚀 (Flying: 11s × 16 km/s = 176 km)
Seconds 185-346:  😴😴😴😴😴😴😴😴😴... (Resting: 162s)
...and so on

One complete cycle = 173 seconds
Distance per cycle = 176 km
```

---

## 📊 Race Timeline Visualization

### First 200 Seconds: Comet vs Dancer

```
Time    Comet State    Comet Dist    Dancer State   Dancer Dist    Leader
------  -------------  ------------  -------------  -------------  --------
0       START          0 km          START          0 km           Tie
1-10    🚀 FLYING      14→140 km     🚀 FLYING      16→160 km      Dancer
11      😴 RESTING     140 km        🚀 FLYING      176 km         Dancer
12-137  😴 RESTING     140 km        😴 RESTING     176 km         Dancer
138-147 🚀 FLYING      154→280 km    😴 RESTING     176 km         Comet*
148-173 😴 RESTING     280 km        😴 RESTING     176 km         Comet
174-183 😴 RESTING     280 km        🚀 FLYING      192→352 km     Dancer*

*Leadership change!
```

### Distance Over Time Graph
```
Distance (km)
↑
400│                                      ••Dancer
   │                                  ••••
350│                              ••••
   │                          ••••
300│         ••••Comet    ••••
   │     ••••         ••••
250│ ••••         ••••
   │          ••••
200│      ••••
   │  •••••••••
150│••            Dancer leads initially
   │
100│
   │
50 │
   │
0  └──────────────────────────────────────► Time (seconds)
   0    50   100  150  200  250  300
```

---

## 🎯 Mathematical Approach (Part 1)

### The Formula

Instead of simulating second-by-second, we can calculate directly!

```
Given:
- speed: km/s
- flyTime: seconds of flying per cycle
- restTime: seconds of resting per cycle
- totalTime: 2503 seconds (race duration)

Calculate:
cycleTime = flyTime + restTime
completeCycles = totalTime ÷ cycleTime (integer division)
remaining = totalTime % cycleTime (modulo)

Distance from complete cycles:
distanceComplete = completeCycles × flyTime × speed

Distance from remaining time:
flyingInRemaining = min(remaining, flyTime)
distanceRemaining = flyingInRemaining × speed

Total distance:
totalDistance = distanceComplete + distanceRemaining
```

---

## 📊 Example: Comet at 1000 Seconds

### Given Data
```
Comet:
- Speed: 14 km/s
- Fly time: 10 seconds
- Rest time: 127 seconds
- Total time: 1000 seconds
```

### Step-by-Step Calculation

**Step 1: Calculate cycle time**
```
cycleTime = flyTime + restTime
         = 10 + 127
         = 137 seconds
```

**Step 2: Complete cycles**
```
completeCycles = 1000 ÷ 137
              = 7 (integer division)

Time used: 7 × 137 = 959 seconds
```

**Step 3: Distance from complete cycles**
```
Each cycle, Comet flies for 10 seconds at 14 km/s
Distance per cycle = 10 × 14 = 140 km

distanceComplete = 7 × 140
                = 980 km
```

**Step 4: Remaining time**
```
remaining = 1000 - 959
         = 41 seconds

In these 41 seconds, what happens?
- First 10 seconds: FLYING (Comet's fly time)
- Next 31 seconds: RESTING

flyingInRemaining = min(41, 10) = 10 seconds
```

**Step 5: Distance from remaining time**
```
distanceRemaining = 10 × 14
                 = 140 km
```

**Step 6: Total distance**
```
totalDistance = 980 + 140
             = 1120 km
```

### Visual Breakdown
```
┌───────────────────────────────────────────────────────────┐
│ Complete Cycles: 7 cycles                                │
│ Each cycle: 137 seconds, 140 km                          │
│                                                           │
│ Cycle 1 (0-137s):     🚀 10s → 140 km, 😴 127s → 0 km   │
│ Cycle 2 (137-274s):   🚀 10s → 140 km, 😴 127s → 0 km   │
│ Cycle 3 (274-411s):   🚀 10s → 140 km, 😴 127s → 0 km   │
│ Cycle 4 (411-548s):   🚀 10s → 140 km, 😴 127s → 0 km   │
│ Cycle 5 (548-685s):   🚀 10s → 140 km, 😴 127s → 0 km   │
│ Cycle 6 (685-822s):   🚀 10s → 140 km, 😴 127s → 0 km   │
│ Cycle 7 (822-959s):   🚀 10s → 140 km, 😴 127s → 0 km   │
│                                                           │
│ Subtotal: 7 × 140 km = 980 km                            │
└───────────────────────────────────────────────────────────┘

┌───────────────────────────────────────────────────────────┐
│ Remaining: 41 seconds (959-1000s)                        │
│                                                           │
│ Seconds 959-969:  🚀 FLYING  (10s × 14 km/s = 140 km)   │
│ Seconds 970-1000: 😴 RESTING (31s × 0 km/s = 0 km)      │
│                                                           │
│ Subtotal: 140 km                                         │
└───────────────────────────────────────────────────────────┘

TOTAL: 980 + 140 = 1120 km ✓
```

---

## 📊 Example: Dancer at 1000 Seconds

### Given Data
```
Dancer:
- Speed: 16 km/s
- Fly time: 11 seconds
- Rest time: 162 seconds
- Total time: 1000 seconds
```

### Calculation
```
Step 1: cycleTime = 11 + 162 = 173 seconds

Step 2: completeCycles = 1000 ÷ 173 = 5
        Time used: 5 × 173 = 865 seconds

Step 3: distanceComplete = 5 × (11 × 16) = 5 × 176 = 880 km

Step 4: remaining = 1000 - 865 = 135 seconds
        flyingInRemaining = min(135, 11) = 11 seconds

Step 5: distanceRemaining = 11 × 16 = 176 km

Step 6: totalDistance = 880 + 176 = 1056 km
```

### Result at 1000 Seconds
```
Comet:  1120 km  ← WINNER by distance!
Dancer: 1056 km
```

---

## 🏆 Part 2: Points-Based Scoring

### New Rules

**Scoring System:**
```
- At the END of each second, check who is in the LEAD
- Award 1 POINT to the leader(s)
- If TIE for first place → ALL tied reindeer get 1 point
- After 2503 seconds, highest points wins
```

**Why This Changes Everything:**

**Part 1 Focus:** Total distance after 2503 seconds
```
Strategy: Maximize distance over full duration
Winner: Reindeer who goes farthest
```

**Part 2 Focus:** Be in the lead as often as possible
```
Strategy: Consistent leading throughout race
Winner: Reindeer who leads most seconds
```

**Key Difference:**
```
Example:
Reindeer A: Fast burst, then long rest
  → Might win on total distance (Part 1)
  → But only leads during burst periods (fewer points)

Reindeer B: Steady consistent speed
  → Might lose on total distance
  → But leads more often (more points) ✓
```

---

## 🔍 Part 2: Second-by-Second Simulation

### Why We Need Simulation

**Part 1:** Used mathematical formula ✓
```
No need to track each second
Just calculate final distance
```

**Part 2:** Formula won't work! ✗
```
Points depend on RELATIVE positions each second
Leaders change dynamically
Must simulate second-by-second!
```

### Simulation Algorithm
```
1. Initialize all reindeer: distance = 0, points = 0, cycle_position = 0
2. For each second from 1 to 2503:
   a. Update each reindeer's state (flying or resting)
   b. Move flying reindeer (add speed to distance)
   c. Find maximum distance (current leader)
   d. Award 1 point to ALL reindeer at max distance
3. Return maximum points
```

---

## 📊 Part 2: Detailed Timeline Example

### Setup
```
Comet:  speed=14, fly=10, rest=127
Dancer: speed=16, fly=11, rest=162
```

### First 200 Seconds Trace

```
Second 1:
  Update:
    Comet: flying → distance = 14 km
    Dancer: flying → distance = 16 km
  Leader: Dancer (16 km)
  Award: Dancer +1 point
  Scoreboard: Comet: 0 pts, Dancer: 1 pt

Second 2-10:
  Both flying
  Dancer stays ahead each second
  Scoreboard: Comet: 0 pts, Dancer: 10 pts

Second 11:
  Update:
    Comet: resting → distance = 140 km
    Dancer: flying → distance = 176 km
  Leader: Dancer (176 km)
  Award: Dancer +1 point
  Scoreboard: Comet: 0 pts, Dancer: 11 pts

Seconds 12-137:
  Both resting
  Dancer maintains lead (176 > 140)
  Dancer gets 1 point per second
  Scoreboard: Comet: 0 pts, Dancer: 137 pts

Second 138:
  Update:
    Comet: flying (new cycle!) → distance = 154 km
    Dancer: resting → distance = 176 km
  Leader: Dancer (176 > 154)
  Award: Dancer +1 point
  Scoreboard: Comet: 0 pts, Dancer: 138 pts

Second 139:
  Update:
    Comet: flying → distance = 168 km
    Dancer: resting → distance = 176 km
  Leader: Dancer (176 > 168)
  Award: Dancer +1 point
  Scoreboard: Comet: 0 pts, Dancer: 139 pts

Second 140:
  Update:
    Comet: flying → distance = 182 km
    Dancer: resting → distance = 176 km
  Leader: COMET! (182 > 176) ← Leadership change!
  Award: Comet +1 point
  Scoreboard: Comet: 1 pt, Dancer: 139 pts

Seconds 141-147:
  Comet continues flying and leading
  Comet gets 1 point per second
  Scoreboard: Comet: 8 pts, Dancer: 139 pts

Seconds 148-173:
  Both resting, Comet still ahead
  Comet gets 1 point per second
  Scoreboard: Comet: 34 pts, Dancer: 139 pts

Second 174:
  Update:
    Comet: resting → distance = 280 km
    Dancer: flying (new cycle!) → distance = 192 km
  Leader: Comet (280 > 192)
  Award: Comet +1 point
  Scoreboard: Comet: 35 pts, Dancer: 139 pts
```

---

## 📊 Points Accumulation Graph

### After 1000 Seconds
```
Points
↑
700│                    ••••• Dancer: 689 pts (WINNER!)
   │                ••••
600│            ••••
   │        ••••
500│    ••••
   │••••
400│
   │
300│              •••••••• Comet: 312 pts
   │          ••••
200│      ••••
   │  ••••
100│••
   │
0  └───────────────────────────────────────► Time (seconds)
   0    200   400   600   800   1000

Distance at 1000s:
Comet:  1120 km ← Wins Part 1
Dancer: 1056 km

Points at 1000s:
Comet:  312 pts
Dancer: 689 pts ← Wins Part 2!
```

### Why Dancer Wins Part 2
```
Dancer leads for the first ~139 seconds
  → Accumulates 139 points early

Even though Comet overtakes on distance later,
Dancer's early lead gives huge point advantage!

Key Insight: Early consistent lead > Late burst
```

---

## 💻 Implementation: State Tracking

### Reindeer State Structure
```
For each reindeer, track:

name: string
  "Comet", "Dancer", etc.

speed: int
  km/s when flying

flyTime: int
  How many seconds of flying per cycle

restTime: int
  How many seconds of resting per cycle

distance: int
  Current cumulative distance

points: int
  Current cumulative points

timeInCycle: int
  Position within current cycle (0 to cycleTime-1)
```

### State Transitions
```
Flying Phase:
  timeInCycle: 0 to (flyTime - 1)
  Action: distance += speed each second

Resting Phase:
  timeInCycle: flyTime to (flyTime + restTime - 1)
  Action: distance unchanged each second

Cycle Reset:
  When timeInCycle reaches (flyTime + restTime),
  reset to 0 and start new cycle
```

---

## 🔄 Visual State Diagram

### Comet's State Machine
```
                    ┌──────────┐
                    │   START      │
                    │ distance: 0  │
                    │ cycle_pos: 0 │
                    └──────────┘
                           │
                           ↓
        ┌──────────────────────────────────┐
        │       FLYING PHASE               │
        │  Cycle positions: 0-9            │
        │  Duration: 10 seconds            │
        │  Action: distance += 14 km/s     │
        └──────────────────────────────────┘
                       │ After 10 seconds
                       ↓
        ┌──────────────────────────────────┐
        │       RESTING PHASE              │
        │  Cycle positions: 10-136         │
        │  Duration: 127 seconds           │
        │  Action: distance unchanged      │
        └──────────────────────────────────┘
                       │ After 127 seconds
                       └────┐
                            │ Reset to position 0
                            └────┐
                                 ↓
                    ┌─────────────────┐
                    │   NEW CYCLE       │
                    │ Back to FLYING    │
                    └─────────────────┘
```

---

## 📋 Algorithm Trace Example

### Simulation: First 15 Seconds

**Initial State:**
```
Comet:  distance=0, points=0, cycle_pos=0
Dancer: distance=0, points=0, cycle_pos=0
```

**Second 1:**
```
1. Update Comet:
   - Is flying? (0 < 10) → YES
   - distance = 0 + 14 = 14 km
   - cycle_pos = 0 + 1 = 1

2. Update Dancer:
   - Is flying? (0 < 11) → YES
   - distance = 0 + 16 = 16 km
   - cycle_pos = 0 + 1 = 1

3. Find leader:
   - max_distance = max(14, 16) = 16 km
   
4. Award points:
   - Dancer at 16 km == max_distance → points++
   - Dancer points = 0 + 1 = 1

State: Comet[14km, 0pts], Dancer[16km, 1pt]
```

**Second 2:**
```
1. Update Comet:
   - Is flying? (1 < 10) → YES
   - distance = 14 + 14 = 28 km
   - cycle_pos = 1 + 1 = 2

2. Update Dancer:
   - Is flying? (1 < 11) → YES
   - distance = 16 + 16 = 32 km
   - cycle_pos = 1 + 1 = 2

3. Find leader:
   - max_distance = max(28, 32) = 32 km
   
4. Award points:
   - Dancer at 32 km == max_distance → points++
   - Dancer points = 1 + 1 = 2

State: Comet[28km, 0pts], Dancer[32km, 2pts]
```

**Seconds 3-10:**
```
(Similar process)

State after second 10:
Comet:  distance=140km, points=0, cycle_pos=10
Dancer: distance=160km, points=10, cycle_pos=10
```

**Second 11:**
```
1. Update Comet:
   - Is flying? (10 < 10) → NO (resting!)
   - distance = 140 km (unchanged)
   - cycle_pos = 10 + 1 = 11

2. Update Dancer:
   - Is flying? (10 < 11) → YES (still flying!)
   - distance = 160 + 16 = 176 km
   - cycle_pos = 10 + 1 = 11

3. Find leader:
   - max_distance = max(140, 176) = 176 km
   
4. Award points:
   - Dancer at 176 km == max_distance → points++
   - Dancer points = 10 + 1 = 11

State: Comet[140km, 0pts], Dancer[176km, 11pts]
```

---

## 📊 Comparison Table: Part 1 vs Part 2

### Strategy Differences

| Aspect            | Part 1: Distance    | Part 2: Points                  |
|-------------------|---------------------|---------------------------------|
| **Goal**          | Maximum distance    | Maximum points                  |
| **Calculation**   | Mathematical formula| Second-by-second simulation     |
| **Time Matters**  | Only final time     | Every second counts             |
| **Winner Trait**  | Farthest total      | Most consistent leader          |
| **Complexity**    | O(N)                | O(N × T)                        |

### Results Comparison (1000 seconds)

| Reindeer | Distance | Distance Rank | Points | Points Rank | Winner? |
|----------|----------|---------------|--------|-------------|---------|
| Comet    | 1120 km  | 1st ✓        | 312    | 2nd         | Part 1  |
| Dancer   | 1056 km  | 2nd           | 689    | 1st ✓      | Part 2  |

**Key Insight:**
```
Distance winner ≠ Points winner!

Dancer builds early lead → accumulates points
Comet overtakes later → but already too far behind in points
```

---

## 📝 Parsing Input Data

### Input Line Structure
```
Position: 0      1   2   3    4    5   6    7        8   9    10   11   12  13      14
          Name   can fly speed km/s for duration seconds, but then must rest restTime seconds.

Example: "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds."

Extract:
- Name: words[0] = "Comet"
- Speed: number[0] = 14
- Fly time: number[1] = 10
- Rest time: number[2] = 127
```

### Regex Extraction
```
Pattern: (\d+)
Matches: All numeric sequences

Example line:
"Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."

Matches:
  Match 0: "16"  → speed
  Match 1: "11"  → fly time
  Match 2: "162" → rest time
```

---

## 🐛 Common Mistakes

### Mistake 1: Wrong Cycle Boundary
```csharp
// WRONG - Includes fly time boundary incorrectly
if (cycle_pos <= flyTime) // ✗
    distance += speed;

// CORRECT - Fly during [0, flyTime)
if (cycle_pos < flyTime) // ✓
    distance += speed;

Example: flyTime = 10
Fly at positions: 0,1,2,3,4,5,6,7,8,9
Rest at positions: 10,11,12,...
```

### Mistake 2: Forgetting Cycle Reset
```csharp
// WRONG - Never resets
cycle_pos++;

// CORRECT - Reset at cycle end
cycle_pos++;
if (cycle_pos >= flyTime + restTime)
    cycle_pos = 0;
```

### Mistake 3: Not Capping Remaining Time
```csharp
// WRONG - May count rest as flying
distance += remaining * speed; // ✗

// CORRECT - Cap at actual fly time
int flying = Math.Min(remaining, flyTime);
distance += flying * speed; // ✓
```

### Mistake 4: Part 2 Point Timing
```csharp
// WRONG - Points before position update
AwardPoints();      // ✗
UpdatePositions();

// CORRECT - Update first, then award
UpdatePositions();  // ✓
AwardPoints();
```

### Mistake 5: Forgetting Tied Leaders
```csharp
// WRONG - Only one winner
var leader = reindeer.MaxBy(r => r.Distance);
leader.Points++;

// CORRECT - All tied leaders get points
int maxDist = reindeer.Max(r => r.Distance);
foreach (var deer in reindeer)
    if (deer.Distance == maxDist)
        deer.Points++;
```

---

## 📊 Complexity Analysis

### Time Complexity

**Part 1:**
```
O(N) where N = number of reindeer

For each reindeer:
  - Parse: O(1)
  - Calculate: O(1) (mathematical formula)
  - Compare: O(1)

Total: O(N) - Very efficient!
```

**Part 2:**
```
O(N × T) where N = reindeer, T = time (2503)

For each of T seconds:
  - Update N reindeer: O(N)
  - Find max: O(N)
  - Award points: O(N)

Total: O(N × T)
For N=9, T=2503: 9 × 2503 ≈ 22,500 operations
Still very fast!
```

### Space Complexity

```
O(N) for storing reindeer states

Each reindeer stores:
  - name, speed, flyTime, restTime
  - distance, points, cycle_pos

Total: Constant space per reindeer
```

---

## 🧪 Testing Strategy

### Test Case 1: Example from Problem
```
Input: Comet and Dancer at 1000s
Expected Part 1: 1120 (Comet)
Expected Part 2: 689 (Dancer)
Verify: Formula matches simulation for Part 1
```

### Test Case 2: Single Reindeer
```
Input: One reindeer only
Expected: Wins both parts
Part 2: Points = 2503 (leads every second)
```

### Test Case 3: Identical Reindeer
```
Input: Two with same stats
Expected: Tied distance and points
Part 2: Both get 2503 points
```

### Test Case 4: No Rest Time
```
Input: Reindeer with rest=0
Expected: Flies continuously
Distance: speed × 2503
```

### Test Case 5: Very Long Rest
```
Input: Fly 1s, rest 10000s
Expected: Only flies once
Distance: speed × 1
Points: Depends on other reindeer
```

---

## 💡 Key Insights

### Part 1 Insights
```
✓ Mathematical approach is fastest
✓ Complete cycles + remaining time
✓ Shorter cycles may win despite slower speed
✓ O(N) complexity - very efficient
```

### Part 2 Insights
```
✓ Simulation required (no formula shortcut)
✓ Early lead accumulates more points
✓ Consistent speed beats burst speed
✓ Distance winner ≠ points winner
```

### Strategy Differences
```
Part 1: Optimize total distance
  → More flying time over 2503s
  → Complete more cycles

Part 2: Optimize lead time
  → Be ahead as often as possible
  → Early lead is valuable
  → Consistency matters
```

---

## 📝 Summary

**Part 1 - Maximum Distance:**
- 🎯 Use mathematical formula
- 📐 Calculate: complete_cycles × distance_per_cycle + remaining
- ⚡ Very fast: O(N)
- 🏆 Find reindeer with greatest distance

**Part 2 - Maximum Points:**
- 🔄 Simulate second-by-second
- 🎯 Award points to leader(s) each second
- 🤝 Handle ties (multiple leaders get points)
- 🏆 Find reindeer with most points

**Key Formulas:**

Part 1 Distance:
```
cycle_time = fly_time + rest_time
complete = total_time ÷ cycle_time
remaining = total_time % cycle_time
flying_remaining = min(remaining, fly_time)
distance = complete × fly_time × speed + flying_remaining × speed
```

Part 2 Simulation:
```
For each second:
  1. Update all positions
  2. Find max distance
  3. Award points to all at max
Return max points
```

**Memory Aid: "FCPS"**
```
F - Formula for Part 1 (fast calculation)
C - Cycles (fly + rest pattern)
P - Points for Part 2 (simulation needed)
S - Simulation second-by-second
```

---

**Happy reindeer racing! 🦌🎄**
