using System.Text.RegularExpressions;

var instructions = File.ReadAllText(@"..\..\..\CorruptedMemory.txt");
var foo = MulCalculator(instructions).Sum();

Console.WriteLine("Sum of mul: {0}", foo);
return;

IEnumerable<int> MulCalculator(string instructions)
{
    var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)");
    var validMul = regex.Matches(instructions);
    foreach (var value in validMul)
    {
        var (x, y) = ParseMul(value.ToString());
        yield return x * y;
    }
}

(int x, int y) ParseMul(string mul)
{
    var values = mul.Replace("mul(", "").Replace(")", "").Split(",");
    return (int.Parse(values[0]), int.Parse(values[1]));
}