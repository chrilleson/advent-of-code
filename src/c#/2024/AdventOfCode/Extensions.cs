using System.Text.RegularExpressions;

namespace AdventOfCode;

internal static class Extensions
{
    extension(TextReader text) {
        internal IEnumerable<string> ReadLines() {
            while (text.ReadLine() is string line) {
                yield return line;
            }
        }
    }

    extension(string str) {
        internal IEnumerable<int> ParseInts() =>
            Regex.Matches(str, @"\d+").Select(match => int.Parse(match.Value));
        
        internal IEnumerable<long> ParseLongs() =>
            Regex.Matches(str, @"\d+").Select(match => long.Parse(match.Value));

        internal string GetInputPath() =>
            Path.Combine(AppContext.BaseDirectory, "inputs", str + ".txt");  
    }
}