using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
using System.Linq;
namespace Algorithm_Makingword
{
    class Maker
    {
        int N;
        int M;
        List<string> Words;
        public static string path = @"C:\Users\user\source\repos\1506022022\Algorithm\Algorithm_Makingword\Algorithm_Makingword\Input.txt";
        string newWord;
        public Maker()
        {
            Words = new List<string>();
            Input();


        }

        // 입력
        void Input()
        {
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                var temp = reader.ReadLine().Split(" ");

                N = int.Parse(temp[0]);
                M = int.Parse(temp[1]);
                while (!reader.EndOfStream)
                {
                    Words.Add(reader.ReadLine());


                }
            }
        }
        // 출력
        public string Make()
        {
            // 사이에 _가 들어가야 하는 횟수
            int count = M - Words.Sum(x => x.Length);

            // 단어와 단어 사이에 _ 가 몇개 들어가야 하는지
            int _count = count / (N - 1);

            // _ 개수 맞추기
            int are = count % (N - 1);

            foreach (var word in Words) 
            {
                // 뉴워드에 단어를 차례대로 이어붙인다
                newWord += word;

                // _ 가 필요한 만큼 붙여준다
                if (word != Words.Last())
                    for(int i=0;i<_count;i++)
                        newWord += Plus_();
                if (are > 0)
                {
                    newWord += Plus_();
                    are--;
                }
            }

            // 만들어진 뉴워드를 반환한다.
            return newWord;

            string Plus_() => "_";
        }

        

        class Program
    {

        static void Main(string[] args)
        {
                var p = new Maker();
                WriteLine(p.Make());
                
            
        }

        }
    }
}
