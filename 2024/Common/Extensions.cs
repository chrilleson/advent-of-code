using System.Text.RegularExpressions;

namespace Common;

public static class Extensions
{
    public static IEnumerable<string> ReadLines(this TextReader text)
    {
        while (text.ReadLine() is string line)
        {
            yield return line;
        }
    }

    public static IEnumerable<int> ParseInts(this string line) =>
        Regex.Matches(line, @"\d+").Select(match => int.Parse(match.Value));
}