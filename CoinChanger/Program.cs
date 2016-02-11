using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinChanger
{
    class Program
    {
        private static List<int> C = new List<int>() { 25, 10, 5, 1 };
        private static Dictionary<string, long> change = new Dictionary<string, long>();
        private static List<int> leastChange = new List<int>();

        static void Main(string [] args)
        {
            string input = Console.ReadLine();
            int N = 0;
            int.TryParse(input, out N);

            long result = TotalNumberOfWays(N, C.Count);

            LeastNumberOfCoins(ref N);

            Console.WriteLine("Total number of ways to make change: " + result);
            Console.WriteLine("Least coins required to make change: " + leastChange.Count);
            leastChange.ForEach(o => Console.Write(o + " "));

            Console.Read();
        }

        static long TotalNumberOfWays(int N, int m)
        {
            if (change.ContainsKey(N + "_" + m))
            {
                return change [N + "_" + m];
            }
            if (m <= 0 || N < 0)
            {
                change.Add(N + "_" + m, 0);
                return 0;
            }
            if (N == 0)
            {
                change.Add(N + "_" + m, 1);
                return 1;
            }

            long result = TotalNumberOfWays(N, m - 1) + TotalNumberOfWays(N - C [m - 1], m);
            change.Add(N + "_" + m, result);
            return result;
        }

        static void LeastNumberOfCoins(ref int N)
        {
            if (N == 0)
            {
                N = 0;
                leastChange.Add(1);
                return;
            }
            if (N < 0)
            {
                return;
            }
            if (C.Contains(N))
            {
                leastChange.Add(N);
                N = 0;
                return;
            }

            C = C.OrderByDescending(o => o).ToList();

            foreach (int i in C)
            {
                if (i < N)
                {
                    N = N - i;
                    leastChange.Add(i);
                    LeastNumberOfCoins(ref N);
                }
            }
        }
    }
}
