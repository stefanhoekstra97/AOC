using BenchmarkDotNet.Attributes;
using Infrastructure;
using Infrastructure.Entities;
using PuzzleSolving._03_EngineParts;

namespace Benchmarks.Benchmarks;

public class Day03EngineBench
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async void GlobalSetup()
    {
        var puzzleInput = await ReadFile.ReadFromDisk("puzzle_input_mod", 3);
        _input = puzzleInput;
    }

    [Benchmark]
    public void SolvePartOne()
    {
        EngineParts.SolveEasy(_input);
    }
    
    [Benchmark]
    public void SolvePartTwo()
    {
        EngineParts.SolveHard(_input);
    }
}