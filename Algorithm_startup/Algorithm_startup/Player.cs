#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm_startup.Component;
using static System.Console;

namespace Algorithm_startup
{
    public class Player
    {
        // 플레이어 ID
        public readonly int ID;

        // 우선순위
        public readonly bool isDescending;

        // 플레이어가 가진 패
        int[] namePiece;

        // 생성자
        public Player(int id, bool isDescending) 
        {
            // ID 를 가지고
            // 패를 저장할 배열 초기화
            // 우선순위 지정
            ID = id;
            namePiece = new int[26];
            this.isDescending = isDescending;
        }

        // 매니저에게 패를 받는다.
        public void GetPieces(string pieces)
        {
            // 패는 소문자 알파벳만을 가진다.
            pieces.ToLower();

            // 패 저장
            foreach (char piece in pieces)
            {
                try{
                    // 패가 알파벳이 아니면 예외처리
                    if (!('a' <= piece && piece <= 'z'))
                        throw new Exception("범위를 벗어남 : " + piece);

                    // piece to namePiece 직렬화.
                    int index = (int)piece - 97;

                    // 해당하는 패를 하나 늘린다.
                    namePiece[index]++;

                }
                // 알파벳이 아닌 잘못된 값 출력
                catch(Exception e)
                {
                    WriteLine("잘못된 패 입력입니다.");
                    WriteLine(e.Message);
                }
                
            }

        }

        // 턴
        public Choice MyTurn(Infomation info) 
        {
            var choice = new Choice();

            // 우선순위 : 내림차순에 따라 선택
            if (isDescending)
            {
                for (int i = namePiece.Length-1; i >=0; i--)
                    if (namePiece[i] != 0)
                    {
                        choice.value = (char)(i + 97);
                        namePiece[i]--;
                        break;
                    }
            }
            else
                // 우선순위 : 오름차순에 따라 선택
                for (int i = 0; i < namePiece.Length; i++)
                    if (namePiece[i] != 0)
                    {
                        choice.value = (char)(i+97);
                        namePiece[i]--;
                        break;
                    }

            // 주어진 정보에 따라 위치 결정
            choice.index = info.brandUndecidedCount;

            // 결정된 문자와 위치를 전달
            return choice; 
        }

#if DEBUG
        // 플레이어 정보 출력
        public void WritePlayer()
        {
            // [ID]
            WriteLine("\n[플레이어 ID]\n{0}", ID);

            // [소지중인 패]
            WriteLine("\n[패 목록]");
            for (int i = 0; i < namePiece.Length; i++)
                if (namePiece[i] != 0)
                    WriteLine("알파벳 {0} : {1} 개 소지", (char)(i + 97), namePiece[i]);

        }
#endif

    }
}
