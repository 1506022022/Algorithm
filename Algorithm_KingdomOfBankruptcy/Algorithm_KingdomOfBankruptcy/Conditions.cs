using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace Algorithm_KingdomOfBankruptcy
{
    // 조건 구현
    public class Condition
    {
        // 누가 무엇을 해야한다
        // A 가 무엇을 해야하고
        // B 가 무엇을 해야한다
        public List<List<WhoWhat>> Conditions;
        public Condition()
        {
            Conditions = new List<List<WhoWhat>>();
        }
        public enum tradeType {
            repay,          // 대출금을 갚는다 
            repo,           // 대출금을 돌려받는다
            bankrupt    // 파산한다
        }
        public struct WhoWhat
        {
           public int Who;
           public  tradeType What;
        }


    }
}
