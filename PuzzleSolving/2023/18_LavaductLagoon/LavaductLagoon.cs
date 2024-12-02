using Infrastructure.Entities;

namespace PuzzleSolving._2023._18_LavaductLagoon;

public class LavaductLagoon
{
    public static long SolvePartOne(PuzzleInput input, bool printLagoonLayout = false)
    {
        var plan = new LagoonDiggingPlan(input, printLagoonLayout);
        
        return plan.GetLagoonArea();
    }
    
    public static long SolvePartTwo(PuzzleInput input)
    {
        return 0L;
    }
}