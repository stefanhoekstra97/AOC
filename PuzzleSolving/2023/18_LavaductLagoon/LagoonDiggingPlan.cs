using System.Text;
using Infrastructure.Entities;
using Infrastructure.enums;

namespace PuzzleSolving._2023._18_LavaductLagoon;

public class LagoonDiggingPlan : Grid2DBase
{
    private readonly List<DigInstruction> _diggingInstructions = [];

    public LagoonDiggingPlan(PuzzleInput input, bool printLagoonLayout = false)
    {
        var startPoint = new Point2D(0, 0);
        List<Point2D> edgePointsOfLagoon = [];

        for (var index = 0; index < input.Lines.Count; index++)
        {
            var digInstruction = input.Lines[index];
            var digInstr = new DigInstruction(digInstruction);

            _diggingInstructions.Add(digInstr);

            edgePointsOfLagoon.AddRange(digInstr.GetEdgePoints(startPoint, index + 1));
            startPoint = edgePointsOfLagoon.Last();
        }

        var width = edgePointsOfLagoon.Max(p => p.X) - edgePointsOfLagoon.Min(p => p.X);
        var height = edgePointsOfLagoon.Max(p => p.Y) - edgePointsOfLagoon.Min(p => p.Y);
        Console.WriteLine($"Lagoon width: {width}, height: {height}.");

        var offsetX = edgePointsOfLagoon.Min(p => p.X) * -1;
        var offsetY = edgePointsOfLagoon.Min(p => p.Y) * -1;

        GridItems = [];

        for (int y = 0; y <= height; y++)
        {
            GridItems.Add([]);
            for (int x = 0; x <= width; x++)
            {
                GridItems.Last().Add(new Point2D(y, x));
            }
        }

        foreach (var point in edgePointsOfLagoon)
        {
            GridItems[point.Y + offsetY][point.X + offsetX] = new Point2D(point.Y+ offsetY, point.X+ offsetX, point.Value);
        }

        if (printLagoonLayout)
        {
            PrintLagoon();
        }
        
    }

    public int GetLagoonArea()
    {
        var totalArea = 0;
        foreach (var row in GridItems)
        {
            var crossingCount = 0;
            var previousWasUpper = false;
            var lookingForMatch = false;
            
            foreach (var point in row)
            {
                if (point.Value > 0)
                {
                    totalArea++;
                    
                    // On top row no empty spot can be included anyway
                    if (point.Y == 0 || point.Y == GridItems.Count - 1) continue;

                    if (!lookingForMatch)
                    {

                        // Check row above and below on same X spot. Record whether it was horizontal or a bend

                        var above = GridItems[point.Y - 1][point.X];
                        var below = GridItems[point.Y + 1][point.X];
                        
                        if (above.Value > 0 && below.Value > 0)
                        {
                            crossingCount += 1;
                            continue;
                        }

                        if (above.Value > 0)
                        {
                            previousWasUpper = true;
                        }

                        lookingForMatch = true;
                    }
                }
                else
                {
                    if (lookingForMatch)
                    {
                        if (point.Y == 0 || point.Y == GridItems.Count - 1) continue;

                        // Check X - 1,  Y + 1 and Y - 1
                        var above = GridItems[point.Y - 1][point.X - 1];
                        var below = GridItems[point.Y + 1][point.X - 1];
                        
                        if ((previousWasUpper && below.Value > 0) 
                            || (!previousWasUpper && above.Value > 0))
                        {
                            crossingCount++;
                        }
                    }
                    
                    
                    totalArea += crossingCount % 2;
                    lookingForMatch = false;
                }
            }
        }

        return totalArea;
    }
    void PrintLagoon()
    {
        var sb = new StringBuilder();
        foreach (var row in GridItems)
        {
            sb.Append("\r\n");
            foreach (var point in row)
            {
                if (point.Value > 0)
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

internal class DigInstruction
{
    public CardinalDirections Direction;
    public int EdgeSize;
    public string HexCode;

    public DigInstruction(string instruction)
    {
        var split = instruction.Split(' ');
        Direction = (split[0][0]) switch
        {
            'R' => CardinalDirections.East,
            'L' => CardinalDirections.West,
            'U' => CardinalDirections.North,
            'D' => CardinalDirections.South,
            _ => throw new ArgumentException("crash")
        };
        EdgeSize = int.Parse(split[1]);
        HexCode = split[2];
    }

    public IEnumerable<Point2D> GetEdgePoints(Point2D startPoint, int valueForPoint)
    {
        for (int i = 0; i <= EdgeSize; i++)
        {
            yield return (new Point2D(startPoint.Y, startPoint.X, valueForPoint)) +
                         DirectionMapper.DirectionalOffset[Direction] * i;
        }
    }
}