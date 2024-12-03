using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2024._01_HistorianHysteria;
using PuzzleSolving._2024._02_RedNosedReports;

namespace Benchmarks.Benchmarks._2024;

using PuzzleSolving._2024._03_MulItOver;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class Day03Benchmarks2024
{
    private PuzzleInput _input = null!;
    private MulItOver _solver = null!;


    [GlobalSetup]
    public async Task GlobalSetup()
    {
        _input = await ReadFile.ReadFromDisk("silver", 3, 2024);
        _solver = new MulItOver();
    }

    [Benchmark]
    public async Task<string> SolveSilver()
    {
        var result = await _solver.SolveSilver(_input);
        return result;
    }
    
    [Benchmark]
    public async Task<string> SolveGoldBench()
    {
        var result = await _solver.SolveGold(_input);
        return result;
    }

}