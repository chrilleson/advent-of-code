namespace AdventOfCode;

public static class Day6
{
    private const string Orientations = "^>v<";

    public static void Run()
    {
        var map = File.OpenText(@$"{AppContext.BaseDirectory}\inputs\day6.txt")
            .ReadLines()
            .Select(x => x.ToCharArray())
            .ToArray();

        var steps = map
            .Path()
            .Select(x => (x.row, x.col))
            .Distinct()
            .Count();

        var startingPosition = map.StartingPosition();
        var obstructionPointsCount = map
            .Path()
            .Select(x => (x.row, x.col))
            .Where(coord => coord != (startingPosition.row, startingPosition.col))
            .Distinct()
            .Count(x => map.WhatIf(x, map => map.ContainsLoop()));

        Console.WriteLine("Steps count: {0}", steps);
        Console.WriteLine("Obstruction points count: {0}", obstructionPointsCount);
    }

    private static IEnumerable<(int row, int col, char orientation)> Path(this char[][] map)
    {
        var state = map.StartingPosition();
        yield return state;

        var visited = new HashSet<(int row, int col, char orientation)>{ state };

        while (true)
        {
            state = state.orientation switch
            {
                '^' when map.ContainsObstacle(state.row - 1, state.col) =>
                    (state.row, state.col, '>'),
                '^' => (state.row - 1, state.col, state.orientation),
                '>' when map.ContainsObstacle(state.row, state.col + 1) =>
                    (state.row, state.col, 'v'),
                '>' => (state.row, state.col + 1, state.orientation),
                'v' when map.ContainsObstacle(state.row + 1, state.col) =>
                    (state.row, state.col, '<'),
                'v' => (state.row + 1, state.col, state.orientation),
                '<' when map.ContainsObstacle(state.row, state.col - 1) =>
                    (state.row, state.col, '^'),
                '<' => (state.row, state.col - 1, state.orientation),
                _ => throw new InvalidOperationException()
            };

            if (map.IsInside(state.row, state.col)) yield return state;
            else yield break;

            if (!visited.Add(state)) yield break;
        }
    }

    private static (int row, int col, char orientation) StartingPosition(this char[][] map) =>
        map
            .SelectMany((row, rowIndex) => row.Select((cell, colIndex) => (rowIndex, colIndex, cell)))
            .First(x => Orientations.Contains(x.cell));

    private static T WhatIf<T>(this char[][] map, (int row, int col) insertObstacle, Func<char[][], T> func)
    {
        var original = map[insertObstacle.row][insertObstacle.col];
        map[insertObstacle.row][insertObstacle.col] = '#';

        T result = func(map);

        map[insertObstacle.row][insertObstacle.col] = original;
        return result;
    }

    private static bool ContainsLoop(this char[][] map) =>
        map.Path().Count() != map.Path().Distinct().Count();

    private static bool ContainsObstacle(this char[][] map, int row, int col) =>
        map.IsInside(row, col) && map[row][col] == '#';

    private static bool IsInside(this char[][] map, int row, int col) =>
        row >= 0 && row < map.Length && col >= 0 && col < map[row].Length;
}