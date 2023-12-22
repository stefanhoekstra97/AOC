using System.Text;
using Infrastructure.Entities;
using Infrastructure.enums;

namespace PuzzleSolving._17_ClumsyCrucible;

public class LavaCityGrid : Grid2DBase
{
    private List<List<ShadowTile>> VisitedMemory;

    public LavaCityGrid(PuzzleInput input)
    {
        gridItems = [];
        VisitedMemory = [];
        for (var y = 0; y < input.Lines.Count; y++)
        {
            VisitedMemory.Add([]);
            gridItems.Add([]);
            var row = input.Lines[y];
            for (var x = 0; x < row.Length; x++)
            {
                var heatLoss = int.Parse(row[x].ToString());
                gridItems.Last().Add(new Point2D(y, x, heatLoss));
                VisitedMemory.Last().Add(new ShadowTile(){LowestCost = int.MaxValue});
            }
        }
    }

    public long FindShortestPath(Point2D start, Point2D target)
    {
        var averageVal = gridItems.Average(s => s.Average(tile => tile.Value));
        var initialPath = new TraversalPath
        {
            CurrentPosition = start,
            HeuristicValue = HeuristicFunc(start)
        };

        var discoveryPathQueue = new PriorityQueue<TraversalPath, double>();
        discoveryPathQueue.Enqueue(initialPath, initialPath.CombinedHeuristicCost);

        // Loop over possible next paths to check
        // Check each current combined cost (heuristic plus incurred by heat loss)
        //-> Handled by the priority queue, ordered by Heuristic value

        while (discoveryPathQueue.Count > 0)
        {
            var currentPath = discoveryPathQueue.Dequeue();

            if (currentPath.CurrentPosition.ManhattanDistanceTo(target) == 0)
            {
                Console.WriteLine($"{currentPath.NumNodesInPath} nodes visited in this path");
                foreach (var direction in currentPath.PathsTaken)
                {
                    Console.WriteLine(Enum.GetName(typeof(CardinalDirections), direction));
                }
                return currentPath.TotalCumulativeCost;
            }


            // Determine possible next steps
            // Ensure the same direction is not taken more than thrice (that is - a max of 4 moves in the same direction!)
            foreach (var kv in DirectionMapper.DirectionalOffset)
            {
                // Enqueue next possibilities - Ensure next possibilities are possible!
                var newNode = currentPath.CurrentPosition + kv.Value;

                // Check constraint: Last four moves are not allowed to be taken in the same direction
                if (kv.Key == CardinalDirections.East && currentPath.LastDirection.Equals(CardinalDirections.West)
                    || kv.Key == CardinalDirections.West &&
                    currentPath.LastDirection == CardinalDirections.East
                    || kv.Key == CardinalDirections.South &&
                    currentPath.LastDirection == CardinalDirections.North
                    || kv.Key == CardinalDirections.North &&
                    currentPath.LastDirection == CardinalDirections.South)
                {
                    continue;
                }

                if (newNode.X < 0
                    || newNode.Y < 0
                    || newNode.X >= gridItems.First().Count
                    || newNode.Y >= gridItems.Count)
                {
                    continue;
                }

                var newConsecutiveEast = 0;
                var newConsecutiveSouth = 0;
                var newConsecutiveNorth = 0;
                var newConsecutiveWest = 0;
                switch (kv.Key)
                {
                    case CardinalDirections.East:
                        if (currentPath.LastDirection == CardinalDirections.West ||
                            currentPath.NumConsecutiveEast >= 3) continue;
                        newConsecutiveEast = currentPath.NumConsecutiveEast + 1;
                        newConsecutiveSouth = 0;
                        newConsecutiveNorth = 0;
                        newConsecutiveWest = 0;
                        break;
                    case CardinalDirections.West:
                    {
                        if (currentPath.LastDirection == CardinalDirections.East ||
                            currentPath.NumConsecutiveWest >= 3) continue;
                        newConsecutiveEast = 0;
                        newConsecutiveSouth = 0;
                        newConsecutiveNorth = 0;
                        newConsecutiveWest = currentPath.NumConsecutiveWest + 1;
                        break;
                    }
                    case CardinalDirections.South:

                    {
                        if (currentPath.LastDirection == CardinalDirections.North ||
                            currentPath.NumConsecutiveSouth >= 3) continue;
                        newConsecutiveEast = 0;
                        newConsecutiveSouth = currentPath.NumConsecutiveSouth + 1;
                        newConsecutiveNorth = 0;
                        newConsecutiveWest = 0;
                        break;
                    }
                    case CardinalDirections.North:
                    {
                        if (currentPath.LastDirection == CardinalDirections.South ||
                            currentPath.NumConsecutiveNorth >= 3) continue;
                        newConsecutiveEast = 0;
                        newConsecutiveSouth = 0;
                        newConsecutiveNorth = currentPath.NumConsecutiveNorth + 1;
                        newConsecutiveWest = 0;
                        break;
                    }
                }

                var numConsecutiveDirs =
                    newConsecutiveEast + newConsecutiveNorth + newConsecutiveSouth + newConsecutiveWest;

                // Continue to next item in queue if we already had visited the node, and its cost is lower.
                if (VisitedMemory[newNode.Y][newNode.X].LowestCost <=
                    currentPath.TotalCumulativeCost + gridItems[newNode.Y][newNode.X].Value
                     && VisitedMemory[newNode.Y][newNode.X].Historic.Contains((kv.Key, numConsecutiveDirs)))
                    
                {
                    continue;
                }

                var newPath = new TraversalPath
                {
                    CurrentPosition = newNode,
                    HeuristicValue = HeuristicFunc(newNode),
                    TotalCumulativeCost = currentPath.TotalCumulativeCost + gridItems[newNode.Y][newNode.X].Value,
                    NumConsecutiveEast = newConsecutiveEast,
                    NumConsecutiveWest = newConsecutiveWest,
                    NumConsecutiveSouth = newConsecutiveSouth,
                    NumConsecutiveNorth = newConsecutiveNorth,
                    LastDirection = kv.Key,
                    NumNodesInPath = currentPath.NumNodesInPath + 1,
                    PathsTaken = currentPath.PathsTaken.Append(kv.Key).ToList()
                };
                
                VisitedMemory[newNode.Y][newNode.X].LowestCost = newPath.TotalCumulativeCost;
                VisitedMemory[newNode.Y][newNode.X].Historic.Add((kv.Key, numConsecutiveDirs));
                discoveryPathQueue.Enqueue(newPath, newPath.CombinedHeuristicCost);
            }
        }

        return 0L;

        // Heuristic: manhattan distance. Heuristic is optimistic - each cell has value >= 1 - and thus admissible.
        // double HeuristicFunc(Point2D current) => current.ManhattanDistanceTo(target);
        // double HeuristicFunc(Point2D current) => current.ManhattanDistanceTo(target) * (averageVal - 3);
        double HeuristicFunc(Point2D current) => current.ManhattanDistanceTo(target) * 0.5;
        // double HeuristicFunc(Point2D current) => current.ManhattanDistanceTo(target);

    }
}

internal record struct TraversalPath
{
    public List<CardinalDirections> PathsTaken = [];
    public Point2D CurrentPosition = default;
    public int TotalCumulativeCost = 0;
    public double HeuristicValue = 0;
    public int NumConsecutiveEast = 0;
    public int NumConsecutiveSouth = 0;
    public int NumConsecutiveNorth = 0;
    public int NumConsecutiveWest = 0;
    public CardinalDirections LastDirection;
    public int NumNodesInPath = 0;

    public TraversalPath()
    {
    }

    public int CombinedHeuristicCost => (int)(TotalCumulativeCost + HeuristicValue);
}

internal class ShadowTile
{
    public int LowestCost = default;
    public List<(CardinalDirections enteredFrom, int consecutiveDirections)> Historic = [];
}