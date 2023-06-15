using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace D6B
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(GetAnswer());
        }

        private static int GetAnswer()
        {
            return SizeOfSafeRegion(GetSpace(GetCoords()));
        }

        private static bool[,] GetSpace(IReadOnlyCollection<(int x, int y)> coords)
        {
            var width = coords.Max(c => c.x) + 1;
            var height = coords.Max(c => c.y) + 1;

            var space = new bool[width, height];

            for (int x = 0; x < width; ++x)
                for (int y = 0; y < height; ++y)
                    space[x, y] = IsSpaceSafe(coords, x, y);

            return space;
        }

        private static bool IsSpaceSafe(IEnumerable<(int x, int y)> coords, int x, int y)
        {
            return coords.Sum(c => GetManhattanDistance((x, y), c)) < 10000;
        }

        private static int SizeOfSafeRegion(bool[,] space)
        {
            int result = 0;

            for (int x = 0; x < space.GetLength(0); ++x)
            {
                for (int y = 0; y < space.GetLength(1); ++y)
                {
                    if (space[x, y])
                        result++;
                }
            }

            return result;
        }
        
        private static int GetManhattanDistance((int x, int y) p, (int x, int y) q)
        {
            return Math.Abs(p.x - q.x) + Math.Abs(p.y - q.y);
        }

        private static List<(int x, int y)> GetCoords()
        {
            var coords = new List<(int, int)>();

            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var parts = line.Split(',');
                coords.Add((int.Parse(parts[0]), int.Parse(parts[1])));
            }

            return coords;
        }
    }
}
