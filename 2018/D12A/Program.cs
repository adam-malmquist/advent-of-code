using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace D12A
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine(GetAnswer());
        }

        static int GetAnswer()
        {
            var (initial, rules) = ParseInput(File.ReadAllLines("input.txt"));

            var pots = new Pots(initial, rules);
            var generation = pots.GetGeneration(20);

            var (negatives, positives) = generation.SplitToTuple(Pots.Marker);

            int result = 0;
            for (int i = 0; i < negatives.Length; ++i)
                if (negatives[i] == '#')
                    result -= negatives.Length - i;

            for (int i = 0; i < positives.Length; ++i)
                if (positives[i] == '#')
                    result += i;

            return result;
        }

        private static (string, Dictionary<string,char>) ParseInput(string[] input)
        {
            var initial = ParseInitialState(input);
            var rules = ParseRules(input);

            return (initial, rules);
        }

        private static string ParseInitialState(string[] input)
        {
            var (_, initial) = input[0].SplitToTuple(":");
            return initial;
        }

        private static Dictionary<string, char> ParseRules(string[] input)
        {
            var rules = new Dictionary<string, char>();

            for (int i = 2; i < input.Length; ++i)
            {
                var (pattern, result) = input[i].SplitToTuple("=>");
                rules[pattern] = Convert.ToChar(result);
            }

            return rules;
        }

        private static (string s1, string s2) SplitToTuple(this string s, string separator)
        {
            var split = s.Split(separator);
            return (split[0].Trim(), split[1].Trim());
        }
    }

    public class Pots
    {
        public const string Marker = "-";

        private readonly string initial;
        private readonly Dictionary<string, char> rules;

        public Pots(string initial, Dictionary<string, char> rules)
        {
            this.initial = initial;
            this.rules = rules;
        }

        public string GetGeneration(int n)
        {
            return n == 0 ? OriginalPots() : UpdatedPots(GetGeneration(n - 1));
        }

        private string OriginalPots()
        {
            return Marker + initial;
        }

        private string UpdatedPots(string state)
        {
            if (!state.StartsWith("....."))
                state = "....." + state;

            if (!state.EndsWith("....."))
                state += ".....";

            var markerPosition = state.IndexOf(Marker);

            state = state.Remove(markerPosition, Marker.Length);
            state = UpdatedPotsWithoutMarker(state);
            state = state.Insert(markerPosition, Marker);

            return state;
        }

        private string UpdatedPotsWithoutMarker(string state)
        {
            var builder = new StringBuilder();

            builder.Append(state, 0, 2);

            for (int i = 0; i + 4 < state.Length; ++i)
            {
                var part = state.Substring(i, 5);
                builder.Append(rules[part]);
            }

            builder.Append(state, state.Length - 2, 2);

            return builder.ToString();
        }
    }
}
