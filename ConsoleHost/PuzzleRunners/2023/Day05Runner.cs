using Infrastructure;
using Infrastructure.Readers;
using PuzzleSolving._2023._05_Seeds;

namespace ConsoleHost.PuzzleRunners;

public static class Day05Runner
{
    public static async Task RunSamplePartOne()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 5);

        var result = SeedPlanter.SolvePartOne(input);

        Console.WriteLine($"Part one output: {result}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 5);

        var result = SeedPlanter.SolvePartOne(input);

        Console.WriteLine($"Part one output: {result}");
    }
    
    public static async Task RunSamplePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 5);

        var result = SeedPlanter.SolvePartTwo(input);

        Console.WriteLine($"Part two output: {result}");
    }
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 5);

        var result = SeedPlanter.SolvePartTwo(input);

        Console.WriteLine($"Part two output: {result}");
    }
}