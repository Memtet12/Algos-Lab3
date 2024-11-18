using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class ExtensionedStack
    {
        public static bool IsEmpty<T>(this Stack<T> values)
        {
            return values.Count == 0;
        }

        public static T Top<T>(this Stack<T> values)
        {
            T temp = values.Pop();
            values.Push(temp);
            return temp;
        }

        public static List<T> Print<T>(this Stack<T> values)
        {
            return values.ToList();
        }
    }
}
