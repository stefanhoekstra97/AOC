using BenchmarkDotNet.Attributes;
using Infrastructure;
using PuzzleSolving;

namespace Benchmarks.DayOneBenchmarks;

public class CalibrateLaunchBench
{
    private string _input;
    private CalibrateLaunch _solver;
    
    [GlobalSetup]
    public async void GlobalSetup()
    {
        var puzzleInput = await ReadFile.ReadFromDisk("puzzle_input", 1);
        _input = puzzleInput.Input;
    
        _solver = new CalibrateLaunch();
    }

    [Benchmark]
    public void SolveHardBench()
    {
        _solver.SolveHard(_input);
    }
    
    [Benchmark]
    public async Task SolveHardBenchIncReadInput()
    {
        var puzzleInput = await ReadFile.ReadFromDisk("puzzle_input", 1);
        var inp = puzzleInput.Input;
        
        var solv = new CalibrateLaunch();
        solv.SolveHard(inp);
    }
    
    [Benchmark]
    public void SolveHardBenchNoParrallel()
    {
        _solver.SolveHardSlower(_input);
    }
    
    [Benchmark]
    public async Task SolveHardBenchNoParrallelIncReadInput()
    {
        var puzzleInput = await ReadFile.ReadFromDisk("puzzle_input", 1);
        var inp = puzzleInput.Input;
        
        var solv = new CalibrateLaunch();
        solv.SolveHardSlower(inp);
    }
}