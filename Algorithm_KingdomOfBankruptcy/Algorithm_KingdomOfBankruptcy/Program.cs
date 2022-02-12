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
            

        }
    }
}
