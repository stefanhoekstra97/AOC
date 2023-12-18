using Infrastructure.Entities;

namespace PuzzleSolving._14_ParabolicReflector;

public class ParabolicReflector
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var reflectorDish = new ReflectorDish(input);

        reflectorDish.MoveMovableRocksNorth();

        var load = reflectorDish.CountNorthSupportBeamLoad();
        return load;
    }
    
    public static long SolvePartTwo(PuzzleInput input)
    {
        var reflectorDish = new ReflectorDish(input);
        reflectorDish.PerformSpinCycle(1000);
        Console.WriteLine($"Current load: {reflectorDish}");

        for (var i = 0; i < 50; i++)
        {
            reflectorDish.PerformSpinCycle(1);
            Console.WriteLine($"Current load: {reflectorDish.CountNorthSupportBeamLoad()}, cycle {1000 + i}");
        }
        
        
        var load = reflectorDish.CountNorthSupportBeamLoad();
        return load;
    }
}