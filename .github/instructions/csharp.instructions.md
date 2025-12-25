---
applyTo: "src/c#/**/*.cs"
---

# C# Advent of Code Implementation Guide

## Project Structure

This is a .NET console application targeting .NET 8.0+ with the following structure:

```
src/c#/AdventOfCode/
├── AdventOfCode.csproj    # Project file
├── Program.cs             # Entry point with interactive menu
├── Extensions.cs          # Shared extension methods
├── Day1.cs - Day9.cs      # Individual day solutions
└── inputs/                # Input text files (not in source control)
    ├── day1.txt
    ├── day2.txt
    └── ...
```

## Code Standards and Patterns

### File Organization
- One static class per day (`public static class Day1`, `Day2`, etc.)
- All day classes in the `AdventOfCode` namespace
- Extension methods in a separate `Extensions.cs` file

### Naming Conventions
- **Classes**: PascalCase - `Day1`, `Day2`
- **Public methods**: PascalCase - `Run()`
- **Private methods**: camelCase - `calcDistance()`, `solvePart1()`
- **Variables**: camelCase - `orderedList1`, `totalDistance`

### Day Solution Pattern

Each day solution MUST follow this structure:

```csharp
namespace AdventOfCode;

public static class DayX
{
    public static void Run()
    {
        // Read input file
        var input = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\dayX.txt")
            .ReadLines();
        
        // Process and display results
        Console.WriteLine("Description: {0}", SolvePart1(input));
        Console.WriteLine("Description: {0}", SolvePart2(input));
    }

    private static int SolvePart1(IEnumerable<string> input)
    {
        // Part 1 implementation
        return 0;
    }

    private static int SolvePart2(IEnumerable<string> input)
    {
        // Part 2 implementation
        return 0;
    }
}
```

### Input File Reading

**ALWAYS** use this pattern for reading input files:

```csharp
var input = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\dayX.txt")
    .ReadLines();
```

**Why**:
- `AppContext.BaseDirectory` ensures correct path resolution regardless of working directory
- `ReadLines()` extension method provides lazy enumeration
- Verbatim string literal (`@`) handles backslashes correctly

### Extension Methods

Available extension methods in `Extensions.cs`:

1. **ReadLines()** - Stream lines from a TextReader
```csharp
public static IEnumerable<string> ReadLines(this TextReader text)
```

Usage:
```csharp
var lines = File.OpenText(path).ReadLines();
```

2. **ParseInts()** - Extract all integers from a string
```csharp
public static IEnumerable<int> ParseInts(this string line)
```

Usage:
```csharp
var numbers = "foo 123 bar 456".ParseInts(); // [123, 456]
```

3. **ParseLongs()** - Extract all long integers from a string
```csharp
public static IEnumerable<long> ParseLongs(this string line)
```

Usage:
```csharp
var numbers = "12345678901 98765432109".ParseLongs();
```

**When to add new extensions**:
- Pattern is used in 3+ day solutions
- Logic is generic and reusable
- Place in `Extensions.cs` and make it a `public static` extension method

### LINQ and Functional Style

Prefer LINQ and functional patterns:

**Good**:
```csharp
var result = list
    .Where(x => x > 0)
    .Select(x => x * 2)
    .Sum();
```

**Avoid**:
```csharp
var result = 0;
foreach (var item in list)
{
    if (item > 0)
    {
        result += item * 2;
    }
}
```

### Modern C# Features

This project uses C# 12 features. Utilize:

1. **Collection expressions**:
```csharp
List<int> numbers = [1, 2, 3, 4];
if (acc.Count <= i) acc.Add([]);
```

2. **Pattern matching**:
```csharp
private static (IEnumerable<int> left, IEnumerable<int> right) ToPair<T>(this List<T> list) => list switch
{
    [IEnumerable<int> a, IEnumerable<int> b] => (a, b),
    _ => throw new ArgumentException()
};
```

3. **String interpolation**:
```csharp
Console.WriteLine("Total distance: {0}", distance);
```

4. **Range operators**:
```csharp
var first = array[0];
var rest = array[1..];
```

### Helper Method Patterns

Keep helper methods **private** and focused:

```csharp
private static int CalcDistance(IEnumerable<int> list1, IEnumerable<int> list2)
{
    var orderedList1 = list1.OrderBy(x => x).ToList();
    var orderedList2 = list2.OrderBy(x => x).ToList();

    return orderedList1
        .Select((t, i) => t - orderedList2[i])
        .Sum(result => result < 0 ? result * -1 : result);
}
```

**Guidelines**:
- One responsibility per method
- Descriptive names that indicate purpose
- Use LINQ for transformations
- Avoid mutation when possible

### Error Handling

For Advent of Code solutions, minimal error handling is acceptable:

```csharp
// Simple, direct approach
var numbers = line.ParseInts().ToList();
var first = numbers[0];  // Will throw if empty - that's OK for AoC
```

Only add error handling if:
- The input format is ambiguous
- You're debugging a specific issue
- The solution requires it (rare)

### Console Output

Use descriptive output with string formatting:

```csharp
Console.WriteLine("Total distance: {0}", CalcDistance(foo.left, foo.right));
Console.WriteLine("Similarity score: {0}", CalcSimilarity(foo.left, foo.right));
```

**Format**:
- Start with a description of what the value represents
- Use `{0}`, `{1}` placeholders with Console.WriteLine
- Keep descriptions concise but clear

### Registering a New Day in Program.cs

After creating a new `DayX.cs` file, register it in `Program.cs`:

```csharp
static IReadOnlyDictionary<int, Action> MenuItems() => new Dictionary<int, Action>
{
    { 1, Day1.Run },
    { 2, Day2.Run },
    { 3, Day3.Run },
    // ... existing days
    { X, DayX.Run },  // Add your new day here
};
```

**IMPORTANT**: Always update this dictionary when adding a new day solution.

### Common Parsing Patterns

**Splitting columns**:
```csharp
var lines = File.OpenText(path)
    .ReadLines()
    .Select(x => x.ParseInts())
    .ToList();
```

**Transposing data** (columns to rows):
```csharp
var transposed = data
    .Aggregate(
        new List<List<T>>(),
        (acc, row) =>
        {
            var i = 0;
            foreach (var cell in row)
            {
                if (acc.Count <= i) acc.Add([]);
                acc[i++].Add(cell);
            }
            return acc;
        });
```

**Converting to tuples**:
```csharp
(IEnumerable<int> left, IEnumerable<int> right) = list switch
{
    [IEnumerable<int> a, IEnumerable<int> b] => (a, b),
    _ => throw new ArgumentException()
};
```

### Performance Considerations

For Advent of Code:
- Readability > Performance (unless solution is too slow)
- Use `.ToList()` when you need multiple enumeration
- `OrderBy()` is fine for most cases
- Consider `Dictionary` or `HashSet` for lookups if needed

### Building and Running

**Build**:
```bash
cd src/c#/AdventOfCode
dotnet build
```

**Run**:
```bash
dotnet run
```

**Clean**:
```bash
dotnet clean
```

### Project File

The `AdventOfCode.csproj` uses:
- OutputType: Exe
- TargetFramework: net8.0 or later
- Nullable: enable
- ImplicitUsings: enable

### Testing Solutions

1. Run the program: `dotnet run`
2. Select your day from the menu
3. Compare output with Advent of Code expected answer
4. Press Enter to return to menu

### Input Files

- Location: `src/c#/AdventOfCode/inputs/`
- Naming: `day1.txt`, `day2.txt`, etc. (no leading zeros)
- Encoding: UTF-8
- **NOT** committed to source control (`.gitignore`)
- Download from your personal Advent of Code account

## Anti-Patterns to Avoid

1. **Don't** use absolute paths:
```csharp
// BAD
var input = File.OpenText(@"C:\Users\...\inputs\day1.txt");

// GOOD
var input = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\day1.txt");
```

2. **Don't** make helper methods public:
```csharp
// BAD
public static int CalcDistance(...)

// GOOD
private static int CalcDistance(...)
```

3. **Don't** mix concerns in the Run method:
```csharp
// BAD - logic in Run()
public static void Run()
{
    var input = File.OpenText(...).ReadLines();
    var result = 0;
    foreach (var line in input) { /* complex logic */ }
    Console.WriteLine(result);
}

// GOOD - delegate to helper methods
public static void Run()
{
    var input = File.OpenText(...).ReadLines();
    Console.WriteLine("Result: {0}", Solve(input));
}

private static int Solve(IEnumerable<string> input)
{
    // Complex logic here
}
```

4. **Don't** forget to update the menu in Program.cs when adding a new day

## Quick Reference

**Create new day**:
1. Copy `Day1.cs` → `DayX.cs`
2. Rename class to `DayX`
3. Update input file path to `dayX.txt`
4. Add to `Program.cs` menu
5. Create `inputs/dayX.txt`
6. Implement solution

**Common imports**:
```csharp
using System.Text.RegularExpressions;  // If needed for custom parsing
```

**Utility methods available**:
- `ReadLines()` - on TextReader
- `ParseInts()` - on string
- `ParseLongs()` - on string
