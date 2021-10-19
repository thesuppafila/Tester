namespace MatrixTestGenerator
{
    public class Task
    {
        public string body;

        public int[,] value = new int[3, 3];

        public int answer;

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
