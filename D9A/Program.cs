using System;
using System.Collections.Generic;
using System.Linq;

namespace D9A
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(GetHighScore(459, 72103));
        }

        private static int GetHighScore(int numberOfPlayers, int valueOfLastMarble)
        {
            var score = new int[numberOfPlayers];
            var currentPlayer = 0;
            var currentMarble = 0;

            var marbles = new Marbles();

            while (currentMarble != valueOfLastMarble)
            {
                score[currentPlayer] += marbles.Play(++currentMarble);
                currentPlayer = (currentPlayer + 1) % numberOfPlayers;
            }

            return score.Max();
        }
    }

   class Marbles
    {
        private List<int> marbles;
        private int currentMarble;

        public Marbles()
        {
            marbles = new List<int>();
            currentMarble = 0;

            marbles.Add(0);
        }

        public int Play(int marble)
        {
            if (marble % 23 != 0)
            {
                return Normal(marble);
            }
            else
            {
                return marble + Special();
            }
        }

        public int Normal(int marble)
        {
            if (currentMarble == marbles.Count - 1)
                currentMarble = 1;
            else
                currentMarble += 2;

            marbles.Insert(currentMarble, marble);

            return 0;
        }

        public int Special()
        {
            currentMarble -= 7;
            if (currentMarble < 0)
                currentMarble = marbles.Count + currentMarble;
            int score = marbles[currentMarble];
            marbles.RemoveAt(currentMarble);

            return score;
        }
    }
}
