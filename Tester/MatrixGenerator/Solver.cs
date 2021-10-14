using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixTestGenerator
{
    class Solver
    {
        //Количество четных в матрице
        public int EvenCount(int[,] matrix)
        {
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 2 == 0)
                        counter++;
            return counter;
        }

        //Количество четных в четных строках
        public int EvenCountEvenRows(int[,] matrix)
        {
            int counter = 0;
            for (int i = 1; i < matrix.GetLength(0); i+=2)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 2 == 0)
                        counter++;
            return counter;
        }

        //Количество четных в нечетных строках
        public int EvenCountOddRows(int[,] matrix)
        {
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i+=2)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 2 == 0)
                        counter++;
            return counter;
        }

        //Количество четных столбцах
        public int EvenCountEvenColumns(int[,] matrix)
        {
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 1; j < matrix.GetLength(1); j+=2)
                    if (matrix[i, j] % 2 == 0)
                        counter++;
            return counter;
        }

        //Количество нечетных столбцах
        public int EvenCountOddColumns(int[,] matrix)
        {
            int counter = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 1; j < matrix.GetLength(1); j += 2)
                    if (matrix[i, j] % 2 == 0)
                        counter++;
            return counter;
        }

        //Скалярное произведение векторов
        public int Scalar(int[,] matrix)
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
