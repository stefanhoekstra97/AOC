using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;

namespace Infrastructure.Entities;

[JsonSerializable(typeof(PuzzleInput))]
public class PuzzleInput
{
    [JsonPropertyName(nameof(Input))]
    public string Input { get; set; }

    public List<string> Lines => Input.Split("\r\n").ToList();

    public PuzzleInput(string input)
    {
        Input = input;
    }
    
}