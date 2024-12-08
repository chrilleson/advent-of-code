
using System.Text.RegularExpressions;

var pagesToProduce = File.OpenText(@"..\..\..\pagestoproduce.txt").ReadPagesToProduce().ToList();
var orderingRules = File.OpenText(@"..\..\..\pageorderingrules.txt").ReadOrderingRules().ToHashSet();
var comparer = Comparer<int>.Create((x, y) =>
    orderingRules.Contains((x, y))
        ? -1
        : orderingRules.Contains((y, x))
            ? 1
            : 0);

var middlePageSum = pagesToProduce
    .Where(pages => pages.ToList().IsSorted(comparer))
    .Sum(pages => pages.MiddlePage());

var correctedMiddlePageSum = pagesToProduce
    .Where(pages => pages.ToList().IsSorted(comparer) is false)
    .Sum(pages => pages.Order(comparer).MiddlePage());

Console.WriteLine("Middle page sum: {0}", middlePageSum);
Console.WriteLine("Corrected middle page sum: {0}", correctedMiddlePageSum);

internal static class Extensions
{
    public static IEnumerable<(int before, int after)> ReadOrderingRules(this TextReader text) =>
        text.ReadLines().TakeWhile(line => string.IsNullOrWhiteSpace(line) is false).Select(ToSortOrder);

    public static IEnumerable<IEnumerable<int>> ReadPagesToProduce(this TextReader text) =>
        text.ReadLines().Select(ParseInt);

    public static bool IsSorted(this List<int> pages, IComparer<int> comparer) =>
        pages.SelectMany((prev, index) => pages[(index + 1)..].Select(next => (prev, next)))
            .All(pair => comparer.Compare(pair.prev, pair.next) <= 0);

    public static int MiddlePage(this IEnumerable<int> pages)
    {
        using var half = pages.GetEnumerator();
        using var full = pages.GetEnumerator();

        while (full.MoveNext() && half.MoveNext() && full.MoveNext()) { }

        return half.Current;
    }

    private static (int, int) ToSortOrder(this string line)
    {
        var parts = line.Split('|');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }

    private static IEnumerable<string> ReadLines(this TextReader text)
    {
        while (text.ReadLine() is string line)
        {
            yield return line;
        }
    }

    private static IEnumerable<int> ParseInt(this string line) =>
        Regex.Matches(line, @"\d+").Select(match => int.Parse(match.Value));
}