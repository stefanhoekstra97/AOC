using Infrastructure.Entities;

namespace PuzzleSolving._10_PipeMaze;

internal class Maze
{
    private readonly PipeMazeTile[,] _tiles;
    private readonly PipeMazeTile _startingPipeMazeTile;

    public Maze(PuzzleInput input)
    {
        _tiles = new PipeMazeTile[input.Lines.Count, input.Lines.First().Length];
        for (var posY = 0; posY < input.Lines.Count; posY++)
        {
            var line = input.Lines[posY];
            for (var posX = 0; posX < line.Length; posX++)
            {
                _tiles[posY, posX] = new PipeMazeTile(line[posX], new Point2D(posY, posX));
                if (line[posX].Equals('S'))
                {
                    _startingPipeMazeTile = _tiles[posY, posX];
                }
            }
        }
    }

    public int GetEnclosedArea(List<PipeMazeTile> fullPath)
    {
        // Replace token S by correct pipe symbol:
        var requiredCombinedOffset = fullPath.Last().Location - fullPath[1].Location;
        var actualStartTile = new PipeMazeTile(PipeMazeTile.MoveOffsets
                .First(kv => (kv.Value.First() - kv.Value.Last()).Equals(requiredCombinedOffset)).Key
            , _startingPipeMazeTile.Location);
        _tiles[_startingPipeMazeTile.Location.Y, _startingPipeMazeTile.Location.X] = actualStartTile;

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

    public (int pathLength, IEnumerable<PipeMazeTile> visitedPoints) CalculateLongestPathInLoop()
    {
        // All moves from S
        var startMoves = _startingPipeMazeTile.GetMoveOffsets();
        // Bind moves by grid size
        var possibleMoves = startMoves.Where(m =>
                (m + _startingPipeMazeTile.Location).X >= 0
                && (m + _startingPipeMazeTile.Location).Y >= 0
                && (m + _startingPipeMazeTile.Location).X < _tiles.GetLength(1)
                && (m + _startingPipeMazeTile.Location).Y < _tiles.GetLength(0))
            .ToList();

        var currentTiles = possibleMoves.Select(_ => _startingPipeMazeTile).ToList();
        var listOfVisitedTiles = possibleMoves.Select(_ => new List<PipeMazeTile>()).ToList();
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
                if (currentTiles[index].Location.Equals(_startingPipeMazeTile.Location)
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
                    currentTiles[index] = new PipeMazeTile('.', new Point2D(-1, -1));
                    shouldContinue = true;
                }
            }
        }

        return (0, Enumerable.Empty<PipeMazeTile>());
    }
}