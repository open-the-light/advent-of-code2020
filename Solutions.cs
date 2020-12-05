using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace advent_of_code
{
    class Program
    {
        static void Main(string[] args)
        {
            Day5.Solution();
        }
    }

    class Day5
    {
        public static void Solution()
        {
            string[] input = File.ReadAllLines("./data/input_day5.txt").ToArray();
            int maxValue = 0;
            List<int> seats = new List<int>();
            foreach (string ticket in input)
            {
                int minR = 1;
                int maxR = 128;
                int minC = 1;
                int maxC = 8;
                foreach (char letter in ticket)
                {
                    switch (letter)
                    {
                        case 'F':
                            maxR -= (maxR - minR + 1) / 2;
                            break;
                        case 'B':
                            minR += (maxR - minR + 1) / 2;
                            break;
                        case 'R':
                            minC += (maxC - minC + 1) / 2;
                            break;
                        case 'L':
                            maxC -= (maxC - minC + 1) / 2;
                            break;
                    }
                }
                int ID = (minR - 1) * 8 + (minC - 1);
                seats.Add(ID);
                maxValue = ID > maxValue ? ID : maxValue;
            }
            seats = seats.OrderBy(s => s).ToList();
            for (int i = 0; i < seats.Count - 1; i++)
            {
                if (seats[i] != seats[i + 1] - 1)
                {
                    Console.WriteLine(seats[i] + 1);
                }
            }
        }
    }

    class Day4
    {
        public static void Solution()
        {
            string[] input = File.ReadAllText("./data/input_day4.txt").Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            Regex fields = new Regex(@"([a-z]+):");
            int valid = 0;
            foreach (string document in input)
            {
                MatchCollection matches = fields.Matches(document);
                if (matches.Count == 8 || (matches.Count == 7 && !matches.Cast<Match>().Select(m => m.Value).ToList().Contains("cid:")))
                {
                    valid++;
                }
            }
            Console.WriteLine(valid);
        }

        public static void SolutionPart2()
        {
            string[] input = File.ReadAllText("./data/input_day4.txt").Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            Regex fields = new Regex(@"([a-z]+):");
            int valid = 0;

            List<string> eyes = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            Regex birthYear = new Regex(@"byr:(\d+)\s*");
            Regex issueYear = new Regex(@"iyr:(\d+)\s*");
            Regex expYear = new Regex(@"eyr:(\d+)\s*");
            Regex height = new Regex(@"hgt:([\d\w]+)\s*");
            Regex hair = new Regex(@"hcl:#[0-9a-f]{6}\s*");
            Regex eye = new Regex(@"ecl:([0-9A-Za-z]+)\s*");
            Regex pid = new Regex(@"pid:[0-9]{9}");

            foreach (string document in input)
            {
                MatchCollection matches = fields.Matches(document);
                if (matches.Count == 8 || (matches.Count == 7 && !matches.Cast<Match>().Select(m => m.Value).ToList().Contains("cid:")))
                {
                    int year = int.Parse(birthYear.Match(document).Groups[1].ToString());
                    if (year < 1920 || year > 2002) { continue; }
                    int iyear = int.Parse(issueYear.Match(document).Groups[1].ToString());
                    if (iyear < 2010 || iyear > 2020) { continue; }
                    int eyear = int.Parse(expYear.Match(document).Groups[1].ToString());
                    if (eyear < 2020 || eyear > 2030) { continue; }

                    string h = height.Match(document).Groups[1].ToString();
                    if (h.Contains("in"))
                    {
                        int hV = int.Parse(h.Replace("in", ""));
                        if (hV < 59 || hV > 76) { continue; }
                    }
                    else if (h.Contains("cm"))
                    {
                        int hV = int.Parse(h.Replace("cm", ""));
                        if (hV < 150 || hV > 193) { continue; }
                    }
                    else { continue; }

                    if (!hair.IsMatch(document)) { continue; }
                    if (!eyes.Contains(eye.Match(document).Groups[1].ToString())) { continue; }
                    if (!pid.IsMatch(document)) { continue; }
                    Console.WriteLine("\n");
                    Console.WriteLine(pid.Match(document));
                    valid++;
                }
            }
            Console.WriteLine(valid);
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
