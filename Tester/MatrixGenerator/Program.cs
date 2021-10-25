using System;
using System.Collections.Generic;
using System.IO;

namespace MatrixGenerator
{
    class Program
    {
        static Dictionary<int, TaskType> taskTypes = new Dictionary<int, TaskType>();

        static int[,] matrix;

        static void Main(string[] args)
        {
            LoadTaskTypes();
            LoadMatrix();
            Output();
        }

        static void LoadTaskTypes()
        {
            int index = 0;
            foreach (TaskType type in Enum.GetValues(typeof(TaskType)))
            {
                taskTypes[index] = type;
                Console.WriteLine(String.Format("{0} -> {1}", index, taskTypes[index]));
                index++;
            }
        }

        static void LoadMatrix()
        {
            string[] size = Console.ReadLine().Split(' ');

            int n = int.Parse(size[0]);
            int m = int.Parse(size[1]);

            matrix = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                string[] str = Console.ReadLine().Split(' ');
                for (int j = 0; j < m; j++)
                    matrix[i, j] = int.Parse(str[j]);
            }
            Console.WriteLine("Прочитано");
        }

        static Task GetTask(int taskNumber)
        {
            Generator generator = new Generator(matrix, taskNumber);
            return generator.Generate();
        }

        static void OutputTasks()
        {
            DirectoryInfo directoryInfo = Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\output\");
            for (int i = 0; i < taskTypes.Count; i++)
                using (StreamWriter sw = new StreamWriter(directoryInfo.FullName + (i + 1).ToString() + ".out"))
                {
                    sw.WriteLine(GetTask(i).answer);
                    sw.Close();
                }
        }

        static void Output()
        {
            DirectoryInfo directoryInfo = Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\output\");

            using (StreamWriter sw = new StreamWriter(directoryInfo.FullName + /*(i + 1).ToString() +*/ "full.out"))
            {
                for (int i = 0; i < taskTypes.Count; i++)
                    sw.WriteLine(taskTypes[i] + " -> " + GetTask(i).answer);
                sw.Close();
            }
        }
    }
}
