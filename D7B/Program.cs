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
            int time = 0;
            var workers = new (char job, int done)[5];


            while (instructions.Count > 0)
            {
                var unassignedJobs = new SortedSet<char>();

                foreach (var instruction in instructions.Where(kvp => kvp.Value.Count == 0))
                {
                    unassignedJobs.Add(instruction.Key);
                    for (int i = 0; i < workers.Length; ++i)
                    {
                        if (unassignedJobs.Contains(workers[i].job))
                        {
                            unassignedJobs.Remove(workers[i].job);
                            break;
                        }
                    }
                }

                for (int i = 0; i < workers.Length; ++i)
                {
                    if (unassignedJobs.Count == 0)
                        break;

                    if (workers[i].job != default(char))
                        continue;

                    var nextJob = unassignedJobs.First();
                    unassignedJobs.Remove(nextJob);
                    workers[i] = (nextJob, time + GetDuration(nextJob));
                }

                for (int i = 0; i < workers.Length; ++i)
                {
                    if (workers[i].done == time)
                    {
                        char job = workers[i].job;

                        instructions.Remove(job);
                        foreach (var dependencies in instructions.Values)
                            dependencies.Remove(job);

                        builder.Append(job);
                        workers[i] = default((char,int));
                    }
                }

                ++time;
            }

            return builder.ToString();
        }

        private static int GetDuration(char nextJob)
        {
            return 60 + nextJob - 'A' + 1;
        }
    }
}
