using System.Reflection;
using System.Resources;

namespace MatrixGenerator
{
    public class Generator
    {
        public int[,] matrix;

        public Task task;

        int taskNumber;

        public Generator(int[,] matrix, int taskNumber)
        {
            this.matrix = matrix;
            this.taskNumber = taskNumber;
        }

        public Task Generate()
        {
            task = new Task();
            task.body = GetBody();
            task.matrix = matrix;
            task.answer = GetAnswer();
            return task;
        }

        public string GetBody()
        {
            ResourceManager rm = new ResourceManager("MatrixGenerator.Properties.Resources", Assembly.GetExecutingAssembly());
            return rm.GetString(((TaskType)taskNumber).ToString());
        }

        public string GetBody(int taskNumber)
        {
            ResourceManager rm = new ResourceManager("MatrixGenerator.Properties.Resources", Assembly.GetExecutingAssembly());
            return rm.GetString(((TaskType)taskNumber).ToString());
        }

        public int GetAnswer()
        {
            Solver solver = new Solver(matrix);
            MethodInfo method = solver.GetType().GetMethod(((TaskType)taskNumber).ToString());
            var result = method.Invoke(solver, null);
            return int.Parse(result.ToString());
        }
    }

}
