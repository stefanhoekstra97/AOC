using Infrastructure;
using PuzzleSolving._03_EngineParts;

namespace ConsoleHost.TestPuzzle;

public static class DayThree
{
    public static async Task TestSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 3);
        var solver = new EngineParts();

        var result = solver.SolveEasy(input);

        Console.WriteLine($"Result easy: {result}");

    }
    
    
    public static async Task TestSampleSecond()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 3);
        var solver = new EngineParts();

        var result = solver.SolveHard(input);

        Console.WriteLine($"Result hard: {result}");

    }
    
    public static async Task TestSecond()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input_mod", 3);
        var solver = new EngineParts();

        var result = solver.SolveHard(input);

        Console.WriteLine($"Result hard: {result}");

    }
    
    public static async Task TestEasy()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input_mod", 3);
        var solver = new EngineParts();

        var result = solver.SolveEasy(input);

        Console.WriteLine($"Result easy: {result}");

    }
}