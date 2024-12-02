using Infrastructure.Entities;

namespace PuzzleSolving;

public interface IPuzzleSolver
{
    Task<string> SolveSilver(PuzzleInput input);
    Task<string> SolveGold(PuzzleInput input);
}