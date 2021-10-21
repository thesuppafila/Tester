using System;

namespace MatrixTestGenerator
{
    class Solver
    {
        static Random rand = new Random();

        public int[,] matrix;

        public int x;

        public int y;

        public Solver(int[,] matrix)
        {
            this.matrix = matrix;
            this.x = rand.Next(3);
            this.y = rand.Next(3);
        }

        //Количество четных в матрице
        public int EvenCount()
        {
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 2 == 0)
                        counter++;
            return counter;
        }

        //Количество четных в четных строках
        public int EvenCountEvenRows()
        {
            int counter = 0;
            for (int i = 1; i < matrix.GetLength(0); i += 2)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 2 == 0)
                        counter++;
            return counter;
        }

        //Количество четных в нечетных строках
        public int EvenCountOddRows()
        {
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i += 2)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 2 == 0)
                        counter++;
            return counter;
        }

        //Количество четных столбцах
        public int EvenCountEvenColumns()
        {
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 1; j < matrix.GetLength(1); j += 2)
                    if (matrix[i, j] % 2 == 0)
                        counter++;
            return counter;
        }

        //Количество нечетных столбцах
        public int EvenCountOddColumns()
        {
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 1; j < matrix.GetLength(1); j += 2)
                    if (matrix[i, j] % 2 == 0)
                        counter++;
            return counter;
        }


        //Сумма элементов в строке X
        public int SumXRow()
        {
            int result = 0;
            for (int j = 0; j < matrix.GetLength(1); j++)
                result += matrix[x, j];
            return result;
        }

        //Сумма элементов в столбце Y
        public int SumYColumn()
        {
            int result = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
                result += matrix[i, y];
            return result;
        }

        //Сумма элементов побочной диагонали
        public int SideDiagonalSum()
        {
            int result = 0;
            int j = matrix.GetLength(1);
            for (int i = 0; i < matrix.GetLength(0);)
                result += matrix[i++, j--];
            return result;
        }

        //Сумма элементов побочной диагонали
        public int MainDiagonalSum()
        {
            int result = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                result += matrix[i, i];
            return result;
        }

        //Скалярное произведение векторов
        public int Scalar()
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[0, j] *= matrix[i, j];

            int result = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
                result += matrix[0, i];

            return result;
        }
    }
}
