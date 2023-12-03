using Infrastructure;
using PuzzleSolving._02_CubeGame;

namespace ConsoleHost.TestPuzzle;

public class TestDayTwo
{
    public static async Task TestEasySample()
    {
        var input = await ReadFile.ReadFromDisk("sample_easy", 2);
        var solver = new CubeGame();

        var result = solver.SolveEasy(input);
        Console.WriteLine($"Easy result: {result}");
    }
    
    public static async Task TestEasy()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 2);
        var solver = new CubeGame();

        var result = solver.SolveEasy(input);
        Console.WriteLine($"Easy result: {result}");
    }
    
    public static async Task TestEasyRegex()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 2);
        var solver = new CubeGame();

        var result = solver.SolveEasyRegex(input);
        Console.WriteLine($"Easy regexed result: {result}");
    }
    
    public static async Task TestHardSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_hard", 2);
        var solver = new CubeGame();

        var result = solver.SolveHard(input);
        Console.WriteLine($"Hard result: {result}");
    }
    
    public static async Task TestHard()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 2);
        var solver = new CubeGame();

        var result = solver.SolveHard(input);
        Console.WriteLine($"Hard result: {result}");
    }
}