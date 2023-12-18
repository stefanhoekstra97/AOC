namespace PuzzleSolving._13_Mirrors;

internal enum Pattern
{
    Ash,
    Rock,
}

internal class AshPattern
{
    private readonly Pattern[][] _pattern;
    

    public AshPattern(string inputPattern)
    {
        var splitPattern = inputPattern.Split("\r\n");
        _pattern = new Pattern[splitPattern.Length][];

        for (var y = 0; y < splitPattern.Length; y++)
        {
            _pattern[y] = splitPattern[y].Select(c => c.Equals('.') ? Pattern.Ash : Pattern.Rock).ToArray();
        }
    }

    public int GetValueOfReflectionLines()
    {
        var horizontalAxis = GetHorizontalFromSmudge(0);
        var verticalAxis = GetVerticalFromSmudge(0);
        return horizontalAxis * 100 + verticalAxis;
    }
    
    public int GetValueOfReflectionLinesWithASmudge()
    {
        var horizontalAxis = GetHorizontalFromSmudge(1);
        var verticalAxis = GetVerticalFromSmudge(1);
        return horizontalAxis * 100 + verticalAxis;
    }


    private int GetHorizontalFromSmudge(int allowedSmudges = 0)
    {
        // For each possible horizontal mirror line ( x =  # of rows above mirror)
        for (var potentialMirrorPos = 0; potentialMirrorPos < _pattern.Length - 1; potentialMirrorPos++)
        {
            // Set differences found to 0
            var countedDiff = 0;

            // Iterate over all possible rows included in the reflection ( i >= 0, x + 1 + (x - i) < pattern line count) 
            for (var yPos = potentialMirrorPos;
                 yPos >= 0 && (potentialMirrorPos + 1) + (potentialMirrorPos - yPos) < _pattern.Length;
                 yPos--)
            {
                for (var xPos = 0; xPos < _pattern[0].Length; xPos++)
                {
                    if (countedDiff > allowedSmudges)
                    {
                        continue;
                    }

                    if (!_pattern[yPos][xPos]
                            .Equals(_pattern[(potentialMirrorPos + 1) + (potentialMirrorPos - yPos)][xPos]))
                    {
                        countedDiff++;
                    }
                }
            }

            if (countedDiff == allowedSmudges)
            {
                return potentialMirrorPos + 1;
            }

        }

        return 0;
    }
    
    private int GetVerticalFromSmudge(int allowedSmudges = 0)
    {
        // For each possible horizontal mirror line ( x =  # of rows above mirror)
        for (var potentialMirrorPos = 0; potentialMirrorPos < _pattern[0].Length - 1; potentialMirrorPos++)
        {
            // Set differences found to 0
            var countedDiff = 0;

            // Iterate over all possible rows included in the reflection ( i >= 0, x + 1 + (x - i) < pattern line count) 
            for (var xPos = potentialMirrorPos;
                 xPos >= 0 && potentialMirrorPos + 1 + (potentialMirrorPos - xPos) < _pattern[0].Length;
                 xPos--)
            {
                for (var yPos = 0; yPos < _pattern.Length; yPos++)
                {
                    if (countedDiff > allowedSmudges)
                    {
                        continue;
                    }

                    if (!_pattern[yPos][xPos]
                            .Equals(_pattern[yPos][potentialMirrorPos + 1 + (potentialMirrorPos - xPos)]))
                    {
                        countedDiff++;
                    }
                }
            }

            if (countedDiff == allowedSmudges)
            {
                return potentialMirrorPos + 1;
            }

        }

        return 0;
    }
}