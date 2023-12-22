namespace PuzzleSolving._16_FloorIsLava;

internal class FloorTile(FloorTileType type)
{
    public readonly FloorTileType Type = type;
    public bool TileIsLit = false;
    public bool TileLitFromNorth = false;
    public bool TileLitFromEast = false;
    public bool TileLitFromSouth = false;
    public bool TileLitFromWest = false;

    public void ResetTile()
    {
        TileIsLit = false;
        TileLitFromEast = false;
        TileLitFromNorth = false;
        TileLitFromSouth = false;
        TileLitFromWest = false;
    }
}