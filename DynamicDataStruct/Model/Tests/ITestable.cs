using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Tests
{
    public interface ITestable<T>
    {
        public void Push(T elem);
        public T Pop();
        public List<T> Print();
        public bool IsEmpty();
        public T Top();
        public void Clear();

    }
}
