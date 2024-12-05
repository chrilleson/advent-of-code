
var pageOrderingRules = ParsePageOrderingRules();
var pagesToProduce = ParsePagesToProduce(pageOrderingRules);

pageOrderingRules =
[
    new PageOrderingRules(47, 53),
    new PageOrderingRules(97, 13),
    new PageOrderingRules(97, 61),
    new PageOrderingRules(97, 47),
    new PageOrderingRules(75, 29),
    new PageOrderingRules(61, 13),
    new PageOrderingRules(75, 53),
    new PageOrderingRules(29, 13),
    new PageOrderingRules(97, 29),
    new PageOrderingRules(53, 29),
    new PageOrderingRules(61, 53),
    new PageOrderingRules(97, 53),
    new PageOrderingRules(61, 29),
    new PageOrderingRules(47, 13),
    new PageOrderingRules(75, 47),
    new PageOrderingRules(97, 75),
    new PageOrderingRules(47, 61),
    new PageOrderingRules(75, 61),
    new PageOrderingRules(47, 29),
    new PageOrderingRules(75, 13),
    new PageOrderingRules(53, 13),
];

pagesToProduce =
[
    new PagesToProduce([75,47,61,53,29], pageOrderingRules),
    new PagesToProduce([97,61,53,29,13], pageOrderingRules),
    new PagesToProduce([75,29,13], pageOrderingRules),
    new PagesToProduce([75,97,47,61,53], pageOrderingRules),
    new PagesToProduce([61,13,29], pageOrderingRules),
    new PagesToProduce([97,13,75,29,47], pageOrderingRules),
];

var validPagesToProduce = pagesToProduce.Where(x => x.IsCorrectOrdered()).ToList();
Console.WriteLine("Number of correct ordered pages: {0}", validPagesToProduce.Count);
// Console.WriteLine("Correct ordered pages: {0}", string.Join(Environment.NewLine, validPagesToProduce.Select(x => string.Join(",", x.PageNumbers))));
Console.WriteLine("Middle page number of correct ordered pages: {0}", validPagesToProduce.Sum(MiddlePageNumber));
// Console.WriteLine("Middle page number of all pages: {0}", string.Join(Environment.NewLine, validPagesToProduce.Select(MiddlePageNumber)));
var correctedPages = SetToCorrectOrder(pagesToProduce);
Console.WriteLine("Number of corrected pages: {0}", correctedPages.Count());
return;

static IEnumerable<PageOrderingRules> ParsePageOrderingRules()
{
    var pageOrderingRules = new List<PageOrderingRules>();
    var lines = File.ReadAllLines(@"..\..\..\PageOrderingRules.csv");
    foreach (var line in lines)
    {
        var rules = line
            .Split(',')
            .SelectMany(x => x.Split('|', StringSplitOptions.RemoveEmptyEntries))
            .Select(int.Parse);
        pageOrderingRules.Add(new PageOrderingRules(rules.First(), rules.Last()));
    }

    return pageOrderingRules;
}

static IEnumerable<PagesToProduce> ParsePagesToProduce(IEnumerable<PageOrderingRules> pageOrderingRules)
{
    var pagesToProduce = new List<PagesToProduce>();
    var lines = File.ReadAllLines(@"..\..\..\PagesToProduce.csv");
    foreach (var line in lines)
    {
        var pageNumbers = line
            .Replace("\"", string.Empty)
            .Split(',')
            .SelectMany(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(int.Parse);
        pagesToProduce.Add(new PagesToProduce(pageNumbers, pageOrderingRules));
    }

    return pagesToProduce;
}

static int MiddlePageNumber(PagesToProduce pagesToProduce)
{
    var pageNumbersList = pagesToProduce.PageNumbers.ToList();
    var lastIndex = pageNumbersList.Count;
    return pageNumbersList[lastIndex / 2];
}

static IEnumerable<PagesToProduce> SetToCorrectOrder(IEnumerable<PagesToProduce> pagesToProduce)
{
    return pagesToProduce.Select(x => x.SetToCorrectOrder());
}

internal record PageOrderingRules(int X, int Y);

internal record PagesToProduce(IEnumerable<int> PageNumbers, IEnumerable<PageOrderingRules> OrderingRules)
{
    public bool IsCorrectOrdered()
    {
        var pageNumbersList = PageNumbers.ToList();
        var orderingRulesList = OrderingRules.ToList();
        foreach (var pageNumber in pageNumbersList)
        {
            var xRules = orderingRulesList.Where(rule => rule.X == pageNumber).Select(x => x).ToList();
            var yRules = orderingRulesList.Where(rule => rule.Y == pageNumber).Select(x => x).ToList();

            if (!xRules.Any() && !yRules.Any())
            {
                continue;
            }
            var yRulesCorrect = yRules.All(rule => YRulesCorrect(pageNumber, pageNumbersList, rule));
            var xRulesCorrect = xRules.All(rule => XRulesCorrect(pageNumber, pageNumbersList, rule));

            if (yRulesCorrect is false || xRulesCorrect is false)
            {
                return false;
            }
        }
        return true;
    }

    public PagesToProduce SetToCorrectOrder()
    {
        var pageNumbersList = PageNumbers.ToList();
        var orderingRulesList = OrderingRules.ToList();
        foreach (var pageNumber in pageNumbersList)
        {
            var xRules = orderingRulesList.Where(rule => rule.X == pageNumber).Select(x => x).ToList();
            var yRules = orderingRulesList.Where(rule => rule.Y == pageNumber).Select(x => x).ToList();

            if (!xRules.Any() && !yRules.Any())
            {
                continue;
            }
            var yRulesCorrect = yRules.All(rule => YRulesCorrect(pageNumber, pageNumbersList, rule));
            var xRulesCorrect = xRules.All(rule => XRulesCorrect(pageNumber, pageNumbersList, rule));

            if (yRulesCorrect is false || xRulesCorrect is false)
            {
                var indexOfY = pageNumbersList.IndexOf(yRules.First().Y);
                var indexOfX = pageNumbersList.IndexOf(xRules.First().X);
                var temp = pageNumbersList[indexOfY];
                pageNumbersList[indexOfY] = pageNumbersList[indexOfX];
                pageNumbersList[indexOfX] = temp;
            }
        }
        return new PagesToProduce(pageNumbersList, OrderingRules);
    }

    private static bool YRulesCorrect(int pageNumber, IEnumerable<int> pageNumbers, PageOrderingRules rule)
    {
        var pageNumberIndex = pageNumbers.ToList().IndexOf(pageNumber);
        var indexOfX = pageNumbers.ToList().IndexOf(rule.X);
        return indexOfX < 0 || indexOfX < pageNumberIndex;
    }

    private static bool XRulesCorrect(int pageNumber, IEnumerable<int> pageNumbers, PageOrderingRules rule)
    {
        var pageNumberIndex = pageNumbers.ToList().IndexOf(pageNumber);
        var indexOfY = pageNumbers.ToList().IndexOf(rule.Y);
        return indexOfY < 0 || pageNumberIndex < indexOfY;
    }
}