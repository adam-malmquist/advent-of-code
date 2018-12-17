using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace D10A
{
    class Program
    {
        static void Main()
        {
            var input = GetInput();

            Console.SetBufferSize(400, 400);

            int time = 0;
            while (true)
            {
                Print(input, time++);
                //Console.ReadKey(true);
            }
        }

        static void Print(List<Point> points, int time)
        {
            if (time == 10596)
            {
                Console.Clear();
            }

            Console.Clear();

            for (int i = 0; i < points.Count; ++i)
            {
                var p = points[i];
                if (p.x >= 0 && p.y >= 0 && p.x < Console.BufferWidth && p.y < Console.BufferWidth)
                {
                    Console.SetCursorPosition(p.x, p.y);
                    Console.Write('#');
                }
                p.Tick();
            }
        }

        static List<Point> GetInput()
        {
            var result = new List<Point>();
            var regex = new Regex("position=<(.*),(.*)> velocity=<(.*),(.*)>");

            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var match = regex.Match(line);
                var p = new Point
                {
                    x = int.Parse(match.Groups[1].Value),
                    y = int.Parse(match.Groups[2].Value),
                    vx = int.Parse(match.Groups[3].Value),
                    vy = int.Parse(match.Groups[4].Value)
                };
                result.Add(p);
            }

            return result;
        }
    }

    public class Point
    {
        public int x;
        public int y;
        public int vx;
        public int vy;

        public Point() { }

        public Point(int x, int y, int vx, int vy)
        {
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
        }

        public void Tick()
        {
            x += vx;
            y += vy;
        }
    }
}
