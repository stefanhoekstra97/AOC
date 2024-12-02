using Infrastructure.Readers;
using PuzzleSolving._2023._18_LavaductLagoon;

namespace ConsoleHost.PuzzleRunners._2023;

public static class Day18Runner
{
    public static async Task RunSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 18);
        var result = LavaductLagoon.SolvePartOne(input, true);
        Console.WriteLine($"Sample part one: {result}");
        var result2 = LavaductLagoon.SolvePartTwo(input);
        Console.WriteLine($"Sample part two: {result2}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 18);
        var result = LavaductLagoon.SolvePartOne(input, true);
        Console.WriteLine($"Puzzle one: {result}");
    }
    
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 18);
        var result = LavaductLagoon.SolvePartTwo(input);
        Console.WriteLine($"Puzzle two: {result}");
    }
    
}