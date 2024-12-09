namespace AdventOfCode;

public static class Day4
{
    public static void Run()
    {
        var lines = File.ReadLines($@"{AppContext.BaseDirectory}\inputs\Day4.txt").ToArray();
        var rows = lines.Length;
        var cols = lines[0].Length;
        var grid = new char[rows, cols];

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }

        var word = grid.SearchWord("XMAS");
        var pattern = grid.SearchPattern();

        Console.WriteLine("Word found {0} times.", word);
        Console.WriteLine("Pattern found {0} times.", pattern);
    }

    private static int SearchWord(this char[,] grid, string word)
    {
        var count = 0;
        var directions = new List<int[]>
        {
            new[] { 0, 1 }, new[] { 1, 0 }, new[] { 1, 1 }, new[] { 1, -1 },
            new[] { 0, -1 }, new[] { -1, 0 }, new[] { -1, -1 }, new[] { -1, 1 }
        };

        for (var row = 0; row < grid.GetLength(0); row++)
        {
            for (var col = 0; col < grid.GetLength(1); col++)
            {
                count += directions.Count(direction => grid.CheckWord(word, row, col, direction[0], direction[1]));
            }
        }

        return count;
    }

    private static int SearchPattern(this char[,] grid)
    {
        var count = 0;

        for (var row = 0; row < grid.GetLength(0); row++)
        {
            for (var col = 0; col < grid.GetLength(1); col++)
            {
                if (grid.CheckPattern(row, col)) count++;
            }
        }

        return count;
    }

    private static bool CheckWord(this char[,] grid, string word, int startRow, int startCol, int rowDir, int colDir)
    {
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);
        var wordLength = word.Length;

        for (var i = 0; i < wordLength; i++)
        {
            var newRow = startRow + i * rowDir;
            var newCol = startCol + i * colDir;

            if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols || grid[newRow, newCol] != word[i])
            {
                return false;
            }
        }

        return true;
    }

    private static bool CheckPattern(this char[,] grid, int startRow, int startCol)
    {
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);

        if (startRow < 0 || startRow >= rows || startCol < 0 || startCol >= cols || grid[startRow, startCol] != 'A')
        {
            return false;
        }

        var topRight = startRow > 0 && startCol < cols - 1 ? grid[startRow - 1, startCol + 1] : '.';
        var topLeft = startRow > 0 && startCol > 0 ? grid[startRow - 1, startCol - 1] : '.';
        var bottomRight = startRow < rows - 1 && startCol < cols - 1 ? grid[startRow + 1, startCol + 1] : '.';
        var bottomLeft = startRow < rows - 1 && startCol > 0 ? grid[startRow + 1, startCol - 1] : '.';

        var match1 = topRight == 'M' && bottomLeft == 'S';
        var match2 = topLeft == 'M' && bottomRight == 'S';
        var match3 = bottomLeft == 'M' && topRight == 'S';
        var match4 = bottomRight == 'M' && topLeft == 'S';

        return (match1 && match2) || (match1 && match3) || (match1 && match4) ||
               (match2 && match3) || (match2 && match4) || (match3 && match4);
    }
}