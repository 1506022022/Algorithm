#define DEBUG

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm_startup.Component;
using static System.Console;

namespace Algorithm_startup
{
    public class Manager
    {
        public Brand brand;
        public List<Player> players;
        Queue<string> namePieces;
        int brandLength;
        int BrandUndecidedCount;

        public Manager()
        {
            GameStart();
        }

        // 게임을 시작한다
        public void GameStart()
        {
            // 패를 불러온다
            namePieces = new Queue<string>();
            GetPieces();

            // 참여 플레이어 목록 생성
            players = new List<Player>();

            // 구사과와 큐브러버 참여
            JoinPlayer(new Player(1, false));   // 구사과
            JoinPlayer(new Player(2, true));   // 큐브러버
            // 플레이어 들에게 패 배분

            foreach (var player in players)
                HandOut(player, namePieces.Dequeue());

            // 게임에 사용할 브랜드 생성
            brand = new Brand(brandLength);

            // {0}
            // 차례가 된 플레이어에게 턴을 제공한다
            // 플레이어에게 브랜드 이름 <문자, 위치> 를 받아 적용한다
            // 턴이 끝나면 다름 차례로 넘긴다
            // 브랜드의 이름이 모두 결정될 때까지 반복한다 go -> {0}
            while (brand.UndecidedCount != 0)
            {
                foreach (var player in players)
                    if (brand.UndecidedCount != 0)
                    {
                        Turn(player);
                        
                    }
                    else break;
            }

            // 브랜드의 이름이 모두 결정되었으므로
            // 결과를 출력한다
            brand.WriteBrand();

        }

        // 플레이어 참여
        void JoinPlayer(Player player)=>players.Add(player);

        // 패를 배분한다
        void HandOut(Player player, string pieces)  => player.GetPieces(pieces); 

        // 패를 불러온다
        void GetPieces()
        {
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
                while (!reader.EndOfStream)
                {
                    var pieces = reader.ReadLine();
                    namePieces.Enqueue(pieces);

                    // 브랜드의 길이를 저장한다
                    brandLength = pieces.Length;
                    
                }
        }

        // 턴 동작
        void Turn(Player player)
        {
            var playerChoice =  
                player.MyTurn(
                    new Infomation() {
                        brandUndecidedCount = brandLength- brand.UndecidedCount }
                    );

            brand.Decided(playerChoice);
            BrandUndecidedCount = brand.UndecidedCount;
        }

#if DEBUG
        // 패 정보 출력
        void WritePiece()
        {
            foreach (var piece in namePieces)
                WriteLine(piece);
        }
#endif


    }
}
