using DynamicDataStruct.Model.DataStruct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Markup;
using Tests;

namespace Model
{
    public class MyStack<T> : ITestable<T>, ISimilar, MyICollection<T> where T : IComparable
    {
        private static Dictionary<int, string> _convertIntToString = new Dictionary<int, string>()
        {
            {1,"Push" },
            {2,"Pop" },
            {3,"Top" },
            {4,"IsEmpty" },
            {5,"Print" }
        };

        private MyList<T> list;

        public MyStack(params T[] data)
        {
            list = new MyList<T>(data);
        }

        public MyStack()
        {
            list = new MyList<T>();
        }

        public void Push(T elem)
        {
            list.Add(elem);
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        public T Pop()
        {
            if (list.Count == 0) throw new InvalidOperationException($"Stack is empty");

            T temp = list[list.Count - 1].Value;
            list.RemoveAt(list.Count - 1);
            return temp;

        }

        public List<T> Print() => list.ToArrayList();

        public bool IsEmpty() => list.IsEmpty();

        public T Top()
        {
            if (list.Count == 0) throw new InvalidOperationException($"Stack is empty");
            return list[list.Count - 1].Value;
        }

        public void Clear()
        {
            list.Clear();
        }

        public static List<Tuple<int, string?>> ShowStackChanges(List<string> dataSet)
        {
            List<Tuple<int, string>> result = new List<Tuple<int, string>>();

            foreach (string data in dataSet)
            {
                if (data.Length == 1)
                {
                    result.Add(Tuple.Create(int.Parse(data[0].ToString()), (string)null));
                }
                else
                {
                    result.Add(Tuple.Create(int.Parse(data[0].ToString()), data.Substring(1)));
                }

            }
            return result;
        }

        public double TestOnBasic(List<string> dataSet)
        {
            Stopwatch sw = Stopwatch.StartNew();

            sw.Start();
            for (int j = 0; j < 5; j++)
            {
                Stack<string> entity = new Stack<string>();
                foreach (string data in dataSet)
                {
                    if (data[0] == '1') entity.Push(data.Substring(1));
                    else if (data[0] == '2') entity.Pop();
                    else if (data[0] == '3') entity.Top();
                    else if (data[0] == '4') entity.IsEmpty();
                    else if (data[0] == '5') entity.Print();
                    else
                    {
                        throw new ArgumentException($"{data[0]} stack doesn't have operation with this index");
                    }
                }
            }
            sw.Stop();

            return sw.Elapsed.TotalMilliseconds / 5;
        }
    }
}