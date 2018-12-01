using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace D01b
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(GetRepeatingFrequency());
        }

        private static int GetRepeatingFrequency()
        {
            var reachedFrequencies = new HashSet<int>();
            var frequencies = GetFrequencies();
            var sum = 0;

            while (true)
            {
                foreach (var frequency in frequencies)
                {
                    if (!reachedFrequencies.Add(sum += frequency))
                        return sum;
                }
            }
        }

        private static IEnumerable<int> GetFrequencies()
        {
            return File.ReadAllLines("input.txt").Select(int.Parse).ToArray();
        }
    }
}
