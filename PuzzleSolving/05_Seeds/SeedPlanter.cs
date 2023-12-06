using Infrastructure.Entities;
// ReSharper disable PossibleMultipleEnumeration

namespace PuzzleSolving._05_Seeds;

public static class SeedPlanter
{
    private static StreamReader GetStream(PuzzleInput input)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);

        var lineno = 0;
        foreach (string line in input.Lines)
        {
            if (lineno < 2)
            {
                lineno += 1;
                continue;
            }
            writer.WriteLine(line);
            
        }
        writer.Flush();
        stream.Position = 0;
        return new StreamReader(stream);
    }
    
    public static long SolvePartOne(PuzzleInput input)
    {
        var seeds = input.Lines[0][7..]
            .Split(" ", StringSplitOptions.TrimEntries)
            .Select(long.Parse);
        
        var reader = GetStream(input);

        var mapper = new MapOfMaps(reader);

        return seeds.Min(s => mapper.MapTo(s));
    }
    
    public static long SolvePartTwo(PuzzleInput input)
    {
        var seedsNumbers = input.Lines[0][7..]
            .Split(" ", StringSplitOptions.TrimEntries)
            .Select(long.Parse)
            .ToArray();

        var seeds = new List<(long start, long range)>();
        for (var i = 0; i < seedsNumbers.Length / 2; i++)
        {
            seeds.Add((seedsNumbers[i * 2], seedsNumbers[(i * 2) + 1]));
        }
        
        var reader = GetStream(input);

        var mapper = new MapOfMaps(reader);

        var allResults = seeds.Select(s => mapper.MapSmallestFromRange(s)).ToList();

        return allResults.Min();
    }


    private class MapOfMaps
    {
        private readonly List<MapToType> _availableMaps = new();
        
        public MapOfMaps(StreamReader linesToMap)
        {
            while (!linesToMap.EndOfStream)
            {
                var line = linesToMap.ReadLine();
                var splitMapType = line!.Replace(" map:", "").Split("-to-");

                var mapToAdd = new MapToType(linesToMap, splitMapType.First(), splitMapType.Last());
                _availableMaps.Add(mapToAdd);
            }
        }

        public long MapTo(long seedValue, string destinationType = "location")
        {
            var currentType = "seed";
            
            while (!currentType.Equals(destinationType))
            {
                var mapToUse = _availableMaps.First(m => m.FromType.Equals(currentType));

                seedValue = mapToUse.MapSource(seedValue);    
                currentType = mapToUse.ToType;
            }

            return seedValue;
        }

        public long MapSmallestFromRange((long start, long range) seed, string destinationType = "location")
        {
            var currentType = "seed";
            var ranges = new List<(long start, long range)>() { seed };
            
            while (!currentType.Equals(destinationType))
            {
                    var mapToUse = _availableMaps.First(m => m.FromType.Equals(currentType));
                    var newRanges = new List<(long start, long range)>();
                    foreach (var rangeofSeed in ranges)
                    {
                        newRanges.AddRange(mapToUse.MapFromRange(rangeofSeed));
                    }

                    ranges.Clear();
                    ranges = newRanges;
                    currentType = mapToUse.ToType;

            }

            return ranges.Min(s => s.start);
        }
    }

    private class MapToType
    {
        public readonly string FromType;
        public readonly string ToType;

        private readonly List<SeedMapping> _maps = new();

        public MapToType(StreamReader stream, string fromType, string toType)
        {
            FromType = fromType;
            ToType = toType;
            while (!stream.EndOfStream)
            {
                var line = stream.ReadLine();
                
                if (string.IsNullOrWhiteSpace(line)) break;
                
                _maps.Add(new SeedMapping(line));
            }
        }

        public long MapSource(long source)
        {
            foreach (var seedMapping in _maps.Where(seedMapping => seedMapping.Source <= source && seedMapping.Source + seedMapping.Range > source))
            {
                return source + (seedMapping.Destination - seedMapping.Source);
            }

            return source;
        }
        
        public IEnumerable<(long start, long range)> MapFromRange((long start, long range) seed)
        {
            var matchingMaps = new List<(long start, long range)>();
            foreach (var seedMapping in _maps)
            {
                // Case: Range overflows left
                if (seed.start - seedMapping.Source > 0 && (seedMapping.Source + seedMapping.Range) > seed.start)
                {
                    var newRange = Math.Min((seedMapping.Source + seedMapping.Range) - (seed.start), seed.range);
                    var newStart = seed.start + (seedMapping.Destination - seedMapping.Source);
                    matchingMaps.Add((newStart, newRange));
                }
                
                // Case: Range overflows right ( and possibly left)
                else if ((seed.start + seed.range) >= seedMapping.Source && (seedMapping.Source + seedMapping.Range) >= (seed.start + seed.range))
                {
                    long allowedRangeOffset = 0;
                    if (seed.start < seedMapping.Source)
                    {
                        allowedRangeOffset = seed.start - seedMapping.Source;
                    }
                    if (seedMapping.Source + seedMapping.Range >= seed.start + seed.range)
                    {
                        allowedRangeOffset += seed.range;
                    }
                    else
                    {
                        allowedRangeOffset += seedMapping.Range - (seedMapping.Source + seedMapping.Range) -
                                             (seed.start + seed.range);
                    }
                    matchingMaps.Add((seedMapping.Destination, allowedRangeOffset));
                }
                
                // Case: start and end are within original range
                else if (seed.start <= seedMapping.Source &&
                         seed.start + seed.range >= seedMapping.Source + seedMapping.Range)
                {
                    matchingMaps.Add((seedMapping.Destination, seedMapping.Range));
                }
            }
            return FindAndFillGaps(matchingMaps, seed, _maps).ToList();
        }

        private static IEnumerable<(long start, long range)> FindAndFillGaps(IEnumerable<(long start, long range)> mappingToFill,
            (long start, long range) seed,
            List<SeedMapping> availableMaps
            )
        {
            availableMaps.Sort((firstItem, secondItem) => firstItem.Source.CompareTo(secondItem.Source));
            var currentRanges = mappingToFill.ToList();
            currentRanges.Sort((one, two) => one.start.CompareTo(two.start)); 
            
            var startFrom = seed.start;
            var mapIndex = 0;
            
            while (startFrom < seed.start + seed.range)
            {
                if (mapIndex >= availableMaps.Count)
                {
                    mappingToFill = mappingToFill.Append((startFrom, seed.start + seed.range - startFrom));
                    break;
                }
                if (availableMaps[mapIndex].Source > startFrom)
                {
                    mappingToFill = mappingToFill.Append((startFrom, availableMaps[mapIndex].Source - startFrom));
                    startFrom = availableMaps[mapIndex].Source + availableMaps[mapIndex].Range + 1;
                    mapIndex += 1;
                }
                else
                {
                    startFrom = Math.Max(availableMaps[mapIndex].Source + availableMaps[mapIndex].Range + 1, startFrom);
                    mapIndex += 1;
                }
            }
            
            return mappingToFill;
        }
    }

    internal record SeedMapping
    {
        public readonly long Source;
        public readonly long Destination;
        public readonly long Range;
        
        public SeedMapping(string verbatimMap)
        {
            var values = verbatimMap.Split(" ").ToArray();
            Destination = long.Parse(values[0]);
            Source = long.Parse(values[1]);
            Range = long.Parse(values[2]);
        }
    }
}