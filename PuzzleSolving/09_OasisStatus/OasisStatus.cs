using Infrastructure.Entities;

namespace PuzzleSolving._09_OasisStatus;

public static class OasisStatus
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var sequences = ExtractSequences(input);

        return sequences.Sum(seq => PredictNextStatus(seq, seq.Last()));
    }

    public static long SolvePartTwo(PuzzleInput input)
    {
        var sequences = ExtractSequencesReversed(input);

        return sequences.Sum(seq => PredictNextStatus(seq, seq.Last()));
    }

    private static IEnumerable<List<long>> ExtractSequences(PuzzleInput input)
    {
        return input.Lines.Select(line => line.Split(" ").Select(long.Parse).ToList());
    }

    private static IEnumerable<List<long>> ExtractSequencesReversed(PuzzleInput input)
    {
        var sequences = input.Lines.Select(line => line.Split(" ").Select(long.Parse).ToList()).ToList();
        sequences.ForEach(seq => seq.Reverse());
        return sequences;
    }

    private static long PredictNextStatus(IReadOnlyList<long> currentSequence, long firstValue, long currentOffset = 0L)
    {
        if (currentSequence.All(entry => entry.Equals(currentSequence[0])))
        {
            return currentOffset + firstValue;
        }

        var t = new List<long>();

        for (var i = 0; i < currentSequence.Count - 1; i++)
        {
            t.Add(currentSequence[i + 1] - currentSequence[i]);
        }

        return PredictNextStatus(t, firstValue, currentOffset + t.Last());
    }
}