using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D7A
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(GetAnswer());
        }

        private static string GetAnswer()
        {
            var instructions = new SortedDictionary<char, SortedSet<char>>();

            foreach (var line in File.ReadAllLines("input.txt"))
            {
                char id = line[36], dependsOn = line[5];

                if (!instructions.ContainsKey(id))
                    instructions.Add(id, new SortedSet<char>());

                if (!instructions.ContainsKey(dependsOn))
                    instructions.Add(dependsOn, new SortedSet<char>());

                instructions[id].Add(dependsOn);
            }

            var builder = new StringBuilder();

            while (instructions.Count > 0)
            {
                var (nonBlocked, _) = instructions.First(kvp => kvp.Value.Count == 0);

                instructions.Remove(nonBlocked);
                foreach (var dependencies in instructions.Values)
                    dependencies.Remove(nonBlocked);

                builder.Append(nonBlocked);
            }

            return builder.ToString();
        }
    }
}
