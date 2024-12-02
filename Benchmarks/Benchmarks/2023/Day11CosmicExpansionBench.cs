using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._11_CosmicExpansion;

namespace Benchmarks.Benchmarks._2023;

public class Day11CosmicExpansionBench
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async Task GlobalSetup()
    {
        _input = await ReadFile.ReadFromDisk("puzzle_input", 11);
    }

    [Benchmark]
    public void CosmicExpansionPartOne()
    {
        var r = CosmicExpansion.SolvePuzzle(_input);
    }
    
    [Benchmark]
    public void CosmicExpansionPartTwo()
    {
        var r = CosmicExpansion.SolvePuzzle(_input, expansionRate: 1000000);
    }
}