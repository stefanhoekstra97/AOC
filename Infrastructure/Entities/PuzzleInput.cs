namespace Infrastructure.Entities;

public class PuzzleInput(string input)
{
    public string Input { get; set; } = input;

    public List<string> Lines => Input.Split("\r\n").ToList();
}