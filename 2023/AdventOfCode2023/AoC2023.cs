using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class AoC2023
    {
        static void Main(string[] args)
        {
            
            BenchmarkRunner.Run<Day1>();
            Run(new Day1());
            //BenchmarkRunner.Run<Day2>();
            //Day2.Run();
            //Day3.Run();
            //Day4.Run();
            //Day5.Run();
            //Day6.Run();
            //Day7.Run();
            //Day8.Run();
            //Day9.Run();
            //Day10.Run();
            //Day11.Run();
            //Day12.Run();
            //Day13.Run();
            //Day14.Run();
            //Day15.Run();
            //Day16.Run();
            //Day17.Run();
            //Day18.Run();
            //Day19.Run();
            //Day20.Run();
            //Day21.Run();
            //Day22.Run();
            //Day23.Run();
            //Day24.Run();
            //Day25.Run();
        }

        private static void Run(Day day)
        {
            Console.WriteLine("Part 1");
            Console.WriteLine(day.Part1());
            Console.WriteLine("Part 2");
            Console.WriteLine(day.Part2());
        }
    }
}
