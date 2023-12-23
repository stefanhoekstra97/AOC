using Infrastructure.Entities;

namespace PuzzleSolving._17_ClumsyCrucible;

public class ClumsyCrucible
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var grid = new LavaCityGrid(input);

        return grid.FindShortestPathSmallCrucible(grid.GridItems.First().First(), grid.GridItems.Last().Last());
    }

    public static long SolvePartTwo(PuzzleInput input)
    {
        var grid = new LavaCityGrid(input);
        
        return grid.FindShortestPathUltraCrucible(grid.GridItems.First().First(), grid.GridItems.Last().Last());
    }
}