
using System.Text;
using AdventOfCode;

Console.OutputEncoding = Encoding.UTF8;
Application.Run();


internal abstract class Application
{
    private static readonly IReadOnlyDictionary<int, Action> MenuItems = new Dictionary<int, Action>
    {
        { 1, Day1.Run },
        { 2, Day2.Run },
        { 3, Day3.Run },
    };

    public static void Run()
    {
        Console.Clear();
        Console.WriteLine("🎅Advent of Code 2024🎅{0}", Environment.NewLine);
        MenuItems.ToList().ForEach(item => Console.WriteLine("{0} Day {0}", item.Key));
        Console.WriteLine("{0} Exit", MenuItems.Count + 1);
        Console.WriteLine("> ");
        Console.SetCursorPosition(2, Console.CursorTop - 1);

        try
        {
            var key = int.Parse(Console.ReadLine() ?? string.Empty);
            if (key == MenuItems.Count + 1)
            {
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
                return;
            }
            Console.Clear();
            var selected = MenuItems[key];
            Console.WriteLine("🎅Advent of Code 2024🎅{0}Day {1}{0}", Environment.NewLine, key);
            selected();
            Console.ReadLine();
            Run();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("{0}❌ {1}", Environment.NewLine, ex.Message);
        }
    }
}