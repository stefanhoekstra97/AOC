using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._08_HauntedWasteland;

namespace Benchmarks.Benchmarks._2023;

public class Day08HauntedDesertBench
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async Task GlobalSetup()
    {
        _input = await ReadFile.ReadFromDisk("puzzle_input", 8);
    }

    [Benchmark]
    public void HauntedWastelandPartOne()
    {
        HauntedWasteland.SolvePartOne(_input);
    }
    
    // [Benchmark]
    public void HauntedWastelandPartTwo()
    {
        HauntedWasteland.SolvePartTwo(_input);
    }
    
    [Benchmark]
    public void HauntedWastelandPartOneAlt()
    {
        HauntedWasteland.SolvePartOneAlternative(_input);
    }
    
    [Benchmark]
    public void HauntedWastelandPartTwoAlt()
    {
        HauntedWasteland.SolvePartTwoAlternative(_input);
    }
}