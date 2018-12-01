using System;
using System.IO;

namespace D01a
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            foreach (var line in File.ReadAllLines("input.txt"))
                sum += int.Parse(line);

            Console.WriteLine(sum);
        }
    }
}
