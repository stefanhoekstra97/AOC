using Infrastructure;
using PuzzleSolving._11_CosmicExpansion;

namespace ConsoleHost.PuzzleRunners;

public static class Day11Runner
{
    public static async Task RunSamples()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 11);
        var result = CosmicExpansion.SolvePuzzle(input);
        Console.WriteLine($"Sample ER=2 result: {result}");
        
        var result2 = CosmicExpansion.SolvePuzzle(input, expansionRate: 10);
        Console.WriteLine($"Sample ER=10 result: {result2}");
        
        var result3 = CosmicExpansion.SolvePuzzle(input, expansionRate: 100);
        Console.WriteLine($"Sample ER=100 result: {result3}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 11);
        var result = CosmicExpansion.SolvePuzzle(input);
        Console.WriteLine($"Puzzle part one result: {result}");
    }
    
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 11);
        var result = CosmicExpansion.SolvePuzzle(input, expansionRate: 1000000);
        Console.WriteLine($"Puzzle part two result: {result}");
    }
    
}