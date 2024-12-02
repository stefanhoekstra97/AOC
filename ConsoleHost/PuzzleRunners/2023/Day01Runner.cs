using Infrastructure.Readers;
using PuzzleSolving._2023._01_calibrate_launch;

namespace ConsoleHost.PuzzleRunners;

public static class Day01Runner
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
    public static async Task TestHardW()
    {
        var solver = new CalibrateLaunch();
        var input = await ReadFile.ReadFromDisk("WouterInput", 1);
        var inputTransformed = await ReadFile.ReadFromDisk("WouterTransformed", 1);

        var solutionHard = solver.SolveHard(input.Input);
        var solutionTransformed = solver.SolveHard(inputTransformed.Input);

        Console.WriteLine($"Hard: {solutionHard}");
        Console.WriteLine($"Hard transformed: {solutionTransformed}");

    }
}