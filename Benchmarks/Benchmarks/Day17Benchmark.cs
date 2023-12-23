using BenchmarkDotNet.Attributes;
using Infrastructure;
using Infrastructure.Entities;
using PuzzleSolving._17_ClumsyCrucible;

namespace Benchmarks.Benchmarks;

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
