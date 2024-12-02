using Infrastructure.Readers;
using PuzzleSolving._2023._13_Mirrors;

namespace ConsoleHost.PuzzleRunners._2023;

public static class Day13Runner
{
    public static async Task RunSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 13);
        var result = Mirrors.SolvePartOne(input);
        Console.WriteLine($"Sample part one: {result}");
        var result2 = Mirrors.SolvePartTwo(input);
        Console.WriteLine($"Sample part two: {result2}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 13);
        var result = Mirrors.SolvePartOne(input);
        Console.WriteLine($"Puzzle one: {result}");
    }
    
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 13);
        var result = Mirrors.SolvePartTwo(input);
        Console.WriteLine($"Puzzle two: {result}");
    }
    
}