using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm_KingdomOfBankruptcy.Condition;
using static System.Console;
namespace Algorithm_KingdomOfBankruptcy
{
    public class Kingdom
    {
        public readonly int id;  // 국가 구분
        Condition survive; // 생존 조건
        Condition bankruptcy; // 파산 조건
        int[] debt;
        Input input;
        bool isBankruptcyed = false;

        // 생성자
        public Kingdom(int id, string debt, Input input)
        {
            this.id = id;
            this.debt = debt.Split().Select(x => int.Parse(x)).ToArray();
            this.input = input;

            CreateCondition();

        }

        // 생존 , 파산 조건 생성
        void CreateCondition()  
        {
            survive = new Condition();
            bankruptcy = new Condition();

            List<int> repoList = new List<int>();       // 빚을 상환받는 국가
            List<int> repayList = new List<int>();     // 상환해야 하는 국가

            survive = new Condition();

            // 장부 정리
            for(int i = 0; i<debt.Length; i++)
            {
                // 자기 자신이다
                if (i == id) continue;

                // 빚을 상환해야 하는 국가다
                if (debt[i] > 0)
                    repayList.Add(i);

                // 빚을 상환받아야 하는 국가다
                else if (debt[i] < 0)
                    repoList.Add(i);

                
            }



            // 상환받는 경우의 수
            var repoCases = Cases(repoList);
            // 상환하는 경우의 수
            var repayCases = Cases(repayList);




            // 생존 case 1 : 다른 모든 국가가 파산한 경우
            List<WhoWhat> temp = new List<WhoWhat>();
            foreach (var pac in repayList) 
                 temp.Add(new WhoWhat() { Who = pac, What = tradeType.bankrupt });
                
            // 채권국가가 모두 파산해야 생존
            survive.Conditions.Add(temp);


            // 파산 case 1 : 돌려받지 않은 상태에서 상환
            
            foreach (var pac in repayList)
            {
                temp = new List<WhoWhat>();
                temp.Add(new WhoWhat() { Who = pac, What = tradeType.repay });
                bankruptcy.Conditions.Add(temp);
            }

            foreach (var poc in repoCases)
            {
                var RCcopy = repayCases;
                while (RCcopy.Count != 0)
                {



                    var pac = RCcopy.First();
                    RCcopy.RemoveAt(0);

                    // 돌려받은 총액
                    // 상환해야하는 총액
                    int totalPoc = 0;
                    int totalPac = 0;
                    foreach (var i in poc)
                        totalPoc += (-debt[i]);
                    foreach (var i in pac)
                        totalPac += (debt[i]);


                    // 생존 case 2 : 상환받은 금액이 갚아야 할 금액이상인 경우
                    if (totalPac <= totalPoc)
                    {
                        // [생존 조건 추가]
                        // 현재 경우 : 빚을 일부 갚을 여력이 있다
                        // 생존 조건 : 갚지 못하는 빚의 채권자가 파산해야 생존
                        temp = new List<WhoWhat>();

                        // 채무자들에게 돌려받는다
                        foreach (var repo in poc)
                            temp.Add(new WhoWhat() { Who = repo, What = tradeType.repo });

                        // 갚지 못하는 빚의 채권자들이 모두 파산한다
                        foreach (var repay in repayList)
                            if (pac.Any(f => f != repay))
                                temp.Add(new WhoWhat() { Who = repay, What = tradeType.bankrupt });

                        survive.Conditions.Add(temp);

                        // 파산 case 2 : 채무자가 파산해서 빚을 받을수 없는 경우

                        // [생존 조건 추가]
                        // 현재 경우 : 빚을 일부 갚을 여력이 있다
                        // 생존 조건 : 갚지 못하는 빚의 채권자가 파산해야 생존

                        // 채무자들에게 돌려받는다
                        foreach (var repo in poc)
                        {
                            temp = new List<WhoWhat>();
                            temp.Add(new WhoWhat() { Who = repo, What = tradeType.bankrupt });
                            
                            foreach (var repay in pac)
                            {
                                List<WhoWhat> temp1 = new List<WhoWhat>();
                                temp1 = temp.ToList();

                                temp1.Add(new WhoWhat() { Who = repay, What = tradeType.repay });
                                bankruptcy.Conditions.Add(temp1);

                            }


                        }
                        

                    }


                }
            }
        }
            
        // 겹치지 않는 모든 경우의 수
       public static List<List<T>> Cases<T>(List<T> list)
        {
            List<List<T>> cases = new List<List<T>>();

            for (int i = 1; i <= list.Count; i++)
            {
                // 사본 복사
                var list_copy = list.ToList();

                while (list_copy.Count != 0)
                {
                    var temp = new List<T>();
                    temp.Add(list_copy.First());
                    list_copy.RemoveAt(0);

                    // 사본 복사
                    var list_2_copy = list_copy.ToList();

                    for (int j = i; j >1; j--)
                    {


                        if (j != 2)
                        {
                            temp.Add(list_2_copy.First());
                            list_2_copy.RemoveAt(0);
                        }
                        else
                        {
                            foreach (var l in list_2_copy)
                            {
                                var temp1 = temp.ToList();
                                temp1.Add(l);
                                cases.Add(temp1);

                            }
                            break;
                        }

                        
                            
                    }
                    if (i == 1)
                    {
                        cases.Add(temp);
                        continue;
                    }
                    if (list_2_copy.Count < i)
                        break;
                }
                
            }

            return cases;


        }

        //디버깅용
        public void WriteKingdom()
        {
            WriteLine("{0} 번 국가 ###########################################" +
                "##########################################\n", id+1);
            WriteLine("[생존 조건]");
            foreach (var s in survive.Conditions)
            {
                foreach (var condition in s)
                    Write("{0} 국가가 {1} ", condition.Who+1, condition.What);
                WriteLine("해야 생존할 수 있음");
            }

            WriteLine("\n[파산 조건]");
            foreach (var s in bankruptcy.Conditions)
            {
                foreach (var condition in s)
                    Write("{0} 국가가 {1} ", condition.Who+1, condition.What);
                WriteLine("하면 파산");
            }
            WriteLine();
                WriteLine();
            WriteLine();
        }

        //void Start()
        //{
        //    var surviveList = survive.Conditions;
        //    foreach (var list in surviveList)
        //    {
        //        bool isGo1 = false;

        //        // pase 1 : 내 생존 조건을 실행했을 때
        //        //              영원히 생존하는 국가가 있는가
        //        foreach (var choice in list)
        //        {
        //            var temp = debt.ToList();
        //            temp.Remove(id);
        //            temp.Remove(choice.Who);
        //            if (isSurvives(temp, new List<WhoWhat>() { choice }))
        //            {
        //                isGo1 = false;
        //                break;
        //            }
        //            else isGo1 = true;
        //        }
        //        // pase 2 : 없다면,
        //        //              이 조건을 실행했을 때의 여파로
        //        //              영원히 생존하는 국가가 있는가
        //        //if (isGo1)
        //        //{
        //        //    bool isGo2 = false;
        //        //    foreach (var choice in list)
        //        //        if (input.kingdoms[choice.Who].bankruptcy)


        //        //}

        //        // 왕국이 파산하면 생존하는 다른 왕국이 없다면
        //        // 파산시킨다.


        //    }
        //}

        public bool Start()
        {
            // 내 생존 조건들
            var myServiveList = survive.Conditions;

            // 참이면 혼자 생존하는 경우가 있다
            bool isGo = false;

            foreach (var list in myServiveList)
            {

                // 생존 조건들 모두가, 혼자 생존할 수 있는 결과를 내놓는가
                isGo = AreOnly(id, list, null);

                if (isGo)
                    break;
            }

            return isGo;
        }

        private bool AreOnly(int id, List<WhoWhat> list, List<int> targets)
        {
            
            bool isAre = false;
            foreach (var condition in list)
            {
                if (targets == null)
                {
                    // 다른 누군가들 (자신과 생존조건 대상은 제외)
                    targets = new List<int>();
                    for (int i = 0; i < debt.Length; i++)
                        targets.Add(i);
                    targets.Remove(id);
                    targets.Remove(condition.Who);
                }

                //  id 가 유일하게 생존할 수 있는가?
                isAre = isOnly(id, condition,targets);
            }

            return isAre;
        }

        private bool isOnly(int id, WhoWhat condition, List<int> targets)
        {
            bool only = true;

            // 내가 전달받은 조건이
            switch(condition.What)
            {
                // 상대방의 파산일 때
                case tradeType.bankrupt:

                    // 상대방이 파산조건을 충족하는가?
                    if (input.kingdoms[ (int)condition.Who].debt.Sum(x=>x)<0)
                        return false;

                    // 상대방이 파산하는게 생존조건인, 타겟이 있는가? => 있으면 유일한 생존이 아님
                    if (isSurvives(targets, new List<WhoWhat>() { condition }))
                            return false;
                    
                   
                        
                    
                    // 상대방이 파산해도 유일한 생존일 수는 있지만
                    // 상대방이 파산하는 조건을, 생존 조건으로 가지고 있는 타겟이 없어서
                    // 유일한 생존을 보장하는가? 
                    foreach (var bankruptcy in input.kingdoms[condition.Who].bankruptcy.Conditions) {
                        only = AreOnly(condition.Who, bankruptcy, targets);
                    }
                    
                    break;

                // 상대방의 수금일 때
                case tradeType.repay:
                    // 나의 상환이 상대방의 생존인가? 유일한 생존을 해치는가?
                    var newTargets = new List<int>() { condition.Who };
                    if (isSurvives(newTargets, new List<WhoWhat>() { new WhoWhat() { Who = id, What = tradeType.repo} }))
                    {
                        return false;
                    }
                    break;

                // 상대방의 상환일 때
                case tradeType.repo:
                    // 상대방이 상환 후 파산하는가?
                    if (input.kingdoms[condition.Who].bankruptcy.Conditions.Contains(new List<WhoWhat>() {
                    new WhoWhat(){Who = id, What = tradeType.repay } }))
                    {
                        // 파산한다면, 상대방의 파산이 생존 조건인 다른 국가가 있는가?
                        newTargets = targets.ToList();
                        newTargets.Remove(condition.Who);
                        if (isSurvives(newTargets, new List<WhoWhat>() { new WhoWhat() {
                            Who = condition.Who,What = tradeType.bankrupt} })) 
                        {
                            return false;
                        }
                    }
                    

                    break;
            }
                

            return only;
        }

        bool isSurvives(List<int> list, List<WhoWhat> condition)
        {
            foreach (var kingdoms in list)
            {
                int index = kingdoms < 0 ? -kingdoms : kingdoms;
                var surviveConditions = input.kingdoms[index].survive.Conditions;
                return surviveConditions.Any(a=>a==condition);


            }

            return false;
        }
        bool isBankruptcy(List<int> list, int id , List<WhoWhat> condition) 
        {
            foreach (var conditions in input.kingdoms[id].bankruptcy.Conditions)
            {
                foreach (var c in conditions)
                { 
                    if (c.What == tradeType.repay)
                    {
                        // id 국가의 파산 조건이 a 국가에게의 상환이고
                        // a 국가의 생존 조건이 id 국가로부터의 상환받기일때
                        // 파산시키면 안됨
                        if(input.kingdoms[c.Who].survive.Conditions.Any(
                            a => a == new List<WhoWhat>()
                            {new WhoWhat(){Who = id, What = tradeType.repo } }))
                        {
                            break;
                        }
                        

                    }
                    if (c.What == tradeType.bankrupt)
                    {

                        // id 국가의 파산 조건이 다른 나라의 파산이고
                        // 다른 나라가 파산할 때, 영원히 생존하는 나라가 있으면
                        // 파산시키면 안됨
                        if (isSurvives(list, new List<WhoWhat>()
                        {new WhoWhat(){ Who = c.Who, What= tradeType.bankrupt} }))
                        {
                            return false;
                        }
                        else
                        {

                        }
                    }
                    
                }
            }
            return true;
        }
        void Bankruptcy()
        {
            foreach (var kingdom in input.kingdoms)
                kingdom.Bankruptcy(id);
            isBankruptcyed = true;
        }
        public void Bankruptcy(int id)
        {
            var conditions = survive.Conditions;

            // 파산한 국가가 영향을 미치지 않으면 종료
            if (!conditions.Any(a=>a.Any(x => x.Who == id))) return;

                for (int i = 0; i < conditions.Count; i++)
            {
                var what = conditions[i].Find(f => f.Who == id).What;

                switch (what)
                {
                    case tradeType.bankrupt :
                        for (int j = 0; j < conditions[i].Count; j++)
                            if (conditions[i][j].Who == id) { 
                                conditions[i].RemoveAt(j);
                                
                            }
                        break;
                    case tradeType.repay :
                        new Exception("성립할 수 없는 경우입니다.");
                        break;
                    case tradeType.repo :
                        conditions.RemoveAt(i);
                        break;
                    default:
                        new Exception("정의되지 않은 타입");
                        break;
                }
                clearner();
                void clearner()
                {
                    for (int j = 0; j < survive.Conditions.Count; j++)
                        if (survive.Conditions[j].All(a => a.What == tradeType.repo))
                            survive.Conditions.RemoveAt(j);



                }
            }


        }

        public void Repay(int repoKingdom)
        {
            input.kingdoms[repoKingdom].Repo(id);
            for (int i = 0; i < bankruptcy.Conditions.Count; i++)
            {
                if (bankruptcy.Conditions[i].Contains(
                    new WhoWhat() { Who = repoKingdom, What = tradeType.bankrupt }))
                {
                    bankruptcy.Conditions.RemoveAt(i);
                }
            }
            

        }
        public void Repo(int id)
        {
            // 상환할 돈이 없으면 파산
            if (bankruptcy.Conditions.Contains(new List<WhoWhat>() {
            new WhoWhat(){ Who = id, What = tradeType.repay} })) 
            {
                Bankruptcy();
            }
                
        }
    }

    public class Input
    {
        string path = @"C:\Users\user\source\repos\1506022022\Algorithm\Algorithm_KingdomOfBankruptcy\Algorithm_KingdomOfBankruptcy\Input.txt";


        int kingdomCount;
        int testCaseCount;

        public List<Kingdom> kingdoms;
        public Input()
        {
            kingdoms = new List<Kingdom>();

            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {

                testCaseCount = int.Parse(reader.ReadLine());
                kingdomCount = int.Parse(reader.ReadLine());

                for (int i = 0; i < kingdomCount; i++)
                    kingdoms.Add(new Kingdom(i, reader.ReadLine(),this));



            }
        }
    }
}