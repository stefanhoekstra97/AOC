using Infrastructure.Entities;

namespace PuzzleSolving._2023._07_CamelCards;

public static class CamelCards
{
    public static long SolvePartOne(PuzzleInput input)
    {
        var allHands = input.Lines.Select(line => new CamelHand(line));

        var maxRank = input.Lines.Count;

        var sortedHandsInGroups = allHands
            .GroupBy(h => h.DetermineGroupForPartOne())
            .OrderBy(group => group.Key);

        var sumOfWinnings = 0L;
        foreach (var handsInGroup in sortedHandsInGroups)
        {
            foreach (var hand in handsInGroup.OrderBy(c => c))
            {
                sumOfWinnings += hand.GetWinnings(maxRank);
                maxRank -= 1;
            }
        }
        
        return sumOfWinnings;
    }
    
    public static long SolvePartTwo(PuzzleInput input)
    {
        var allHands = input.Lines.Select(line => new CamelHand(line, isPartOne: false));

        var maxRank = input.Lines.Count;
        var sumOfWinnings = 0L;

        var sortedHandsInGroups = allHands
            .GroupBy(h => h.DetermineGroupForPartTwo())
            .OrderBy(group => group.Key);

        foreach (var handsInGroup in sortedHandsInGroups)
        {
            foreach (var hand in handsInGroup.OrderBy(c => c))
            {
                sumOfWinnings += hand.GetWinnings(maxRank);
                maxRank -= 1;
            }
        }

        // Alternative: No impact on performance. Not readable though..
        // return sortedHandsInGroups.Sum(g => g.OrderBy(c => c).Aggregate(0L, (current, hand) => current + hand.GetWinnings(maxRank--)));
        return sumOfWinnings;
    }


    private readonly record struct CamelHand : IComparable<CamelHand>
    {
        private readonly List<CamelCard> _cards;

        private readonly int _handValue;

        public CamelHand(string lineWithHand, bool isPartOne = true)
        {
            var splitLine = lineWithHand.Split(" ");
            _handValue = int.Parse(splitLine[1]);
            _cards = splitLine[0].Select(c => new CamelCard(c, isPartOne)).ToList();
        }
        
        public CardGroup DetermineGroupForPartOne()
        {
            var threeOfAKindValue = 0;
            var twoOfAKindValue = 0;

            foreach (var currentCard in _cards)
            {
                var current = currentCard;
                var matching = _cards.Count(c => current.CompareToCard(c) == 0);

                if (threeOfAKindValue == current.CardValue ||
                    twoOfAKindValue == current.CardValue)
                {
                    // Already found the card and identified as pair / three of a kind. Continue
                    continue;
                }

                if (matching == 5)
                    // Check types: 5 of a kind
                    return CardGroup.FiveOfAKind;
                // 4 of a kind
                if (matching == 4)
                    return CardGroup.FourOfAKind;
                // 3 of a kind found
                if (matching == 3 && twoOfAKindValue != 0)
                    return CardGroup.FullHouse;
                // pair
                if (matching == 3 && threeOfAKindValue == 0)
                    threeOfAKindValue = current.CardValue;
                else if (matching == 2 && threeOfAKindValue != 0)
                    return CardGroup.FullHouse;
                else if (matching == 2 && twoOfAKindValue == 0)
                    twoOfAKindValue = current.CardValue;
                else if (matching == 2)
                    // two pair found
                    return CardGroup.TwoPair;
            }

            // Administration: If a single pair has been found, if a three of a kind, full house
            if (threeOfAKindValue != 0)
                return CardGroup.ThreeOfAKind; 
            if (twoOfAKindValue != 0)
                return CardGroup.OnePair;
            
            return CardGroup.HighCard;
        }

        public CardGroup DetermineGroupForPartTwo()
        {
            var threeOfAKindValue = 0;
            var twoOfAKindValue = 0;
            var countOfJokers = _cards.Count(c => c.CardValue == 0);
            var jokerUsed = false;
            foreach (var current in _cards)
            {
                var current1 = current;
                var matching = _cards.Count(c => current1.CompareToCard(c) == 0);
                if (current.CardValue == 0)
                {
                    continue;
                }
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
                        return countOfJokers == 1 ? CardGroup.FiveOfAKind : CardGroup.FourOfAKind;
                    // Three of the same and two jokers
                    case 3 when countOfJokers == 2:
                        return CardGroup.FiveOfAKind;
                    // Three of a kind, one joker
                    case 3 when countOfJokers == 1:
                        return CardGroup.FourOfAKind;
                    // 3 of a kind found (and a two of a kind prior)
                    case 3 when twoOfAKindValue != 0:
                        return CardGroup.FullHouse;
                    case 3 when threeOfAKindValue == 0:
                        threeOfAKindValue = current.CardValue;
                        break;
                    // pairs
                    case 2 when countOfJokers == 3:
                        return CardGroup.FiveOfAKind;
                    case 2 when countOfJokers == 2:
                        return CardGroup.FourOfAKind;
                    case 2 when countOfJokers == 1 && !jokerUsed:
                        jokerUsed = true;
                        threeOfAKindValue = current.CardValue;
                        break;
                    case 2 when threeOfAKindValue != 0:
                        return CardGroup.FullHouse;
                    case 2 when twoOfAKindValue == 0:
                        twoOfAKindValue = current.CardValue;
                        break;
                    case 2:
                        // two pair found without jokers
                        return CardGroup.TwoPair;
                }
            }

            // Administration: If a single pair has been found, if a three of a kind, full house
            if (threeOfAKindValue != 0)
            {
                // In this case we should not have jokers in the hand (or it is already used for the three of a kind)
                return CardGroup.ThreeOfAKind;
            }

            if (twoOfAKindValue != 0)
            {
                // We should not have jokers in our hand that would affect our score.
                return CardGroup.OnePair;
            }

            return countOfJokers switch
            {
                0 => CardGroup.HighCard,
                1 => CardGroup.OnePair,
                2 => CardGroup.ThreeOfAKind,
                3 => CardGroup.FourOfAKind,
                4 => CardGroup.FiveOfAKind,
                5 => CardGroup.FiveOfAKind,
                _ => CardGroup.HighCard
            };
        }
        
        public long GetWinnings(int rank) => rank * _handValue;

        int IComparable<CamelHand>.CompareTo(CamelHand other) => _cards.Select((t, i) => t.CompareToCard(other._cards.ElementAt(i))).FirstOrDefault(comp => comp != 0);
    }

    private enum CardGroup
    {
        FiveOfAKind = 1,
        FourOfAKind = 2,
        FullHouse = 3,
        ThreeOfAKind = 4,
        TwoPair = 5,
        OnePair = 6,
        HighCard = 7
    }

    private readonly struct CamelCard(char original, bool isPartOne = true)
    {
        public readonly int CardValue = char.IsDigit(original)
            ? int.Parse(original.ToString())
            : original switch
            {
                'T' => 10,
                'J' => isPartOne ? 11 : 0,
                'Q' => 12,
                'K' => 13,
                'A' => 14,
                _ => throw new ArgumentException($"Not recognized as card: {original}")
            };

        public int CompareToCard(CamelCard other)
        {
            return other.CardValue.CompareTo(this.CardValue);
        }
    }
}