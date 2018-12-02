using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace D02b
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(GetTheId());
        }

        private static string GetTheId()
        {
            var processedIds = new HashSet<string>();

            foreach (var newId in GetAllIds())
            {
                foreach (var processedId in processedIds)
                {
                    if (DiffersByOne(newId, processedId, out var diffsRemoved))
                        return diffsRemoved;
                }

                processedIds.Add(newId);
            }

            throw new InvalidOperationException();
        }

        private static IEnumerable<string> GetAllIds()
        {
            return File.ReadLines("input.txt");
        }

        private static bool DiffersByOne(string id1, string id2, out string diffsRemoved)
        {
            int diffs = 0;
            var resultBuilder = new StringBuilder();

            for (int i = 0; i < id1.Length; ++i)
            {
                if (id1[i] == id2[i])
                {
                    resultBuilder.Append(id1[i]);
                }
                else if (++diffs > 1)
                {
                    diffsRemoved = null;
                    return false;
                }
            }

            diffsRemoved = resultBuilder.ToString();
            return true;
        }
    }
}
