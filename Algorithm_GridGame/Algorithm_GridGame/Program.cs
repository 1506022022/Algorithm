using Algorithm_Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static System.Console;

namespace Algorithm_GridGame
{

    class Program
    {
        // 입력
        static Input input;
        static string path = @"C:\Users\user\source\repos\1506022022\Algorithm\Algorithm_GridGame\Algorithm_GridGame\Input.txt";
        
        // 흑돌과 백돌의 그룹
        static List<List<Vector2>> white;
        static List<List<Vector2>> black;

        // 입력된 바둑판
        static bool[,] grid;

        static void Main(string[] args)
        {
            // 출력
            WriteLine(Start());


        }

        private static int Start()
        {
            // 색 바꾸기의 최소횟수
            int min = 0;

            // 입력
            input = new Input(path);

            // 그룹 목록 생성
            white = new List<List<Vector2>>();
            black = new List<List<Vector2>>();

            // 입력 데이터 불러오기
            GetGrid();

            // 그룹화
            //CreateGroup();
            for (int y = 0; y < grid.GetLength(0); y++)
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    var current = new Vector2() { X = x, Y = y };

                    // 이미 목록에 있으면 넘어감
                    if (white.Any(a => a.Any(x => x == current)) || black.Any(a => a.Any(x => x == current))) { }
                    else
                    {
                        // 그룹 추가
                        var list = Grouping(new List<Vector2>(), current);

                        // 새 그룹 추가
                        if (grid[y, x] == false) white.Add(list);
                        else black.Add(list);
                    }
                }

                    // 흑돌과 백돌중 그룹의 수가 적은 쪽을
                    // 다른 색으로 바꾸면, 색 바꾸기의 최소횟수가 된다.
                    min = white.Count < black.Count ? white.Count : black.Count;

            return min;
        }

        // 그룹화
        private static List<Vector2> Grouping(List<Vector2> list,Vector2 me)
        {
            // 새 그룹에 자신 추가
            if (!list.Contains(me)) list.Add(me);

            // 상하좌우
            var temp = new List<Vector2>();

            int x = (int)me.X;
            int y = (int)me.Y;


            // 상하좌우 중 같은 색인 목록을 순회하며
            // 상
            if (y > 0)
                if (grid[y - 1, x] == grid[y, x] && !list.Contains(new Vector2() { X = x, Y = y - 1 })) temp.Add(new Vector2() { X = x, Y = y - 1 });
            // 하
            if (y + 1 < grid.GetLength(0))
                if (grid[y + 1, x] == grid[y, x] && !list.Contains(new Vector2() { X = x, Y = y + 1 })) temp.Add(new Vector2() { X = x, Y = y + 1 });
            // 좌
            if (x > 0)
                if (grid[y, x - 1] == grid[y, x] && !list.Contains(new Vector2() { X = x-1, Y = y })) temp.Add(new Vector2() { X = x-1, Y = y });
            // 우
            if (x + 1 < grid.GetLength(1))
                if (grid[y, x + 1] == grid[y, x] && !list.Contains(new Vector2() { X = x + 1, Y = y })) temp.Add(new Vector2() { X = x + 1, Y = y });


            // 그룹에 추가
            foreach (var t in temp)
                Grouping(list, t);


            return list;
        }

        // 바둑판 정보 읽기
        private static void GetGrid()
        {
            // 바둑판 정보
            var list = new List<string>();

            // 바둑판 사이즈
            int sizeY = int.Parse(input.inputs.First().Split(" ")[0]);
            int sizeX = int.Parse(input.inputs.First().Split(" ")[1]);

            // 바둑판 읽어오기
            foreach (var line in input.inputs) 
            {
                string temp = "";
                foreach (var s in line.Split(" "))
                    temp += s;
                list.Add(temp);
            }

            // 바둑판 정보에서 사이즈 내용은 제거
            list.RemoveAt(0);

            // 바둑판 정보 쓰기
            grid = new bool[sizeY, sizeX];
            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                    grid[y, x] = list[y][x] == '0' ? false : true;


        }
    }
}
