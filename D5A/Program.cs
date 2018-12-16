using System;
using System.Collections.Generic;
using System.IO;

namespace D5A
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(GetAnswer(GetInput()));
        }

        static string GetInput()
        {
            return File.ReadAllText("input.txt");
        }

        static int GetAnswer(string input)
        {
            var polymers = new Stack<char>();

            foreach (var unit in input)
            {
                if (!char.IsLetter(unit))
                    continue;

                if (polymers.Count == 0 || !Reacts(unit, polymers.Peek()))
                    polymers.Push(unit);
                else
                    polymers.Pop();
            }

            return polymers.Count;
        }

        private static bool Reacts(char u1, char u2)
        {
            return SameType(u1, u2) && !SamePolarity(u1, u2);
        }

        private static bool SameType(char u1, char u2)
        {
            return char.ToLower(u1) == char.ToLower(u2);
        }

        private static bool SamePolarity(char u1, char u2)
        {
            var p1 = char.IsLower(u1);
            var p2 = char.IsLower(u2);

            return p1 == p2;
        }
    }
}
