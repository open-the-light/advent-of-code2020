using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace advent_of_code
{
    class Program
    {
        static void Main(string[] args)
        {
            Day2.Solution();
            Day2.SolutionPart2();
        }
    }

    class Day2
    {
        public static void Solution()
        {
            string[] input = File.ReadAllLines("./data/input_day2.txt").ToArray();
            Regex getMin = new Regex(@"^\d+");
            Regex getMax = new Regex(@"-(\d+)");
            Regex getLetter = new Regex(@" (\w):");
            Regex getPassword = new Regex(@": (\w+)$");

            int validPasswords = 0;
            foreach (string line in input)
            {
                int min = int.Parse(getMin.Match(line).Groups[0].Value);
                int max = int.Parse(getMax.Match(line).Groups[1].Value);
                char letter = char.Parse(getLetter.Match(line).Groups[1].Value);
                string password = getPassword.Match(line).Groups[1].Value;

                int count = password.Count(l => l == letter);
                if (count >= min && count <= max) { validPasswords++; }
            }
            Console.WriteLine(validPasswords);
        }

        public static void SolutionPart2()
        {
            string[] input = File.ReadAllLines("./data/input_day2.txt").ToArray();
            Regex getMin = new Regex(@"^\d+");
            Regex getMax = new Regex(@"-(\d+)");
            Regex getLetter = new Regex(@" (\w):");
            Regex getPassword = new Regex(@": (\w+)$");

            int validPasswords = 0;
            foreach (string line in input)
            {
                int min = int.Parse(getMin.Match(line).Groups[0].Value);
                int max = int.Parse(getMax.Match(line).Groups[1].Value);
                char letter = char.Parse(getLetter.Match(line).Groups[1].Value);
                string password = getPassword.Match(line).Groups[1].Value;

                if (password[min - 1] == letter ^ password[max - 1] == letter) { validPasswords++; }
            }
            Console.WriteLine(validPasswords);
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
                        break;
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
                            break;
                        }
                    }
                }
            }
        }
    }
}
