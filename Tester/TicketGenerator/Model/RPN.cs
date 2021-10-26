using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGenerator
{
    [Serializable]
    class RPN
    {
        static public string Calculate(string input, int rankCount)
        {
            string output = GetExpression(input); //Преобразовываем выражение в постфиксную запись
            string result = Counting(output, rankCount); //Решаем полученное выражение
            return result; //Возвращаем результат
        }

        static private string Counting(string input, int rankCount)
        {
            int result = 0; //Результат
            var temp = new Stack<string>(); //Dhtvtyysq стек для решения

            for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
            {
                if (input[i] == '<' || input[i] == '>')
                {
                    string tmp = input[i].ToString() + input[++i].ToString();
                    int a = int.Parse(temp.Pop()); //Берем 2 последних значения из стека
                    string s = temp.Pop().ToString();
                    uint b = Convert.ToUInt32(s);
                    b <<= 32 - rankCount;
                    uint res = 0;
                    switch (tmp) //И производим над ними действие, согласно оператору
                    {
                        case "<<": res = b << a; break;
                        case ">>": res = b >> a; break;
                    }
                    res >>= 32 - rankCount;
                    temp.Push(Convert.ToString(res)); //Результат вычисления записываем обратно в стек
                }
                //Если символ - цифра, то читаем все число и записываем на вершину стека
                else if (Char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i].ToString()) && !IsOperator(input[i].ToString())) //Пока не разделитель
                    {
                        a += input[i]; //Добаляем
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(a); //Записываем в стек
                    i--;
                }
                else if (IsOperator(input[i].ToString())) //Если символ - оператор
                {
                    string aa = temp.Pop().ToString();
                    int a = Convert.ToInt32(aa); //Берем 2 последних значения из стека
                    string bb = temp.Pop().ToString();
                    int b = Convert.ToInt32(bb);
                    switch (input[i].ToString()) //И производим над ними действие, согласно оператору
                    {
                        case "&": result = a & b; break;
                        case "^": result = a ^ b; break;
                        case "|": result = a | b; break;
                        case "+": if ((a + b) >= Math.Pow(2, rankCount)) result =(int)Math.Pow(2, rankCount) - 1; else result = a + b; break;
                        case "-": result = a - b; break;
                        case "/": result = a / b; break;
                        case "*": result = a * b; break;
                    }
                    temp.Push(Convert.ToString(result)); //Результат вычисления записываем обратно в стек
                }
            }

            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }

        static public string GetExpression(string input)
        {
            string output = string.Empty; //Строка для хранения выражения
            var operStack = new Stack<string>(); //Стек для хранения операторов

            for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
            {
                if (input[i] == '<' || input[i] == '>')
                {
                    string temp = input[i].ToString() + input[++i].ToString();
                    if (operStack.Count > 0) //Если в стеке есть элементы
                        if (GetPriority(temp) <= GetPriority(operStack.Peek())) //И если приоретет нашего оператора меньше или равен приоретету оператора на вершине стека
                            output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением

                    operStack.Push(temp); //Если стек пуст, или же приоретет оператора выше - добавляем оператов на вершину стека
                }

                //Разделители пропускаем
                if (IsDelimeter(input[i].ToString()))
                    continue; //Переходим к следующему символу

                //Если символ - цифра, то считывем все число
                if (Char.IsLetterOrDigit(input[i])) //Если цифра
                {
                    //Читаем до разделителя или оператора, что бы получить число
                    while (!IsDelimeter(input[i].ToString()) && !IsOperator(input[i].ToString()))
                    {
                        output += input[i]; //Добаляем каждую цифру числа к нашей строке
                        i++; //Переходим к следующему символу

                        if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                    }

                    output += " "; //Дописываем после числа пробел в строку с выражением
                    i--; //Возвращаемся на один символ назад, к символу перед разделителем
                }

                //Если символ - оператор
                if (IsOperator(input[i].ToString())) //Если оператор
                {
                    if (input[i] == '(') //Если символ - открывающая скобка
                        operStack.Push(input[i].ToString()); //Записываем её в стек
                    else if (input[i] == ')') //Если символ - закрывающая скобка
                    {
                        //Выписываем все операторы до открывающей скобки в строку
                        string s = operStack.Pop();

                        while (s != "(")
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else //Если любой другой оператор
                    {
                        if (operStack.Count > 0) //Если в стеке есть элементы
                            if (GetPriority(input[i].ToString()) <= GetPriority(operStack.Peek())) //И если приоретет нашего оператора меньше или равен приоретету оператора на вершине стека
                                output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением

                        operStack.Push(input[i].ToString()); //Если стек пуст, или же приоретет оператора выше - добавляем оператов на вершину стека

                    }
                }
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; //Возвращаем выражение в постфиксной записи
        }

        //Метод возвращает приоритет оператора
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

        //Метод возвращает true, если проверяемый символ - оператор
        static private bool IsOperator(string с)
        {
            var operators = new string[] { "<<", ">>", "&", "^", "|", "(", ")", "+", "-", "*", "/" };
            if (Array.IndexOf(operators, с) != -1)
                return true;
            return false;
        }

        //Метод возвращает true, если проверяемый символ - разделитель
        static private bool IsDelimeter(string c)
        {
            if (" =".IndexOf(c) != -1)
                return true;
            return false;
        }
    }
}
