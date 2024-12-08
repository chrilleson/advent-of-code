using System.Text.RegularExpressions;

namespace AdventOfCode;

public static class Day3
{
    public static void Run()
    {

        var instructions = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\day3.txt")
            .ReadLines()
            .MulCalculator()
            .Sum();

        Console.WriteLine("Sum of mul: {0}", instructions);
    }

    private static IEnumerable<int> MulCalculator(this IEnumerable<string> instructions)
    {
        var mulRegex = new Regex(@"mul\(\d{1,3},\d{1,3}\)");

        var matches = Regex.Matches(string.Join("", instructions), @"do\(\)|don't\(\)|mul\(\d{1,3},\d{1,3}\)");
        var currentOperator = string.Empty;

        foreach (Match match in matches)
        {
            currentOperator = match.Value switch
            {
                "do()" => "do",
                "don't()" => "don't",
                _ => currentOperator
            };

            if (currentOperator == "don't" || (currentOperator is "do" && mulRegex.IsMatch(match.Value) is false))
            {
                continue;
            }

            var (x, y) = ParseMul(match.Value);
            yield return x * y;
        }
    }

    private static (int x, int y) ParseMul(string mul)
    {
        var values = mul.Replace("mul(", "").Replace(")", "").Split(",");
        return (int.Parse(values[0]), int.Parse(values[1]));
    }
}