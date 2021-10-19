using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGenerator
{
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
                if (Children[0].type == Type.Operator && (Array.IndexOf(operators, Children[0].value) >= Array.IndexOf(operators, value)))
                    child1 = "(" + Children[0].GetValue() + ")";
                string child2 = Children[1].GetValue();
                if (Children[1].type == Type.Operator && (Array.IndexOf(operators, Children[1].value) >= Array.IndexOf(operators, value)))
                    child2 = "(" + Children[1].GetValue() + ")";
                return child1 + " " + value + " " + child2;
            }
            else
                return value;
        }
    }
}
