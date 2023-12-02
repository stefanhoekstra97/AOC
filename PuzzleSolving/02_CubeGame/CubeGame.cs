using System.Text.RegularExpressions;
using Infrastructure.Entities;

namespace PuzzleSolving._02_CubeGame;

public class CubeGame
{
    private readonly Dictionary<string, int> _easyConstraintColors = new Dictionary<string, int>()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };
    
    public int SolveEasy(PuzzleInput input)
    {
        var lines = input.Input.Split("\r\n");
        var possibleIds = (from line in lines let gameId = ExtractGameId(line) where IsGamePossible(line) select gameId).ToList();

        return possibleIds.Sum();
    }

    public int SolveHard(PuzzleInput input)
    {
        var lines = input.Input.Split("\r\n");
        return lines.Sum(GetPowerFromInput);
    }

    private static int GetPowerFromInput(string input)
    {
        List<int> powers = new List<int>(3);
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
        return gameEntries.All(IsRevealPossible);
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
}