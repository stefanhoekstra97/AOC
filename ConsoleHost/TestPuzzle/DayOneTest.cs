using Infrastructure;
using PuzzleSolving;

namespace ConsoleHost.TestPuzzle;

public static class DayOneTest
{
    public static async Task TestEasy()
    {
        var input = await ReadFile.ReadFromDisk("puzzle_input", 1);
        var solver = new CalibrateLaunch();

        var solutionEasy = solver.SolveEasy(input.Input);

        Console.WriteLine($"Easy: {solutionEasy}");
    }

    public static async Task TestHard()
    {
        var solver = new CalibrateLaunch();
        var input = await ReadFile.ReadFromDisk("puzzle_input", 1);

        var solutionHard = solver.SolveHard(input.Input);

        Console.WriteLine($"Hard: {solutionHard}");

    }
}