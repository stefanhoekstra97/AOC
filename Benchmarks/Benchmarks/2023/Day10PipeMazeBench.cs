using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._10_PipeMaze;

namespace Benchmarks.Benchmarks._2023;

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