using Infrastructure.Entities;
using Infrastructure.Readers;

namespace Infrastructure;

public static class ReadFile
{
    public static async Task<PuzzleInput> ReadFromDisk(string filename, int day)
    {
        StreamReader file = new StreamReader($"Resources/{day:D2}/{filename}.txt");

        var puzzleInput = new PuzzleInput(await file.ReadToEndAsync());
        return puzzleInput;
    }
}