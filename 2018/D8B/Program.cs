using System;
using System.IO;
using System.Linq;

namespace D8B
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
            var valueOfChildren = new int[children];

            for (int i = 0; i < children; ++i)
            {
                var (sum, skip) = Sum(tree, n + result.skip);
                valueOfChildren[i] = sum;
                result.skip += skip;
            }

            for (int i = 0; i < entries; ++i)
            {
                int v = tree[n + result.skip + i];
                if (children == 0)
                    result.sum += v;
                else if (v != 0 && v <= children)
                    result.sum += valueOfChildren[v - 1];
            }

            result.skip += entries;

            return result;
        }
    }
}
