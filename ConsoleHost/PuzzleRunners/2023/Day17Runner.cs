using Infrastructure;
using Infrastructure.Readers;
using PuzzleSolving._2023._17_ClumsyCrucible;

namespace ConsoleHost.PuzzleRunners;

public static class Day17Runner
{
    public static async Task RunSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 17);
        
        // var result = ClumsyCrucible.SolvePartOne(input);
        // Console.WriteLine($"Sample part one: {result}");
        //
        var result2 = ClumsyCrucible.SolvePartTwo(input);
        Console.WriteLine($"Sample part two: {result2}");
    }
    
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 17);
        var result = ClumsyCrucible.SolvePartOne(input);
        Console.WriteLine($"Puzzle one: {result}");
    }
    
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 17);
        var result = ClumsyCrucible.SolvePartTwo(input);
        Console.WriteLine($"Puzzle two: {result}");
    }
    
}