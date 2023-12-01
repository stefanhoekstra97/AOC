using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using PuzzleSolving;

namespace AOC2023.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DayOneController : ControllerBase
{
    private readonly CalibrateLaunch _solver = new();

    [HttpPost("/Easy")]
    public Task<IActionResult> SolveEasy([FromBody] PuzzleInput input)
    {
        var inputForPuzzle = input.Input.Replace(" ", "\r\n");
        return Task.FromResult<IActionResult>(Ok(new
        {
            value = _solver.SolveEasy(inputForPuzzle)
        }));
    }
    
    [HttpPost("/Hard")]
    public Task<IActionResult> SolveHard([FromBody] PuzzleInput input)
    {
        var inputForPuzzle = input.Input.Replace(" ", "\r\n");
        return Task.FromResult<IActionResult>(Ok(new
        {
            value = _solver.SolveHard(inputForPuzzle)
        }));
    }
}