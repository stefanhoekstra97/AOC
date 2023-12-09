using Infrastructure;
using PuzzleSolving._08_HauntedWasteland;

namespace ConsoleHost.PuzzleRunners;

public static class Day08Runner
{
    public static async Task RunSample()
    {
        var input = await ReadFile.ReadFromDisk("sample_input", 8);
        var secondInput = await ReadFile.ReadFromDisk("second_sample_input", 8);
        var inputPartTwo = await ReadFile.ReadFromDisk("sample_input_part_two", 8);

        var result = HauntedWasteland.SolvePartOne(input);
        Console.WriteLine(result);

        var secondResult = HauntedWasteland.SolvePartOne(secondInput);
        Console.WriteLine(secondResult);
        
        var partTwo = HauntedWasteland.SolvePartTwo(inputPartTwo);
        Console.WriteLine(partTwo);
    }
    
    public static async Task RunPuzzleOne()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 8);

        var result = HauntedWasteland.SolvePartOne(input);
        Console.WriteLine($"Part one: {result}");
    }
    
    public static async Task RunPuzzleTwo()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 8);

        var result = HauntedWasteland.SolvePartTwo(input);
        Console.WriteLine($"Part two: {result}");
    }
    
    public static async Task RunPuzzleOneAlt()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 8);

        var resultOne = HauntedWasteland.SolvePartOneAlternative(input);
        
        Console.WriteLine($"Part two: {resultOne}");
    }
    
    public static async Task RunPuzzleTwoAlt()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 8);
        var result = HauntedWasteland.SolvePartTwoAlternative(input);
        Console.WriteLine($"Part two: {result}");
    }
}