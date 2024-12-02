using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2024._01_HistorianHysteria;
using PuzzleSolving._2024._02_RedNosedReports;

namespace Benchmarks.Benchmarks._2024;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class Day02Benchmarks2024
{
    private PuzzleInput input;
    private RedNosedReports solver;


    [GlobalSetup]
    public async void GlobalSetup()
    {
        input = await ReadFile.ReadFromDisk("silver", 2, 2024);
        solver = new RedNosedReports();
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