using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class TestSetGenerator
    {
        public static List<string> GenerateForStackAndQueue(int size)
        {
            int countPushes = 0;
            int num;

            List<string> result = new List<string>();
            Random operationGenerator = new Random();

            for (int i = 1; i <= size; i++)
            {
                if (i == 1 || countPushes == 0)
                {
                    while (true)
                    {
                        num = operationGenerator.Next(1, 6);
                        if (num != 2 && num != 3 && num != 5) break;
                    }
                    countPushes = AddElement(num, result, countPushes);
                }
                else
                {
                    num = operationGenerator.Next(1, 6);
                    countPushes = AddElement(num, result, countPushes);
                }
            }

            return result;
        }

        public static List<string> GenerateStrings(int listSize, int MaxSize, int MinSize)
        {
            List<string> result = new List<string>();
            Random rand = new Random();

            for (int i = 0; i < listSize; i++)
            {
                result.Add(GenerateString(MaxSize - rand.Next(MinSize, MaxSize - 1)));
            }

            return result;
        }

        public static List<string> GenerateStringsWithDublicats(int listSize, int MaxSize, int MinSize)
        {
            List<string> result = new List<string>();
            Random rand = new Random();
            int count = 0;

            for (int i = 0; i < listSize; i++)
            {
                string str = GenerateString(MaxSize - rand.Next(MinSize, MaxSize - 1));
                int j = rand.Next(0, 2);
                if (j == 1 && count < listSize / 3)
                {
                    int addRand = rand.Next(0, (listSize / 10) + 1);
                    for (int j2 = 0; j2 < addRand; j2++)
                    {
                        result.Add(str);
                        i++;
                        count++;
                    }
                }

                result.Add(str);

            }


            return result.OrderBy(item => rand.Next()).ToList();
        }

        public static List<string> GenerateNumsWithDublicats(int listSize, int MaxSize, int MinSize)
        {
            List<string> result = new List<string>();
            Random rand = new Random();
            int count = 0;


            for (int i = 0; i < listSize; i++)
            {
                string str = GenerateNumString(MaxSize - rand.Next(MinSize, MaxSize - 1));
                int j = rand.Next(0, 2);
                if (j == 1 && count < listSize / 3)
                {
                    int addRand = rand.Next(0, (listSize / 10) + 1);
                    for (int j2 = 0; j2 < addRand; j2++)
                    {
                        result.Add(str);
                        i++;
                        count++;
                    }
                }

                result.Add(str);

            }

            return result.OrderBy(item => rand.Next()).ToList();
        }

        private static string GenerateNumString(int stringLen)
        {
            Random rand = new Random();
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < stringLen; i++)
            {
                str.Append(Convert.ToChar(rand.Next(49, 57)));
            }

            return str.ToString();
        }

        private static string GenerateString(int stringLen)
        {
            Random rand = new Random();
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < stringLen; i++)
            {
                char symbol;
                while (true)
                {
                    symbol = Convert.ToChar(rand.Next(48, 90));
                    if (symbol != 61 && symbol != ';' && symbol != ':') break;

                }
                str.Append(symbol);
            }

            return str.ToString();
        }

        private static int AddElement(int num, List<string> result, int countPushes)
        {
            Random numberGenerator = new Random();

            if (num == 1)
            {
                result.Add($"{num},{GenerateString(numberGenerator.Next(0, 10))}");
                countPushes++;
            }
            else if (num == 2 || num == 3)
            {
                result.Add($"{num}");
                countPushes--;
            }
            else
            {
                result.Add($"{num}");
            }

            return countPushes;
        }
    }
}
