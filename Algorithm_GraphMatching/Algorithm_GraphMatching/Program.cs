using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static System.Console;
namespace Algorithm_GraphMatching
{
    class Program
    {
        static int N = 100;

        public Program(int n)
        {
            N = n;

        }

        static void Go(int n)
        {
            N = n;

            
        }

        static void Main(string[] args)
        {
            Go(100);
            decimal sum = 0;
            for(int i = 0; i <=N/2; i++)
            {
                sum += GetCount(i);

            }


            WriteLine(sum);
        }

       static decimal GetCount(int n)
        {
            if (n == 0) return 1;
            if (n == 1) return N;
            if (n == N /2) 
            {
                if (N % 2 == 0)
                    return 2;
                else
                    return 0;
            }
            // 구현 case 1 : 무조건 2로 나눔

            decimal c = 1;
            // 전체 경우의 수
            for (decimal i = N; i > N-n; i--)
                c *= i;
            //  1개의 경우의 수N
            c = (c / N)-2;

            // 전체의 겹치지 않는 경우의 수
            c = (c * N) / n;


            WriteLine(n + "개의 간선 : " + c);
            return c;
        }

        
    }
}
