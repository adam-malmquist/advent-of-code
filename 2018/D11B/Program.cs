
using System;

namespace D11B
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(GetAnswer(4842));
        }

        static string GetAnswer(int serialNumber)
        {
            var fuelCells = GetFuelCells(serialNumber);

            var (x, y, size) = GetCellOfGreatestPower(fuelCells);

            return $"{x+1},{y+1},{size}";
        }

        private static int[,] GetFuelCells(int serialNumber)
        {
            var cells = new int[300, 300];

            for (int x = 0; x < cells.GetLength(0); ++x)
                for (int y = 0; y < cells.GetLength(1); ++y)
                    cells[x, y] = GetFuelLevel(x + 1, y + 1, serialNumber);

            return cells;
        }

        private static int GetFuelLevel(int x, int y, int serialNumber)
        {
            int rackId = x + 10;

            int result = rackId * y;
            result += serialNumber;
            result *= rackId;
            result = GetHundredsDigit(result);
            result -= 5;

            return result;
        }

        private static int GetHundredsDigit(int n)
        {
            return n < 100 ? 0 : (n / 100) % 10;
        }

        static (int x, int y, int size) GetCellOfGreatestPower(int[,] cells)
        {
            var lengthX = cells.GetLength(0);
            var lengthY = cells.GetLength(1);
            var result = default((int x, int y, int size));
            var max = int.MinValue;

            for (int x = 0; x < lengthX; ++x)
            {
                for (int y = 0; y < lengthY; ++y)
                {
                    int previous = 0;

                    for (int size = 0; x + size < lengthX && y + size < lengthY; ++size)
                    {
                        var candidate = previous + CalculateAdditionalPower(cells, x, y, size);
                        if (candidate > max)
                        {
                            max = candidate;
                            result = (x, y, size + 1);
                        }
                        previous = candidate;
                    }
                }
            }

            return result;
        }

        static int CalculateAdditionalPower(int[,] cells, int x, int y, int size)
        {
            int result = cells[x+size, y+size];
            for (int offset = 0; offset < size; ++offset)
            {
                result += cells[x + size, y + offset];
                result += cells[x + offset, y + size];
            }
            return result;
        }
    }
}
