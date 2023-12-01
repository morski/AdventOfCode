// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("Day1\\input");
var timer = new Stopwatch();
timer.Start();
Day1Part1(lines);
timer.Stop();
Console.WriteLine($"Part 1: {timer.ElapsedMilliseconds} ms");
timer.Restart();
Day1Part2(lines);
timer.Stop();
Console.WriteLine($"Part 2: {timer.ElapsedMilliseconds} ms");

void Day1Part1(string[] lines) {
    var sum = 0;
    foreach (var line in lines)
    {
        string digit = "";

        for (int i = 0; i < line.Length; i++)
        {
            if (Char.IsDigit(line[i]))
            {
                digit += line[i];
                break;
            }
        }

        for (int i = line.Length - 1; i >= 0; i--)
        {
            if (Char.IsDigit(line[i]))
            {
                digit += line[i];
                break;
            }
        }

        sum += int.Parse(digit);
    }

    Console.WriteLine(sum);
}


void Day1Part2(string[] lines)
{
    var sum = 0;

    string[] digits = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    foreach (var line in lines)
    {
        string value = "";

        var firstIndex = int.MaxValue;
        var firstValue = "";
        for(int i = 0; i < digits.Length; i++)
        {
            var index = line.IndexOf(digits[i]);
            if (index < firstIndex && index != -1)
            {
                firstIndex = index;
                firstValue = (i + 1).ToString();
            }
        }

        for (int i = 0; i < line.Length; i++)
        {
            if (Char.IsDigit(line[i]))
            {
                if(i < firstIndex)
                {
                    firstValue = line[i].ToString();
                }
                break;
            }
        }


        var secondIndex = int.MinValue;
        var secondValue = "";

        for (int i = 0; i < digits.Length; i++)
        {
            var index = line.LastIndexOf(digits[i]);
            if (index > secondIndex)
            {
                secondIndex = index;
                secondValue = (i + 1).ToString();
            }
        }

        for (int i = line.Length - 1; i >= 0; i--)
        {
            if (Char.IsDigit(line[i]))
            {
                if (i > secondIndex)
                {
                    secondValue = line[i].ToString();
                }
                break;
            }
        }

        value = firstValue + secondValue;
        sum += int.Parse(value);
    }

    Console.WriteLine(sum);
}

