using System;
using System.IO;
using System.Linq;

namespace D01a
{
    class Program
    {
        static void Main()
        {
            var result = File.ReadAllLines("input.txt").Sum(int.Parse);

            Console.WriteLine(result);
        }
    }
}
