using Infrastructure.Readers;
using PuzzleSolving._2023._15_LensLibrary;

namespace ConsoleHost.PuzzleRunners._2023;

public static class Day15Runner
{
    public static async Task RunSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 15);
        var result = LensLibrary.SolvePartOne(input);
        Console.WriteLine($"Sample part one: {result}");
        var result2 = LensLibrary.SolvePartTwo(input);
        Console.WriteLine($"Sample part two: {result2}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 15);
        var result = LensLibrary.SolvePartOne(input);
        Console.WriteLine($"Puzzle one: {result}");
    }
    
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 15);
        var result = LensLibrary.SolvePartTwo(input);
        Console.WriteLine($"Puzzle two: {result}");
    }
    
}