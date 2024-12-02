using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._05_Seeds;

namespace Benchmarks.Benchmarks._2023;

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