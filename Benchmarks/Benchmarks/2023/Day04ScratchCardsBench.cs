using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._04_ScratchCards;

namespace Benchmarks.Benchmarks._2023;

public class Day04ScratchCardsBench
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async void GlobalSetup()
    {
        var puzzleInput = await ReadFile.ReadFromDisk("puzzle_input", 4);
        _input = puzzleInput;
    }

    // [Benchmark]
    public void SolvePartOne()
    {
        ScratchCards.SolvePartOne(_input);
    }
    
    // [Benchmark]
    public void SolvePartTwo()
    {
        ScratchCards.SolvePartTwo(_input);
    }
    [Benchmark]
    public void SolvePartTwoAlt()
    {
        ScratchCards.SolvePartTwoAlternative(_input);
    }
    
}