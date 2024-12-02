using Infrastructure.Entities;

namespace PuzzleSolving._2023._04_ScratchCards;

public class ScratchCards
{
    public static int SolvePartOne(PuzzleInput input)
    {
        var values = input.Lines.Select(line =>
        {
            var cards = line.Split(": ").Last().Split(" | ");
            var winningNumbers = new ScratchCardObj(cards[0]);
            var myCard = new ScratchCardObj(cards[1]);
            return myCard.GetValue(winningNumbers);
        });

        return values.Sum();
    }

    public static int SolvePartTwo(PuzzleInput input)
    {
        var cardCounter = Enumerable.Range(1, input.Lines.Count).Select(n => 1).ToArray();
        
        foreach (var line in input.Lines.Select((value, i) => (value, i)))
        {
            var cards = line.value.Split(": ").Last().Split(" | ");
            var winningNums = new ScratchCardObj(cards[0]);
            var myCard = new ScratchCardObj(cards[1]);
            var score = myCard.GetNumberOfMatches(winningNums);
            for (var i = 1; i < score + 1; i++)
            {
                cardCounter[i + line.i] += cardCounter[line.i];
            }
        }

        return cardCounter.Sum();
    }

    public static int SolvePartTwoAlternative(PuzzleInput input)
    {
        var cardCounter = Enumerable.Range(1, input.Lines.Count).Select(n => 1).ToArray();
        foreach (var line in input.Lines.Select((value, i) => (value, i)))
        {
            var cards = line.value.Split(": ").Last().Split(" | ");
            
            var winningNums = cards[0].Split(" ").Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(int.Parse);
            
            var myCard = cards[1].Split(" ").Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(int.Parse);
            var score = winningNums.Count(n => myCard.Contains(n));
            var curIndexVal = cardCounter[line.i];
            for (var i = 1; i < score + 1; i++)
            {
                cardCounter[i + line.i] += curIndexVal;
            }
        }

        return cardCounter.Sum();
    }
    
    internal class ScratchCardObj
    {
        private readonly IEnumerable<int> _numbers;

        public ScratchCardObj(string numbersOnCard)
        {
            _numbers = numbersOnCard
                .Split(" ", StringSplitOptions.TrimEntries)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(int.Parse);
        }

        public int GetValue(ScratchCardObj winningNumbers)
        {
            var power = GetNumberOfMatches(winningNumbers);
            power -= 1;
            if (power == -1)
            {
                return 0;
            }

            return (int)Math.Pow(2, power);
        }

        public int GetNumberOfMatches(ScratchCardObj winningNumbers)
        {
            return _numbers.Count(number => winningNumbers._numbers.Contains(number));
        }
    }
}