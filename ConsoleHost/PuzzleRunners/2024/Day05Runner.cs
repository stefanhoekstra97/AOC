using PuzzleSolving._2024._04_CeresSearch;

namespace ConsoleHost.PuzzleRunners._2024;

using Infrastructure.Readers;
using PuzzleSolving._2024._05_PrintQueue;

public class Day05Runner
{
    public static async Task RunAll()
    {
        await RunSampleAsync();
        await RunSilverAsync();
        await RunGoldAsync();
    }
    
    public static async Task RunSampleAsync()
    {
        Console.WriteLine("Running PrintQueue sample...");
        var solver = new PrintQueue();
        var puzInput = await ReadFile.ReadFromDisk("sample", 5, 2024);
        var resultS = await solver.SolveSilver(puzInput);
        Console.WriteLine($"Got result: {resultS}");

        var resultG = await solver.SolveGold(puzInput);
        Console.WriteLine($"Got result Gold: {resultG}");
    }

    public static async Task RunSilverAsync()
    {
        Console.WriteLine("Running PrintQueue Silverstar...");
        var solver = new PrintQueue();
        var puzInput = await ReadFile.ReadFromDisk("silver", 5, 2024);
        var result = await solver.SolveSilver(puzInput);
        Console.WriteLine($"Got result silver: {result}");
    }

    public static async Task RunGoldAsync()
    {
        Console.WriteLine("Running PrintQueue Goldstar...");
        var solver = new PrintQueue();
        var puzInput = await ReadFile.ReadFromDisk("silver", 5, 2024);
        var result = await solver.SolveGold(puzInput);
        Console.WriteLine($"Got result gold: {result}");
    }
}