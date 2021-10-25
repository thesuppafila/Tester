using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGenerator
{
    [Serializable]
    public class Node
    {
        public enum Type
        {
            Operator,
            Const,
            Operand,
            Variable
        }

        string[] operators = new string[] { "<<", ">>", "&", "^", "|" };
        public Type type;
        public string value;
        public List<Node> Children;

        public Node(Type _type)
        {
            type = _type;
            Children = new List<Node>();
        }

        public string GetValue()
        {
            if (type == Type.Operator)
            {
                string child1 = Children[0].GetValue();
                if (Children[0].type == Type.Operator && (GetPriority(Children[0].value) < GetPriority(value)))
                    child1 = "(" + Children[0].GetValue() + ")";
                string child2 = Children[1].GetValue();
                if (Children[1].type == Type.Operator && (GetPriority(Children[0].value) < GetPriority(value)))
                    child2 = "(" + Children[1].GetValue() + ")";
                return child1 + " " + value + " " + child2;
            }
            else
                return value;
        }

        static private byte GetPriority(string s)
        {
            switch (s)
            {
                case "*": return 7;
                case "/": return 7;
                case "%": return 7;
                case "+": return 6;
                case "-": return 6;
                case "<<": return 5;
                case ">>": return 5;
                case "&": return 4;
                case "^": return 3;
                case "|": return 2;
                case "(": return 1;
                case ")": return 1;
                default: return 0;
            }
        }
    }
}
