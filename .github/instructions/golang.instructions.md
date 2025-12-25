---
applyTo: "src/go-lang/**/*.go"
---

# Go Advent of Code Implementation Guide

## Project Structure

This is a Go module with year-based package organization:

```
src/go-lang/
├── go.mod                 # Module definition
├── main.go                # Entry point with year selection menu
├── 2024/                  # Year 2024 solutions
│   ├── day1.go
│   ├── day2.go
│   └── year.go            # Year-specific menu
├── 2025/                  # Year 2025 solutions
│   ├── day1.go
│   └── year.go            # Year-specific menu
├── utils/                 # Shared utilities
│   └── utils.go           # Input file reading
├── inputs/                # Input files (not in source control)
│   ├── 2024/
│   │   ├── day1.txt
│   │   └── day2.txt
│   └── 2025/
│       └── day1.txt
└── advent-of-code.exe     # Built binary
```

## Code Standards and Patterns

### Package Organization

- **main package**: Entry point in `main.go`
- **year packages**: `year2024`, `year2025` in respective directories
- **utils package**: Shared utilities in `utils/`

Package imports:
```go
package year2024  // or year2025

import (
    "advent-of-code/utils"
    "fmt"
    "strings"
    "strconv"
    // Other standard library imports
)
```

### Naming Conventions

- **Exported functions**: PascalCase - `Day1()`, `Day2()`, `RunYear2024()`
- **Unexported functions**: camelCase - `solvePart1()`, `calcDistance()`, `splitLists()`
- **Variables**: camelCase - `totalDistance`, `listOne`, `cleanLines`
- **Constants**: PascalCase or SCREAMING_SNAKE_CASE - `TripleSpaceSeparator` or `TRIPLE_SPACE_SEPARATOR`
- **Files**: lowercase - `day1.go`, `day2.go`, `year.go`

### Day Solution Pattern

Each day solution MUST follow this structure:

```go
package yearYYYY

import (
    "advent-of-code/utils"
    "fmt"
    "strings"
)

func DayX() {
    // Read input file
    data := utils.ReadInputFile("YYYY", "dayX")
    cleanLines := strings.Split(strings.TrimSpace(string(data)), "\n")

    // Clean lines (trim whitespace)
    for i, line := range cleanLines {
        cleanLines[i] = strings.TrimSpace(line)
    }

    // Solve and display results
    fmt.Println("Part 1:", solvePart1(cleanLines))
    fmt.Println("Part 2:", solvePart2(cleanLines))
}

func solvePart1(lines []string) int {
    // Part 1 implementation
    return 0
}

func solvePart2(lines []string) int {
    // Part 2 implementation
    return 0
}
```

### Input File Reading

**ALWAYS** use the `utils.ReadInputFile()` helper:

```go
data := utils.ReadInputFile("2024", "day1")
```

**Why**:
- Centralized error handling
- Consistent path resolution
- Returns `[]byte` that can be converted to string

**Standard parsing pattern**:
```go
data := utils.ReadInputFile("2024", "day1")
cleanLines := strings.Split(strings.TrimSpace(string(data)), "\n")

// Always trim each line
for i, line := range cleanLines {
    cleanLines[i] = strings.TrimSpace(line)
}
```

### Utils Package Functions

Available in `utils/utils.go`:

**ReadInputFile(year, day string) []byte**
```go
func ReadInputFile(year string, day string) []byte {
    filePath := fmt.Sprintf("inputs/%s/%s.txt", year, day)
    data, err := os.ReadFile(filePath)
    if err != nil {
        log.Fatal(err)
    }
    return data
}
```

Usage:
```go
data := utils.ReadInputFile("2024", "day1")
```

**When to add new utils**:
- Function is used in 3+ day solutions
- Logic is year-agnostic and generic
- Place in `utils/utils.go` as exported function

### Helper Function Patterns

Keep helper functions **unexported** (lowercase) and focused:

```go
func calcDistance(listOne []int, listTwo []int) int {
    slices.Sort(listOne)
    slices.Sort(listTwo)

    totalDistance := 0
    for i := range listOne {
        totalDistance += int(math.Abs(float64(listOne[i] - listTwo[i])))
    }

    return totalDistance
}
```

**Guidelines**:
- One responsibility per function
- Descriptive names (verbs for actions)
- Accept parameters, return results (avoid global state)
- Keep functions pure when possible

### Common Parsing Patterns

**Split by separator**:
```go
numbers := strings.Split(line, "   ")  // Triple space
```

**Parse integers**:
```go
num, err := strconv.Atoi(str)
if err != nil {
    log.Fatal(err)
}
```

**Parse multiple integers from a line**:
```go
parts := strings.Fields(line)  // Split on any whitespace
numbers := make([]int, len(parts))
for i, part := range parts {
    num, err := strconv.Atoi(part)
    if err != nil {
        log.Fatal(err)
    }
    numbers[i] = num
}
```

**Build slices**:
```go
listOne := []int{}
listTwo := []int{}

for _, line := range lines {
    numbers := strings.Split(line, "   ")
    numOne, _ := strconv.Atoi(numbers[0])
    numTwo, _ := strconv.Atoi(numbers[1])
    
    listOne = append(listOne, numOne)
    listTwo = append(listTwo, numTwo)
}
```

### Standard Library Usage

Prefer standard library packages:

**Common imports**:
```go
import (
    "fmt"           // Printing, string formatting
    "strings"       // String manipulation
    "strconv"       // String to number conversion
    "slices"        // Slice operations (Go 1.21+)
    "math"          // Math operations
    "log"           // Error logging
)
```

**Useful functions**:
- `slices.Sort(slice)` - Sort in place
- `strings.Split(s, sep)` - Split string
- `strings.TrimSpace(s)` - Remove leading/trailing whitespace
- `strings.Fields(s)` - Split on any whitespace
- `math.Abs(x)` - Absolute value
- `fmt.Sprintf(format, args...)` - Format string
- `fmt.Println(args...)` - Print to console

### Error Handling

For Advent of Code, use simple `log.Fatal()` for errors:

```go
num, err := strconv.Atoi(str)
if err != nil {
    log.Fatal(err)
}
```

**Why**:
- Solutions assume valid input
- Simplifies code
- Quick feedback if input format is wrong

Only add detailed error handling if:
- Debugging a specific issue
- Input format is ambiguous
- Solution logic requires it

### Console Output

Use descriptive output:

```go
fmt.Println("Total distance:", calcDistance(listOne, listTwo))
fmt.Println("Total similarity:", calcSimilarity(listOne, listTwo))
```

**Format**:
- Start with a description
- Use comma separator (not `fmt.Printf` unless formatting needed)
- Keep descriptions concise

**For debugging**:
```go
fmt.Printf("Debug - value: %d, list: %v\n", value, list)
```

### Year Menu System

Each year has a `year.go` file with the menu system:

**Structure** (`2024/year.go`):
```go
package year2024

import (
    "bufio"
    "fmt"
    "os"
    "strconv"
    "strings"
)

func RunYear2024() {
    menuItems := MenuItemsYear2024()
    
    for {
        // Display menu
        fmt.Print("\033[2J\033[H")  // Clear screen
        fmt.Println("🎅 Advent of Code 2024 🎅")
        fmt.Println()
        
        for day := 1; day <= len(menuItems); day++ {
            fmt.Printf("%d : Day %d\n", day, day)
        }
        fmt.Printf("%d : Back\n", len(menuItems)+1)
        fmt.Print("> ")
        
        // Read input
        reader := bufio.NewReader(os.Stdin)
        input, err := reader.ReadString('\n')
        if err != nil {
            fmt.Printf("\n❌ Error: %v\n", err)
            continue
        }
        
        // Parse choice
        choice, err := strconv.Atoi(strings.TrimSpace(input))
        if err != nil {
            fmt.Print("\033[2J\033[H")
            fmt.Printf("\n❌ Invalid input\n")
            continue
        }
        
        // Handle choice
        if choice == len(menuItems)+1 {
            return  // Back to year selection
        }
        
        if fn, ok := menuItems[choice]; ok {
            fmt.Print("\033[2J\033[H")
            fmt.Printf("🎅 Advent of Code 2024 🎅\nDay %d\n\n", choice)
            fn()
            fmt.Print("\nPress Enter to continue...")
            reader.ReadString('\n')
        } else {
            fmt.Print("\033[2J\033[H")
            fmt.Printf("\n❌ Invalid day: %d\n", choice)
        }
    }
}

func MenuItemsYear2024() map[int]func() {
    return map[int]func(){
        1: Day1,
        2: Day2,
        // Add more days here
    }
}
```

**When adding a new day**:
Add to the `MenuItemsYear2024()` (or appropriate year) map:
```go
func MenuItemsYear2024() map[int]func() {
    return map[int]func(){
        1: Day1,
        2: Day2,
        X: DayX,  // Add your new day here
    }
}
```

### Main Entry Point

The `main.go` provides year selection:

```go
package main

import (
    year2024 "advent-of-code/2024"
    year2025 "advent-of-code/2025"
    // Import other years as needed
)

func main() {
    Run()
}

func Run() {
    // Year selection menu
    // Calls year2024.RunYear2024() or year2025.RunYear2025()
}
```

### Modular Arithmetic Pattern

For problems involving circular buffers or wrapping (like Day 1 2025):

```go
// Modulo operation that handles negatives correctly
currentPosition = ((currentPosition + distance) % 100 + 100) % 100
currentPosition = ((currentPosition - distance) % 100 + 100) % 100
```

### Sorting and Searching

**Sort slices**:
```go
import "slices"

slices.Sort(numbers)  // Sorts in place
```

**Count occurrences**:
```go
occurrences := 0
for _, num := range listTwo {
    if num == target {
        occurrences++
    }
}
```

**Find in sorted list** (binary search):
```go
import "slices"

index := slices.BinarySearch(sortedList, target)
```

### Building and Running

**Build**:
```bash
cd src/go-lang
go build
```

**Run without building**:
```bash
go run main.go
```

**Run built binary**:
```bash
# Windows
./advent-of-code.exe

# Linux/Mac
./advent-of-code
```

**Clean build cache**:
```bash
go clean
```

### Module Setup

The `go.mod` file:
```go
module advent-of-code

go 1.21  // Or later
```

**No external dependencies** - use standard library only

### Testing Solutions

1. Run the program: `go run main.go`
2. Select the year
3. Select the day
4. Compare output with Advent of Code expected answer
5. Press Enter to return to menu

### Input Files

- Location: `src/go-lang/inputs/YEAR/`
- Naming: `day1.txt`, `day2.txt`, etc. (no leading zeros)
- Encoding: UTF-8
- **NOT** committed to source control (`.gitignore`)
- Download from your personal Advent of Code account

### Adding a New Year

1. Create new directory: `src/go-lang/YYYY/`
2. Create `year.go` with menu system (copy from existing year)
3. Create input directory: `src/go-lang/inputs/YYYY/`
4. Update `main.go` to import and handle new year
5. Add year option to main menu

## Anti-Patterns to Avoid

1. **Don't** use hardcoded paths:
```go
// BAD
data, _ := os.ReadFile("C:\\Users\\...\\inputs\\2024\\day1.txt")

// GOOD
data := utils.ReadInputFile("2024", "day1")
```

2. **Don't** export helper functions:
```go
// BAD
func CalcDistance(...) int

// GOOD
func calcDistance(...) int
```

3. **Don't** ignore errors in production code (AoC is an exception):
```go
// OK for AoC
num, _ := strconv.Atoi(str)

// Better (if you want to be safe)
num, err := strconv.Atoi(str)
if err != nil {
    log.Fatal(err)
}
```

4. **Don't** forget to update the year menu when adding a new day

5. **Don't** use global variables for solution state:
```go
// BAD
var globalState int

func Day1() {
    globalState = 0
    // ...
}

// GOOD - pass data as parameters
func Day1() {
    state := solvePart1(lines)
    // ...
}
```

## Performance Considerations

For Advent of Code:
- Readability > Performance (unless too slow)
- Use `map` for O(1) lookups when needed
- `slices.Sort()` is efficient (quicksort)
- Avoid unnecessary allocations in hot loops

**Map for counting**:
```go
counts := make(map[int]int)
for _, num := range numbers {
    counts[num]++
}
```

**Preallocate slices if size known**:
```go
result := make([]int, 0, expectedSize)
```

## Quick Reference

**Create new day**:
1. Create `YYYY/dayX.go` with day function
2. Add to `YYYY/year.go` menu map
3. Create `inputs/YYYY/dayX.txt`
4. Implement solution
5. Test: `go run main.go`

**Common patterns**:
```go
// Read input
data := utils.ReadInputFile("2024", "day1")
lines := strings.Split(strings.TrimSpace(string(data)), "\n")

// Parse integers
num, err := strconv.Atoi(str)

// Sort
slices.Sort(numbers)

// Absolute value
abs := int(math.Abs(float64(x - y)))

// Format output
fmt.Println("Result:", result)
```

**Useful packages**:
- `fmt` - formatting and printing
- `strings` - string operations
- `strconv` - conversions
- `slices` - slice utilities (Go 1.21+)
- `math` - math functions
- `log` - logging
