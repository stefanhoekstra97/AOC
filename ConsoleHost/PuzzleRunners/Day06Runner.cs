using Infrastructure;
using PuzzleSolving._06_Boat;

namespace ConsoleHost.PuzzleRunners;

public static class Day06Runner
{
    public static async Task RunSampleOne()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 6);
        var result = BoatGame.SolvePartOne(input);
        Console.WriteLine($"result for part one: {result}");
    }
    
    public static async Task RunSampleTwo()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 6);
        var result = BoatGame.SolvePartTwo(input);
        Console.WriteLine($"result for part TWO: {result}");
    }
    
    public static async Task RunPuzzleOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 6);
        var result = BoatGame.SolvePartOne(input);
        Console.WriteLine($"result for part one: {result}");
    }
    
    public static async Task RunPuzzleTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 6);
        var result = BoatGame.SolvePartTwo(input);
        Console.WriteLine($"result for part TWO: {result}");
    }
}