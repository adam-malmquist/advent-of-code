using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace D03a
{
    static class ProgramB
    {
        static void Main()
        {
            var claimedBy = new int[1000, 1000];
            var intactClaims = new HashSet<int>();
            var regex = new Regex(@"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)");

            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var match = regex.Match(line);

                if (!match.Success)
                {
                    throw new InvalidOperationException();
                }

                int id = int.Parse(match.Groups[1].Value);
                int left = int.Parse(match.Groups[2].Value);
                int top = int.Parse(match.Groups[3].Value);
                int width = int.Parse(match.Groups[4].Value);
                int height = int.Parse(match.Groups[5].Value);

                bool conflict = false;

                for (int y = top; y < top + height; ++y)
                {
                    for (int x = left; x < left + width; ++x)
                    {
                        if (claimedBy[x, y] == 0)
                        {
                            claimedBy[x, y] = id;
                        }
                        else
                        {
                            intactClaims.Remove(claimedBy[x, y]);
                            conflict = true;
                        }
                    }
                }

                if (!conflict)
                {
                    intactClaims.Add(id);
                }
            }

            Console.WriteLine(intactClaims.Single());
        }
    }
}
