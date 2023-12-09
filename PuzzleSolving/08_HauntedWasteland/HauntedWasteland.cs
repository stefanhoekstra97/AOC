using System.Collections.Immutable;
using Infrastructure.Entities;

namespace PuzzleSolving._08_HauntedWasteland;

public static class HauntedWasteland
{
    public static int SolvePartOne(PuzzleInput input)
    {
        var instructions = input.Lines[0];
        var map = CreateDesertMap(input);

        var currentNode = "AAA";
        var numOfSteps = 0;
        while (!currentNode.Equals("ZZZ"))
        {
            var success = map.TryGetValue(currentNode, out var to);
            if (!success) throw new ArgumentException($"No mapping found for key {currentNode}");
            currentNode = instructions[numOfSteps % instructions.Length] switch
            {
                'L' => to.left,
                'R' => to.right,
                _ => throw new ArgumentException(
                    $"Not parseable instr: {instructions[numOfSteps % instructions.Length]}")
            };

            numOfSteps++;
        }

        return numOfSteps;
    }

    public static long SolvePartTwo(PuzzleInput input)
    {
        var instructions = input.Lines[0];
        var map = CreateDesertMap(input);

        var startingNodes = map.Keys.Where(key => key.Last().Equals('A'));
        var numOfSteps = 0L;
        while (!CheckForEnd(startingNodes))
        {
            var indexOfInstr = (int)(numOfSteps % instructions.Length);

            var newNodes = startingNodes.Select(node => map.First((kv) => kv.Key.Equals(node)))
                .Select(mapping => instructions[indexOfInstr] switch
                {
                    'L' => mapping.Value.left,
                    'R' => mapping.Value.right,
                    _ => throw new ArgumentException($"Not parseable instr: {indexOfInstr}")
                });

            startingNodes = newNodes;
            numOfSteps++;
        }

        return numOfSteps;
        bool CheckForEnd(IEnumerable<string> currentNodes) => currentNodes.All(id => id.Last().Equals('Z'));
    }


    private static Dictionary<string, (string left, string right)> CreateDesertMap(PuzzleInput input)
    {
        var map = new Dictionary<string, (string left, string right)>(input.Lines.Count);
        for (int i = 2; i < input.Lines.Count; i++)
        {
            var line = input.Lines[i].Replace("(", "").Replace(")", "").Replace(" = ", " ").Replace(",", "").Split(" ");
            map.Add(line[0], (line[1], line[2]));
        }

        return map;
    }

    
    public static long SolvePartOneAlternative(PuzzleInput input)
    {
        var graph = new DesertGraph(input, false);
        return graph.GetFirstEndingSpot();

    }
    
    public static long SolvePartTwoAlternative(PuzzleInput input)
    {
        var graph = new DesertGraph(input, true);
        return graph.GetFirstEndingSpot();
    }
}

internal record DesertGraph
{
    private readonly IEnumerable<Node> _currentNodes;
    private readonly Dictionary<Node, Node> _leftMap = new();
    private readonly Dictionary<Node, Node> _rightMap = new();

    private readonly string _walkOrder;

    public DesertGraph(PuzzleInput input, bool isPartTwo)
    {
        _walkOrder = input.Lines[0];
        for (var i = 2; i < input.Lines.Count; i++)
        { 
            var splitLine = input.Lines[i].Replace("(", "").Replace(")", "").Replace(" = ", " ").Replace(",", "").Split(" ");
            _leftMap.Add(new Node(splitLine[0], isPartTwo), new Node(splitLine[1], isPartTwo));
            _rightMap.Add(new Node(splitLine[0], isPartTwo), new Node(splitLine[2], isPartTwo));
        }

        _currentNodes = _leftMap.Where(pair => pair.Key.IsAtStart).Select(pair => pair.Key);
    }

    public long GetFirstEndingSpot()
    {
        List<long> numbers = new();
        foreach (var node in _currentNodes)
        {
            var iter = 0L;
            var currentNode = node;
            while (!currentNode.IsAtEnd)
            {
                currentNode = _walkOrder[(int)(iter % _walkOrder.Length)] == 'L' 
                    ? _leftMap.First(n => n.Key.Label.Equals(currentNode.Label)).Value 
                    : _rightMap.First(n => n.Key.Label.Equals(currentNode.Label)).Value;
                iter++;
            }
            numbers.Add(iter);
        }

        return numbers.Aggregate(1L, (l, l1) => FindLCM(l, l1));

    }

    private static long FindLCM(long a, long b)
    {
        long num1, num2;

        a /= 283;
        b /= 283;
        if (a > b)
        {
            num1 = a;
            num2 = b;
        }
        else
        {
            num1 = b;
            num2 = a;
        }


        for (var i = 1; i <= num2; i++)
        {
            if ((num1 * i) % num2 == 0)
            {
                return i * num1;
            }
        }
        return num2;
    }
}

internal record Node(string Label, bool IsPartTwo = false)
{
    public bool IsAtEnd => IsPartTwo ? Label.Last().Equals('Z') : Label.Equals("ZZZ");
    public bool IsAtStart => IsPartTwo ? Label.Last().Equals('A') : Label.Equals("AAA");
}