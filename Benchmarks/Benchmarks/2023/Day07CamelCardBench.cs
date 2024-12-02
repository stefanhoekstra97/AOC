using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._07_CamelCards;

namespace Benchmarks.Benchmarks._2023;

public class Day07CamelCardBench
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async Task GlobalSetup()
    {
        _input = await ReadFile.ReadFromDisk("puzzle_input", 7);
    }

    [Benchmark]
    public void CamelGamePartOne()
    {
        CamelCards.SolvePartOne(_input);
    }
    
    [Benchmark]
    public void CamelGamePartTwo()
    {
        CamelCards.SolvePartTwo(_input);
    }
}