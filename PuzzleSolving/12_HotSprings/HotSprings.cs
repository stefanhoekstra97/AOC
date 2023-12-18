using System.Collections.Immutable;
using Infrastructure.Entities;

namespace PuzzleSolving._12_HotSprings;

public class HotSprings
{
    public static long SolvePartOne(PuzzleInput input)
    { 
        var results = new List<long>(input.Lines.Count);
        foreach (var line in input.Lines)
        {
            results.Add(SolveForLine(line, 1));
        }

        return results.Sum();
    }

    public static long SolvePartTwo(PuzzleInput input)
    {
        var results = new List<long>(input.Lines.Count);
        foreach (var line in input.Lines)
        {
            results.Add(SolveForLine(line, 5));
        }

        return results.Sum();
    }

    private static long SolveForLine(string line, int unfoldAmount = 1)
    {
        var split = line.Split(" ");

        var currentState = string.Join('?', Enumerable.Repeat(split[0], unfoldAmount));
        var contiguousGroups = Enumerable.Repeat(split[1].Split(",").Select(int.Parse).ToList(), unfoldAmount).ToList()
            .SelectMany(k => k).ToList();
        contiguousGroups.Reverse();

        return GetOptionsForUnfoldedLine(currentState, ImmutableStack.CreateRange(contiguousGroups));
    }
    private static long GetOptionsForUnfoldedLine(string pattern, ImmutableStack<int> contiguousGroups)
    {
        return NextStep(pattern, contiguousGroups, new Dictionary<(string, ImmutableStack<int>), long>());
    }

    private static long NextStep(string pattern, ImmutableStack<int> nums, Dictionary<(string, ImmutableStack<int>), long> cache) {
        if (!cache.ContainsKey((pattern, nums))) {
            cache[(pattern, nums)] = HandleNextChar(pattern, nums, cache);
        }
        return cache[(pattern, nums)];
    }

    private static long HandleNextChar(string pattern, ImmutableStack<int> nums, Dictionary<(string, ImmutableStack<int>), long> cache) {
        return pattern.FirstOrDefault() switch {
            '.' => ProcessDot(pattern, nums, cache),
            '?' => ProcessQuestion(pattern, nums, cache),
            '#' => ProcessHash(pattern, nums, cache),
            _ => ProcessEnd(pattern, nums, cache),
        };
    }

    private static long ProcessEnd(string _, ImmutableStack<int> nums, Dictionary<(string, ImmutableStack<int>), long> __) {
        return !nums.IsEmpty ? 0 : 1;
    }

    private static long ProcessDot(string pattern, ImmutableStack<int> nums, Dictionary<(string, ImmutableStack<int>), long> cache) {
        return NextStep(pattern[1..], nums, cache);
    }

    private static long ProcessQuestion(string pattern, ImmutableStack<int> nums, Dictionary<(string, ImmutableStack<int>), long> cache) {
        // recurse both ways
        return NextStep("." + pattern[1..], nums, cache) + NextStep("#" + pattern[1..], nums, cache);
    }

    private static long ProcessHash(string pattern, ImmutableStack<int> nums, Dictionary<(string, ImmutableStack<int>), long> cache) {
        // take the first number and consume that many '#', recurse

        if (nums.IsEmpty) {
            return 0;  // Hash but no pattern is left
        }

        var n = nums.Peek();
        nums = nums.Pop();

        var potentiallyDead = pattern.TakeWhile(s => s is '#' or '?').Count();

        if (potentiallyDead < n) {
            return 0;  
        }

        if (pattern.Length == n) {
            return NextStep("", nums, cache);
        }

        if (pattern[n] == '#') {
            return 0; 
        }

        return NextStep(pattern[(n + 1)..], nums, cache);
    }
}
