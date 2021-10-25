using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGenerator
{
    [Serializable]
    public class Expression
    {
        static Random random = new Random();

        public Dictionary<string, int> Variables;

        private string _pattern;
        public string Pattern
        {
            get { return _pattern; }
        }

        private Node _tree;
        public Node Tree
        {
            get { return _tree; }
        }

        public string TaskText
        {
            get { return GetTask(); }
        }

        private int _countOfRanks;
        public int CountOfRanks
        {
            get { return _countOfRanks; }
        }

        public string RPNExpression
        {
            get { return RPN.GetExpression(Pattern); }
        }

        public string Text
        {
            get { return ToString(); }
        }

        public string Solve
        {
            get { return GetSolve(); }
        }

        public Expression(string pattern)
        {
            Variables = new Dictionary<string, int>();
            _pattern = pattern;
            _tree = FromPattern(RPNExpression);
            _countOfRanks = random.Next(3, 6);
            Generate(Tree);
        }

        private string GetSolve()
        {
            string text = Text;
            foreach (var v in Variables)
            {
                text = text.Replace(v.Key, v.Value.ToString());
            }
            var solve = RPN.Calculate(text, CountOfRanks);
            return solve;
        }

        private void Generate(Node curPoint)
        {
            foreach (Node n in curPoint.Children)
                Generate(n);

            if (curPoint.type == Node.Type.Operator)
            {
                curPoint.value = RandomOperator();
            }
            else if (curPoint.type == Node.Type.Variable)
            {
                curPoint.value = RandomVariable();
                if (!Variables.ContainsKey(curPoint.value))
                    Variables.Add(curPoint.value, RandomConst());
            }
        }

        public override string ToString()
        {
            return Tree.GetValue();
        }

        static string RandomOperator()
        {
            string[] operators = new string[] { "<<", ">>", "&", "^", "|", "+", "-"};
            return operators[random.Next(0, operators.Length)];
        }

        static string RandomVariable()
        {
            return ((char)random.Next(97, 123)).ToString();
        }

        private int RandomConst()
        {
            return random.Next(0, (int)Math.Pow(2, CountOfRanks));
        }

        private string GetTask()
        {
            return string.Format("{0}\nt = {1}", string.Join("\n", Variables.Select(x => x.Key + " = " + x.Value)), ToString());
        }

        private Node FromPattern(string pattern)
        {
            var temp = new Stack<Node>();

            for (int i = 0; i < pattern.Length; i++)
            {
                if (Char.IsLetterOrDigit(pattern[i]))
                {
                    string a = string.Empty;

                    while (Char.IsLetterOrDigit(pattern[i]))
                    {
                        a += pattern[i];
                        i++;
                        if (i == pattern.Length) break;
                    }
                    temp.Push(new Node(Node.Type.Variable));
                    i--;
                }
                else if (IsOperator(pattern[i]))
                {
                    var a = temp.Pop();
                    var b = temp.Pop();
                    var node = new Node(Node.Type.Operator);
                    node.Children.Add(a);
                    node.Children.Add(b);
                    temp.Push(node);
                }
            }

            return temp.Peek();
        }

        private bool IsOperator(char character) => new List<char>() { '+', '-', '*', '/', '&', '|', '^' }.Contains(character);
    }
}
