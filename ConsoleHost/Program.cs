// See https://aka.ms/new-console-template for more information

using Infrastructure;
using PuzzleSolving;

var input = await ReadFile.ReadFromDisk("puzzle_input", 1);
// var input = await ReadFile.ReadFromDisk("sample_hard", 1);
var solver = new CalibrateLaunch();

// var solutionEasy = solver.SolveEasy(input.Input);

var solutionHard = solver.SolveHard(input.Input);
// Console.WriteLine($"Easy: {solutionEasy}");
Console.WriteLine($"Hard: {solutionHard}");
