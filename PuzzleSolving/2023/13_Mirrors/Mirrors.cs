using Infrastructure.Entities;

namespace PuzzleSolving._2023._13_Mirrors;

public class Mirrors
{
    public static int SolvePartOne(PuzzleInput input)
    {
        var patterns = ExtractPatterns(input);

        var value = patterns.Sum(pattern => pattern.GetValueOfReflectionLines());
        
        return value;
    }
    
    public static int SolvePartTwo(PuzzleInput input)
    {
        var patterns = ExtractPatterns(input);
        
        return patterns.Sum(pattern => pattern.GetValueOfReflectionLinesWithASmudge());
    }

    private static IEnumerable<AshPattern> ExtractPatterns(PuzzleInput input)
    {
        var rawPatterns = input.Input.Split("\r\n\r\n");
        var allPatterns = rawPatterns.Select(s => new AshPattern(s));
        return allPatterns;
    }
}