using Infrastructure.Entities;

namespace PuzzleSolving._2024._01_HistorianHysteria;

public class Hysteria : IPuzzleSolver
{
    public Task<string> SolveSilver(PuzzleInput input)
    {
        var firstNumbers = new List<int>(input.Lines.Count);
        var secondNumbers = new List<int>(input.Lines.Count);

        foreach (var split in input.Lines.Select(line => line.Split("  ")))
        {
            firstNumbers.Add(int.Parse(split[0]));
            secondNumbers.Add(int.Parse(split[1]));
        }

        firstNumbers.Sort();
        secondNumbers.Sort();

        return Task.FromResult(firstNumbers.Zip(secondNumbers).Sum((a) => Math.Abs(a.First - a.Second)).ToString());
    }

    public Task<string> SolveGold(PuzzleInput input)
    {
        var firstNumbers = new List<int>(input.Lines.Count);
        var secondNumbers = new List<int>(input.Lines.Count);
        
        Dictionary<int, (int add, int mult)> firstNumberCounts = new ();

        foreach (var split in input.Lines.Select(line => line.Split("  ")))
        {
            firstNumbers.Add(int.Parse(split[0]));
            secondNumbers.Add(int.Parse(split[1]));
        }

        foreach (var left in firstNumbers)
        {
            if (firstNumberCounts.TryGetValue(left, out var value))
            {
                firstNumberCounts[left] = (value.add + 1, value.mult);
            }
            else
            {
                var mult = secondNumbers.Count(n => n == left);
                if (mult > 0)
                {
                    firstNumberCounts.Add(left, (1, mult));
                }
            }
        }
        return Task.FromResult(firstNumberCounts.Sum(a => (a.Key * a.Value.mult) * a.Value.add).ToString());
    }
}