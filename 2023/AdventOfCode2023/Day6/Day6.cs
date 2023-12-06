using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day6 : Day
    {
        private readonly string[] lines;

        public Day6()
        {
            lines = File.ReadAllLines("Day6\\input");
        }

        public int Part1()
        {
            var result = 1;
            var times = lines[0].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
            var records = lines[1].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();

            for(int i = 0; i < times.Count; i++)
            {
                int possibilities = 0;

                for (int j = 0; j <= times[i]; j++)
                {
                    var speed = j;
                    var raceTime = times[i] - j;

                    var distance = raceTime * speed;

                    if(distance > records[i])
                    {
                        possibilities++;
                    }
                }

                result *= possibilities;
            }


            return result;
        }

        public int Part2()
        {
            var result = 0;
            var time = long.Parse(lines[0].Split(':')[1].Trim().Replace(" ", string.Empty));
            var record = long.Parse(lines[1].Split(':')[1].Trim().Replace(" ", string.Empty));

            for (long speed = 0; speed <= time; speed++)
            {
                var raceTime = time - speed;
                var distance = raceTime * speed;

                if (distance > record)
                {
                    result++;
                }
            }


            return result;
        }
    }
}
