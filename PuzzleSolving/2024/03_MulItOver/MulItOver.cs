namespace PuzzleSolving._2024._03_MulItOver;

using System.Text.RegularExpressions;
using Infrastructure.Entities;

public partial class MulItOver : IPuzzleSolver
{

    public Task<string> SolveSilver(PuzzleInput input)
    {
        var totalInstructionsCounter = 0;
        var reg = MulMatcher();
        var digitGroupExtractor = DigitGroupMatcher();
        
        var matches = reg.Matches(input.Input);
        foreach (Match match in matches)
        {
            var digitGroupsMatches = digitGroupExtractor.Matches(match.Value);
            totalInstructionsCounter += int.Parse(digitGroupsMatches[0].Value) * int.Parse(digitGroupsMatches[1].Value);
        }

        return Task.FromResult(totalInstructionsCounter.ToString());
    }

    public Task<string> SolveGold(PuzzleInput input)
    {
        var totalInstructionsCounter = 0;
        var enabledInstruction = true;

        var doDontMulMatcher = ComplexMulMatcher();
        var digitGroupExtractor = DigitGroupMatcher();
        
        var mulMatches = doDontMulMatcher.Matches(input.Input);
        
        
        foreach (Match match in mulMatches)
        {
            if (match.ValueSpan.StartsWith("don't"))
            {
                enabledInstruction = false;
                continue;
            }

            if (match.ValueSpan.StartsWith("do"))
            {
                enabledInstruction = true;
                continue;
            }

            if (enabledInstruction)
            {
                var digitGroupsMatches = digitGroupExtractor.Matches(match.Value);
                
                totalInstructionsCounter += int.Parse(digitGroupsMatches[0].Value) * int.Parse(digitGroupsMatches[1].Value);
            }
        }

        return Task.FromResult(totalInstructionsCounter.ToString());
    }
    
    [GeneratedRegex(@"(mul\(\d+,\d+\)|don't\(\)|do\(\))")]
    private static partial Regex ComplexMulMatcher();

    [GeneratedRegex(@"(mul\(\d+,\d+\))")]
    private static partial Regex MulMatcher();
    
    [GeneratedRegex(@"(\d+)")]
    private static partial Regex DigitGroupMatcher();
}