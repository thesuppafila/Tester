using System;

namespace MatrixTestGenerator
{
    class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            Task task = new Task("Задание:", 3);
            for (int i = 0; i < task.value.GetLength(0); i++)
                for (int j = 0; j < task.value.GetLength(1); j++)
                {
                    task.value[i, j] = rand.Next(0, 10);
                }
            Console.WriteLine(task.ToString());
            Console.ReadKey();
        }
    }
}
