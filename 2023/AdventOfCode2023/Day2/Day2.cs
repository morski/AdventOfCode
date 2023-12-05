using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day2 : Day
    {
        private readonly string[] lines;

        public Day2()
        {
            lines = File.ReadAllLines("Day2\\input");
        }

        [Benchmark]
        public int Part1()
        {
            var maxColors = new Dictionary<string, int>()
            {
                { "blue", 14 },
                { "green", 13 },
                { "red", 12 },
            };

            var idSum = 0;

            foreach (var line in lines)
            {
                bool gamePossible = true;

                var game = line.Split(':');
                var id = int.Parse(game[0].Split(' ')[1]);

                var rounds = game[1].Split(";");

                foreach (var round in rounds)
                {
                    var colors = round.Split(',');

                    foreach (var color in colors)
                    {
                        var colorInfo = color.Trim().Split(" ");
                        if (int.Parse(colorInfo[0]) > maxColors[colorInfo[1]])
                        {
                            gamePossible = false;
                        }
                    }
                }

                if (gamePossible)
                {
                    idSum += id;
                }


            }

            return idSum;
        }

        [Benchmark]
        public int Part2()
        {
            var sum = 0;

            foreach (var line in lines)
            {
                var game = line.Split(':');
                var rounds = game[1].Split(";");

                var minAmounts = new Dictionary<string, int>()
                {
                    { "blue", 0 },
                    { "green", 0 },
                    { "red", 0 },
                };

                foreach (var round in rounds)
                {
                    var colors = round.Split(',');

                    foreach (var color in colors)
                    {
                        var colorInfo = color.Trim().Split(" ");

                        if (minAmounts[colorInfo[1]] < int.Parse(colorInfo[0]))
                        {
                            minAmounts[colorInfo[1]] = int.Parse(colorInfo[0]);
                        }
                    }
                }

                sum += minAmounts.Aggregate(1, (x, y) => x * y.Value);
            }

            return sum;
        }
    }
}
