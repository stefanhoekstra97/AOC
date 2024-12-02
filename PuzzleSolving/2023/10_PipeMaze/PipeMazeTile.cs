using Infrastructure.Entities;

namespace PuzzleSolving._2023._10_PipeMaze;

internal readonly record struct PipeMazeTile(char OriginalChar, Point2D Location)
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

    public bool CanMoveToTile(PipeMazeTile other)
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