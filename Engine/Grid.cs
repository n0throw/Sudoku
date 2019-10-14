using System;

namespace SudokuGeneration
{
    public class Grid
    {
        private readonly Random Rand = new Random();
        private delegate void GridMethod();
        private int[,] gridTable { get; set; } = new int[9, 9];

        public int[,] GridTable { get => gridTable; set => gridTable = value; }


        public Grid(int n = 3)
        {
            // Генерация базовой сетки матрицы
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    GridTable[y, x] = ((y * n + y / n + x) % (int)(Math.Pow(n, 2)) + 1);
                }
            }
        }

        /* Перемешивание матрицы */
        public void Mix(int n = 10)
        {
            GridMethod[] action =
            {
                new GridMethod(TransposingGrid),
                new GridMethod(Swap_rows_small),
                new GridMethod(Swap_colums_small),
                new GridMethod(Swap_rows_area),
                new GridMethod(Swap_colums_area)
            };

            for (int i = 0; i < n; i++)
            {
                action[Rand.Next(0, action.Length - 1)]();
            }
        }

        /* Транспонирование матрицы */
        public void TransposingGrid()
        {
            var temp = new int[GridTable.GetLength(1), GridTable.GetLength(0)];
            for (int i = 0; i < GridTable.GetLength(0); i++)
            {
                for (int j = 0; j < GridTable.GetLength(1); j++)
                {
                    temp[j, i] = GridTable[i, j];
                }
            }

            GridTable = temp;
        }

        /* Обмен двух строк в пределах одного района */
        public void Swap_rows_small()
        {
            int area = Rand.Next(0, 3);

            int line1 = Rand.Next(0, 3);
            int line2;

            do
            {
                line2 = Rand.Next(0, 3);
            } while (line1 == line2);

            line1 += area * 3;
            line2 += area * 3;

            int temp;

            for (int i = 0; i < 9; i++)
            {
                temp = GridTable[line1, i];
                GridTable[line1, i] = GridTable[line2, i];
                GridTable[line2, i] = temp;
            }
        }

        /* Обмен двух столбцов в пределах одного района */
        public void Swap_colums_small()
        {
            TransposingGrid();
            Swap_rows_small();
            TransposingGrid();
        }

        /* Обмен двух районов по горизонтали */
        public void Swap_rows_area()
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
                    temp = GridTable[area1 * 3 + i, j];
                    GridTable[area1 * 3 + i, j] = GridTable[area2 * 3 + i, j];
                    GridTable[area2 * 3 + i, j] = temp;
                }
            }
        }

        /* Обмен двух районов по вертикали */
        public void Swap_colums_area()
        {
            TransposingGrid();
            Swap_rows_area();
            TransposingGrid();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Grid g = obj as Grid;
            if (g as Grid == null)
                return false;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.GridTable[i, j] != g.GridTable[i, j])
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = gridTable[0,8]^gridTable[8,0];
            return hashCode;
        }

        public static bool operator ==(Grid g1, Grid g2)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (g1.GridTable[i, j] != g2.GridTable[i, j])
                        return false;
                }
            }

            return true;
        }
        public static bool operator !=(Grid g1, Grid g2)
        {
            if (g1 == null || g2 == null)
                return false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (g1.GridTable[i, j] != g2.GridTable[i, j])
                        return true;
                }
            }

            return false;
        }
    }
}
