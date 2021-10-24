using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;

namespace MatrixGenerator
{
    public class Generator
    {
        static Random random = new Random();

        public Task task;

        public Generator()
        {
        }

        public Task Generate(int taskNumber)
        {
            task = new Task();
            task.body = GetBody(taskNumber);
            task.matrix = GetMatrix();
            task.answer = GetAnswer(taskNumber, task.matrix);
            return task;
        }

        public Task Generate(int taskNumber, int[,] matrix)
        {
            task = new Task();
            task.body = GetBody(taskNumber);
            task.matrix = matrix;
            task.answer = GetAnswer(taskNumber, task.matrix);
            return task;
        }

        //public int[,] GetMatrixFromFile()
        //{
        //    string[] input = File.ReadAllLines("input.in");
        //    string[] size = input[0].Split(' ');

        //    int n = int.Parse(size[0]);
        //    int m = int.Parse(size[1]);

        //    int[,] matrix = new int[n, m];
        //    for (int i = 0; i < n; i++)
        //    {
        //        stringinput[i+1]
        //    }

        //}

        public int[,] GetMatrix()
        {
            int[,] matrix = new int[3, 3];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(0, 10);
                }
            return matrix;
        }

        public string GetBody(int taskNumber)
        {
            ResourceManager rm = new ResourceManager("MatrixGenerator.Properties.Resources", Assembly.GetExecutingAssembly());
            return rm.GetString(((TaskType)taskNumber).ToString());
        }

        public int GetAnswer(int taskNumber, int[,] matrix)
        {
            Solver solver = new Solver(matrix);
            MethodInfo method = solver.GetType().GetMethod(((TaskType)taskNumber).ToString());
            var result = method.Invoke(solver, null);
            return int.Parse(result.ToString());
        }
    }

}
