using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day4 : Day
    {
        private readonly string[] lines;

        public Day4()
        {
            lines = File.ReadAllLines("Day4\\input");
        }

        [Benchmark]
        public int Part1()
        {
            //Onliner =D
            //var x = lines.Select(l => l.Split(':')[1].Split('|').Select(x => x.Trim().Split(' ').Where(x => x != "")).Aggregate((x, y) => x.Intersect(y)).Count()).Where(x => x > 0).Select(x => Math.Pow(2, x - 1)).Sum();

            // Maybe a bit more readable 
            int sum = 0;
            
            foreach (var line in lines)
            {
                var intersect = line.Split(':')[1].Split('|').Select(x => x.Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x))).Aggregate((previousList, nextList) => previousList.Intersect(nextList)).Count();

                if(intersect > 0)
                {
                    var points = (int)Math.Pow(2, intersect - 1);
                    sum += points;
                }
            }

            return sum;
            //return x;
        }

        [Benchmark]
        public int Part2()
        {
            int[] totalAmountOfCards = Enumerable.Repeat(1, lines.Length).ToArray();

            foreach (var line in lines) 
            {
                var gameNumber = int.Parse(line.Split(':')[0].Split(' ')[^1]) - 1;
                var intersect = line.Split(':')[1].Split('|').Select(x => x.Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x))).Aggregate((previousList, nextList) => previousList.Intersect(nextList)).Count();
                for (int i = 0; i < intersect; i++)
                {
                    totalAmountOfCards[gameNumber + 1 + i] += 1 * totalAmountOfCards[gameNumber];
                }
            }

            return totalAmountOfCards.Sum();
        }
    }
}
