using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class InfixEntry
    {
        private static Dictionary<string, int> operationList = new Dictionary<string, int>()
        {
            {"ln",1 },
            {"sin",1 },
            {"cos",1 },
            {"sqrt",1},
            {"^",1 },
            {"*",2 },
            {"/",2},
             {"+",3 },
            {"-",3},
            {")",4},
            {"(",5},

        };

        private static Dictionary<string, Func<double, double>> _function = new Dictionary<string, Func<double, double>>()
        {
            {"ln", (x)=>Math.Log(x) },
            {"cos", (x)=>Math.Cos(Math.PI * x / 180.0) },
            {"sin", (x)=>Math.Sin(Math.PI * x / 180.0) },
            {"sqrt", (x)=> Math.Sqrt(x) },
        };

        private static List<string> TransformExpression(string expression) // 5|+|559|*||sin|25 
        {
            foreach (var operation in operationList)
            {
                expression = expression.Replace(operation.Key, $"|{operation.Key}|");
            }
            List<string> result = expression.Split("|").ToList();
            result.ForEach(x => x.Trim());
            return result.Where(x => x != string.Empty).ToList();
        }

        private static bool IsNumber(string num)
        {
            if (double.TryParse(num, out double res))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string InfixToPostfix(string exp)
        {
            StringBuilder result = new StringBuilder();
            MyStack<string> stack = new MyStack<string>();
            List<string> expression = TransformExpression(exp);

            bool prevNum = false;

            for (int i = 0; i < expression.Count; i++)
            {
                if (IsNumber(expression[i]))
                {
                    prevNum = true;
                }
                else if (_function.ContainsKey(expression[i]) && prevNum)
                {
                    throw new ArgumentException($"Неверная запись");
                }
                else
                {
                    prevNum = false;
                }
            }

            try
            {

                foreach (string el in expression)
                {
                    string current = el;

                    if (IsNumber(current))
                    {
                        result.Append(current);
                        result.Append(" ");
                    }
                    else if (current == "(")
                    {
                        stack.Push(current);
                    }
                    else if (current == ")")
                    {
                        while (stack.Count > 0 && stack.Top() != "(")
                        {
                            result.Append(stack.Pop());
                            result.Append(" ");
                        }

                        if (stack.Count > 0 && stack.Top() != "(")
                        {
                            throw new ArgumentException("Invalid Expression");
                        }
                        else
                        {
                            stack.Pop();
                        }
                    }
                    else
                    {
                        while (stack.Count > 0 && operationList[current.ToString()] >= operationList[stack.Top().ToString()])
                        {
                            result.Append(stack.Pop());
                            result.Append(" ");
                        }
                        stack.Push(current);
                    }
                }
                while (stack.Count > 0)
                {
                    result.Append(stack.Pop());
                    result.Append(" ");
                }


            }
            catch (Exception e)
            {
                throw new ArgumentException("Не корректный формат записи");
            }

            return result.ToString().Trim();
        }

    }
}