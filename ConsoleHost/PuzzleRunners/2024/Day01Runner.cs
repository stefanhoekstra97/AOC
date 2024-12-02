using Infrastructure.Readers;
using PuzzleSolving._2024._01_HistorianHysteria;

namespace ConsoleHost.PuzzleRunners._2024;

public class Day01Runner
{

    
    public static async Task RunSampleAsync()
    {
        Console.WriteLine("Running Day01 sample...");
        var solver = new Hysteria();
        var puzInput = await ReadFile.ReadFromDisk("sample", 1, 2024);
        var result = await solver.SolveEasy(puzInput);
        Console.WriteLine(result);
    }
    
    public static async Task RunSilverAsync()
    {
        Console.WriteLine("Running Day01 sample...");
    }
    
    public static async Task RunGoldAsync()
    {
        Console.WriteLine("Running Day01 sample...");
    }
}