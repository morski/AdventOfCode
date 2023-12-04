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
                var winningNumber = line.Split(':')[1].Split('|')[0].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();
                var myNumbers = line.Split(':')[1].Split('|')[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();

                var intersect = winningNumber.Intersect(myNumbers).Count();

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
                var x = line.Split(':')[0].Split(' ');
                var cardNumber = int.Parse(x[x.Length - 1]) - 1;
                var winningNumber = line.Split(':')[1].Split('|')[0].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();
                var myNumbers = line.Split(':')[1].Split('|')[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                var intersect = winningNumber.Intersect(myNumbers).Count();
                for (int j = 0; j < totalAmountOfCards[cardNumber]; j++)
                {
                    for (int i = 0; i < intersect; i++)
                    {
                        totalAmountOfCards[cardNumber + 1 + i] += 1;
                    }
                }
            }

            Console.WriteLine(totalAmountOfCards.Sum());
        }
    }
}
