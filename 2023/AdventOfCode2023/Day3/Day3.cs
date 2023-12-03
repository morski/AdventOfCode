using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2023
{
    public static class Day3
    {
        static readonly List<Tuple<int, int>> UsedCoordinates = new List<Tuple<int, int>>();
        public static void Run()
        {
            var lines = File.ReadAllLines("Day3\\input");
            Part1(lines);
            Part2(lines);
        }

        private static void Part1(string[] lines)
        {
            var grid = lines.Select(l => l.Select(c => c).ToList()).ToList();

            var result = 0;

            for(int y = 0; y < grid.Count; y++)
            {
                for(int x = 0;  x < grid[y].Count; x++)
                {
                    if (!Char.IsDigit(grid[y][x]) && grid[y][x] != '.')
                    {
                        var numbers = GetConnectedNumbers(y, x, grid);
                        result += numbers.Sum();
                    }
                }
            }
 
            Console.WriteLine(result);
        }

        private static List<int> GetConnectedNumbers(int y, int x, List<List<char>> grid) 
        {
            List<int> foundNumbers = new();

            if(y > 0)
            {
                foundNumbers.Add(GetWholeNumber(y - 1, x, grid));

                if (x > 0)
                {
                    foundNumbers.Add(GetWholeNumber(y - 1, x - 1, grid));
                }

                if (x != grid[y].Count - 1)
                {
                    foundNumbers.Add(GetWholeNumber(y - 1, x + 1, grid));
                }
                
            }
            

            if(y != grid.Count - 1)
            {
                foundNumbers.Add(GetWholeNumber(y + 1, x, grid));

                if (x > 0)
                {
                    foundNumbers.Add(GetWholeNumber(y + 1, x - 1, grid));
                }

                if (x != grid[y].Count - 1)
                {
                    foundNumbers.Add(GetWholeNumber(y + 1, x + 1, grid));
                }
            }

            if(x > 0)
            {
                foundNumbers.Add(GetWholeNumber(y, x - 1, grid));
            }
            
            if(x != grid[y].Count - 1)
            {
                foundNumbers.Add(GetWholeNumber(y, x + 1, grid));
            }

            UsedCoordinates.RemoveAll(u => true);

            return foundNumbers;
        }

        private static int GetWholeNumber(int y, int x, List<List<char>> grid)
        {
            if (!Char.IsDigit(grid[y][x]) || UsedCoordinates.Any(c => c.Item1 == y && c.Item2 == x))
            {
                return 0;
            }

            var numberStartX = x;
            for (int i = x - 1; i >= 0; i--)
            {
                if (Char.IsDigit(grid[y][i]))
                {
                    numberStartX = i;
                }
                else
                {
                    break;
                }
            }
            var number = string.Empty;
            for (int i = numberStartX; i < grid[y].Count; i++)
            {
                if (Char.IsDigit(grid[y][i]))
                {
                    number += grid[y][i];
                    UsedCoordinates.Add(new Tuple<int, int>(y,i));
                }
                else
                {
                    break;
                }
            }



            return int.Parse(number);
        }

        private static void Part2(string[] lines)
        {
            var grid = lines.Select(l => l.Select(c => c).ToList()).ToList();

            var result = 0;

            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    if (grid[y][x] == '*')
                    {
                        var numbers = GetConnectedNumbers(y, x, grid);
                        if(numbers.Where(n => n > 0).Count() == 2)
                        {
                            result += numbers.Where(n => n > 0).Aggregate((a, x) => a * x);
                        }
                    }
                }
            }

            Console.WriteLine(result);
        }
    }
}
