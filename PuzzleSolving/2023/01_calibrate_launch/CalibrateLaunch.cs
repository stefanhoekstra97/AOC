namespace PuzzleSolving._2023._01_calibrate_launch;

public class CalibrateLaunch
{
    public int SolveEasy(string input)
    {
        return input.Split("\r\n").AsParallel().Sum(line => int.Parse($"{line.First(char.IsDigit)}{line.Last(char.IsDigit)}"));
    }
    
    public int SolveHard(string input)
    {
        return SolveEasy(input
            .Replace("one", "o1e")
            .Replace("two", "t2o")
            .Replace("three", "t3e")
            .Replace("four", "4")
            .Replace("five", "5e")
            .Replace("six", "6")
            .Replace("seven", "7n")
            .Replace("eight", "e8t")
            .Replace("nine", "n9e")
        );
    }
    
    public int SolveEasySlower(string input)
    {
        return input.Split("\r\n").Sum(line => int.Parse($"{line.First(char.IsDigit)}{line.Last(char.IsDigit)}"));
    }
    
    public int SolveHardSlower(string input)
    {
        return SolveEasySlower(input
            .Replace("one", "o1e")
            .Replace("two", "t2o")
            .Replace("three", "t3e")
            .Replace("four", "4")
            .Replace("five", "5e")
            .Replace("six", "6")
            .Replace("seven", "7n")
            .Replace("eight", "e8t")
            .Replace("nine", "n9e")
        );
    }
}