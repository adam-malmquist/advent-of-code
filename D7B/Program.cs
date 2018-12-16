using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D7B
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(GetAnswer());
        }

        private static int GetAnswer()
        {
            return new Solver(GetInstructions()).GetAnswer();

        }

        private static SortedDictionary<char, SortedSet<char>> GetInstructions()
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

            return instructions;
        }

    }

    class Solver
    {
        private readonly SortedDictionary<char, SortedSet<char>> todo;
        private readonly Dictionary<char, int> wip;
        private int time = 0;

        public Solver(SortedDictionary<char, SortedSet<char>> work)
        {
            todo = work;
            wip = new Dictionary<char, int>();
        }

        internal int GetAnswer()
        {
            var idleWorkers = 5;

            while (true)
            {
                foreach (var id in GetFinishedTasks())
                {
                    FinishTask(id);
                    ++idleWorkers;
                }

                if (IsAllDone())
                {
                    return time;
                }

                foreach (var id in GetReadyTasks())
                {
                    if (idleWorkers == 0)
                        break;

                    StartTask(id);
                    --idleWorkers;
                }

                ++time;
            }
        }

        private void FinishTask(char id)
        {
            wip.Remove(id);
            foreach (var deps in todo.Values)
                deps.Remove(id);
        }

        private void StartTask(char id)
        {
            todo.Remove(id);
            wip[id] = time + GetDuration(id);
        }

        private bool IsAllDone()
        {
            return todo.Count == 0 && wip.Count == 0;
        }

        private List<char> GetFinishedTasks()
        {
            return wip.Where(x => x.Value == time).Select(x => x.Key).ToList();
        }

        private List<char> GetReadyTasks()
        {
            return todo.Where(x => x.Value.Count == 0).Select(x => x.Key).ToList();
        }

        private static int GetDuration(char nextJob)
        {
            return 60 + nextJob - 'A' + 1;
        }
    }
}
