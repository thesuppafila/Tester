namespace MatrixGenerator
{
    public class Task
    {
        public string body;

        public int[,] matrix = new int[3, 3];

        public int answer;

        public override string ToString()
        {
            return body;
        }
    }
}
