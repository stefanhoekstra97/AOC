using Infrastructure.Entities;

namespace Infrastructure.Readers;

public static class ReadFile
{
    public static async Task<PuzzleInput> ReadFromDisk(string filename, int day, int year = 2023)
    {
        StreamReader file = new StreamReader($"Resources/{year}/{day:D2}/{filename}.txt");

        var puzzleInput = new PuzzleInput(await file.ReadToEndAsync());
        return puzzleInput;
    }
}
