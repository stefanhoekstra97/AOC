using BenchmarkDotNet.Attributes;
using Infrastructure;
using Infrastructure.Entities;
using PuzzleSolving._07_CamelCards;

namespace Benchmarks.Benchmarks;

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