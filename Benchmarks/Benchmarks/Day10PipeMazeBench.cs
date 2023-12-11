using BenchmarkDotNet.Attributes;
using Infrastructure;
using Infrastructure.Entities;
using PuzzleSolving._10_PipeMaze;

namespace Benchmarks.Benchmarks;

public class Day10PipeMazeBench
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async Task GlobalSetup()
    {
        _input = await ReadFile.ReadFromDisk("puzzle_input", 10);
    }

    [Benchmark]
    public void PipeMazePartOne()
    {
        var r = PipeMaze.SolvePartOne(_input);
    }

    [Benchmark]
    public void PipeMazePartTwo()
    {
        var r = PipeMaze.SolvePartTwo(_input);
    }
}