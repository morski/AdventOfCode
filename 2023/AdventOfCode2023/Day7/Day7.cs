using PEFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day7 : Day
    {
        private readonly string[] lines;

        public Day7()
        {
            lines = File.ReadAllLines("Day7\\input");
        }

        public int Part1()
        {
            return CalculateWinnings(false);
        }

        public int Part2()
        {
            return CalculateWinnings(true);
        }

        private int CalculateWinnings(bool part2)
        {
            var result = 0;

            var hands = lines.Select(line => new Hand(line, part2)).ToList();

            var orderedHands = hands.OrderByDescending(x => x.Type)
                .ThenByDescending(x => x.Cards[0])
                .ThenByDescending(x => x.Cards[1])
                .ThenByDescending(x => x.Cards[2])
                .ThenByDescending(x => x.Cards[3])
                .ThenByDescending(x => x.Cards[4]).ToList();

            for (int i = orderedHands.Count - 1; i >= 0; i--)
            {
                result += (orderedHands.Count - i) * orderedHands[i].Bid;
            }

            return result;
        }
    }

    class Hand
    {
        public List<int> Cards = new();

        public int Bid;

        public int Type;

        public Hand(string hand, bool part2)
        {
            var handAndBid = hand.Split(' ');
            Bid = int.Parse(handAndBid[1]);
            foreach (var card in handAndBid[0])
            {
                Cards.Add(CardToInt(card, part2));
            }

            Type = GetHandType(part2);
        }

        private static int CardToInt(char card, bool part2)
        {
            return card switch
            {
                'T' => 10,
                'J' => part2 ? 1 : 11,
                'Q' => 12,
                'K' => 13,
                'A' => 14,
                _ => card - '0',
            };
        }

        private int GetHandType(bool part2)
        {
            var groupedCards = Cards.GroupBy(x => x).OrderByDescending(grp => grp.Count()).ToDictionary(group => group.Key, group => group.ToList());

            if (part2 && groupedCards.Any(x => x.Key == 1))
            {
                var jokerAmount = groupedCards.First(x => x.Key == 1).Value.Count;
                if(jokerAmount < 5)
                {
                    groupedCards.First(x => x.Key != 1).Value.AddRange(Enumerable.Repeat(1, jokerAmount));

                    groupedCards.Remove(1);
                }
            }

            //Five of a kind
            if(groupedCards.Count == 1)
            {
                return 7;
            }

            //Four of a kind
            if(groupedCards.Any(x => x.Value.Count == 4)) 
            {
                return 6;
            }

            // Full house
            if(groupedCards.Any(x => x.Value.Count == 3) && groupedCards.Any(x => x.Value.Count == 2))
            {
                return 5;
            }

            //Three of a kind
            if(groupedCards.Any(x => x.Value.Count == 3) && groupedCards.Count == 3)
            {
                return 4;
            }

            //Two pairs
            if(groupedCards.Count(x => x.Value.Count == 2) == 2)
            {
                return 3;
            }

            //One Pair
            if(groupedCards.Count == 4)
            {
                return 2;
            }

            return 1;
        }
    }
}
