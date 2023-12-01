using System.Text.Json.Serialization;

namespace Infrastructure.Entities;

[JsonSerializable(typeof(PuzzleInput))]
public class PuzzleInput
{
    [JsonPropertyName(nameof(Input))]
    public string Input { get; set; }


    public PuzzleInput(string input)
    {
        Input = input;
    }
}