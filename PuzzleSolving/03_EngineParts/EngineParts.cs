using Infrastructure.Entities;

namespace PuzzleSolving._03_EngineParts;

internal class GearChar
{
    private int GearVerticalIndex { get; }
    private int GearHorizontalIndex { get; }

    public int GearPower
    {
        get
        {
            if (_partNumbers.Count == 2)
            {
                return _partNumbers.First() * _partNumbers.Last();
            }

            return 0;
        }
    }

    private readonly List<int> _partNumbers = new();
    public GearChar(int gearHorizontalindex, int gearVerticalIndex)
    {
        GearVerticalIndex = gearVerticalIndex;
        GearHorizontalIndex = gearHorizontalindex;
    }

    public void DetermineGearPower(PuzzleInput input)
    {
        if (GearVerticalIndex > 0)
        {
            ScanForParts(input.Lines[GearVerticalIndex - 1]);
        }

        if (GearVerticalIndex < input.Lines.Count - 1)
        {
            ScanForParts(input.Lines[GearVerticalIndex + 1]);
        }
        ScanForParts(input.Lines[GearVerticalIndex]);
    }

    private void ScanForParts(string line)
    {
        if (char.IsDigit(line[GearHorizontalIndex]))
        {
            var foundNumber = GetFullPartNumber(line, GearHorizontalIndex);
            if (!_partNumbers.Contains(foundNumber))
            {
                _partNumbers.Add(foundNumber);
            }
        }

        if (line.Length > GearHorizontalIndex + 1 && char.IsDigit(line[GearHorizontalIndex + 1]))
        {
            var foundNumber = GetFullPartNumber(line, GearHorizontalIndex + 1);
            if (!_partNumbers.Contains(foundNumber))
            {
                _partNumbers.Add(foundNumber);
            }
        }

        if (GearHorizontalIndex <= 0 || !char.IsDigit(line[GearHorizontalIndex - 1])) return;
        {
            var foundNumber = GetFullPartNumber(line, GearHorizontalIndex - 1);
            if (!_partNumbers.Contains(foundNumber))
            {
                _partNumbers.Add(foundNumber);
            }
        }
    }

    private static int GetFullPartNumber(string partLine, int startFrom)
    {
        var startOfPartIndex = startFrom;
        var endOfPartIndex = startFrom;
        while (char.IsDigit(partLine[startOfPartIndex]))
        {
            startOfPartIndex -= 1;
        }
        while (char.IsDigit(partLine[endOfPartIndex]))
        {
            endOfPartIndex += 1;
        }
        
        return int.Parse(partLine.Substring(startOfPartIndex + 1, endOfPartIndex - startOfPartIndex - 1));
    }
}

public class EngineParts
{
    public static int SolveHard(PuzzleInput input)
    {
        var listOfGears = new List<GearChar>();
        foreach (var lineWithIndex in input.Lines.Select((value, index) => (value, index))
                     .Where(tuple => tuple.value.Contains('*')))
        {
            listOfGears.AddRange(lineWithIndex.value.Select((gearChar, gearIndex) => (gearChar, gearIndex))
                .Where(tuple => tuple.gearChar.Equals('*'))
                .Select(gearChar => new GearChar(gearChar.gearIndex, lineWithIndex.index)));
        }
        listOfGears.AsParallel().ForAll(g => g.DetermineGearPower(input));
        
        return listOfGears.Sum(g => g.GearPower);
    }
    
    public static int SolveEasy(PuzzleInput input)
    {
        var sumOfParts = 0;
        foreach (var lineWithIndex in input.Lines.Select((value, i) => (value, i)))
        {
            var startOfNumber = 0;
            foreach (var charWithHorizontalIndex in lineWithIndex.value.Select((character, i) => (character, i)))
            {
                if (IsPartOfNumber(charWithHorizontalIndex.character))
                {
                    startOfNumber += 1;
                }
                else
                {
                    if (startOfNumber <= 0) continue;
                    var isAddedToPartsSum = false;
                    var partValue = int.Parse(lineWithIndex.value.Substring(charWithHorizontalIndex.i - startOfNumber, startOfNumber));
                    // Determine if it is part of engine partnumber
                    var left = charWithHorizontalIndex.i - startOfNumber - 1;
                    if (left < 0)
                    {
                        left = 0;
                    }
                        
                    if (lineWithIndex.i > 0)
                    {
                        // Check row above
                        var rowAbove = input.Lines[lineWithIndex.i - 1];
                        var stringToSearch = rowAbove!.Substring(left, startOfNumber + 2);
                        if (!isAddedToPartsSum && stringToSearch.Any(IsSpecialCharacter))
                        {
                            sumOfParts += partValue;
                            isAddedToPartsSum = true;
                        }
                    }

                    if (!isAddedToPartsSum && lineWithIndex.value.Substring(left, startOfNumber + 2)
                            .Any(IsSpecialCharacter))
                    {
                        sumOfParts += partValue;
                        isAddedToPartsSum = true;
                    } 

                    if (lineWithIndex.i < (input.Lines.Count - 1))
                    {
                        var rowBelow = input.Lines[lineWithIndex.i + 1];
                        var stringToSearch = rowBelow!.Substring(left, startOfNumber + 2);
                        if (!isAddedToPartsSum && stringToSearch.Any(IsSpecialCharacter))
                        {
                            sumOfParts += partValue;
                        }
                    }
                    startOfNumber = 0;
                }
            }
        }
        return sumOfParts;
    }

    private static bool IsSpecialCharacter(char c)
    {
        return !c.Equals('.') && !char.IsDigit(c);
    }

    private static bool IsPartOfNumber(char c)
    {
        return char.IsDigit(c) && !c.Equals('.');
    }
}