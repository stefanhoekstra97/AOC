using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._09_OasisStatus;

namespace Benchmarks.Benchmarks._2023;

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