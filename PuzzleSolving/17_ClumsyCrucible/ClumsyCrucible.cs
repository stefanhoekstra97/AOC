using Infrastructure.Entities;

namespace PuzzleSolving._17_ClumsyCrucible;

public class ClumsyCrucible
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var grid = new LavaCityGrid(input);
        // Console.WriteLine(grid);
        Console.WriteLine(grid.FindShortestPath(grid.gridItems.First().First(), grid.gridItems.Last().Last()));
        return 0L;
    }
    
    public static long SolvePartTwo(PuzzleInput input)
    {
        return 0L;
    }
}