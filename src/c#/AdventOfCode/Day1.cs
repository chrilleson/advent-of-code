namespace AdventOfCode;

public static class Day1
{
    public static void Run()
    {
        var foo = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\day1.txt")
            .ReadLines()
            .Select(x => x.ParseInts())
            .Transpose()
            .ToPair();

        Console.WriteLine("Total distance: {0}", CalcDistance(foo.left, foo.right));
        Console.WriteLine("Similarity score: {0}", CalcSimilarity(foo.left, foo.right));
    }

    private static int CalcDistance(IEnumerable<int> list1, IEnumerable<int> list2)
    {
        var orderedList1 = list1.OrderBy(x => x).ToList();
        var orderedList2 = list2.OrderBy(x => x).ToList();

        return orderedList1
            .Select((t, i) => t - orderedList2[i])
            .Sum(result => result < 0 ? result * -1 : result);
    }

    private static int CalcSimilarity(IEnumerable<int> list1, IEnumerable<int> list2)
    {
        var total = 0;
        var orderedList1 = list1.OrderBy(x => x).ToList();
        var orderedList2 = list2.OrderBy(x => x).ToList();

        foreach (var number in orderedList1)
        {
            var occurrences = orderedList2
                .CountBy(i => i == number)
                .Where(x => x.Key)
                .Sum(x => x.Value);
            total += number * occurrences;
        }
        return total;
    }

    private static (IEnumerable<int> left, IEnumerable<int> right) ToPair<T>(this List<T> list) => list switch
    {
        [IEnumerable<int> a, IEnumerable<int> b] => (a, b),
        _ => throw new ArgumentException()
    };

    private static List<List<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> values) =>
        values.Aggregate(
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
}