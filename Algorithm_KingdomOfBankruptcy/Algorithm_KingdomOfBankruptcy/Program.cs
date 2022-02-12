using System;
using System.Collections.Generic;
using static System.Console;
using static Algorithm_KingdomOfBankruptcy.Condition;
using static Algorithm_KingdomOfBankruptcy.Kingdom;

namespace Algorithm_KingdomOfBankruptcy
{
    class Program
    {
        static void Main(string[] args)
        {
            var pro = new Input();

            foreach (var a in pro.kingdoms)
                a.WriteKingdom();


            foreach(var a in pro.kingdoms)

            WriteLine("{0} 국가의 유일한 생존 가능 여부 : {1}",a.id +1,a.Start());


            // 선택된 왕국이 유일하게 살아남으려면
            // 왕국의 생존 조건을 확인하고
            // 그 조건 리스트 중에서
            // 실행했을 때 유일하게 생존할 수 있는가를 판단한 뒤
            // 참이면 유일국 여부가 참을 반환

            // 만약, 생존 조건이 다른 국가의 파산일 때,
            // 지금은 다른 국가가 파산 조건을 만족하지 못하지만
            // 나중에라도 만족하게 됐을 경우를 확인하지 못하는 부분이 있습니다.




        }
    }
}
