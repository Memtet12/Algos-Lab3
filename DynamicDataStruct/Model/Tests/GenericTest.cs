using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class GenericTest<TEntity> : ITest where TEntity : class, ITestable<string>, ISimilar, new()
    {
        public int Step { get; private set; } = 1;

        public int MaxSize { get; private set; } = 1;

        public int MinSize { get; private set; } = 1;
        
        private TEntity _entity;

        public GenericTest(int step, int maxSize, int minSize , TEntity entity )
        {
            Step = step;
            MaxSize = maxSize;
            MinSize = minSize;
            _entity = entity;
        }

        public void SetStep(int step)
        {
            Step = step;
        }

        public void SetMinSize(int minSize)
        {
            MinSize = minSize;
        }

        public void SetMaxSize(int maxSize)
        {
            MaxSize = maxSize;
        }


        public Dictionary<int, Tuple<double, double>> StartTest(int sizeConst=0)
        {
            Dictionary<int, Tuple<double, double>> result = new Dictionary<int, Tuple<double, double>>();


            for (int i = MinSize; i <= MaxSize; i += Step)
            {
                if (sizeConst == 0)
                {
                    result = Test(result, i , i);
                }
                else
                {
                    result = Test(result, sizeConst , i);
                }
                
            }

            return result;
        }

        private Dictionary<int, Tuple<double, double>> Test(Dictionary<int, Tuple<double, double>> result, int size , int num)
        {
            List<string> dataSet = TestSetGenerator.GenerateForStackAndQueue(size);
            Stopwatch sw = Stopwatch.StartNew();          

            sw.Start();
            for (int j = 0; j < 5; j++)
            {

                foreach (string data in dataSet)
                {
                    if (data[0] == '1') _entity.Push(data.Substring(1));
                    else if (data[0] == '2') _entity.Pop();
                    else if (data[0] == '3') _entity.Top();
                    else if (data[0] == '4') _entity.IsEmpty();
                    else if (data[0] == '5') _entity.Print();
                    else
                    {
                        throw new ArgumentException($"{data[0]} stack doesn't have operation with this index");
                    }
                }

                _entity.Clear();
            }
            sw.Stop();


            result.Add(num,
                new Tuple<double, double>(sw.Elapsed.TotalMilliseconds / 5, _entity.TestOnBasic(dataSet)));

            return result;
        }
    }
}

