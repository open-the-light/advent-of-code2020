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
            Day3.Solution();
            Day3.SolutionPart2();
        }
    }

    class Day3
    {
        public static void Solution()
        {
            string[] input = File.ReadAllLines("./data/input_day3.txt").ToArray();
            int trees = 0, row = 0, col = 0;
            int maxLength = input[0].Length;
            int maxRows = input.Length;
            while (++row < maxRows)
            {
                col += 3;
                if (col >= maxLength) { col -= maxLength; }
                if (input[row][col] == '#') { trees++; }
            }
            Console.WriteLine(trees);
        }

        public static void SolutionPart2()
        {
            string[] input = File.ReadAllLines("./data/input_day3.txt").ToArray();
            double move1 = CountTrees(input, 1, 1);
            double move2 = CountTrees(input, 1, 3);
            double move3 = CountTrees(input, 1, 5);
            double move4 = CountTrees(input, 1, 7);
            double move5 = CountTrees(input, 2, 1);
            double solution = checked(move1 * move2 * move3 * move4 * move5);
            Console.WriteLine(solution);
        }

        static double CountTrees(string[] pattern, int rowInc, int colInc)
        {
            double trees = 0;
            int row = 0, col = 0;
            int maxLength = pattern[0].Length;
            int maxRows = pattern.Length;
            while ((row += rowInc) < maxRows)
            {
                col += colInc;
                if (col >= maxLength) { col -= maxLength; }
                if (pattern[row][col] == '#') { trees++; }
            }
            return trees;
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
                        goto LoopEnd;
                    }
                }
            }

        LoopEnd:
            Console.WriteLine("Done!");
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
                            goto LoopEnd;
                        }
                    }
                }
            }

        LoopEnd:
            Console.WriteLine("Done!");
        }
    }
}
