using System;

namespace MatrixGenerator
{
    class Solver
    {
        public int[,] matrix;

        public Solver(int[,] matrix)
        {
            this.matrix = Clone(matrix);
        }

        public int[,] Clone(int[,] array)
        {
            int[,] clone = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    clone[i, j] = array[i, j];
            return clone;
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

        //Сумма элементов побочной диагонали
        public int SideDiagonalSum()
        {
            int result = 0;
            int j = matrix.GetLength(1);
            for (int i = 0; i < matrix.GetLength(0);)
                result += matrix[i++, --j];
            return result;
        }

        //Сумма элементов главной диагонали
        public int MainDiagonalSum()
        {
            int result = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                result += matrix[i, i];
            return result;
        }

        //Минимальный элемент главной диагонали
        public int MinMainDiagonal()
        {
            int min = int.MaxValue;
            for (int i = 0; i < matrix.GetLength(0); i++)
                if (matrix[i, i] < min)
                    min = matrix[i, i];
            return min;
        }

        //Минимальный элемент побочной диагонали
        public int MinSideDiagonal()
        {
            int min = int.MaxValue;
            for (int i = 0; i < matrix.GetLength(0); i++)
                if (matrix[i, matrix.GetLength(0) - 1 - i] < min)
                    min = matrix[i, i];
            return min;
        }

        //Делится на 3, не делится на 2
        public int Mul3Unmul2()
        {
            int count = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 3 == 0 && matrix[i, j] % 2 != 0)
                        count++;
            return count;
        }

        //Делится на 3, не делится на 5
        public int Mul3Unmul5()
        {
            int count = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 3 == 0 && matrix[i, j] % 5 != 0)
                        count++;
            return count;
        }

        //Произведение четных элементов диагоналей
        public int MulEvenDiagonalNumbers()
        {
            int main = 0;
            int side = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, i] % 2 == 0)
                    main += matrix[i, i];
                if (matrix[i, matrix.GetLength(0) - 1 - i] % 2 == 0)
                    side += matrix[i, matrix.GetLength(0) - 1 - i];
            }
            return main * side;
        }

        //Произведение элементов диагоналей
        public int MulMainAndSideDiagonal()
        {
            int main = 0;
            int side = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                main += matrix[i, i];
                side += matrix[i, matrix.GetLength(0) - 1 - i];
            }
            return main * side;
        }

        //Произведение нечетных элементов диагоналей
        public int MulOddDiagonalNumbers()
        {
            int main = 0;
            int side = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, i] % 2 == 1)
                    main += matrix[i, i];
                if (matrix[i, matrix.GetLength(0) - 1 - i] % 2 == 1)
                    side += matrix[i, matrix.GetLength(0) - 1 - i];
            }
            return main * side;
        }

        //Произведение простых чисел диагонали
        public int MulPrimeDiagonalNumbers()
        {
            int main = 0;
            int side = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 2; j < matrix[i, i] - 1; j++)
                {
                    if (matrix[i, i] % j == 0)
                        break;
                    main += matrix[i, i];
                }
                for (int j = 2; j < matrix[i, matrix.GetLength(0) - 1 - i] - 1; j++)
                {
                    if (matrix[i, matrix.GetLength(0) - 1 - i] % j == 0)
                        break;
                    side += matrix[i, matrix.GetLength(0) - 1 - i];
                }
            }
            return main * side;
        }

        //Произведение сумм четных и нечетных элементов матрицы
        public int MulSumEvenSumOdd()
        {
            int even = 0;
            int odd = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 2 == 0)
                        even += matrix[i, j];
                    else
                        odd += matrix[i, j];
            return even * odd;
        }

        //Номер строки с минимальной суммой элементов
        public int NumberRowMinSum()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 1; j < matrix.GetLength(1); j++)
                    matrix[i, 0] += matrix[i, j];

            int min = int.MaxValue;
            int index = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
                if (matrix[i, 0] < min)
                {
                    min = matrix[i, 0];
                    index = i;
                }
            return index;
        }

        //Количество нечетных элементов
        public int OddCount()
        {
            int count = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 2 != 0)
                        count++;
            return count;
        }

        //Количество простых элементов
        public int PrimeNumberCount()
        {
            int count = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    for (int k = 2; k < matrix[i, j]; k++)
                    {
                        if (matrix[i, j] % k == 0)
                            break;
                        count++;
                    }
            return count;
        }

        //Сумма индексов максимального элемента
        public int SumIndexMaxNumber()
        {
            int sum = 0;
            int max = int.MinValue;

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                        sum = i + j;
                    }
            return sum;
        }

        //Сумма индексов минимального элемента
        public int SumIndexMinNumber()
        {
            int sum = 0;
            int min = int.MaxValue;

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] < min)
                    {
                        min = matrix[i, j];
                        sum = i + j;
                    }
            return sum;
        }

        //Сумма максимальных элементов каждого столбца
        public int SumMaxInColumn()
        {
            int sum = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int max = int.MinValue;
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                    if (matrix[j, i] > max)
                    {
                        max = matrix[j, i];
                    }
                sum += max;
            }
            return sum;
        }

        //Сумма минимальных элементов каждого столбца
        public int SumMinInColumn()
        {
            int sum = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int min = int.MaxValue;
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                    if (matrix[j, i] < min)
                    {
                        min = matrix[j, i];
                    }
                sum += min;
            }
            return sum;
        }

        //Сумма максимальных элементов каждой строки
        public int SumMaxInRow()
        {
            int sum = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int min = int.MaxValue;
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                    if (matrix[i, j] < min)
                    {
                        min = matrix[i, j];
                    }
                sum += min;
            }
            return sum;
        }

        //Сумма минимальных элементов каждой строки
        public int SumMinInRow()
        {
            int sum = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int min = int.MaxValue;
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                    if (matrix[i, j] < min)
                    {
                        min = matrix[i, j];
                    }
                sum += min;
            }
            return sum;
        }

        //Сумма элементов, кратных 4 и некратным 6
        public int SumMul4Unmul6()
        {
            int sum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] % 4 == 0 && matrix[i, j] % 6 != 0)
                        sum += matrix[i, j];
            return sum;
        }

        //Сумма простых элементов
        public int SumPrimeNumbers()
        {
            int sum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    for (int k = 2; k < matrix[i, j]; k++)
                    {
                        if (matrix[i, j] % k == 0)
                            break;
                        sum += matrix[i, j];
                    }
            return sum;
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

        //Количество чисел в двоичной записи которых не более 3-х единиц
        public int BytesLess3()
        {
            int count = 0;
            int element;
            for (int i = 1; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int oneCount = 0;
                    element = matrix[i, j];
                    while (element > 0)
                    {
                        if (element % 2 != 0)
                            oneCount++;
                        element /= 2;
                    }
                    if (oneCount < 4)
                        count++;
                }
            return count;
        }

        //Количество чисел в двоичной записи которых четное количество единиц
        public int BytesEvenCount()
        {
            int count = 0;
            int element;
            for (int i = 1; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int oneCount = 0;
                    element = matrix[i, j];
                    while (element > 0)
                    {
                        if (element % 2 != 0)
                            oneCount++;
                        element /= 2;
                    }
                    if (oneCount % 2 == 0)
                        count++;
                }
            return count;
        }
    }
}