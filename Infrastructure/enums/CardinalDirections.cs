using Infrastructure.Entities;

namespace Infrastructure.enums;

[Flags]
public enum CardinalDirections
{
    North = 1,
    East = 2,
    South = 4,
    West = 8,
    Horizontal = East | West,
    Vertical = North | South,
}

public static class DirectionMapper
{
    public static readonly Dictionary<CardinalDirections, Point2D> DirectionalOffset = new()
    {
        { CardinalDirections.East, new Point2D(0, 1) },
        { CardinalDirections.South, new Point2D(1, 0) },
        { CardinalDirections.West, new Point2D(0, -1) },
        { CardinalDirections.North, new Point2D(-1, 0) },
    };
} 