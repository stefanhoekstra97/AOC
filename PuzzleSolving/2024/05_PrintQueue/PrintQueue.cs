namespace PuzzleSolving._2024._05_PrintQueue;

using Infrastructure.Entities;

public class PrintQueue : IPuzzleSolver
{
    private List<(int, int)>? _instructions;
    
    public Task<string> SolveSilver(PuzzleInput input)
    {
        var lines = input.Lines;
        var total = 0;

        (Dictionary<int, List<int>> leftRules, Dictionary<int, List<int>> rightRules, int end) = GetPrintingRules(lines);
        
        for (var j = end; j < lines.Count; j++)
        {
            var parts = lines[j].Split(",").Select(int.Parse).ToList();
            if (IsRuleValid(leftRules, rightRules, parts))
            {
                total += parts[parts.Count / 2];
            }

        }
        
        return Task.FromResult($"{total}");
    }

    public Task<string> SolveGold(PuzzleInput input)
    {
        var lines = input.Lines;
        var total = 0;

        (List<(int, int)> instr, int end) = TuplePrinterInstructions(lines);
        _instructions = instr;
        
        
        for (var j = end; j < lines.Count; j++)
        {
            var parts = lines[j].Split(",").Select(int.Parse).ToList();

            parts.Sort(MagicSortingHat);
            total += parts[parts.Count / 2];
            
        }
        
        return Task.FromResult($"{total}");
    }
    
    private bool IsRuleValid(Dictionary<int, List<int>> leftRules, Dictionary<int, List<int>> rightRules, List<int> instruction)
    {
        
        for (var i = 0; i < instruction.Count - 1; i++)
        {
            for (int j = i + 1; j < instruction.Count; j++)
            {
                if (leftRules.ContainsKey(instruction[j]) )
                {
                    if (leftRules[instruction[j]].Contains(instruction[i]))
                    {
                        return false;
                    }
                }

                if (rightRules.ContainsKey(instruction[i]))
                {
                    if (rightRules[instruction[i]].Contains(instruction[j]))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
    
    private int MagicSortingHat(int i, int j)
    {
        if (_instructions == null)
        {
            throw new Exception("Instructions not set");
        }
        
        if (_instructions.Contains((j, i)))
        {
            return 1;
        } 
        else if (_instructions.Contains((i, j)))
        {
            return -1;
        }
        return 0;
    }

    private (List<(int a, int b)>, int end) TuplePrinterInstructions(List<string> lines)
    {
        var i = 0;
        List<(int, int)> instructions = [];
        
        
        while (!string.IsNullOrEmpty(lines[i]))
        {
            var parts = lines[i].Split("|").Select(int.Parse).ToList();
            instructions.Add((parts[0], parts[1]));
            
            i++;
        }

        return (instructions, i + 1);
    }
    private (Dictionary<int, List<int>> leftRules, Dictionary<int, List<int>> rightRules, int end) GetPrintingRules(List<string> lines)
    {
        var i = 0;
        Dictionary<int, List<int>> printingRulesL = new();
        Dictionary<int, List<int>> printingRulesR = new();
        
        
        while (!string.IsNullOrEmpty(lines[i]))
        {
            var parts = lines[i].Split("|").Select(int.Parse).ToList();
            if (!printingRulesL.TryGetValue(parts[0], out List<int>? lefties))
            {
                printingRulesL[parts[0]] = [parts[1]];
            }
            else
            {
                lefties.Add(parts[1]);
            }
            if (!printingRulesR.TryGetValue(parts[1], out List<int>? righties))
            {
                printingRulesR[parts[1]] = [parts[0]];
            }
            else
            {
                righties.Add(parts[0]);
            }
            
            i++;
        }

        return (printingRulesL, printingRulesR, i + 1);
    }
    
    
}