using System;
using System.IO;
using System.Linq;

namespace D02a
{
    class Program
    {
        static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            int twos = 0, threes = 0;

            foreach (var line in lines)
            {
                var occurences = line.GroupBy(ch => ch).Select(Enumerable.Count).ToArray();

                if (occurences.Contains(2))
                    twos++;

                if (occurences.Contains(3))
                    threes++;
            }

            Console.WriteLine(twos * threes);
        }
    }
}
