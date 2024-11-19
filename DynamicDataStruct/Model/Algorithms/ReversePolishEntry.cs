using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;
using Model;

namespace Model
{
    public class ReversePolishEntry
    {
        private MyStack<string> _stack = new MyStack<string>();

        private string _expression;

        private static Dictionary<string, Func<double, double, double>> _operation = new Dictionary<string, Func<double, double, double>>()
        {
            {"+", (x,y)=>x+y },
            {"-", (x,y)=>x-y },
            {"*", (x,y)=>x*y },
            {"/", (x,y)=>y/x },
            {"^", (x,y)=> Math.Pow(y , x) },
        };
        private static Dictionary<string, Func<double, double>> _function = new Dictionary<string, Func<double, double>>()
        {
            {"ln", (x)=>Math.Log(x) },
            {"cos", (x)=>Math.Cos(Math.PI * x / 180.0) },
            {"sin", (x)=>Math.Sin(Math.PI * x / 180.0) },
            {"sqrt", (x)=> Math.Sqrt(x) },
        };
        public ReversePolishEntry(string str)
        {
            _expression = str;
        }

        public List<MyStack<string>> Solve()
        {
            _stack.Clear();
            List<MyStack<string>> stack = new List<MyStack<string>>();

            string[] splitted = _expression.Split(' ');
            try
            {
                foreach (var str in splitted)
                {
                    if (_function.ContainsKey(str))
                    {
                        _stack.Push(str);
                        stack.Add(new MyStack<string>(_stack.Print().ToArray()));
                        _stack.Pop();

                        double first = double.Parse(_stack.Top());
                        _stack.Pop();
                        _stack.Push(_function[str](first).ToString());

                        stack.Add(new MyStack<string>(_stack.Print().ToArray()));
                    }
                    else if (_operation.ContainsKey(str))
                    {
                        _stack.Push(str);
                        stack.Add(new MyStack<string>(_stack.Print().ToArray()));
                        _stack.Pop();

                        double first = double.Parse(_stack.Top());
                        _stack.Pop();
                        double second = double.Parse(_stack.Top());
                        _stack.Pop();
                        _stack.Push(_operation[str](first, second).ToString());

                        stack.Add(new MyStack<string>(_stack.Print().ToArray()));
                    }
                    else
                    {
                        _stack.Push(str);
                    }
                }
                stack.Add(new MyStack<string>(_stack.Print().ToArray()));
            }
            catch(Exception ex) 
            {
                throw new ArgumentException("Неверная запись");
            }
            return stack;
        }
    }
}