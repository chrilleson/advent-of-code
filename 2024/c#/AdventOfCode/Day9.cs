namespace AdventOfCode;

public static class Day9
{
    public static void Run()
    {
        var disk = File.OpenText($@"{AppContext.BaseDirectory}\inputs\day9.txt").ReadDisk().ToList();
        var checksum = disk.Compact(MoveBlocks).Sum(x => x.Checksum);
        var movedFileChecksum = disk.Compact(MoveFiles).Sum(x => x.Checksum);

        Console.WriteLine("The checksum of the disk is {0}", checksum);
        Console.WriteLine("The checksum of the disk after moving the file is {0}", movedFileChecksum);
    }

    private static int MoveBlocks(FileSection file) => 0;

    private static int MoveFiles(FileSection file) => file.Length;

    private static IEnumerable<FileSection> Compact(this IEnumerable<Fragment> fragments, Func<FileSection, int> constraint)
    {
        var files = fragments.OfType<FileSection>().OrderByDescending(x => x.Position);
        var gaps = fragments.OfType<Gap>().OrderBy(x => x.Position).ToList();

        foreach (var file in files)
        {
            var remainingGaps = new List<Gap>();
            var pendingBlocks = file.Length;

            foreach (var gap in gaps.Where(x => x.Position < file.Position))
            {
                var move = Math.Min(pendingBlocks, gap.Length);
                if (move < constraint(file))
                {
                    remainingGaps.Add(gap);
                    continue;
                }

                if (move > 0) yield return file with { Position = gap.Position, Length = move };
                pendingBlocks -= move;

                if (gap.Remove(move) is Gap remainder) remainingGaps.Add(remainder);
            }

            if (pendingBlocks > 0) yield return file with { Length = pendingBlocks };
            gaps = remainingGaps;
        }
    }

    private static IEnumerable<Fragment> ReadDisk(this TextReader reader)
    {
        var position = 0;
        foreach (var (fileId, blocks) in reader.ReadInput())
        {
            if (fileId.HasValue) yield return new FileSection(fileId.Value, position, blocks);
            else yield return new Gap(position, blocks);
            position += blocks;
        }
    }

    private static IEnumerable<(int? fileId, int blocks)> ReadInput(this TextReader reader) =>
        (reader.ReadLine() ?? string.Empty)
        .Select(x => x - '0')
        .Select((value, index) => (index % 2 == 0 ? (int?)(index / 2) : null, value));
}

internal record Fragment(int Position, int Length);

internal record FileSection(int FileId, int Position, int Length) : Fragment(Position, Length)
{
    public long Checksum => (long)FileId * Length * (2 * Position + Length - 1) / 2;
}

internal record Gap(int Position, int Length) : Fragment(Position, Length)
{
    public Gap? Remove(int blocks) =>
        blocks >= Length ? null : new Gap(Position + blocks, Length - blocks);
}