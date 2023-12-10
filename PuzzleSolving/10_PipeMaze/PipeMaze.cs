using Infrastructure.Entities;

namespace PuzzleSolving._10_PipeMaze;

public class PipeMaze
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

internal class Maze
{
    private readonly Tile[,] _tiles;
    private readonly Tile _startingTile;

    public Maze(PuzzleInput input)
    {
        _tiles = new Tile[input.Lines.Count, input.Lines.First().Length];
        for (var posY = 0; posY < input.Lines.Count; posY++)
        {
            var line = input.Lines[posY];
            for (var posX = 0; posX < line.Length; posX++)
            {
                _tiles[posY, posX] = new Tile(line[posX], new Point2D(posY, posX));
                if (line[posX].Equals('S'))
                {
                    _startingTile = _tiles[posY, posX];
                }
            }
        }
    }

    public int GetEnclosedArea(List<Tile> fullPath)
    {
        // Replace token S by correct pipe symbol:
        var requiredCombinedOffset = fullPath.Last().Location - fullPath[1].Location;
        var actualStartTile = new Tile(Tile.MoveOffsets
                .First(kv => (kv.Value.First() - kv.Value.Last()).Equals(requiredCombinedOffset)).Key
            , _startingTile.Location);
        _tiles[_startingTile.Location.Y, _startingTile.Location.X] = actualStartTile;

        var area = 0;
        
        // Even number of crossings = not in area, odd number of crossings = in area.
        for (var posY = 0; posY < _tiles.GetLength(0); posY++)
        {
            var crossings = 0;
            var foundF = false;
            
            for (var posX = 0; posX < _tiles.GetLength(1); posX++)
            {
                var current = _tiles[posY, posX].OriginalChar;
                var isInPath = fullPath.Any(tile => tile.Location.Y == posY && tile.Location.X == posX);
                
                if (!isInPath)
                {
                    area += crossings % 2;
                    continue;
                }
                if ("|".Contains(current))
                {
                    crossings += 1;
                    continue;
                }

                if ("F".Contains(current))
                {
                    foundF = true;
                    // We can do crossings + 2 here but unnecessary as mod 2
                    continue;

                }

                if ("J".Contains(current))
                {
                    if (foundF)
                    {
                        foundF = false;
                        crossings += 1;
                        continue;
                    }
                }

                if ("7".Contains(current))
                {
                    if (foundF)
                    {
                        foundF = false;
                        // We can do crossings + 2 here but unnecessary as mod 2
                        continue;
                    }
                    crossings += 1;
                }
            }   
        }
        
        return area;
    }

    public (int pathLength, IEnumerable<Tile> visitedPoints) CalculateLongestPathInLoop()
    {
        // All moves from S
        var startMoves = _startingTile.GetMoveOffsets();
        // Bind moves by grid size
        var possibleMoves = startMoves.Where(m =>
                (m + _startingTile.Location).X >= 0
                && (m + _startingTile.Location).Y >= 0
                && (m + _startingTile.Location).X < _tiles.GetLength(1)
                && (m + _startingTile.Location).Y < _tiles.GetLength(0))
            .ToList();

        var currentTiles = possibleMoves.Select(_ => _startingTile).ToList();
        var listOfVisitedTiles = possibleMoves.Select(_ => new List<Tile>()).ToList();
        var counters = possibleMoves.Select(_ => 0).ToList();
        var shouldContinue = true;

        while (shouldContinue)
        {
            shouldContinue = false;

            // Check if all are at S or at a .
            if ((currentTiles.All(tile => tile.OriginalChar == '.' || tile.OriginalChar == 'S')) && counters.Max() != 0)
            {
                // Loop has finished. Find tiles that are at S, halve the max number of the counter and return it.
                var indexOfMaxCounter = currentTiles
                    .Select((tile, index) => (tile, index))
                    .Where(kv => kv.tile.OriginalChar == 'S')
                    .MaxBy(kv => counters[kv.index]);
                return (counters[indexOfMaxCounter.index] / 2,
                    listOfVisitedTiles[indexOfMaxCounter.index]);
            }

            for (var index = 0; index < possibleMoves.Count; index++)
            {
                // Check if is back at S
                if (currentTiles[index].Location.Equals(_startingTile.Location)
                    && counters[index] != 0
                    || currentTiles[index].OriginalChar.Equals('.'))
                {
                    continue;
                }

                var move = possibleMoves[index];
                var nextLocation = currentTiles[index].Location + move;
                var nextTile = _tiles[nextLocation.Y, nextLocation.X];
                if (currentTiles[index].CanMoveToTile(nextTile))
                {
                    listOfVisitedTiles[index] = listOfVisitedTiles[index].Append(currentTiles[index]).ToList();
                    counters[index]++;
                    possibleMoves[index] = nextTile.GetNextTileLocation(previous: currentTiles[index].Location);
                    currentTiles[index] = nextTile;
                    shouldContinue = true;
                }
                else
                {
                    // We could not find path from here. Stop search for this tile.
                    currentTiles[index] = new Tile('.', new Point2D(-1, -1));
                    shouldContinue = true;
                }
            }
        }

        return (0, Enumerable.Empty<Tile>());
    }
}

internal readonly record struct Tile(char OriginalChar, Point2D Location)
{
    public readonly char OriginalChar = OriginalChar;
    public readonly Point2D Location = Location;

    public static readonly Dictionary<char, List<Point2D>> MoveOffsets = new()
    {
        { '.', [new Point2D(0, 0)] },
        { '-', [new Point2D(0, 1), new Point2D(0, -1)] },
        { '|', [new Point2D(-1, 0), new Point2D(1, 0)] },
        { 'F', [new Point2D(0, 1), new Point2D(1, 0)] },
        { 'L', [new Point2D(-1, 0), new Point2D(0, 1)] },
        { 'J', [new Point2D(-1, 0), new Point2D(0, -1)] },
        { '7', [new Point2D(0, -1), new Point2D(1, 0)] },
        { 'S', [new Point2D(-1, 0), new Point2D(1, 0), new Point2D(0, -1), new Point2D(0, 1)] },
    };
    

    public override string ToString()
    {
        return OriginalChar.ToString();
    }

    public IEnumerable<Point2D> GetMoveOffsets()
    {
        return MoveOffsets[OriginalChar];
    }

    public bool CanMoveToTile(Tile other)
    {
        return other.GetMoveOffsets().Contains(Location - other.Location)
               && this.GetMoveOffsets().Contains(other.Location - Location);
    }

    public Point2D GetNextTileLocation(Point2D previous)
    {
        var moves = GetMoveOffsets().ToList();
        return (Location + moves[0]).Equals(previous) ? moves[1] : moves[0];
    }
}

internal readonly struct Point2D(int y, int x)
{
    public readonly int Y = y;
    public readonly int X = x;

    public static Point2D operator -(Point2D a) => a;
    public static Point2D operator -(Point2D a, Point2D b) => new(a.Y - b.Y, a.X - b.X);
    public static Point2D operator +(Point2D a) => a;
    public static Point2D operator +(Point2D a, Point2D b) => new(a.Y + b.Y, a.X + b.X);
};