using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Model
{
    public class MyList<T> : IEnumerable<Node<T>> where T : IComparable
    {
        public Node<T> Head { get; protected set; }

        public Node<T> Tail { get; protected set; }

        public MyList(params T[] data)
        {
            foreach (var item in data)
            {
                Add(item);
            }
        }

        public int Count { get; protected set; } = 0;

        public bool IsEmpty() => Head == null;

        public Node<T> this[int i]
        {
            get
            {
                int count = 0;

                if (i < 0)
                {
                    throw new IndexOutOfRangeException($"{i} less than zero");
                }

                foreach (var item in this)
                {
                    if (count == i) return item;
                    count++;
                }
                throw new IndexOutOfRangeException($"{i} outside of the List");

            }
            set
            {
                int count = 0;
                if (value == null) throw new ArgumentNullException($"value can bot bq null");

                foreach (var item in this)
                {
                    if (count == i)
                    {
                        Node<T> temp = item;
                        temp = value;
                    }
                    count++;

                }
            }
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public bool Contains(T data)
        {
            Node<T> current = Head;
            while (current != null)
            {
                if (current.Value.Equals(data))
                {
                    return true;
                }

                current = current.Next;
            }
            return false;
        }

        public void Add(T value)
        {
            if (IsEmpty())
            {
                Tail = Head = new Node<T>(value, null);
            }
            else
            {
                var item = new Node<T>(value, null);
                Tail.Next = item;
                Tail = item;
            }
            Count++;
        }

        public void RemoveAt(int index)
        {
            int count = 0;
            Node<T> current = Head;

            if (index >= Count || index < 0) throw new ArgumentOutOfRangeException("index is outside of list");

            if (index == 0)
            {
                Head = Head.Next;
            }
            else
            {
                while (count != index - 1)
                {
                    current = current.Next;
                    count++;
                }

                if (index == Count - 1)
                {
                    Tail = current;
                }

                current.Next = current.Next.Next;
            }


            Count--;

        }

        public List<Node<T>> ToNodeList()
        {
            List<Node<T>> list = new List<Node<T>>();
            foreach (Node<T> item in this)
            {
                list.Add(item);
            }
            return list;
        }

        public List<T> ToArrayList()
        {
            List<T> list = new List<T>();
            foreach (Node<T> item in this)
            {
                list.Add(item.Value);
            }
            return list;
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void SwapHeadWithTail(bool isFirstToTail)
        {
            if (Count == 1) return;
            if (isFirstToTail)
            {
                Node<T> newFirst = new Node<T>(Head.Value, null);
                Head = Head.Next;
                Tail.Next = newFirst;
                Tail = newFirst;
            }
            else
            {
                Node<T> newHead = new Node<T>(Tail.Value, Head);
                Head = newHead;
                Tail = this[Count - 1];
                Tail.Next = null;
            }
        }

        public void SwapElements(T firstValue, T secondValue)
        {
            int firstIndex = -1;
            int secondIndex = -1;
            int count = 0;

            foreach (var el in this)
            {
                if (Equals(el.Value, firstValue) && firstIndex == -1)
                {
                    firstIndex = count;
                }
                if (Equals(el.Value, secondValue) && secondIndex == -1)
                {
                    secondIndex = count;
                }
                count++;
            }

            if (firstIndex != -1 && secondIndex != -1)
            {
                T temp = this[firstIndex].Value;
                this[firstIndex].Value = this[secondIndex].Value;
                this[secondIndex].Value = temp;
            }
        }

        public void DoubleList()
        {
            AddList(new MyList<T>(ToArrayList().ToArray()));
        }

        public void AddList(MyList<T> newList)
        {
            if (newList == this)
            {
                newList = new MyList<T>(newList.ToArrayList().ToArray());
            }
            Tail.Next = newList.Head;
            Tail = newList.Tail;

            Count += newList.Count;
        }

        public void AddElementBefore(Node<T> addedElement, Node<T> element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Equals(this[i].Value, element.Value))
                {
                    if (i == 0)
                    {
                        addedElement.Next = this[i];
                        Head = addedElement;
                    }
                    else
                    {
                        addedElement.Next = this[i];
                        this[i - 1].Next = addedElement;

                    }
                    Count++;
                    return;
                }
            }

        }

        public void RemoveSameElements(Node<T> element)
        {
            int count = 0;

            foreach (var el in this)
            {
                if (Equals(el.Value, element.Value))
                {
                    RemoveAt(count);
                }
                else
                {
                    count++;
                }

            }
        }

        public void Reverse()
        {
            MyList<T> newList = new MyList<T>();

            for (int i = Count - 1; i >= 0; i--)
            {
                newList.Add(this[i].Value);
            }

            Head = newList.Head;
            Tail = newList.Tail;
        }

        public void AddListAfterElement(Node<T> node)
        {

            foreach (var el in this)
            {
                if (Equals(el.Value, node.Value))
                {
                    MyList<T> newList = new MyList<T>(ToArrayList().ToArray());

                    Count += newList.Count;
                    newList.Tail.Next = el.Next;
                    el.Next = newList.Head;


                    return;
                }
            }
        }

        public int CountUniqueElements()
        {
            HashSet<T> set = new HashSet<T>();

            foreach (var el in this)
            {
                set.Add(el.Value);
            }

            return set.Count;
        }

        public MyList<T> SplitList(Node<T> node)
        {
            MyList<T> newList = new MyList<T>();
            bool isFound = false;

            foreach (var el in this)
            {
                if (isFound)
                {
                    newList.Add(el.Value);
                }
                if (Equals(el.Value, node.Value))
                {
                    isFound = true;
                }
            }
            return newList;
        }

        public void AddElementIfNotDesc(Node<T> node)
        {

            Node<T> prev = null;
            Node<T> first = null;
            Node<T> second = null;
            bool isFound = false;

            foreach (var el in this)
            {
                if (prev != null)
                {
                    if (prev.Value.CompareTo(el.Value) > 0)
                    {
                        return;
                    }

                    bool checkFirst = (node.Value.CompareTo(prev.Value) > 0 || node.Value.CompareTo(prev.Value) == 0);
                    bool checkSecond = (node.Value.CompareTo(el.Value) < 0 || node.Value.CompareTo(el.Value) == 0);

                    if (!isFound && checkFirst && checkSecond)
                    {
                        first = prev;
                        second = el;
                    }
                }
                prev = el;
            }

            if (first == null || second == null)
            {
                if (node.Value.CompareTo(Head.Value) < 0)
                {
                    node.Next = Head;
                    Head = node;
                }
                else if (node.Value.CompareTo(Tail.Value) > 0)
                {
                    Tail.Next = node;
                    Tail = node;
                    node.Next = null;
                }
            }
            else
            {
                node.Next = second;
                first.Next = node;
            }

            Count++;
        }

        public void RemoveNotUnique()
        {
            HashSet<T> set = new HashSet<T>();

            foreach (var el in this)
            {
                set.Add(el.Value);
            }

            MyList<T> list = new MyList<T>(set.ToArray());

            Count = set.Count;
            Head = list.Head;
            Tail = list.Tail;
        }
    }
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }

        public Node(T value, Node<T> next)
        {
            Value = value;
            Next = next;
        }
    }
}