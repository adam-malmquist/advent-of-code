using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace D12B
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine(GetAnswer());
        }

        static long GetAnswer()
        {
            var (initial, rules) = ParseInput(File.ReadAllLines("input.txt"));
            var (offset, state) = new Pots(initial, rules).GetGeneration(50000000000);

            long result = 0;
            for (int i = 0; i < state.Length; ++i)
            {
                if (state[i] == '#')
                    result += i + offset;
            }

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
        private readonly string initial;
        private readonly Dictionary<string, char> rules;

        public Pots(string initial, Dictionary<string, char> rules)
        {
            this.initial = initial;
            this.rules = rules;
        }

        public (long offset, string pots) GetGeneration(long n)
        {
            var state = InitialPots();
            var previous = default(string);

            for (long i = 0; i < n; ++i)
            {
                previous = state.pots;
                state = UpdatedPots(state);

                if (state.pots == previous)
                    return (state.offset + n - (i + 1), state.pots);
            }

            return state;
        }

        private (long offset, string pots) InitialPots()
        {
            return (0, initial);
        }

        private (long offset, string pots) UpdatedPots((long offset, string pots) state)
        {
            var newState = GetNextState($".....{state.pots}.....");
            var newOffset = state.offset + (newState.IndexOf('#') - 5);

            newState = newState.Trim('.');

            return (newOffset, newState);
        }

        private string GetNextState(string state)
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
