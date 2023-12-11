namespace Benchmarks.Benchmarks;

using PuzzleSolving._09_OasisStatus;
using BenchmarkDotNet.Attributes;
using Infrastructure;
using Infrastructure.Entities;

public class Day09OasisStatusBench
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async Task GlobalSetup()
    {
        _input = await ReadFile.ReadFromDisk("puzzle_input", 9);
    }

    [Benchmark]
    public void OasisStatusPartOne()
    {
        var r = OasisStatus.SolvePartOne(_input);
    }

    [Benchmark]
    public void OasisStatusPartTwo()
    {
        var r = OasisStatus.SolvePartTwo(_input);
    }
}