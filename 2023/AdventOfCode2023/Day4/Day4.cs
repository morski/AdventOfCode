using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day4
    {
        public static void Run()
        {
            var lines = File.ReadAllLines("Day4\\input");
            Part1(lines);
            Part2(lines);
        }

        private static void Part1(string[] lines)
        {
            double sum = 0;
            foreach (var line in lines)
            {
                var intersect = line.Split(':')[1].Split('|').Select(x => x.Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x))).Aggregate((previousList, nextList) => previousList.Intersect(nextList)).Count();

                if(intersect > 0)
                {
                    var points = Math.Pow(2, intersect - 1);
                    sum += points;
                }
            }
            Console.WriteLine(sum);
        }

        private static void Part2(string[] lines)
        {

            int[] totalAmountOfCards = Enumerable.Repeat(1, lines.Length).ToArray();

            foreach (var line in lines) 
            {
                var gameNumber = int.Parse(line.Split(':')[0].Split(' ')[^1]) - 1;
                var intersect = line.Split(':')[1].Split('|').Select(x => x.Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x))).Aggregate((previousList, nextList) => previousList.Intersect(nextList)).Count();
                for (int j = 0; j < totalAmountOfCards[gameNumber]; j++)
                {
                    for (int i = 0; i < intersect; i++)
                    {
                        totalAmountOfCards[gameNumber + 1 + i] += 1;
                    }
                }
            }

            Console.WriteLine(totalAmountOfCards.Sum());
        }
    }
}
