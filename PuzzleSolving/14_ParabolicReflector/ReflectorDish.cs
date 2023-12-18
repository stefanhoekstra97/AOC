using System.Text;
using Infrastructure.Entities;

namespace PuzzleSolving._14_ParabolicReflector;

internal enum FloorEntity
{
    Empty,
    MovableRock,
    SquareRock,
}

internal class ReflectorDish
{
    private List<List<FloorEntity>> _reflectorDish;

    public ReflectorDish(PuzzleInput input)
    {
        _reflectorDish = new List<List<FloorEntity>>(input.Lines.Count);
        foreach (var line in input.Lines)
        {
            var floorLine = new List<FloorEntity>(line.Length);
            floorLine.AddRange(line.Select(c =>
                c.Equals('.') ? FloorEntity.Empty : c.Equals('#') ? FloorEntity.SquareRock : FloorEntity.MovableRock));
            _reflectorDish.Add(floorLine);
        }
    }

    public long CountNorthSupportBeamLoad()
    {
        var currentLoad = 0L;
        for (int i = _reflectorDish.Count - 1; i >= 0; i--)
        {
            for (int x = 0; x < _reflectorDish[0].Count; x++)
            {
                if (_reflectorDish[i][x].Equals(FloorEntity.MovableRock))
                {
                    currentLoad += _reflectorDish.Count - i;
                }
            }
        }

        return currentLoad;
    }

    public void MoveMovableRocksNorth()
    {
        for (int xPos = 0; xPos < _reflectorDish[0].Count; xPos++)
        {
            for (int yPos = 0; yPos < _reflectorDish.Count; yPos++)
            {
                if (_reflectorDish[yPos][xPos].Equals(FloorEntity.MovableRock))
                {
                    // Move upwards if possible
                    for (var i = yPos - 1; i >= 0; i--)
                    {
                        if (_reflectorDish[i][xPos].Equals(FloorEntity.Empty))
                        {
                            _reflectorDish[i][xPos] = FloorEntity.MovableRock;
                            _reflectorDish[i + 1][xPos] = FloorEntity.Empty;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    private void MoveMovableRocksSouth()
    {
        for (int xPos = 0; xPos < _reflectorDish[0].Count; xPos++)
        {
            for (int yPos = _reflectorDish.Count - 1; yPos >= 0; yPos--)
            {
                if (_reflectorDish[yPos][xPos].Equals(FloorEntity.MovableRock))
                {
                    // Move downwards if possible
                    for (var i = yPos + 1; i < _reflectorDish.Count; i++)
                    {
                        if (_reflectorDish[i][xPos].Equals(FloorEntity.Empty))
                        {
                            _reflectorDish[i][xPos] = FloorEntity.MovableRock;
                            _reflectorDish[i - 1][xPos] = FloorEntity.Empty;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }


    private void MoveMovableRocksWest()
    {
        for (int yPos = 0; yPos < _reflectorDish[0].Count; yPos++)
        {
            for (int xPos = 0; xPos < _reflectorDish.Count; xPos++)
            {
                if (_reflectorDish[yPos][xPos].Equals(FloorEntity.MovableRock))
                {
                    // Move westwards if possible
                    for (var i = xPos - 1; i >= 0; i--)
                    {
                        if (_reflectorDish[yPos][i].Equals(FloorEntity.Empty))
                        {
                            _reflectorDish[yPos][i] = FloorEntity.MovableRock;
                            _reflectorDish[yPos][i + 1] = FloorEntity.Empty;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
    
    private void MoveMovableRocksEast()
    {
        for (int yPos = 0; yPos < _reflectorDish[0].Count; yPos++)
        {
            for (int xPos = _reflectorDish.Count - 1; xPos >= 0; xPos--)
            {
                if (_reflectorDish[yPos][xPos].Equals(FloorEntity.MovableRock))
                {
                    // Move westwards if possible
                    for (var i = xPos + 1; i < _reflectorDish[0].Count; i++)
                    {
                        if (_reflectorDish[yPos][i].Equals(FloorEntity.Empty))
                        {
                            _reflectorDish[yPos][i] = FloorEntity.MovableRock;
                            _reflectorDish[yPos][i - 1] = FloorEntity.Empty;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    public void PerformSpinCycle(int amountOfCycles = 1)
    {
        Console.WriteLine($"Performing {amountOfCycles} cycles");
        for (var currentNumCycle = 0; currentNumCycle < amountOfCycles; currentNumCycle++)
        {
            MoveMovableRocksNorth();
            MoveMovableRocksWest();
            MoveMovableRocksSouth();
            MoveMovableRocksEast();
        }            

        
        
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Dish:");
        foreach (var line in _reflectorDish)
        {
            sb.AppendLine(" ");
            foreach (var entity in line)
            {
                switch (entity)
                {
                    case FloorEntity.Empty:
                        sb.Append(".");
                        break;
                    case FloorEntity.MovableRock:
                        sb.Append('O');
                        break;
                    case FloorEntity.SquareRock:
                        sb.Append('#');
                        break;
                }
            }
        }

        return sb.ToString();
    }
}