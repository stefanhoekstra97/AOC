using Infrastructure.Entities;

namespace PuzzleSolving._2023._11_CosmicExpansion;

public static class CosmicExpansion
{
    public static long SolvePuzzle(PuzzleInput input, int expansionRate = 2)
    {
        var map = new GalaxyMap(input, expansionRate);

        return map.GetPathLengthOfGalaxyPairs();
    }
}

internal class GalaxyMap
{
    private readonly List<Point2D> _galaxies = [];

    public GalaxyMap(PuzzleInput input, int expansionRate = 2)
    {
        var observation = new List<List<char>>(input.Lines.Count);
        observation.AddRange(input.Lines.Select(line => line.ToList()));

        var horizontalsWithoutGalaxy = observation
            .Select((line, index) => (line, index))
            .Where(kv => !kv.line.Contains(StarMap.Galaxy))
            .Select(kv => kv.index)
            .ToList();

        var verticalsWithoutGalaxy = observation[0]
            .Select((obs, ind) => (obs, ind))
            .Where(kv => kv.obs.Equals(StarMap.Empty))
            .Select(kv => kv.ind)
            .ToList();

        verticalsWithoutGalaxy = observation[1..]
            .Aggregate(verticalsWithoutGalaxy, (current, line) => current.Where(x => line[x].Equals(StarMap.Empty))
                .ToList());

        var lineLength = input.Lines[0].Length;
        for (var y = 0; y < input.Lines.Count; y++)
        {
            for (var x = 0; x < lineLength; x++)
            {
                if (!observation[y][x].Equals(StarMap.Galaxy)) continue;
                // Calculate pos of galaxy
                // actual X position = x position + #(vertical without galaxy preceeding x) * (expansionrate - 1)
                // Same for Y position
                var actualXCoordinate = x + verticalsWithoutGalaxy.Count(ind => x > ind) * (expansionRate - 1);
                var actualYCoordinate = y + horizontalsWithoutGalaxy.Count(ind => y > ind) * (expansionRate - 1);
                _galaxies.Add(new Point2D(actualYCoordinate, actualXCoordinate));
            }
        }
    }

    public long GetPathLengthOfGalaxyPairs()
    {
        // For each pair get the manhattan distance. Included in the Point2D struct
        var pairs = _galaxies
            .SelectMany((g1, i) => _galaxies.Skip(i + 1)
                .Select(g2 => (g1, g2)))
            .ToList();
        return pairs.Sum(galaxyPair => galaxyPair.g1.ManhattanDistanceTo(galaxyPair.g2));
    }
}

internal readonly struct StarMap
{
    public const char Galaxy = '#';
    public const char Empty = '.';
}