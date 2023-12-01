﻿namespace PuzzleSolving;

public class CalibrateLaunch
{
    public int SolveEasy(string input)
    {
        return input.Split("\r\n").Sum(line => int.Parse($"{line.First(char.IsDigit)}{line.Last(char.IsDigit)}"));
    }

    public int SolveHard(string? input)
    {
        return SolveEasy(input
            .Replace("one", "o1ne")
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