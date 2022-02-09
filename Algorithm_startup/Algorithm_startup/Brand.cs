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
    public class Brand
    {
        // 미정 카운트
        int undecidedCount;
        public int UndecidedCount
        {
            get { return undecidedCount; }
            set 
            {
                // 카운트는 0 이상이어야 한다
                undecidedCount = value>=0?value:0;
            }
        }

        // 브랜드 이름
        char[] brandName;

        // 생성자
        public Brand()
        {
            // 브랜드 이름 길이를 넘겨받지 못했을 때에는
            int brandLength;

            // 직접 읽어온다.
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
                brandLength = reader.ReadLine().Length;
            

            // 브랜드 이름의 길이를 입력받는다.
            brandName = new char[brandLength];

            // ? 로 구성된 N 길이의 브랜드 이름을 생성한다.
            InitArray(brandName, brandLength, '?');

            // 브랜드 이름의 미정 카운트를 초기치로 설정한다.
            undecidedCount = brandLength;

        }
        public Brand(int brandLength) 
        {
            // 브랜드 이름의 길이를 입력받는다.
            brandName = new char [brandLength];

            // ? 로 구성된 N 길이의 브랜드 이름을 생성한다.
            InitArray(brandName, brandLength, '?');

            // 브랜드 이름의 미정 카운트를 초기치로 설정한다.
            undecidedCount = brandLength;
            
        }
        
        // 브랜드 이름 <1자> 결정
        public void Decided(Choice choice)
        {
            int index = choice.index;
            char value = choice.value;

            // 브랜드 이름이 결정될 때마다
            // <미정 카운트>를 감소시킨다.
            brandName[index] = value;
            undecidedCount--;

        }

        // 브랜드 이름 현재상태 출력
        public void WriteBrand()
        {
            WriteLine("\n[브랜드 이름]");
            foreach (char n in brandName)
                Write(n);

            WriteLine();
        }


    }
}
