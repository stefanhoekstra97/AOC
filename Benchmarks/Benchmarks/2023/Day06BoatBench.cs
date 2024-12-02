using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._06_Boat;

namespace Benchmarks.Benchmarks._2023;

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
    public void BoatGamePartOneBSearch()
    {
        BoatGame.SolvePartOneBSearch(_input);
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