using System;
using System.IO;
using System.Linq;

namespace advent_of_code
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1.Solution();
            Day1.SolutionPart2();
        }
    }

    class Day1
    {
        public static void Solution()
        {
            double[] lines = File.ReadAllLines("./data/input_day1.txt").Select(n => double.Parse(n)).Distinct().ToArray();
            foreach (double n in lines)
            {
                foreach (double m in lines)
                {
                    if (n + m == 2020 && n != m)
                    {
                        Console.WriteLine(n * m);
                    }
                }
            }
        }

        public static void SolutionPart2()
        {
            double[] lines = File.ReadAllLines("./data/input_day1.txt").Select(n => double.Parse(n)).Distinct().ToArray();
            foreach (double n in lines)
            {
                foreach (double m in lines)
                {
                    foreach (double l in lines)
                    {
                        if (n + m + l == 2020)
                        {
                            Console.WriteLine(n * m * l);
                        }
                    }
                }
            }
        }
    }
}
