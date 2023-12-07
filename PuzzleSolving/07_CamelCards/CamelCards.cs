using Infrastructure.Entities;

namespace PuzzleSolving._07_CamelCards;

public class CamelCards
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var allHands = input.Lines.Select(line => new CamelHand(line)).ToList();

        var maxRank = allHands.Count;
        long sumOfWinnings = 0;
        

        var sortedHandsInGroups = allHands
            .GroupBy(h => h.DetermineGroup())
            .OrderBy(group => group.Key);

        foreach (var handsInGroup in sortedHandsInGroups)
        {
            // Console.WriteLine($"current group: {handsInGroup.Key}");
            var t = handsInGroup.OrderBy(c => c, new HandComparer());
            foreach (var hand in t)
            {
                // Console.WriteLine($"Current maxrank: {maxRank}, card: {hand.OriginalFormat}");
                sumOfWinnings += hand.GetWinnings(maxRank);
                maxRank -= 1;
            }
        }



        
        return sumOfWinnings;
    }

    internal class HandComparer : IComparer<CamelHand>
    {
        public int Compare(CamelHand x, CamelHand y)
        {
            if (null == x || null == y)
            {
                throw new AggregateException($"Comparer: Got null value");
            }
            return x.CompareToHand(y);
        }
    }

    internal class CamelHand
    {
        public string OriginalFormat;

        private List<CamelCard> cards;

        private int HandValue;

        private int rankInDeck;
        
        public CamelHand(string lineWithHand)
        {
            OriginalFormat = lineWithHand;
            var splitLine = lineWithHand.Split(" ");
            HandValue = int.Parse(splitLine[1]);
            cards = splitLine[0].Select(c => new CamelCard(c)).ToList();
        }

        public CardGroup DetermineGroup()
        {
            var threeOfAKindValue = 0;
            var twoOfAKindValue = 0;
            
            for (int i = 0; i < cards.Count; i++)
            {
                var current = cards[i];
                var matching = cards.Count(c => current.CompareToCard(c) == 0);

                if (threeOfAKindValue == current.CardValue ||
                    twoOfAKindValue == current.CardValue)
                {
                    // Already found the card and identified as pair / three of a kind. Continue
                    continue;
                }

                switch (matching)
                {
                    // Check types: 5 of a kind
                    case 5:
                        return CardGroup.FiveOfAKind;
                    // 4 of a kind
                    case 4:
                        return CardGroup.FourOfAKind;
                    // 3 of a kind found
                    case 3 when twoOfAKindValue != 0:
                        return CardGroup.FullHouse;
                    // pair
                    case 3 when threeOfAKindValue == 0:
                        threeOfAKindValue = current.CardValue;
                        break;
                    case 2 when threeOfAKindValue != 0:
                        return CardGroup.FullHouse;
                    case 2 when twoOfAKindValue == 0:
                        twoOfAKindValue = current.CardValue;
                        break;
                    case 2:
                        // two pair found
                        return CardGroup.TwoPair;
                }
            }

            // Administration: If a single pair has been found, if a three of a kind, full house
            return threeOfAKindValue != 0 ? CardGroup.ThreeOfAKind :
                twoOfAKindValue != 0 ? CardGroup.OnePair :
                CardGroup.HighCard;
        }


        public long GetWinnings(int rank)
        {
            return (long)rank * (long)HandValue;
        }

        public int CompareToHand(CamelHand other)
        {
            return cards.Select((t, i) => t.CompareToCard(other.cards[i])).FirstOrDefault(comp => comp != 0);
        }

    }

    internal enum CardGroup
    {
        FiveOfAKind = 1,
        FourOfAKind = 2,
        FullHouse = 3,
        ThreeOfAKind = 4,
        TwoPair = 5,
        OnePair = 6,
        HighCard = 7
    }
    internal class CamelCard
    {
        public string OriginalCardCharacter;
        public int CardValue;
        
        public CamelCard(char original)
        {
            OriginalCardCharacter = original.ToString();
            if (char.IsDigit(original))
            {
                CardValue = int.Parse(original.ToString());
            }
            else
            {
                CardValue = original switch
                {
                    'T' => 10,
                    'J' => 11,
                    'Q' => 12,
                    'K' => 13,
                    'A' => 14,
                    _ => throw new ArgumentException($"Not recognized as card: {original}")
                };
            }
        }

        public int CompareToCard(CamelCard other)
        {
            return other.CardValue.CompareTo(this.CardValue);
        }
    }
}