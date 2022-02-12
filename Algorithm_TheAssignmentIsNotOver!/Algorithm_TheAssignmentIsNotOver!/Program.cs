using System;
using System.Collections.Generic;
using System.Linq;
using Algorithm_Component;
using static System.Console;

namespace Algorithm_TheAssignmentIsNotOver_
{
    public struct Homework 
    {
        public int time;
        public int score;
    }

    class Program
    {
        static Input input;

        static void Main(string[] args)
        {
            WriteLine(Start());    
        }

        static int Start()
        {
            // 성적
            int score =0;

            // 학기 기간
            int Limited;

            input = new Input(@"C:\Users\user\source\repos\1506022022\Algorithm\Algorithm_TheAssignmentIsNotOver!\Algorithm_TheAssignmentIsNotOver!\Input.txt");
            Limited = int.Parse(input.inputs.First());
            input.inputs.RemoveAt(0);

            // 이제부터 과제가 시작된다.
            Stack<Homework> list = new Stack<Homework>();

            // 학기가 끝날때까지 과제를 받고 과제를 한다.
            for (int time = 0; time < Limited; time++)
            {
                var info = input.inputs.First().Split(" ");
                input.inputs.RemoveAt(0);

                // 과제 유무 정보를 읽어들인다
                bool existHomework = 0 != int.Parse(info[0]);

                // 과제가 있으면 리스트에 추가
                if (existHomework)
                {
                    list.Push(new Homework()
                    {
                        score = int.Parse(info[1]),
                        time = int.Parse(info[2])
                    });
                }

                // 과제가 있으면
                if (list.Count > 0)
                {
                    // 과제를 한다.
                    var temp = list.Pop();
                    temp.time--;

                    // 과제가 끝났으면 점수를 얻는다.
                    if (temp.time == 0)
                        score += temp.score;
                    // 끝나지 않았으므로 이어서 한다.
                    else
                        list.Push(temp);
                }
            }

            // 학기가 끝났으므로 성적을 받는다.
            return score;
        }
    }
}
