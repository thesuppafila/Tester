using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace MatrixTestGenerator
{
    public class Generator
    {
        static Random random = new Random();

        static Dictionary<string, MethodInfo> methodDictionary = new Dictionary<string, MethodInfo>();

        public Task task;

        public Generator()
        {
        }

        public Task Generate(int taskNumber)
        {
            task = new Task();
            task.body = GetBody(taskNumber);
            task.value = GetMatrix();
            task.answer = GetAnswer(taskNumber, task.value);
            return task;
        }
        

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
            ResourceManager rm = new ResourceManager("MatrixGenerator.Resources.TaskResources", Assembly.GetExecutingAssembly());
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
