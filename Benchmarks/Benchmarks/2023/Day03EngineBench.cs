using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._03_EngineParts;

namespace Benchmarks.Benchmarks._2023;

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