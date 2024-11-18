using DynamicDataStruct.Model.DataStruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using Tests;

namespace Model
{
    public class MyQueue<T> : MyList<T>, ISimilar, ITestable<T>, MyICollection<T> where T : IComparable
    {
        private static Dictionary<int, string> _convertIntToString = new Dictionary<int, string>()
        {
            {1,"Enque" },
            {2,"Deque" },
            {3,"Top" },
            {4,"IsEmpty" },
            {5,"Print" }
        };
        public MyQueue(params T[] data) : base(data) { }

        public MyQueue() : base() { }

        public T Dequeue()
        {
            if (Head == null)
            {
                throw new InvalidOperationException();
            }

            T result = Head.Value;
            Head = Head.Next;

            if (Head == null)
            {
                Tail = null;
            }

            Count--;

            return result;
        }

        public void Enque(T item)
        {
            Add(item);
        }

        public T Top()
        {
            if (Head != null)
            {
                return Head.Value;
            }

            throw new InvalidOperationException("Queue is empty");
        }

        public void Push(T elem)
        {
            Add(elem);
        }

        public T Pop()
        {
            return Dequeue();
        }

        public List<T> Print()
        {
            return ToArrayList();
        }

        public T Last
        {
            get
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException("Queue is empty");
                }

                return Tail.Value;
            }
        }

        public static List<Tuple<int, string?>> ShowQueueChanges(List<string> dataSet)
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
                Queue<string> entity = new Queue<string>();
                foreach (string data in dataSet)
                {
                    if (data[0] == '1') entity.Enqueue(data.Substring(1));
                    else if (data[0] == '2') entity.Dequeue();
                    else if (data[0] == '3') entity.Peek();
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
