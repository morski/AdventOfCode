using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day5 : Day
    {
        private readonly string[] lines;

        public Day5()
        {
            lines = File.ReadAllLines("Day5\\input");
        }

        [Benchmark]
        public int Part1()
        {
            var seeds = lines[0].Split(':')[1].Trim().Split(' ').ToList();

            var seedLocations = new List<long>();

            foreach(var seed in seeds)
            {
                var currentNumber = long.Parse(seed);
                bool found = false;
                for (int i = 3; i < lines.Length; i++)
                {
                    if (string.IsNullOrEmpty(lines[i]))
                    {
                        i += 1;
                        //Console.WriteLine(currentNumber);
                        found = false;
                        continue;
                    }

                    var parts = lines[i].Split(' ').Select(x => long.Parse(x)).ToList();

                    if (parts[1] <= currentNumber && parts[1] + parts[2] - 1 >= currentNumber && !found)
                    {
                        var difference = currentNumber - parts[1];
                        currentNumber = parts[0] + difference;
                        found = true;
                    }                    
                }

                seedLocations.Add(currentNumber);
            }

            return int.Parse(seedLocations.Min().ToString());
        }

        [Benchmark]
        public int Part2()
        {
            var seeds = lines[0].Split(':')[1].Trim().Split(' ').Select(x => long.Parse(x)).ToList();

            List<List<List<long>>> almanac = new();
            List<List<long>> almanac2 = new();
            for (int i = 3; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    i += 1;
                    almanac.Add(almanac2.ToList());
                    almanac2.RemoveAll(x => true);
                    continue;
                }

                var parts = lines[i].Split(' ').Select(x => long.Parse(x)).ToList();

                almanac2.Add(parts);
            }
            almanac.Add(almanac2.ToList());
            var timer = new Stopwatch();
            timer.Start();
            long lowestNumber = long.MaxValue;
            for(var x = 0; x < seeds.Count; x += 2)
            {
                Parallel.For(seeds[x], seeds[x] + seeds[x + 1],
                    index =>
                    {
                        var currentNumber = index;
                        for (int q = 0; q < almanac.Count; q++)
                        {
                            var map = almanac[q].FirstOrDefault(x => x[1] <= currentNumber && x[1] + x[2] - 1 >= currentNumber);
                            if (map != null)
                            {
                                var difference = currentNumber - map[1];
                                currentNumber = map[0] + difference;
                            }
                        }

                        if (currentNumber < lowestNumber)
                        {
                            lowestNumber = currentNumber;
                        }
                    }
                );
                Console.WriteLine(((x + 2) * 10) + " %");
            }
            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            Console.WriteLine("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));
            Console.WriteLine(lowestNumber.ToString());
            return int.Parse(lowestNumber.ToString());
        }
    }
}
