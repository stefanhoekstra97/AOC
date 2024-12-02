using Infrastructure.Entities;

namespace PuzzleSolving._2024._01_HistorianHysteria;

public class Hysteria : IPuzzleSolver
{
    public async Task<string> SolveSilver(PuzzleInput input)
    {
        List<int> firstNumbers = new List<int>(input.Lines.Count);
        List<int> secondNumbers = new List<int>(input.Lines.Count);

        foreach (var line in input.Lines)
        {
            var split = line.Split("  ");
            firstNumbers.Add(int.Parse(split[0]));
            secondNumbers.Add(int.Parse(split[1]));
        }

        firstNumbers.Sort();
        secondNumbers.Sort();

        return firstNumbers.Zip(secondNumbers).Sum((a) => Math.Abs(a.First - a.Second)).ToString();
    }

    public async Task<string> SolveGold(PuzzleInput input)
    {
        List<int> firstNumbers = new List<int>(input.Lines.Count);
        List<int> secondNumbers = new List<int>(input.Lines.Count);
        
        Dictionary<int, (int add, int mult)> firstNumberCounts = new ();

        foreach (var line in input.Lines)
        {
            var split = line.Split("  ");
            firstNumbers.Add(int.Parse(split[0]));
            secondNumbers.Add(int.Parse(split[1]));
        }

        foreach (int left in firstNumbers)
        {
            if (firstNumberCounts.ContainsKey(left))
            {
                firstNumberCounts[left] = (firstNumberCounts[left].add + 1, firstNumberCounts[left].mult);
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
        return firstNumberCounts.Sum(a => (a.Key * a.Value.mult) * a.Value.add).ToString();
    }
}