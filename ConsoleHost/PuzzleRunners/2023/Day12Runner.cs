using Infrastructure.Readers;
using PuzzleSolving._2023._12_HotSprings;

namespace ConsoleHost.PuzzleRunners._2023;

public static class Day12Runner
{
    public static async Task RunSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 12);
        // var result = HotSprings.SolvePartOne(input);
        // Console.WriteLine($"Sample part one: {result}");
        var result2 = HotSprings.SolvePartTwo(input);
        Console.WriteLine($"Sample part two: {result2}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 12);
        var result = HotSprings.SolvePartOne(input);
        Console.WriteLine($"Puzzle one: {result}");
    }
    
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 12);
        var result = HotSprings.SolvePartTwo(input);
        Console.WriteLine($"Puzzle two: {result}");
    }
    
}