using Infrastructure.Entities;

namespace Infrastructure.Readers;

public interface IReadFile
{
    public Task<PuzzleInput> ReadFromDisk(string filename, int day);
}