using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        public int Part21()
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

        public int Part2()
        {
            var seeds = lines[0].Split(':')[1].Trim().Split(' ').Select(x => long.Parse(x)).ToList();
            var seedRanges = new List<Tuple<long, long>>();

            for(var i = 0; i < seeds.Count; i+=2)
            {
                seedRanges.Add(new Tuple<long, long>(seeds[i], seeds[i] + seeds[i + 1]));
            }

            List<List<List<long>>> almanac = new();
            List<List<long>> almanac2 = new();

            for (int i = 3; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    i += 1;
                    almanac.Add(almanac2.OrderBy(x => x[0]).ToList());
                    almanac2.RemoveAll(x => true);
                    continue;
                }

                var parts = lines[i].Split(' ').Select(x => long.Parse(x)).ToList();

                almanac2.Add(parts);
            }

            almanac.Add(almanac2.OrderBy(x => x[0]).ToList());
            almanac.Reverse();

            List<long> result = new();

            var blocks = 1000000;

            var timer = new Stopwatch();
            timer.Start();
            for (long i = 0; i < long.MaxValue; i += blocks)
            {
                if(result.Count > 0)
                {
                    break;
                }

                Parallel.For(i, i + blocks, (i, state) =>
                {
                    if (state.IsStopped)
                    {
                        return;
                    }
                    var map = almanac[0].FirstOrDefault(x => x[0] <= i && x[0] + x[2] - 1 >= i);

                    var number = i;

                    if (map != null)
                    {
                        var difference = i - map[0];
                        number = map[1] + difference;
                    }

                    bool valid = FindPrevious(number, almanac, 1, seedRanges);
                    if (valid)
                    {
                        if(number > 0)
                        {
                            result.Add(number);
                        }
                    }
                });                
            }

            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            Console.WriteLine("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));

            return int.Parse(result.Min().ToString());
        }

        private bool FindPrevious(long currentNumber, List<List<List<long>>> almanac, int index, List<Tuple<long, long>> seedRanges)
        {
            if(index == almanac.Count)
            {
                var found = false;
                foreach(var seedRange in seedRanges)
                {
                    if( currentNumber >= seedRange.Item1 && currentNumber <= seedRange.Item2)
                    {
                        found = true;
                    }
                }

                return found;
            }

            var map = almanac[index].FirstOrDefault(x => x[0] <= currentNumber && x[0] + x[2] - 1 >= currentNumber);

            if(map != null)
            {
                var difference = currentNumber - map[0];
                currentNumber = map[1] + difference;
                return FindPrevious(currentNumber, almanac, index+1, seedRanges);
            }
            else
            {
                return FindPrevious(currentNumber, almanac, index+1, seedRanges);
            }
            
            
        }
    }
}
