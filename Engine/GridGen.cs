using System;

namespace SudokuGeneration
{
    public static class GridGen
    {
        private static readonly Random Rand = new Random();

        public static void GenGrid(ref Grid matrix, ref Grid answer, int mix = 120, int hard = 0, int size = 9)
        {
            matrix = new Grid();

            matrix.Mix(mix);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    answer.GridTable[i, j] = matrix.GridTable[i, j];
                }
            }

            

            int del = Convert.ToInt32(Math.Round(Convert.ToDouble(Math.Pow(size, 2) / 2)) + Math.Round(Math.Sqrt(size)));
            switch (hard)
            {
                case 0:
                    {
                        del = Convert.ToInt32(Math.Round(Convert.ToDouble(Math.Pow(size, 2) / 4)) + Math.Round(Math.Sqrt(size)));
                        goto default;
                    }
                case 1:
                    {
                        del = Convert.ToInt32(Math.Round(Convert.ToDouble(Math.Pow(size, 2) / 3)) + Math.Round(Math.Sqrt(size)));
                        goto default;
                    }
                default: break;
            }

            for (int k = 0; k < del; k++)
            {
                int i = Rand.Next(0, 9);
                int j = Rand.Next(0, 9);
                /*
                do
                {
                    j = rand.Next(0,9);
                } while (i == j);
                */

                matrix.GridTable[i, j] = 0;
            }
        }

        public static void PrintGrid(Grid matrix)
        {
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    if ((j - 1) % 3 == 0)
                        Console.Write("|");

                    if (matrix.GridTable[i - 1, j - 1] != 0)
                        Console.Write(matrix.GridTable[i - 1, j - 1]);
                    else
                        Console.Write(" ");
                }

                Console.Write("|");
                Console.WriteLine();
                if (i % 3 == 0)
                {
                    Console.Write("|---|---|---|");
                    Console.WriteLine();
                }
            }
        }
    }
}
