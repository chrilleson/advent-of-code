
using System.Text;
using AdventOfCode;

Console.OutputEncoding = Encoding.UTF8;
Run();
return;

static IReadOnlyDictionary<int, Action> MenuItems() => new Dictionary<int, Action>
{
    { 1, Day1.Run },
    { 2, Day2.Run },
    { 3, Day3.Run },
    { 4, Day4.Run },
    { 5, Day5.Run },
    { 6, Day6.Run },
    { 7, Day7.Run },
    { 8, Day8.Run },
    { 9, Day9.Run },
};

static void Run()
{
    var menuItems = MenuItems();
    Console.Clear();
    Console.WriteLine("🎅Advent of Code 2024🎅{0}", Environment.NewLine);
    menuItems.ToList().ForEach(item => Console.WriteLine("{0} Day {0}", item.Key));
    Console.WriteLine("{0} Exit", menuItems.Count + 1);
    Console.WriteLine("> ");
    Console.SetCursorPosition(2, Console.CursorTop - 1);

    try
    {
        var key = int.Parse(Console.ReadLine()!);
        if (key == menuItems.Count + 1)
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
            return;
        }
        Console.Clear();
        var selected = menuItems[key];
        Console.WriteLine("🎅Advent of Code 2024🎅{0}Day {1}{0}", Environment.NewLine, key);
        selected();
        Console.ReadLine();
        Run();
    }
    catch (Exception ex)
    {
        Console.Clear();
        Console.WriteLine("{0}❌ {1}", Environment.NewLine, ex.Message);
        Console.ReadLine();
        Run();
    }
}