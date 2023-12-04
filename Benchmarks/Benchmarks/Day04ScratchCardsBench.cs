using BenchmarkDotNet.Attributes;
using Infrastructure;
using Infrastructure.Entities;
using PuzzleSolving._04_ScratchCards;

namespace Benchmarks.Benchmarks;

public class Day04ScratchCardsBench
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async void GlobalSetup()
    {
        var puzzleInput = await ReadFile.ReadFromDisk("puzzle_input", 4);
        _input = puzzleInput;
    }

    [Benchmark]
    public void SolvePartOne()
    {
        ScratchCards.SolvePartOne(_input);
    }
    
    [Benchmark]
    public void SolvePartTwo()
    {
        ScratchCards.SolvePartTwo(_input);
    }
}