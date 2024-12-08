
using Common;

var map = File.OpenText(@"..\..\..\input.txt").ReadMap();

var antennas = map.GetAntennas();

var nonResonatingAntiNodes = antennas
    .SelectMany(x => x.GetAntiNodes(map, NonResonatingAntiNodes))
    .Distinct()
    .Count();

var resonatingAntiNodes = antennas
    .SelectMany(x => x.GetAntiNodes(map, ResonatingAntiNodes))
    .Distinct()
    .Count();

Console.WriteLine("Count of non-resonating anti-nodes: {0}", nonResonatingAntiNodes);
Console.WriteLine("Count of resonating anti-nodes: {0}", resonatingAntiNodes);
return;

static IEnumerable<Position> NonResonatingAntiNodes(char[][] map, Position antenna, int rowDiff, int colDiff)
{
    var position = new Position(antenna.Row + rowDiff, antenna.Column + colDiff);
    if (map.IsInside(position)) yield return position;
}

static IEnumerable<Position> ResonatingAntiNodes(char[][] map, Position antenna, int rowDiff, int colDiff)
{
    yield return antenna;
    var position = new Position(antenna.Row + rowDiff, antenna.Column + colDiff);
    while (map.IsInside(position))
    {
        yield return position;
        position = new Position(position.Row + rowDiff, position.Column + colDiff);
    }
}

internal record AntennaSet(IEnumerable<Position> Positions);
internal record Antenna(char Frequency, Position Position);
internal record Position(int Row, int Column);

internal static class Extensions
{
    public static char[][] ReadMap(this TextReader reader) =>
        reader.ReadLines().Select(line => line.ToCharArray()).ToArray();

    public static IEnumerable<AntennaSet> GetAntennas(this char[][] map) =>
        map.GetIndividualAntenna()
            .GroupBy(antenna => antenna.Frequency)
            .Select(group => new AntennaSet(group.Select(antenna => antenna.Position)));

    public static IEnumerable<Position> GetAntiNodes(this AntennaSet antennaSet, char[][] map, Func<char[][], Position, int, int, IEnumerable<Position>> foo) =>
        antennaSet.GetPositionPairs()
            .SelectMany(pair => map.GetAntiNodes(pair.antenna1, pair.antenna2, foo));

    public static bool IsInside(this char[][] map, Position position) =>
        position.Row >= 0 && position.Row < map.Length && position.Column >= 0 && position.Column < map[0].Length;

    private static IEnumerable<Antenna> GetIndividualAntenna(this char[][] map) =>
        map.SelectMany((row, rowIndex) =>
            row.Select((content, colIndex) => (content, rowIndex, colIndex))
                .Where(cell => cell.content != '.')
                .Select(cell => new Antenna(cell.content, new Position(cell.rowIndex, cell.colIndex))));

    private static IEnumerable<Position> GetAntiNodes(this char[][] map, Position antenna1, Position antenna2, Func<char[][], Position, int, int, IEnumerable<Position>> foo)
    {
        var rowDiff = antenna1.Row - antenna2.Row;
        var colDiff = antenna1.Column - antenna2.Column;

        return foo(map, antenna1, rowDiff, colDiff)
            .Concat(foo(map, antenna2, -rowDiff, -colDiff));
    }

    private static IEnumerable<(Position antenna1, Position antenna2)> GetPositionPairs(this AntennaSet antennaSet) =>
        antennaSet.Positions
            .SelectMany((pos1, index1) =>
                antennaSet.Positions.Skip(index1 + 1).Select(pos2 => (pos1, pos2)));
}