using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace D7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetAnswer());
        }

        private static int GetAnswer()
        {
            var coords = GetCoords();
            var space = GetSpace(coords);

            PopulateSpace(space, coords);

            var areas = GetAreaSizes(space);

            return areas.Values.Max();
        }

        private static Dictionary<int, int> GetAreaSizes(int[,] space)
        {
            var areas = new Dictionary<int, int>();
            var excluded = new HashSet<int>();

            for (int x = 0; x < space.GetLength(0); ++x)
            {
                for (int y = 0; y < space.GetLength(1); ++y)
                {
                    var owner = space[x, y];
                    if (owner != 0)
                    {
                        if (TouchesOuterSpaceBorder(x, y, space))
                            excluded.Add(owner);

                        if (!areas.ContainsKey(owner))
                            areas[owner] = 0;

                        areas[owner]++;
                    }
                }
            }

            foreach (var excludeThisOwner in excluded)
                areas.Remove(excludeThisOwner);

            return areas;
        }

        private static bool TouchesOuterSpaceBorder(int x, int y, int[,] space)
        {
            return x == 0 || y == 0 || x == space.GetLength(0) - 1 || y == space.GetLength(1) - 1;
        }

        private static void PopulateSpace(int[,] space, List<(int x, int y)> coords)
        {
            for (int x = 0; x < space.GetLength(0); ++x)
            {
                for (int y = 0; y < space.GetLength(1); ++y)
                {
                    if (space[x, y] == 0)
                        space[x, y] = GetClaimedBy(space, coords, x, y);
                }
            }
        }

        private static int GetClaimedBy(int[,] space, IEnumerable<(int x, int y)> coords, int x, int y)
        {
            int result = 0;
            var closest = coords.GroupBy(c => GetManhattanDistance((x, y), c)).OrderBy(g => g.Key).First();

            if (closest.Count() == 1)
            {
                var positionOfClosest = closest.Single();
                result = space[positionOfClosest.x, positionOfClosest.y];
            }

            return result;
        }

        private static int GetManhattanDistance((int x, int y) p, (int x, int y) q)
        {
            return Math.Abs(p.x - q.x) + Math.Abs(p.y - q.y);
        }

        private static int[,] GetSpace(IReadOnlyCollection<(int x, int y)> coords)
        {
            var space = new int[coords.Max(c => c.x) + 1, coords.Max(c => c.y) + 1];
            var id = 0;

            foreach (var (x, y) in coords)
                space[x, y] = ++id;

            return space;
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
