using System;
using System.IO;
using System.Linq;

namespace D8A
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(GetAnswer());
        }

        private static int GetAnswer()
        {
            var tree = File.ReadAllText("input.txt").Split(' ').Select(int.Parse).ToArray();
            return Sum(tree).sum;
        }

        private static (int sum, int skip) Sum(int[] tree, int n = 0)
        {
            (int sum, int skip) result = (0, 2);

            int children = tree[n], entries = tree[n+1];

            for (int i = 0; i < children; ++i)
            {
                var c = Sum(tree, n + result.skip);
                result.sum  += c.sum;
                result.skip += c.skip;
            }

            for (int i = 0; i < entries; ++i)
            {
                result.sum  += tree[n + result.skip + i];
            }

            result.skip += entries;

            return result;
        }
    }
}
