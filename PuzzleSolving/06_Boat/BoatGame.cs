using Infrastructure.Entities;

namespace PuzzleSolving._06_Boat;

public static class BoatGame
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var timesInMs = input.Lines[0]
            .Split(" ", StringSplitOptions.TrimEntries)[1..]
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(int.Parse).ToArray();
        
        var distancesInMm = input.Lines[1]
            .Split(" ", StringSplitOptions.TrimEntries)[1..]
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(int.Parse).ToArray();
        
        var runningMultiple = 1L;
        
        for (int i = 0; i < timesInMs.Length; i++)
        {
            var resultForSet = GetNumberOfPossibleWins(timesInMs[i], distancesInMm[i]);
            
            runningMultiple *= resultForSet;
            Console.WriteLine($"Result for i {i} is {resultForSet}");
        }
        return runningMultiple;
    }
    
    public static long SolvePartTwo(PuzzleInput input)
    {
        var time = long.Parse(input.Lines[0].Replace(" ", "").Split(":")[1]);
        var distance = long.Parse(input.Lines[1].Replace(" ", "").Split(":")[1]);
        
        return GetNumberOfPossibleWins(time, distance);
    }

    private static long GetNumberOfPossibleWins(long timeAvailable, long distanceToBeat)
    {
        var minButtonTime = GetMinTimeToPushButton(timeAvailable, distanceToBeat);
        return (timeAvailable + 1) - (2 * minButtonTime);
    }

    private static long GetMinTimeToPushButton(long timeAvailable, long distanceToBeat)
    {
        if (distanceToBeat == 0)
        {
            return 1;
        }

        for (var i = 0; i < timeAvailable / 2; i++)
        {
            if (i * (timeAvailable - i) > distanceToBeat)
            {
                return i;
            }
        }

        throw new ArgumentException($"Not beatable.. Time in ms: {timeAvailable}, distance: {distanceToBeat}");
    }
}