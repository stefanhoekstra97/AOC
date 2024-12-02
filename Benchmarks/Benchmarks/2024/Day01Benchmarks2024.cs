using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2024._01_HistorianHysteria;

namespace Benchmarks.Benchmarks._2024;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class Day01Benchmarks2024
{
    private PuzzleInput input;
    private Hysteria solver;


    [GlobalSetup]
    public async void GlobalSetup()
    {
        input = await ReadFile.ReadFromDisk("silver", 1, 2024);
        solver = new Hysteria();
    }

    [Benchmark]
    public async Task<string> SolveSilver()
    {
        var result = await solver.SolveSilver(input);
        return result;
    }
    
    [Benchmark]
    public async Task<string> SolveGoldBench()
    {
        var result = await solver.SolveGold(input);
        return result;
    }

}