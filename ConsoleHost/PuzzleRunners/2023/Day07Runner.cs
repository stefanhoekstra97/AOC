using Infrastructure.Readers;
using PuzzleSolving._2023._07_CamelCards;

namespace ConsoleHost.PuzzleRunners._2023;

public static class Day07Runner
{
    public static async Task RunSampleOne()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 7);

        var result = CamelCards.SolvePartOne(input);
        Console.WriteLine($"Part one result: {result}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 7);

        var result = CamelCards.SolvePartOne(input);
        Console.WriteLine($"Part one result: {result}");
    }
    
    public static async Task RunSampleTwo()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 7);

        var result = CamelCards.SolvePartTwo(input);
        Console.WriteLine($"Part two result: {result}");
    }
    
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 7);

        var result = CamelCards.SolvePartTwo(input);
        Console.WriteLine($"Part two result: {result}");
    }
}