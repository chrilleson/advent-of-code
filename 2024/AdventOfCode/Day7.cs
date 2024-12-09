namespace AdventOfCode;

public static class Day7
{
    public static void Run()
    {
        var calibrations = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\day7.txt").ReadCalibrations().ToList();

        var calibrationResults = calibrations
            .Where(calibration => calibration.CanCreateTarget(Add, Multiply))
            .Sum(x => x.target);

        var concatenatedResults = calibrations
            .Where(calibration => calibration.CanCreateTarget(Add, Multiply, Concatenate))
            .Sum(x => x.target);

        Console.WriteLine("The sum of the calibrations is {0}", calibrationResults);
        Console.WriteLine("The sum of the concatenated calibrations is {0}", concatenatedResults);
        return;

        static IEnumerable<long> Add(long left, long right, long target) =>
            target - left >= right ? new[] { left + right } : [];

        static IEnumerable<long> Multiply(long left, long right, long target) =>
            target / left >= right ? new[] { left * right } : [];

        static IEnumerable<long> Concatenate(long left, long right, long target) =>
            long.TryParse(left + right.ToString(), out var result) &&
            result <= target
                ? new[] { result }
                : [];
    }

    private static IEnumerable<(long target, long first, IEnumerable<long> values)> ReadCalibrations(this TextReader reader) =>
        reader
            .ReadLines()
            .Select(x => x.ParseLongs())
            .Select(x => (x.First(), x.Skip(1).First(), x.Skip(2)));

    private static bool CanCreateTarget(this (long target, long first, IEnumerable<long> values) calibration, params Func<long, long, long, IEnumerable<long>>[] operations)
    {
        var created = new HashSet<long> { calibration.first };

        foreach (var value in calibration.values)
        {
            var result = created.SelectMany(result =>
                operations.SelectMany(operation =>
                    operation(result, value, calibration.target)));
            created = [..result];
        }

        return created.Contains(calibration.target);
    }
}