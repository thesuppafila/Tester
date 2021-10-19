using System;
using System.Collections.Generic;

namespace MatrixTestGenerator
{
    class Program
    {
        static Dictionary<int, TaskType> taskTypeDictionary = new Dictionary<int, TaskType>();

        static void Main(string[] args)
        {
            int index = 0;
            foreach (TaskType type in Enum.GetValues(typeof(TaskType)))
                taskTypeDictionary.Add(index++, type);


            foreach (int key in taskTypeDictionary.Keys)
                Console.WriteLine(String.Format("{0} -> {1}", key, taskTypeDictionary[key]));

            Generator generator = new Generator();

            Task task = generator.Generate(int.Parse(Console.ReadLine()));

            Console.ReadKey();
        }
    }
}
