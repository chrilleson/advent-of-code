# Advent of Code Solutions Repository

## Repository Overview

This repository contains solutions to Advent of Code challenges implemented in two languages:
- **C#** (2024 solutions)
- **Go** (2024 and 2025 solutions)

Each implementation is self-contained with its own menu system for running individual day solutions.

## Repository Structure

```
advent-of-code/
├── src/
│   ├── c#/
│   │   └── AdventOfCode/          # C# solutions (2024)
│   │       ├── Day1.cs - Day9.cs  # Daily solution files
│   │       ├── Extensions.cs      # Helper extension methods
│   │       ├── Program.cs         # Menu system and entry point
│   │       └── inputs/            # Input files (day1.txt, etc.)
│   └── go-lang/
│       ├── 2024/                  # Go solutions for 2024
│       │   ├── day1.go - day2.go  # Daily solution files
│       │   └── year.go            # Year menu system
│       ├── 2025/                  # Go solutions for 2025
│       │   ├── day1.go            # Daily solution files
│       │   └── year.go            # Year menu system
│       ├── utils/                 # Shared utilities
│       │   └── utils.go           # Input reading helpers
│       └── main.go                # Main entry point with year selection
└── .github/
    ├── copilot-instructions.md    # This file
    └── instructions/              # Language-specific instructions
```

## AI Agent Guidance - Learning-Focused Approach

**IMPORTANT**: This repository is used for learning new programming languages and problem-solving techniques.

When assisting with Advent of Code solutions:
- **DO NOT** provide complete, working solutions to puzzle logic
- **DO** guide toward the right approach and direction
- **DO** explain concepts, patterns, and language features
- **DO** point out relevant documentation or examples
- **DO** ask questions that help the developer think through the problem
- **DO** suggest which standard library functions or patterns might be useful
- **DO** help debug issues or explain error messages
- **DO** provide boilerplate code (file reading, menu registration, project structure)
- **DO** show syntax examples for language features unrelated to the puzzle solution

### Suggested Response Pattern

1. **Help break down the problem** into smaller, manageable steps
2. **Suggest appropriate approaches**: Point to data structures, algorithms, or patterns that might work
3. **Highlight language features**: Show which standard library functions or language constructs could be useful
4. **Provide syntax examples**: Small code snippets demonstrating how to use a feature (not the full solution)
5. **Ask guiding questions**: "Have you considered...?", "What if you tried...?", "How would you handle...?"
6. **Let the developer implement**: Allow them to write the actual puzzle logic

### Example - Good vs. Bad Assistance

**BAD** (Complete solution):
```csharp
private static int Solve(IEnumerable<string> input) {
    return input.Select(x => x.ParseInts())
        .Select(nums => nums.Max() - nums.Min())
        .Sum();
}
```

**GOOD** (Guidance):
```
For this problem, you'll want to:
1. Parse each line to extract the numbers
2. For each set of numbers, find the difference between max and min
3. Sum all those differences

The `ParseInts()` extension method can help with step 1.
For finding max/min in C#, LINQ has `Max()` and `Min()` methods.
How would you combine these steps together?
```

**ACCEPTABLE** (Boilerplate/Syntax help):
```csharp
// This is fine - it's project structure, not puzzle logic
public static class DayX
{
    public static void Run()
    {
        var input = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\dayX.txt")
            .ReadLines();
        
        // Developer implements the actual solution here
    }
}
```

**ACCEPTABLE** (Syntax example unrelated to puzzle):
```
To sort a list in Go, you can use:
```go
import "slices"
slices.Sort(numbers)  // Sorts in place
```

### When Complete Solutions Are OK

You may provide complete implementations for:
- **Boilerplate code**: File reading, menu registration, project setup
- **Non-puzzle logic**: Menu systems, input parsing infrastructure
- **Syntax demonstrations**: Showing how a language feature works in isolation
- **Debugging assistance**: Fixing syntax errors, explaining compiler errors
- **Refactoring existing code**: Improving code that already works

### When to Hold Back

Do NOT provide complete implementations for:
- **Core puzzle logic**: The actual algorithm that solves the Advent of Code challenge
- **Part 1 or Part 2 solutions**: The specific problem-solving code
- **Algorithm implementations**: The actual sorting, searching, calculating logic for the puzzle

## Solution Pattern

### Common Structure
Each day's solution follows a consistent pattern:
- **Part 1 and Part 2**: Each Advent of Code day has two parts
- **Input Files**: Located in language-specific input directories
- **Console Output**: Solutions print results to the console

### C# Pattern (Day1.cs, Day2.cs, etc.)
- Static class with `public static void Run()` method
- Helper methods are private
- Extensions methods in `Extensions.cs` for parsing and utilities
- Input files: `inputs/day1.txt`, `inputs/day2.txt`, etc.

### Go Pattern (day1.go, day2.go, etc.)
- Each day is a function: `func Day1()`, `func Day2()`, etc.
- Organized by year in separate packages (`year2024`, `year2025`)
- Input files: `inputs/2024/day1.txt`, `inputs/2025/day1.txt`, etc.
- Utils package provides `ReadInputFile(year, day)` helper

## How to Build and Run

### C# Project
**Prerequisites**: .NET SDK (version compatible with C# 12+)

**Build**:
```bash
cd src/c#/AdventOfCode
dotnet build
```

**Run**:
```bash
dotnet run
```

This launches an interactive menu where you select which day to run.

**Input Files**: Place input files in `src/c#/AdventOfCode/inputs/` as `day1.txt`, `day2.txt`, etc.

### Go Project
**Prerequisites**: Go 1.21+ (uses slices package)

**Build**:
```bash
cd src/go-lang
go build
```

**Run**:
```bash
go run main.go
# Or if built:
./advent-of-code.exe
```

This launches a two-level menu:
1. First select the year (2024 or 2025)
2. Then select the day to run

**Input Files**: Place input files in `src/go-lang/inputs/YEAR/` as `day1.txt`, `day2.txt`, etc.

## Adding New Solutions

### Adding a C# Solution (New Day)

1. Create a new file: `src/c#/AdventOfCode/DayX.cs`
2. Follow this template:
```csharp
namespace AdventOfCode;

public static class DayX
{
    public static void Run()
    {
        var input = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\dayX.txt")
            .ReadLines();
        
        Console.WriteLine("Part 1: {0}", SolvePart1(input));
        Console.WriteLine("Part 2: {0}", SolvePart2(input));
    }

    private static int SolvePart1(IEnumerable<string> input)
    {
        // Implementation
        return 0;
    }

    private static int SolvePart2(IEnumerable<string> input)
    {
        // Implementation
        return 0;
    }
}
```

3. Register in `Program.cs` menu:
```csharp
static IReadOnlyDictionary<int, Action> MenuItems() => new Dictionary<int, Action>
{
    // ... existing days
    { X, DayX.Run },
};
```

4. Add input file: `src/c#/AdventOfCode/inputs/dayX.txt`

### Adding a Go Solution (New Day)

1. Create a new file: `src/go-lang/YEAR/dayX.go`
2. Follow this template:
```go
package yearYYYY

import (
    "advent-of-code/utils"
    "fmt"
    "strings"
)

func DayX() {
    data := utils.ReadInputFile("YYYY", "dayX")
    lines := strings.Split(strings.TrimSpace(string(data)), "\n")
    
    fmt.Println("Part 1:", solvePart1(lines))
    fmt.Println("Part 2:", solvePart2(lines))
}

func solvePart1(lines []string) int {
    // Implementation
    return 0
}

func solvePart2(lines []string) int {
    // Implementation
    return 0
}
```

3. Register in `src/go-lang/YEAR/year.go`:
```go
func MenuItemsYYYY() map[int]func() {
    return map[int]func(){
        // ... existing days
        X: DayX,
    }
}
```

4. Add input file: `src/go-lang/inputs/YYYY/dayX.txt`

## Coding Conventions

### Naming
- **C#**: PascalCase for public methods, camelCase for private methods
- **Go**: Exported functions are PascalCase (e.g., `Day1`), unexported are camelCase
- **File names**: `DayX.cs` (C#), `dayX.go` (Go)
- **Input files**: `dayX.txt` where X is the day number (1-25)

### Code Style
- **C#**: Use LINQ and extension methods for data transformations
- **C#**: Prefer expression-bodied members where appropriate
- **Go**: Keep functions focused and simple
- **Go**: Use standard library packages (`strings`, `strconv`, `slices`, etc.)

### Extension Methods (C#)
Common extensions in `Extensions.cs`:
- `ReadLines()`: Stream lines from a TextReader
- `ParseInts(string)`: Extract all integers from a string using regex
- `ParseLongs(string)`: Extract all long integers from a string using regex

### Utils Package (Go)
Common utilities in `utils/utils.go`:
- `ReadInputFile(year, day string) []byte`: Reads input file for specified year/day

## Input File Handling

**IMPORTANT**: Input files are NOT committed to the repository (see `.gitignore`). Each developer must:
1. Download their personal input from adventofcode.com
2. Place it in the appropriate directory with the correct name

**C# Location**: `src/c#/AdventOfCode/inputs/dayX.txt`
**Go Location**: `src/go-lang/inputs/YEAR/dayX.txt`

## Key Dependencies

### C#
- .NET 8.0+ (uses C# 12 features like collection expressions)
- System.Text.RegularExpressions (for parsing)

### Go
- Go 1.21+ (for `slices` package)
- Standard library only (no external dependencies)

## Common Patterns

### Parsing Input (C#)
```csharp
// Read all lines
var lines = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\day1.txt")
    .ReadLines();

// Extract integers from a line
var numbers = line.ParseInts();  // Returns IEnumerable<int>
var longs = line.ParseLongs();   // Returns IEnumerable<long>
```

### Parsing Input (Go)
```go
// Read file
data := utils.ReadInputFile("2024", "day1")

// Split into lines
lines := strings.Split(strings.TrimSpace(string(data)), "\n")

// Clean lines
for i, line := range lines {
    lines[i] = strings.TrimSpace(line)
}

// Parse integers
num, err := strconv.Atoi(str)
```

## Testing

Currently, there are no automated tests. Solutions are validated by:
1. Running against the official Advent of Code input
2. Comparing output with the expected answer on adventofcode.com

## Menu System Behavior

Both implementations use interactive console menus:
- Select a day to run the solution
- View output (Part 1 and Part 2 answers)
- Press Enter to return to menu
- Invalid selections show error and return to menu
- Exit option available

## Notes for AI Agents

- **LEARNING-FOCUSED REPOSITORY**: Provide guidance and direction rather than complete solutions. Help the developer learn by suggesting approaches, explaining concepts, and asking guiding questions. See "AI Agent Guidance" section above for detailed guidelines.
- When adding new days, ALWAYS update both the solution file AND the menu registration
- Input files use the format `dayX.txt` where X is the day number without leading zeros
- The C# project uses `AppContext.BaseDirectory` for reliable input file paths
- Go uses relative paths from the working directory: `inputs/YEAR/dayX.txt`
- Both implementations expect UTF-8 encoded input files
- Console output includes emoji in prompts (🎅 for Christmas theme)
- Boilerplate code (file I/O, menu registration, project structure) can be provided in full
- Puzzle logic and algorithms should be guided, not solved
