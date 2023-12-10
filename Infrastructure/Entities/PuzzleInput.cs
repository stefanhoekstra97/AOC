using System.Text.Json.Serialization;

namespace Infrastructure.Entities;

[JsonSerializable(typeof(PuzzleInput))]
public class PuzzleInput(string input)
{
    [JsonPropertyName(nameof(Input))]
    public string Input { get; set; } = input;

    public List<string> Lines => Input.Split("\r\n").ToList();
}