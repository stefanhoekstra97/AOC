using Infrastructure;
using PuzzleSolving._07_CamelCards;

namespace ConsoleHost.PuzzleRunners;

public static class Day07Runner
{
    public static async Task RunSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 7);

        var result = CamelCards.SolvePartOne(input);
        Console.WriteLine($"Part one result: {result}");
    }
    
    public static async Task RunExtraSample()
    {
        var input = await ReadFile.ReadFromDisk("extra_sample_input", 7);

        var result = CamelCards.SolvePartOne(input);
        Console.WriteLine($"Part one result: {result}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 7);

        var result = CamelCards.SolvePartOne(input);
        Console.WriteLine($"Part one result: {result}");
    }
}