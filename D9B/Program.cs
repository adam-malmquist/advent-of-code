using System;
using System.Collections.Generic;
using System.Linq;

namespace D9B
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(GetHighScore(459, 72103100));
        }

        private static long GetHighScore(int numberOfPlayers, int valueOfLastMarble)
        {
            var score = new long[numberOfPlayers];
            var currentPlayer = 0;
            var currentMarble = 0;

            var marbles = new Marbles();

            while (currentMarble != valueOfLastMarble)
            {
                score[currentPlayer] += marbles.Play(++currentMarble);
                currentPlayer++;
                if (currentPlayer == numberOfPlayers)
                    currentPlayer = 0;
            }

            return score.Max();
        }
    }

   class Marbles
    {
        private LinkedList<int> marbles;
        private int currentMarble;
        private LinkedListNode<int> currentNode;

        public Marbles()
        {
            marbles = new LinkedList<int>();
            currentMarble = 0;

            currentNode = marbles.AddFirst(0);
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
            {
                currentMarble = 1;
                currentNode = marbles.AddAfter(marbles.First, marble);
            }
            else
            {
                currentMarble += 2;
                currentNode = marbles.AddAfter(currentNode.Next, marble);
            }

            return 0;
        }

        public int Special()
        {
            currentMarble -= 7;
            if (currentMarble < 0)
            {
                currentMarble = marbles.Count + currentMarble;
                currentNode = marbles.First;
                for (int i = 0; i < currentMarble; ++i)
                    currentNode = currentNode.Next;
            }
            else
            {
                for (int i = 0; i < 7; ++i)
                    currentNode = currentNode.Previous;
            }

            int score = currentNode.Value;
            currentNode = currentNode.Next;
            marbles.Remove(currentNode.Previous);

            return score;
        }
    }
}
