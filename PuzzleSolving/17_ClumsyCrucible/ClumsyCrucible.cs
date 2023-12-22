using Infrastructure.Entities;

namespace PuzzleSolving._17_ClumsyCrucible;

public class ClumsyCrucible
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var grid = new LavaCityGrid(input);
        // Console.WriteLine(grid);
        Console.WriteLine(
            grid.FindShortestPathSmallCrucible(grid.GridItems.First().First(), grid.GridItems.Last().Last()));
        return 0L;
    }

    public static long SolvePartTwo(PuzzleInput input)
    {
        var grid = new LavaCityGrid(input);

        Console.WriteLine(
            grid.FindShortestPathUltraCrucible(grid.GridItems.First().First(), grid.GridItems.Last().Last()));
        return 0L;
    }
}