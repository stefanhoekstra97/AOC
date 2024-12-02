using Infrastructure.Entities;

namespace PuzzleSolving._2023._10_PipeMaze;

public static class PipeMaze
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var maze = new Maze(input);
        var kv = maze.CalculateLongestPathInLoop();

        return kv.pathLength;
    }

    public static long SolvePartTwo(PuzzleInput input)
    {
        var maze = new Maze(input);
        var kv = maze.CalculateLongestPathInLoop();
        
        var enclosed = maze.GetEnclosedArea(kv.visitedPoints.ToList());
        return enclosed;
    }
}