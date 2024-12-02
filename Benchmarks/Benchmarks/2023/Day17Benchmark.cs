using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._17_ClumsyCrucible;

namespace Benchmarks.Benchmarks._2023;

public class Day17Benchmark
{
    private PuzzleInput _input = null!;

    [GlobalSetup]
    public async Task GlobalSetup()
    {
        _input = await ReadFile.ReadFromDisk("puzzle_input", 17);
    }

    [Benchmark]
    public void ClumsyCruciblePartOne()
    {
        var r = ClumsyCrucible.SolvePartOne(_input);
        if (r != 674) throw new ArgumentException("Part 1 wrong ans");
    }
    
    [Benchmark]
    public void ClumsyCruciblePartTwo()
    {
        var r = ClumsyCrucible.SolvePartTwo(_input);
        if (r != 773) throw new ArgumentException("Part 2 wrong ans");

    }
}
