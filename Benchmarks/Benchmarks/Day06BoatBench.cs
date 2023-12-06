using BenchmarkDotNet.Attributes;
using Infrastructure;
using Infrastructure.Entities;
using PuzzleSolving._06_Boat;

namespace Benchmarks.Benchmarks;

public class Day06BoatBench
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async Task GlobalSetup()
    {
        _input = await ReadFile.ReadFromDisk("puzzle_input", 6);
    }

    [Benchmark]
    public void BoatGamePartOne()
    {
        BoatGame.SolvePartOne(_input);
    }
    
    [Benchmark]
    public void BoatGamePartTwo()
    {
        BoatGame.SolvePartTwo(_input);
    }
    
    [Benchmark]
    public void BoatGamePartTwoBSearch()
    {
        BoatGame.SolvePartTwoBSearch(_input);
    }
}