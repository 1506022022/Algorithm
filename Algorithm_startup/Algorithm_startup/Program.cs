using System.Collections.Generic;
using System.Linq;
using static System.Console;
namespace Algorithm_startup
{
    class Gamer
    {
        public Queue<char> que;

        public Gamer(string str, bool isDescending)
        {
            que = new Queue<char>();

            // 우선순위에 따라 정렬
            var linq = str.OrderBy((x) => x);

            if (isDescending)
                linq = str.OrderByDescending((x) => x);

            // 큐에 삽입
            foreach (var ssss in linq)
                que.Enqueue(ssss);

        }
    }
 

    class Program
    {
 

        static void Main(string[] args)
        {
            // 시작
            Manager manager = new Manager();


            //Gamer p1 = new Gamer("startlink", false);
            //Gamer p2 = new Gamer("startlink", true);


            //string temp = "";
            //for (int i = 0; i < "startlink".Length; i++)
            //{
            //    if (i % 2 == 0)
            //        temp += p1.que.Dequeue();
            //    else
            //        temp += p2.que.Dequeue();

            //}
            //WriteLine(temp);
        }
    }
}
