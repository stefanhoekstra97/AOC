using System.Text;

namespace Infrastructure.Entities;

public abstract class Grid2DBase
{
    // 2D grid of point2d objects, each representing a tile or square.
    public List<List<Point2D>> GridItems;

    public override string ToString()
    {
        var sb = new StringBuilder();
        
        foreach (var row in GridItems)
        {
            sb.Append("\r\n");
            foreach (var point in row)
            {
                sb.Append(point.ToString());
            }
        }

        return sb.ToString();
    }
}