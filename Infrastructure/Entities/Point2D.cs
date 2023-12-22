namespace Infrastructure.Entities;

public readonly struct Point2D(int y, int x, int value = 0)
{
    public readonly int Y = y;
    public readonly int X = x;
    public readonly int Value = value;
    public static Point2D operator -(Point2D a, Point2D b) => new(a.Y - b.Y, a.X - b.X,a.Value - b.Value);
    public static Point2D operator +(Point2D a, Point2D b) => new(a.Y + b.Y, a.X + b.X, a.Value + b.Value);
    public static Point2D operator *(Point2D a, Point2D b) => new(a.Y * b.Y, a.X * b.X, a.Value * b.Value);
    public static Point2D operator *(Point2D a, int b) => new(a.Y * b, a.X * b, a.Value);

    public long ManhattanDistanceTo(Point2D other)
    {
        var xDistance = other.X > X ? other.X - X : X - other.X;
        var yDistance = other.Y > Y ? other.Y - Y : Y - other.Y;
        
        return xDistance + yDistance;
    }
    
    public override string ToString()
    {
        if (Value != 0)
        {
            return Value.ToString();
        }
        
        return $"(X: {X}, Y: {Y})";
    }
};