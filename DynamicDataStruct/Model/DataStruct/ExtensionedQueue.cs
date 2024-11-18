using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;

namespace Model
{
    public static class ExtensionedQueue
    {
        public static bool IsEmpty<T>(this Queue<T> values)
        {
            return values.Count == 0;
        }

        public static List<T> Print<T>(this Queue<T> values)
        {
            return values.ToList();
        }

    }

   
}
