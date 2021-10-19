using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGenerator
{
    public class Expression
    {
        static Random random = new Random();

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
            get { return RPN.Calculate(Text, CountOfRanks); }
        }

        public Expression(string pattern, int countOfRanks)
        {
            _pattern = pattern;
            _tree = FromPattern(RPNExpression);
            _countOfRanks = countOfRanks;
            Generate(Tree);
        }

        private void Generate(Node curPoint)
        {
            foreach (Node n in curPoint.Children)
                Generate(n);

            if (curPoint.type == Node.Type.Operator)
            {
                curPoint.value = RandomLogicOperator();
            }
            else if (curPoint.type == Node.Type.Variable)
            {
                curPoint.value = RandomVariable(CountOfRanks);
            }
        }

        public override string ToString()
        {
            return Tree.GetValue();
        }

        static string RandomLogicOperator()
        {
            string[] operators = new string[] { "<<", ">>", "&", "^", "|" };
            return operators[random.Next(0, operators.Length)];
        }

        static string RandomVariable(int rankCount)
        {
            return ((char)random.Next(97, 123)).ToString();
        }

        static string RandomConst()
        {
            return random.Next(1, 5).ToString();
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
