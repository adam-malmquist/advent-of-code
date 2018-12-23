using System;
using System.IO;

namespace D13A
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllLines("input.txt");

            foreach (var line in input)
                Console.WriteLine(line);
        }
    }
}
