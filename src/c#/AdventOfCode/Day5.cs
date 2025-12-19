namespace AdventOfCode;

public static class Day5
{
    public static void Run()
    {
        using var input = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\day5.txt");
        var orderingRules = input.ReadOrderingRules().ToHashSet();
        var pagesToProduce = input.ReadPagesToProduce().ToList();
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
    }

    private static IEnumerable<(int before, int after)> ReadOrderingRules(this TextReader text) =>
        text.ReadLines().TakeWhile(line => !string.IsNullOrWhiteSpace(line)).Select(ToSortOrder);

    private static IEnumerable<IEnumerable<int>> ReadPagesToProduce(this TextReader text) =>
        text.ReadLines().Select(x => x.ParseInts());

    private static bool IsSorted(this List<int> pages, IComparer<int> comparer) =>
        pages.SelectMany((prev, index) => pages[(index + 1)..].Select(next => (prev, next)))
            .All(pair => comparer.Compare(pair.prev, pair.next) <= 0);

    private static int MiddlePage(this IEnumerable<int> pages)
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
}