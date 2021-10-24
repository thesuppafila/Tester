using System;
using System.Collections.Generic;

namespace MatrixGenerator
{
    class Program
    {
        static Dictionary<int, TaskType> taskTypes = new Dictionary<int, TaskType>();

        static void Main(string[] args)
        {
            string[] size = Console.ReadLine().Split(' ');

            int n = int.Parse(size[0]);
            int m = int.Parse(size[1]);

            int[,] matrix = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                string[] str = Console.ReadLine().Split(' ');
                for (int j = 0; j < m; j++)
                    matrix[i, j] = int.Parse(str[j]);
            }
            Console.WriteLine("Прочитано");
            for (int i = 0; i < n; i++, Console.WriteLine())
                for (int j = 0; j < m; j++)
                    Console.Write(matrix[i, j] + "\t");

            int index = 0;
            foreach (TaskType type in Enum.GetValues(typeof(TaskType)))
            {
                taskTypes[index] = type;
                Console.WriteLine(String.Format("{0} -> {1}", index, taskTypes[index]));
                index++;
            }

            int taskNumber = int.Parse(Console.ReadLine());
            Console.WriteLine(GetTask(taskNumber));

        }

        static Task GetTask(int taskNumber)
        {
            Generator generator = new Generator();
            return generator.Generate(taskNumber);
        }
    }
}
