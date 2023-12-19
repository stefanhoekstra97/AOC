using System.Collections;
using System.Reflection.Emit;
using System.Text;
using Infrastructure.Entities;

namespace PuzzleSolving._15_LensLibrary;

public class LensLibrary
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var hasher = new Hasher();
        var totalHashVal = 0L;
        var stringsToHash = input.Input.Split(',');
        foreach (var hashableString in stringsToHash)
        {
            totalHashVal += hasher.GetHashValueFromString(hashableString);
        }

        return totalHashVal;
    }
    
    public static long SolvePartTwo(PuzzleInput input)
    {
        var hasher = new Hasher();
        
        var buckets = new List<(string label, int focalStrength)>[256];
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = [];
        }
        var stringsToHash = input.Input.Split(',');
        foreach (var s in stringsToHash)
        {
            
            
            if (s.Last().Equals('-'))
            {
                var label = s.Split('-')[0];
                var hashed = hasher.GetHashValueFromString(label);
                if (buckets[hashed].Any(kv => kv.label.Equals(label)))
                {
                    var itemToRemove = buckets[hashed].Single(kv => kv.label.Equals(label));
                    buckets[hashed].Remove(itemToRemove);
                }
            }
            else
            {
                var label = s.Split('=')[0];
                var focalAmount = int.Parse(s.Split('=')[1]);
                var hashed = hasher.GetHashValueFromString(label);
                
                if (buckets[hashed].Any(kv => kv.label.Equals(label)))
                {
                    var indexOfLens = buckets[hashed]
                                         .Select((kv, i) => (kv, i)).Single(kkv => kkv.kv.label.Equals(label));
                    buckets[hashed][indexOfLens.i] = (indexOfLens.kv.label, focalAmount);

                }
                else
                {
                    buckets[hashed].Add((label, focalAmount));
                }
            }
            
        }

        var totalSum = 0L;
        for (int i = 0; i < buckets.Length; i++)
        {
            for (int j = 0; j < buckets[i].Count; j++)
            {
                totalSum += (i + 1) * buckets[i][j].focalStrength * (j + 1);
            }
        }
        return totalSum;
    }
    
}

internal class Hasher
{

    public int GetHashValueFromString(string s)
    {
        var hashValue = 0;
        foreach (var c in s)
        {
            hashValue += (int)c;
            hashValue *= 17;
            hashValue %= 256;
        }

        return hashValue;
    }
}