using System;

namespace SudokuGeneration
{
    public static class Grid
    {
        public static Random Rand { get; } = new Random();

        public static int[,] GridTable1 { get => GridTable2; set => GridTable2 = value; }
        public static int[,] BaseGridTable1 { get => BaseGridTable2; set => BaseGridTable2 = value; }
        public static int[,] GridTable2 { get; set; } = new int[9, 9];
        public static int[,] BaseGridTable2 { get; set; } = new int[9, 9];

        private static void BaseGridGeneration(int n = 3)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    GridTable1[i, j] = ((i * n + i / n + j) % (n * n) + 1);
                }
            }
        }

        /* столбцы становятся строками и наоборот */
        private static void TransposingGrid()
        {
            var temp = new int[GridTable1.GetLength(1), GridTable1.GetLength(0)];
            for (int i = 0; i < GridTable1.GetLength(0); i++)
            {
                for (int j = 0; j < GridTable1.GetLength(1); j++)
                {
                    temp[j, i] = GridTable1[i, j];
                }
            }

            GridTable1 = temp;
        }

        /* Обмен двух строк в пределах одного района */
        private static void Swap_rows_small()
        {
            int area = Rand.Next(0, 3);

            int line1 = Rand.Next(0, 3);
            int line2;

            do
            {
                line2 = Rand.Next(0, 3);
            } while (line1 == line2);

            // Строки для обмена.
            line1 += area * 3;
            line2 += area * 3;

            int temp;

            for (int i = 0; i < 9; i++)
            {
                temp = GridTable1[line1, i];
                GridTable1[line1, i] = GridTable1[line2, i];
                GridTable1[line2, i] = temp;
            }
        }

        /* Обмен двух столбцов в пределах одного района */
        private static void Swap_colums_small()
        {
            TransposingGrid();
            Swap_rows_small();
            TransposingGrid();
        }

        /* Обмен двух районов по горизонтали */
        private static void Swap_rows_area()
        {
            int area1 = Rand.Next(0, 3);
            int area2;

            do
            {
                area2 = Rand.Next(0, 3);
            } while (area1 == area2);

            int temp;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    temp = GridTable1[area1 * 3 + i, j];
                    GridTable1[area1 * 3 + i, j] = GridTable1[area2 * 3 + i, j];
                    GridTable1[area2 * 3 + i, j] = temp;
                }
            }
        }

        /* Обмен двух районов по вертикали */
        private static void Swap_colums_area()
        {
            TransposingGrid();
            Swap_rows_area();
            TransposingGrid();
        }

        private static void Mix(int n = 10)
        {
            var methods = new Action[5];

            methods[0] = TransposingGrid;
            methods[1] = Swap_rows_small;
            methods[2] = Swap_colums_small;
            methods[3] = Swap_rows_area;
            methods[4] = Swap_colums_area;

            for (int i = 0; i < n; i++)
            {
                methods[Rand.Next(0, methods.Length - 1)]();
            }
        }

        public static void GenGrid(int mix = 120,  int hard = 0, int size = 9)
        {
            BaseGridGeneration();
            Mix(mix);
            BaseGridTable1 = GridTable1;

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
                int i = Rand.Next(0,9);
                int j = Rand.Next(0, 9);
                /*do
                {
                    j = rand.Next(0,9);
                } while (i == j);*/

                GridTable1[i, j] = 0;
            }

            Mix();
        }

        public static void PrintGrid()
        {
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    if ((j - 1) % 3 == 0)
                        Console.Write("|");

                    if (GridTable1[i - 1, j - 1] != 0)
                        Console.Write(GridTable1[i - 1, j - 1]);
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
