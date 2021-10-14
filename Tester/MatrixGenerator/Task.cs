using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixTestGenerator
{
    class Task
    {
        public string body;

        public int[,] value;

        public Task(string body, int matrixSize)
        {
            this.body = body;
            this.value = new int[matrixSize, matrixSize];
        }

        public override string ToString()
        {
            string result = body + "\n";
            for (int i = 0; i < value.GetLength(0); i++, result += "\n")
                for (int j = 0; j < value.GetLength(1); j++)
                    result += value[i, j] + "\t";
            return result;
        }
    }
}
