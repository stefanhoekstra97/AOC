using Infrastructure;
using PuzzleSolving._10_PipeMaze;

namespace ConsoleHost.PuzzleRunners;

public static class Day10Runner
{
    public static async Task RunSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 10);
        var result = PipeMaze.SolvePartOne(input);
        Console.WriteLine($"Puzzle sample: {result}");
        
        var input2 = await ReadFile.ReadFromDisk("second_sample_input", 10);
        var result2 = PipeMaze.SolvePartOne(input2);
        Console.WriteLine($"Second puzzle sample: {result2}");
    }

    public static async Task RunSamplesPartTwo()
    {
        var input1 = await ReadFile.ReadFromDisk("sample_input", 10);
        var result1 = PipeMaze.SolvePartTwo(input1);
        Console.WriteLine($"Sample one: {result1}");
        
        var input2 = await ReadFile.ReadFromDisk("second_sample_input", 10);
        var result2 = PipeMaze.SolvePartTwo(input2);
        Console.WriteLine($"Sample two: {result2}");
        
        
        var input3 = await ReadFile.ReadFromDisk("third_sample_input", 10);
        var result3 = PipeMaze.SolvePartTwo(input3);
        Console.WriteLine($"Sample three: {result3}");
        
        var input4 = await ReadFile.ReadFromDisk("fourth_sample_input", 10);
        var result4 = PipeMaze.SolvePartTwo(input4);
        Console.WriteLine($"Sample four: {result4}");
        
        var input5 = await ReadFile.ReadFromDisk("fifth_sample_input", 10);
        var result5 = PipeMaze.SolvePartTwo(input5);
        Console.WriteLine($"Sample five: {result5}");
    }
    public static async Task RunPuzzlePartOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 10);

        var result = PipeMaze.SolvePartOne(input);
        Console.WriteLine($"Puzzle part one: {result}");
    }
    
    public static async Task RunPuzzlePartTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 10);

        var result = PipeMaze.SolvePartTwo(input);
        Console.WriteLine($"Puzzle part two: {result}");
    }
}