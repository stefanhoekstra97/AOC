using Infrastructure.Entities;

namespace PuzzleSolving._2024._04_CeresSearch;

public class CeresSearch : IPuzzleSolver
{
    public async Task<string> SolveSilver(PuzzleInput input)
    {
        var totalCount = 0;
        for (int y = 0; y < input.Lines.Count; y++)
        {
            for (int x = 0; x < input.Lines[0].Length; x++)
            {
                if (input.Lines[y][x].Equals('X'))
                {
                    totalCount += SearchForXmas(y, x, input);
                }
            }
        }
        return totalCount.ToString();
    }
    
    private static int SearchForXmas(int y, int x, PuzzleInput input)
    {
        var count = 0;
        
        if (x < input.Lines[0].Length - 3) 
        {
            // horizontal to right
            if (input.Lines[y][x+1].Equals('M') && input.Lines[y][x+2].Equals('A') && input.Lines[y][x+3].Equals('S'))
            {
                count++;
            }
        }
        if (x >= 3) 
        {
            // horizontal to left
            if (input.Lines[y][x-1].Equals('M') && input.Lines[y][x-2].Equals('A') && input.Lines[y][x-3].Equals('S'))
            {
                count++;
            }
        }
        
        if (y < input.Lines.Count - 3) 
        {
            // vertical to down
            if (input.Lines[y+1][x].Equals('M') && input.Lines[y+2][x].Equals('A') && input.Lines[y+3][x].Equals('S'))
            {
                count++;
            }
        }
        if (y >= 3) 
        {
            // vertical to up
            if (input.Lines[y-1][x].Equals('M') && input.Lines[y-2][x].Equals('A') && input.Lines[y-3][x].Equals('S'))
            {
                count++;
            }
        }
        
        if (x < input.Lines[0].Length - 3 && y < input.Lines.Count - 3) 
        {
            // diagonal to right down
            if (input.Lines[y+1][x+1].Equals('M') && input.Lines[y+2][x+2].Equals('A') && input.Lines[y+3][x+3].Equals('S'))
            {
                count++;
            }
        }
        
        if (x >= 3 && y < input.Lines.Count - 3) 
        {
            // diagonal to left down
            if (input.Lines[y+1][x-1].Equals('M') && input.Lines[y+2][x-2].Equals('A') && input.Lines[y+3][x-3].Equals('S'))
            {
                count++;
            }
        }   
        
        if (x < input.Lines[0].Length - 3 && y >= 3) 
        {
            // diagonal to right up
            if (input.Lines[y-1][x+1].Equals('M') && input.Lines[y-2][x+2].Equals('A') && input.Lines[y-3][x+3].Equals('S'))
            {
                count++;
            }
        }
        
        if (x >= 3 && y >= 3) 
        {
            // diagonal to left up
            if (input.Lines[y-1][x-1].Equals('M') && input.Lines[y-2][x-2].Equals('A') && input.Lines[y-3][x-3].Equals('S'))
            {
                count++;
            }
        }
        
        return count;
    }

    private static int SearchForCrossMas(int y, int x, PuzzleInput input) 
    {
        var count = 0;

        if (input.Lines[y-1][x-1].Equals('M')) 
        {
            if (input.Lines[y-1][x+1].Equals('M') && 
                input.Lines[y+1][x-1].Equals('S') && input.Lines[y+1][x+1].Equals('S')) 
            {
                count++;
            }
            if (input.Lines[y+1][x-1].Equals('M') && 
                input.Lines[y-1][x+1].Equals('S') && input.Lines[y+1][x+1].Equals('S')) 
            {
                count++;
            }
        }
        
        if (input.Lines[y-1][x-1].Equals('S')) 
        {
            if (input.Lines[y-1][x+1].Equals('S') && 
                input.Lines[y+1][x-1].Equals('M') && input.Lines[y+1][x+1].Equals('M')) 
            {
                count++;
            }
            if (input.Lines[y+1][x-1].Equals('S') && 
                input.Lines[y-1][x+1].Equals('M') && input.Lines[y+1][x+1].Equals('M')) 
            {
                count++;
            }
        }
        
        if (input.Lines[y+1][x+1].Equals('M')) 
        {
            if (input.Lines[y+1][x-1].Equals('M') && 
                input.Lines[y-1][x+1].Equals('S') && input.Lines[y-1][x-1].Equals('S')) 
            {
                count++;
            }
            if (input.Lines[y-1][x+1].Equals('M') && 
                input.Lines[y+1][x-1].Equals('S') && input.Lines[y-1][x-1].Equals('S')) 
            {
                count++;
            }
        }
        
        if (input.Lines[y+1][x+1].Equals('S')) 
        {
            if (input.Lines[y+1][x-1].Equals('S') && 
                input.Lines[y-1][x+1].Equals('M') && input.Lines[y-1][x-1].Equals('M')) 
            {
                count++;
            }
            if (input.Lines[y-1][x+1].Equals('S') && 
                input.Lines[y+1][x-1].Equals('M') && input.Lines[y-1][x-1].Equals('M')) 
            {
                count++;
            }
        }

        return count;
    }
    public async Task<string> SolveGold(PuzzleInput input)
    {
        var totalCount = 0;
        for (int y = 1; y < input.Lines.Count - 1 ; y++)
        {
            for (int x = 1; x < input.Lines[0].Length - 1; x++)
            {
                if (input.Lines[y][x].Equals('A'))
                {
                    totalCount += SearchForCrossMas(y, x, input);
                }
            }
        }
        return (totalCount / 2).ToString();
    }
}