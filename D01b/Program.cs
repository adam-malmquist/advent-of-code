using System;
using System.Collections.Generic;
using System.IO;

namespace D01b
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetRepeatingFrequency());
        }

        private static int GetRepeatingFrequency()
        {
            int sum = 0;
            var frequencies = new List<int>();
            var lines = File.ReadAllLines("input.txt");

            while (true)
            {
                foreach (var line in lines)
                {
                    sum += int.Parse(line);
                    if (!frequencies.Contains(sum))
                    {
                        frequencies.Add(sum);
                    }
                    else
                    {
                        return sum;
                    }
                }
            }
        }
    }
}
