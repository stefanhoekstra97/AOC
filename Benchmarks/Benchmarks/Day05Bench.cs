using BenchmarkDotNet.Attributes;
using Infrastructure;
using Infrastructure.Entities;
using PuzzleSolving._05_Seeds;

namespace Benchmarks.Benchmarks;

public class Day05Bench
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async void GlobalSetup()
    {
        var puzzleInput = await ReadFile.ReadFromDisk("puzzle_input", 5);
        _input = puzzleInput;
    }

    [Benchmark]
    public void SolvePartOne()
    {
        SeedPlanter.SolvePartOne(_input);
    }
    
    [Benchmark]
    public void SolvePartTwo()
    {
        SeedPlanter.SolvePartTwo(_input);
    }
}