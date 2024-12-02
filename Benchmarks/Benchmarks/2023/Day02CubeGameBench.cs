using BenchmarkDotNet.Attributes;
using Infrastructure.Entities;
using Infrastructure.Readers;
using PuzzleSolving._2023._02_CubeGame;

namespace Benchmarks.Benchmarks._2023;

public class Day02CubeGameBench
{
    private PuzzleInput _input;
    private CubeGame _solver;

    [GlobalSetup]
    public async void GlobalSetup()
    {
        var puzzleInput = await ReadFile.ReadFromDisk("puzzle_input", 2);
        _input = puzzleInput;

        _solver = new CubeGame();
    }

    [Benchmark]
    public void SolveEasy()
    {
        _solver.SolveEasy(_input);
    }

    [Benchmark]
    public void SolveEasyRegex()
    {
        _solver.SolveEasyRegex(_input);
    }

    [Benchmark]
    public void SolveSecondCubeBench()
    {
        _solver.SolveHard(_input);
    }

    [Benchmark]
    public async Task SolveSecondCubeBenchIncReadInput()
    {
        var puzzleInput = await ReadFile.ReadFromDisk("puzzle_input", 2);
        var inp = puzzleInput;

        var solv = new CubeGame();
        solv.SolveHard(inp);
    }




}