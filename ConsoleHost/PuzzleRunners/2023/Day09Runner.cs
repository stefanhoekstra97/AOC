using Infrastructure;
using Infrastructure.Readers;
using PuzzleSolving._2023._09_OasisStatus;

namespace ConsoleHost.PuzzleRunners;

public static class Day09Runner
{
    public static async Task RunSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 9);

        var result = OasisStatus.SolvePartOne(input);
        Console.WriteLine($"Sample result: {result}");
        
        var result2 = OasisStatus.SolvePartTwo(input);
        Console.WriteLine($"Sample result: {result2}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 9);
        var result = OasisStatus.SolvePartOne(input);
        Console.WriteLine($"Part one result: {result}");
    }
    
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 9);
        var result = OasisStatus.SolvePartTwo(input);
        Console.WriteLine($"Part one result: {result}");
    }
}