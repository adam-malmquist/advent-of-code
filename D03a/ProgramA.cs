using System;
using System.IO;
using System.Text.RegularExpressions;

namespace D03a
{
    static class ProgramA
    {
        static void Main2()
        {
            var matrix = new byte[1000, 1000];
            var regex = new Regex(@"#\d+ @ (\d+),(\d+): (\d+)x(\d+)");
            var result = 0;

            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    int left = int.Parse(match.Groups[1].Value);
                    int top = int.Parse(match.Groups[2].Value);
                    int width = int.Parse(match.Groups[3].Value);
                    int height = int.Parse(match.Groups[4].Value);

                    for (int y = top; y < top + height; ++y)
                        for (int x = left; x < left + width; ++x)
                            if (++matrix[x, y] == 2)
                                ++result;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            Console.WriteLine(result);
        }
    }
}
