﻿
var reports = GetReports();

var safeReportsCount = reports.Count(report => report.IsSafe());
Console.WriteLine("Safe reports: {0}", safeReportsCount);

var safeReportsCountWithDampener = reports.Count(report => report.IsSafe().WithDampener(report));
Console.WriteLine("Safe reports with dampener: {0}", safeReportsCountWithDampener);
return;

static IEnumerable<Report> GetReports()
{
    var reports = new List<Report>();
    var lines = File.ReadAllLines(@"..\..\..\Reports.txt");

    foreach (var line in lines)
    {
        var levels = line
            .Split(',')
            .SelectMany(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(int.Parse);
        reports.Add(new Report(levels));
    }
    return reports;
}

internal record Report(IEnumerable<int> Levels)
{
    private static readonly Func<IEnumerable<int>, bool>[] Rules =
    [
        levels => IsAscending(levels) || IsDescending(levels),
        HasValidDifference
    ];

    public bool IsSafe() => Rules.All(rule => rule(Levels));

    private static bool IsAscending(IEnumerable<int> levels)
    {
        var list = levels.ToList();
        for (var i = 1; i < list.Count; i++)
        {
            if (list[i] <= list[i - 1])
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsDescending(IEnumerable<int> levels)
    {
        var list = levels.ToList();
        for (var i = 1; i < list.Count; i++)
        {
            if (list[i] >= list[i - 1])
            {
                return false;
            }
        }
        return true;
    }

    private static bool HasValidDifference(IEnumerable<int> levels)
    {
        var list = levels.ToList();
        for (var i = 1; i < list.Count; i++)
        {
            var difference = Math.Abs(list[i] - list[i - 1]);
            if (difference < 1 || difference > 3)
            {
                return false;
            }
        }
        return true;
    }
}

internal static class ReportExtensions
{
    public static bool WithDampener(this bool isSafe, Report report)
    {
        if (isSafe)
        {
            return true;
        }

        return report.Levels
            .Select((_, i) => report.Levels.Where((_, index) => index != i))
            .Select(modifiedLevels => new Report(modifiedLevels))
            .Any(modifiedReport => modifiedReport.IsSafe());
    }
}