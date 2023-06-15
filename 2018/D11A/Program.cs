using System;

namespace D11A
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

            var (x, y) = GetCellOfGreatestPower(fuelCells);

            return $"{x + 1},{y + 1}";
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

        static (int x, int y) GetCellOfGreatestPower(int[,] cells)
        {
            var result = default((int x, int y));
            var max = int.MinValue;

            for (int x = 0; x < cells.GetLength(0) - 2; ++x)
            {
                for (int y = 0; y < cells.GetLength(1) - 2; ++y)
                {
                    var candidate = CalculatePower(cells, x, y);
                    if (candidate > max)
                    {
                        max = candidate;
                        result = (x, y);
                    }
                }
            }

            return result;
        }

        static int CalculatePower(int[,] cells, int x, int y)
        {
            int result = 0;
            for (int offsetX = 0; offsetX < 3; ++offsetX)
                for (int offsetY = 0; offsetY < 3; ++offsetY)
                    result += cells[x + offsetX, y + offsetY];
            return result;
        }
    }
}
