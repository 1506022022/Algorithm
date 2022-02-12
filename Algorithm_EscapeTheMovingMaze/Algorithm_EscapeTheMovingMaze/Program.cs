using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using static System.Console;

namespace Algorithm_EscapeTheMovingMaze
{

    // 입력 -> 체스보드로 변환
    class Input
    {
        public List<ChessBoard> list;

        public Input(string path) 
        {
            list = Get(path);
        }

        // 입력된 체스보드를 보드목록에 읽어들여라
        public List<ChessBoard> Get(string path)
        {
            // 체스보드를 저장할 보드목록
            var temp = new List<ChessBoard>();

            // 입력된 체스보드
            var ChessBoard = new ChessBoard() { board = new bool[8, 8] };

            // 입력 데이터를 체스보드로 변환
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                int i = 0;
                int j = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    foreach (var l in line)
                    {
                        ChessBoard.board[i,j] = l.Equals('#') ? true : false;
                        j++;
                    }
                    j = 0;
                    i++;

                }

            }

            // 보드목록에 체스보드 저장
            temp.Add(ChessBoard);

            // 보드목록 전달
            return temp;
        }

        
        }

    // 체스보드
    class ChessBoard
    {
        public bool[,] board;

        // 체스판에 벽이 존재하는가?
        public bool ExistWall()
        {
            // 벽 서칭
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    // 있으면 true
                    if (board[i, j] == true) return true;

            // 없으면 false
            return false;
        }

        // 벽 하강
        public void DownWall ()
        {
            // 벽 서칭
            for (int i = 7; i >= 0; i--)
                for (int j = 7; j >= 0; j--)
                    // 있으면 벽을 한칸 내려라
                    if (board[i, j] == true) 
                    {
                        if (i + 1 != 8) board[i + 1, j] = true;

                        board[i, j] = false;
                    }
        }

        // 디버깅용
        public void WriteBoard()
        {

            WriteLine("\n[체스보드 출력]\n");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                    Write("{0}", board[i, j] == true ? '■' : '□');

                WriteLine();
            }

        }
    }


    class Program
    {
        // 현재 내 위치
        static int x=0;
        static int y=7;

        // 맵에 벽이 하나라도 있는가?
        static bool existWall;

        // 입력 데이터
        static Input input;

        // 프로그램 시작
        public static int Start()
        {
            // 입력
            input = new Input(@"C:\Users\user\source\repos\1506022022\Algorithm\Algorithm_EscapeTheMovingMaze\Algorithm_EscapeTheMovingMaze\Input.txt");

            // 벽이 사라질 때까지 반복
            while (input.list[0].ExistWall())
            {
                // 이동
                // 이동할 수 없다 => 탈출할 수 없다
                if (!Move()) return 0;
            }

            // 벽이 전부 사라졌다 => 탈출할 수 있다
            return 1;
        }

        // 이동
        public static bool Move()
        {
            // 이동할 수 있는가?
            bool can = false;

            // 이동 방향 저장
            List<Vector2> destinations = new List<Vector2>();
            
            for(int i = -1; i<2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    if (x + j < 0 || x + j > 7) continue;
                    if (y + i < 0 || y + i > 7) continue;

                    destinations.Add(new Vector2() { X = x + j, Y = y + i });
                }
            }

            
            // 이동 방향을 모두 살펴본다
            foreach (var d in destinations)
            {
                // 이동하려는 곳에 벽이 있는가
                if (input.list[0].board[ (int)d.Y, (int)d.X] == true) { can = false; continue; }


                // 이동하면 다음 턴에 벽에 막히는가
                if ((int)d.Y - 1 >= 0)
                    if (input.list[0].board[ (int)d.Y - 1, (int)d.X] == true) { can = false;continue; }

                // 그렇지 않다면 이동한다

                x = (int)d.X;
                y = (int)d.Y;

                can = true;
                break;
            }

            // 이동할 수 있는 방향이 있나?
            if (can)
            {

                //이동 후 벽 하강
                input.list[0].DownWall();

                // 보드에 벽이 존재하는지 다시 확인
                existWall = input.list[0].ExistWall();

            }


            return can;
        }

        static void Main(string[] args)
        {
            Write(Start());
        }
    }
    
}
