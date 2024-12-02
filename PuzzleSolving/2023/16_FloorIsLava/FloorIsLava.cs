using System.Text;
using Infrastructure.Entities;
using Infrastructure.enums;

namespace PuzzleSolving._2023._16_FloorIsLava;

public class FloorIsLava
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var floorLayout = new LavaFloor(input);
        // Console.WriteLine(floorLayout);
        floorLayout.HandleBeam(0, 0, CardinalDirections.East);
        // floorLayout.PrintEnergized();
        return floorLayout.GetEnergizedTiles();
    }


    public static long SolvePartTwo(PuzzleInput input)
    {
        var floorLayout = new LavaFloor(input);
        var cases = new List<long>();
        for (var x = 0; x < input.Lines[0].Length; x++)
        {
            // Top edge:
            floorLayout.HandleBeam(0, (short)x, CardinalDirections.South);
            cases.Add(floorLayout.GetEnergizedTiles());
            floorLayout.ResetFloor();
            
            // Bottom edge:
            floorLayout.HandleBeam((short)(input.Lines.Count - 1), (short)x, CardinalDirections.North);
            cases.Add(floorLayout.GetEnergizedTiles());
            floorLayout.ResetFloor();
            if (x % 10 == 0)
            {
                Console.WriteLine($"Done with horizontal edge {x}");
            }
        }
        
        for (var y = 0; y < input.Lines.Count; y++)
        {
            // Left edge:
            floorLayout.HandleBeam((short)y, 0, CardinalDirections.East);
            cases.Add(floorLayout.GetEnergizedTiles());
            floorLayout.ResetFloor();
            
            // Right edge:
            floorLayout.HandleBeam((short)y, (short)(input.Lines[0].Length - 1), CardinalDirections.West);
            cases.Add(floorLayout.GetEnergizedTiles());
            floorLayout.ResetFloor();
            
            if (y % 10 == 0)
            {
                Console.WriteLine($"Done with vertical edge {y}");
            }
        }
        return cases.Max();
    }
}

internal class LavaFloor
{
    private readonly List<List<FloorTile>> _floorMap;

    public LavaFloor(PuzzleInput input)
    {
        _floorMap = new List<List<FloorTile>>();
        for (int y = 0; y < input.Lines.Count; y++)
        {
            _floorMap.Add([]);
            for (int x = 0; x < input.Lines[0].Length; x++)
            {
                _floorMap[y].Add(item: input.Lines[y][x] switch
                {
                    '.' => new FloorTile(FloorTileType.Empty),
                    '/' => new FloorTile(FloorTileType.ForwardSlashMirror),
                    '\\' => new FloorTile(FloorTileType.BackSlashMirror),
                    '|' => new FloorTile(FloorTileType.VerticalMirror),
                    '-' => new FloorTile(FloorTileType.HorizontalMirror),
                    _ => throw new ApplicationException("Tile not recognized")
                });
            }
        }
    }

    public void HandleBeam(short currentY, short currentX, CardinalDirections currentDirection)
    {
        while (true)
        {
            switch (currentDirection)
            {
                // Do check on direction and boundary
                case CardinalDirections.West when currentX < 0:
                case CardinalDirections.East when currentX > _floorMap[0].Count - 1:
                case CardinalDirections.North when currentY < 0:
                case CardinalDirections.South when currentY > _floorMap.Count - 1:
                    return;
            }

            // Handle current tile light-up and check if need to continue
            // (If already lit up and traveling in same direction we can stop, as we are looping)
            var currentTile = _floorMap[currentY][currentX];
            currentTile.TileIsLit = true;

            if ((currentDirection & CardinalDirections.Horizontal) != 0)
            {
                if (currentTile.TileLitFromEast && currentDirection.Equals(CardinalDirections.East) || currentTile.TileLitFromWest && currentDirection.Equals(CardinalDirections.West))
                {
                    return;
                }

                if (currentDirection.Equals(CardinalDirections.East))
                {
                    currentTile.TileLitFromEast = true;
                }
                else
                {
                    currentTile.TileLitFromWest = true;
                }

                // Check on CURRENT tile type - pass through, reflect 90 degrees, split?
                if (currentTile.Type.Equals(FloorTileType.Empty) || currentTile.Type.Equals(FloorTileType.HorizontalMirror))
                {
                    short nextLocationOffset = (short)(currentDirection.Equals(CardinalDirections.West) ? -1 : 1);
                    currentX = (short)(currentX + nextLocationOffset);
                    continue;
                }

                // Handle beam splitter
                if (currentTile.Type.Equals(FloorTileType.VerticalMirror))
                {
                    HandleBeam((short)(currentY - 1), currentX, CardinalDirections.North);
                    currentY = (short)(currentY + 1);
                    currentDirection = CardinalDirections.South;
                    continue;
                }

                // Handle westbound beams on mirror
                if ((currentDirection.Equals(CardinalDirections.West) && currentTile.Type.Equals(FloorTileType.BackSlashMirror)) || (currentDirection.Equals(CardinalDirections.East) && currentTile.Type.Equals(FloorTileType.ForwardSlashMirror)))
                {
                    // Move up:
                    currentY = (short)(currentY - 1);
                    currentDirection = CardinalDirections.North;
                    continue;
                }
                else
                {
                    currentY = (short)(currentY + 1);
                    currentDirection = CardinalDirections.South;
                    continue;
                }
            }

            if ((currentDirection & CardinalDirections.Vertical) != 0)
            {
                if ((currentTile.TileLitFromNorth && currentDirection.Equals(CardinalDirections.North)) || (currentTile.TileLitFromSouth && currentDirection.Equals(CardinalDirections.South)))
                {
                    return;
                }

                if (currentDirection.Equals(CardinalDirections.North))
                {
                    currentTile.TileLitFromNorth = true;
                }
                else
                {
                    currentTile.TileLitFromSouth = true;
                }

                // Check on CURRENT tile type - pass through, reflect 90 degrees, split?

                // Handle continue in same direction
                if (currentTile.Type.Equals(FloorTileType.Empty) || currentTile.Type.Equals(FloorTileType.VerticalMirror))
                {
                    var nextLocationOffset = currentDirection.Equals(CardinalDirections.North) ? -1 : 1;
                    currentY = (short)(currentY + nextLocationOffset);
                    continue;
                }

                // Handle beam splitter
                if (currentTile.Type.Equals(FloorTileType.HorizontalMirror))
                {
                    HandleBeam(currentY, (short)(currentX - 1), CardinalDirections.West);
                    currentX = (short)(currentX + 1);
                    currentDirection = CardinalDirections.East;
                    continue;
                }

                // Handle bouncing beams
                if ((currentDirection.Equals(CardinalDirections.South) && currentTile.Type.Equals(FloorTileType.BackSlashMirror)) || currentDirection.Equals(CardinalDirections.North) && currentTile.Type.Equals(FloorTileType.ForwardSlashMirror))
                {
                    currentX = (short)(currentX + 1);
                    currentDirection = CardinalDirections.East;
                    continue;
                }
                else
                {
                    currentX = (short)(currentX - 1);
                    currentDirection = CardinalDirections.West;
                    continue;
                }
            }

            break;
        }
    }

    public void ResetFloor()
    {
        foreach (var lineOfTiles in _floorMap)
        {
            foreach (var tile in lineOfTiles)
            {
                tile.ResetTile();
            }
        }
    }
    public int GetEnergizedTiles()
    {
        return _floorMap.Sum(s => s.Count(tile => tile.TileIsLit));
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("Floor layout:");
        foreach (var lineWithTiles in _floorMap)
        {
            sb.AppendLine("");
            foreach (var tile in lineWithTiles)
            {
                sb.Append(value: (tile.Type) switch
                {
                    FloorTileType.Empty => '.',
                    FloorTileType.HorizontalMirror => '-',
                    FloorTileType.VerticalMirror => '|',
                    FloorTileType.BackSlashMirror => '\\',
                    FloorTileType.ForwardSlashMirror => '/',
                    _ => throw new ArgumentOutOfRangeException()
                });
            }
        }

        return sb.ToString();
    }

    public void PrintEnergized()
    {
        var sb = new StringBuilder();
        foreach (var tileRow in _floorMap)
        {
            sb.Append("\r\n");
            foreach (var tile in tileRow)
            {
                if (tile.TileIsLit)
                {
                    sb.Append('#');
                }
                else
                {
                    sb.Append('.');
                }
            }
        }

        Console.WriteLine(sb.ToString());
    }
}