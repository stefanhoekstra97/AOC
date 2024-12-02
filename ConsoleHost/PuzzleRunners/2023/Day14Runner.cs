using Infrastructure;
using Infrastructure.Readers;
using PuzzleSolving._2023._14_ParabolicReflector;


namespace ConsoleHost.PuzzleRunners;

public static class Day14Runner
{
    public static async Task RunSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 14);
        var result = ParabolicReflector.SolvePartOne(input);
        Console.WriteLine($"Sample part one: {result}");
        var result2 = ParabolicReflector.SolvePartTwo(input);
        Console.WriteLine($"Sample part two: {result2}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 14);
        var result = ParabolicReflector.SolvePartOne(input);
        Console.WriteLine($"Puzzle one: {result}");
    }
    
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 14);
        var result = ParabolicReflector.SolvePartTwo(input);
        Console.WriteLine($"Puzzle two: {result}");
    }
    
}