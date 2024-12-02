using Infrastructure.Entities;

namespace PuzzleSolving;

public interface IPuzzleSolver
{
    Task<string> SolveEasy(PuzzleInput input);
    Task<string> SolveHard(PuzzleInput input);
}