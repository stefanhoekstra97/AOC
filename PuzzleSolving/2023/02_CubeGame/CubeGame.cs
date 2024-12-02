using System.Text.RegularExpressions;
using Infrastructure.Entities;

namespace PuzzleSolving._2023._02_CubeGame;

public partial class CubeGame
{
    private readonly Dictionary<string, int> _easyConstraintColors = new Dictionary<string, int>()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };

    private readonly Regex _regexRed = RedRegex();
    private readonly Regex _regexGreen = GreenRegex();
    private readonly Regex _regexBlue = BlueRegex();

    public int SolveEasyRegex(PuzzleInput input)
    {
        return input.Input.Split("\r\n").Where(line => !_regexRed.IsMatch(line) && !_regexBlue.IsMatch(line) && !_regexGreen.IsMatch(line)).Sum(ExtractGameId);
    }
    
    public int SolveEasy(PuzzleInput input)
    {
        var lines = input.Input.Split("\r\n");
        var possibleIds = (from line in lines let gameId = ExtractGameId(line) where !IsGamePossible(line) select gameId).ToList();
        return possibleIds.Sum();
    }

    public int SolveHard(PuzzleInput input)
    {
        var lines = input.Input.Split("\r\n");
        return lines.Sum(GetPowerFromInput);
    }

    private static int GetPowerFromInput(string input)
    {
        List<int> powers = new List<int>();
        var reveals = input.Split(": ").Last().Replace(';', ',').Split(", ");
        IEnumerable<(int amnt, string color)> revealTyped =
            reveals.Select(s => s.Split(" ")).Select(k => (int.Parse(k.First()), k.Last()));

        var groupedByColor = revealTyped.GroupBy(r => r.color);
        powers.AddRange(groupedByColor.Select(clrgrp => clrgrp.Max(k => k.amnt)));

        return powers.Aggregate(1, (current, p) => current * p);
    }
    
    
    private bool IsGamePossible(string line)
    {
        var gameEntries = line.Split(":")[1].Split(";");
        return gameEntries.Any(s => !IsRevealPossible(s));
    }

    private bool IsRevealPossible(string revealed)
    {
        var splitPerColor = revealed.Split(", ");
        return !(from reveal in splitPerColor select reveal.Split(" ").Where(s => s.Length != 0) into numberAndReveal let allowed = _easyConstraintColors.FirstOrDefault(kv => kv.Key.Equals(numberAndReveal.Last())) let actual = int.Parse(numberAndReveal.First()) where actual > allowed.Value select allowed).Any();
    }
    
    private static int ExtractGameId(string line)
    {
        var parts = line.Split(":");
        var number = parts.First().Split(" ").Last();
        return int.Parse(number);
    }

    [GeneratedRegex(@"((1[5-9])|([2-9]\d)|(\d{3,})) blue")]
    private static partial Regex BlueRegex();
    
    [GeneratedRegex(@"((1[4-9])|([2-9]\d)|(\d{3,})) green")]
    private static partial Regex GreenRegex();
    
    [GeneratedRegex(@"((1[3-9])|([2-9]\d)|(\d{3,})) red")]
    private static partial Regex RedRegex();
}