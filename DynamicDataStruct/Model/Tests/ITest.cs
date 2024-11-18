using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public interface ITest
    {
        Dictionary<int, Tuple<double, double>> StartTest(int sizeConst=0);
        void SetMaxSize(int MaxTests);
        void SetStep(int step);
    }
}
