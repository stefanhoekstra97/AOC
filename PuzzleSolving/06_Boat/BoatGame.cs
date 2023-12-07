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

        return timesInMs.Select((t, i) => GetNumberOfPossibleWins(t, distancesInMm[i]))
            .Aggregate(1L, (current, resultForSet) => current * resultForSet);
    }

    public static long SolvePartTwo(PuzzleInput input)
    {
        var time = long.Parse(input.Lines[0].Replace(" ", "").Split(":")[1]);
        var distance = long.Parse(input.Lines[1].Replace(" ", "").Split(":")[1]);

        return GetNumberOfPossibleWins(time, distance);
    }

    public static long? SolvePartTwoBSearch(PuzzleInput input)
    {
        var time = long.Parse(input.Lines[0].Replace(" ", "").Split(":")[1]);
        var distance = long.Parse(input.Lines[1].Replace(" ", "").Split(":")[1]);


        var minTimeToBeat = GetMinTimeBinarySearch(time, distance, null, null);
        return (time + 1) - (2 * minTimeToBeat);
    }

    
    public static long SolvePartOneBSearch(PuzzleInput input)
    {
        var timesInMs = input.Lines[0]
            .Split(" ", StringSplitOptions.TrimEntries)[1..]
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(int.Parse).ToArray();

        var distancesInMm = input.Lines[1]
            .Split(" ", StringSplitOptions.TrimEntries)[1..]
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(int.Parse).ToArray();

        return timesInMs.Select((t, i) => GetNumberOfPossibleWinsBsearch(t, distancesInMm[i]))
            .Aggregate(1L, (current, resultForSet) => current * resultForSet);
    }
    private static long GetNumberOfPossibleWins(long timeAvailable, long distanceToBeat)
    {
        var minButtonTime = GetMinTimeToPushButton(timeAvailable, distanceToBeat);
        return (timeAvailable + 1) - (2 * minButtonTime);
    }

    private static long GetNumberOfPossibleWinsBsearch(long timeAvailable, long distanceToBeat)
    {
        var minTimeToBeat = GetMinTimeBinarySearch(timeAvailable, distanceToBeat, null, null) ?? throw new ArgumentException("no value found");
        return (timeAvailable + 1) - (2 * minTimeToBeat);
    }

    private static long GetMinTimeToPushButton(long timeAvailable, long distanceToBeat)
    {
        for (var i = 1; i < timeAvailable / 2; i++)
        {
            if (i * (timeAvailable - i) > distanceToBeat)
            {
                return i;
            }
        }

        throw new ArgumentException($"Not beatable.. Time in ms: {timeAvailable}, distance: {distanceToBeat}");
    }

    private static long? GetMinTimeBinarySearch(long timeAvailable, long distanceToBeat, long? currentMaxSearchMarker,
        long? currentIndex)
    {
        currentMaxSearchMarker ??= timeAvailable / 2;
        currentIndex ??= currentMaxSearchMarker / 2;

        var markerBeats = currentIndex * (timeAvailable - currentIndex) > distanceToBeat;
        var nextMarkerBeats = (currentIndex + 1) * (timeAvailable - (currentIndex + 1)) > distanceToBeat;

        if (!markerBeats && nextMarkerBeats)
        {
            return currentIndex + 1;
        }
        
        if (markerBeats)
        {
            return GetMinTimeBinarySearch(timeAvailable, distanceToBeat, currentIndex, currentIndex / 2);
        }

        if (!markerBeats && !nextMarkerBeats)
        {
            var nextSearchIndex = currentIndex + (currentMaxSearchMarker - currentIndex) / 2;
            return GetMinTimeBinarySearch(timeAvailable, distanceToBeat, currentMaxSearchMarker, nextSearchIndex);
        }

        throw new ArgumentException("Could not find solution.");
    }
}