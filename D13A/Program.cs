using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace D13A
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllLines("input.txt");

            var width = input[0].Length;
            var height = input.Length;

            var track = new char[width, height];
            var carts = new List<Cart>();

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    var c = input[y][x];

                    if (IsCart(c))
                    {
                        carts.Add(new Cart(x, y, c));
                        c = IsHorizontal(c) ? '-' : '|';
                    }

                    track[x, y] = c;
                }
            }

            while (true)
            {
                var newPositions = new HashSet<(int, int)>();
                foreach (var cart in carts.OrderBy(c => c.y).ThenBy(c => c.x).ToArray())
                {
                    cart.Update(track);
                    if (!newPositions.Add((cart.x, cart.y)))
                    {
                        Console.WriteLine($"{cart.x},{cart.y}");
                        return;
                    }
                }
            }
        }

        private static bool IsCart(char v)
        {
            return IsHorizontal(v) || IsVertical(v);
        }

        private static bool IsHorizontal(char c)
        {
            return c == '<' || c == '>';
        }

        private static bool IsVertical(char c)
        {
            return c == '^' || c == 'v';
        }
    }

    public class Cart
    {
        private static readonly Dictionary<char, char> leftTurns = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> rightTurns = new Dictionary<char, char>();

        static Cart()
        {
            leftTurns['<'] = 'v';
            leftTurns['>'] = '^';
            leftTurns['^'] = '<';
            leftTurns['v'] = '>';

            rightTurns['<'] = '^';
            rightTurns['>'] = 'v';
            rightTurns['^'] = '>';
            rightTurns['v'] = '<';
        }

        public int x, y;
        public char direction, lastTurn;

        public Cart(int x, int y, char direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;

            lastTurn = 'R';
        }

        public void Update(char[,] track)
        {
            UpdatePosition();

            switch (track[x, y])
            {
                case '+':
                    Turn();
                    break;
                case '/':
                    if (direction == '<' || direction == '>')
                        TurnLeft();
                    else
                        TurnRight();
                    break;
                case '\\':
                    if (direction == '<' || direction == '>')
                        TurnRight();
                    else
                        TurnLeft();
                    break;
            }
        }

        private void UpdatePosition()
        {
            if (direction == '>')
                ++x;
            else if (direction == '<')
                --x;
            else if (direction == '^')
                --y;
            else if (direction == 'v')
                ++y;
        }

        public void Turn()
        {
            var nextTurn = GetNextTurn();

            if (nextTurn == 'L')
                TurnLeft();
            else if (nextTurn == 'R')
                TurnRight();

            lastTurn = nextTurn;
        }

        public void TurnLeft()
        {
            direction = leftTurns[direction];
        }

        public void TurnRight()
        {
            direction = rightTurns[direction];
        }

        private char GetNextTurn()
        {
            switch (lastTurn)
            {
                case 'L': return 'S';
                case 'S': return 'R';
                case 'R': return 'L';

                default: throw new InvalidOperationException();
            }
        }
    }
}
