using Infrastructure.Entities;
using Infrastructure.enums;

namespace PuzzleSolving._17_ClumsyCrucible;

public class LavaCityGrid : Grid2DBase
{
    private List<List<ShadowTile>> VisitedMemory;

    public LavaCityGrid(PuzzleInput input)
    {
        GridItems = [];
        VisitedMemory = [];
        for (var y = 0; y < input.Lines.Count; y++)
        {
            VisitedMemory.Add([]);
            GridItems.Add([]);
            var row = input.Lines[y];
            for (var x = 0; x < row.Length; x++)
            {
                var heatLoss = int.Parse(row[x].ToString());
                GridItems.Last().Add(new Point2D(y, x, heatLoss));
                VisitedMemory.Last().Add(new ShadowTile() { LowestCost = int.MaxValue });
            }
        }
    }

    public long FindShortestPathSmallCrucible(Point2D start, Point2D target)
    {
        var initialPath = new TraversalPath
        {
            CurrentPosition = start,
            HeuristicValue = HeuristicFunc(start, target)
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
                // Console.WriteLine($"{currentPath.NumNodesInPath} nodes visited in this path");
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
                    || newNode.X >= GridItems.First().Count
                    || newNode.Y >= GridItems.Count)
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
                    currentPath.TotalCumulativeCost + GridItems[newNode.Y][newNode.X].Value
                    && VisitedMemory[newNode.Y][newNode.X].Historic.Contains((kv.Key, numConsecutiveDirs)))

                {
                    continue;
                }

                var newPath = new TraversalPath
                {
                    CurrentPosition = newNode,
                    HeuristicValue = HeuristicFunc(newNode, target),
                    TotalCumulativeCost = currentPath.TotalCumulativeCost + GridItems[newNode.Y][newNode.X].Value,
                    NumConsecutiveEast = newConsecutiveEast,
                    NumConsecutiveWest = newConsecutiveWest,
                    NumConsecutiveSouth = newConsecutiveSouth,
                    NumConsecutiveNorth = newConsecutiveNorth,
                    LastDirection = kv.Key,
                    NumNodesInPath = currentPath.NumNodesInPath + 1
                };

                VisitedMemory[newNode.Y][newNode.X].LowestCost = newPath.TotalCumulativeCost;
                VisitedMemory[newNode.Y][newNode.X].Historic.Add((kv.Key, numConsecutiveDirs));
                discoveryPathQueue.Enqueue(newPath, newPath.CombinedHeuristicCost);
            }
        }

        return 0L;
    }



    public long FindShortestPathUltraCrucible(Point2D start, Point2D target)
    {
        var initialPath = new TraversalPath
        {
            CurrentPosition = start,
            HeuristicValue = HeuristicFunc(start, target)
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
                // Console.WriteLine($"{currentPath.NumNodesInPath} nodes visited in this path");
                return currentPath.TotalCumulativeCost;
            }


            // Determine possible next steps
            // Do take other direction than last direction taken -> when horizontal, only vertical, and vice versa.
            // Ensure no more than 10 tiles and no less than 4 tiles are moved 
            foreach (var kv in DirectionMapper.DirectionalOffset)
            {

                // Check constraint: Last four moves are not allowed to be taken in the same direction
                if ( ((kv.Key & CardinalDirections.Horizontal) != 0 && (currentPath.LastDirection & CardinalDirections.Horizontal) != 0)
                    || ((kv.Key & CardinalDirections.Vertical) != 0 && (currentPath.LastDirection & CardinalDirections.Vertical) != 0))
                {
                    continue;
                }

                if (currentPath.CurrentPosition.X < 0
                    || currentPath.CurrentPosition.Y < 0
                    || currentPath.CurrentPosition.X >= GridItems.First().Count
                    || currentPath.CurrentPosition.Y >= GridItems.Count)
                {
                    continue;
                }

                for (var len = 4; len <= 9; len++)
                {
                    // Enqueue next possibilities - Ensure next possibilities are possible!
                    var newNode = currentPath.CurrentPosition + (kv.Value * len);
                    
                    if (newNode.X < 0
                        || newNode.Y < 0
                        || newNode.X >= GridItems.First().Count
                        || newNode.Y >= GridItems.Count)
                    {
                        continue;
                    }

                    var incurredCost = (kv.Key) switch
                    {
                        CardinalDirections.East => GridItems[currentPath.CurrentPosition.Y][(currentPath.CurrentPosition.X + 1) .. (newNode.X+1)].Sum(p => p.Value),
                        CardinalDirections.South => GridItems[(currentPath.CurrentPosition.Y + 1) .. (newNode.Y+1)].Sum(row => row[newNode.X].Value),
                        CardinalDirections.North => GridItems[(newNode.Y).. (currentPath.CurrentPosition.Y - 1)].Sum(row => row[newNode.X].Value),
                        CardinalDirections.West => GridItems[currentPath.CurrentPosition.Y][(newNode.X) .. (currentPath.CurrentPosition.X - 1)].Sum(p => p.Value),
                        _ => throw new ArgumentException("Cannot happen")
                    };

                    if (VisitedMemory[newNode.Y][newNode.X].Historic.Contains((kv.Key, len))) continue;
                    
                    VisitedMemory[newNode.Y][newNode.X].LowestCost = currentPath.TotalCumulativeCost + incurredCost;
                    VisitedMemory[newNode.Y][newNode.X].Historic.Add((kv.Key, len));
                    var newPath = new TraversalPath
                    {
                        CurrentPosition = newNode,
                        HeuristicValue = HeuristicFunc(newNode, target),
                        TotalCumulativeCost = currentPath.TotalCumulativeCost + incurredCost,
                        LastDirection = kv.Key,
                        NumNodesInPath = currentPath.NumNodesInPath + 1,
                        
                    };
                    discoveryPathQueue.Enqueue(newPath, newPath.CombinedHeuristicCost);
                }
            }
        }

        return 0L;
    }
    // Heuristic: manhattan distance. Heuristic is optimistic - each cell has value >= 1 - and thus admissible.
    private static double HeuristicFunc(Point2D current, Point2D target) => current.ManhattanDistanceTo(target);
}

internal record struct TraversalPath
{
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
    public readonly List<(CardinalDirections enteredFrom, int consecutiveDirections)> Historic = [];
}