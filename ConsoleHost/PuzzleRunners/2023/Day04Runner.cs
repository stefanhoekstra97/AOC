using Infrastructure.Readers;
using PuzzleSolving._2023._04_ScratchCards;

namespace ConsoleHost.PuzzleRunners._2023;

public static class Day04Runner
{
    public static async Task RunSampleInput()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 4);
        var result = ScratchCards.SolvePartOne(input);

        Console.WriteLine($"Part one result: {result}");
    }
    
    public static async Task RunPartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 4);
        var result = ScratchCards.SolvePartOne(input);

        Console.WriteLine($"Part one result: {result}");
    }
    
    public static async Task RunSampleInputPartTwo()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 4);
        var result = ScratchCards.SolvePartTwo(input);

        Console.WriteLine($"Part two result: {result}");
    }
    
    public static async Task RunPartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 4);
        var result = ScratchCards.SolvePartTwo(input);

        Console.WriteLine($": {result} : Part two result");
    }
    
    public static async Task RunPartTwoAlt()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 4);
        var result = ScratchCards.SolvePartTwoAlternative(input);

        Console.WriteLine($": {result} : Part two ALTERNATIVE result");
    }
}