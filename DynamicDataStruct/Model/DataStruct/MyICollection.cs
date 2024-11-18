using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataStruct.Model.DataStruct
{
    interface MyICollection<T>
    {
        public void Push(T elem);
        public T Pop();
        public T Top();
        public bool IsEmpty();
        public List<T> Print();
    }
}
