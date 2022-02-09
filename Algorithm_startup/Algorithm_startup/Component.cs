#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_startup
{
    public class Component
    {
        // 입력 데이터 텍스트 파일 위치
        public const string path = @"C:\Users\user\source\repos\1506022022\Algorithm\Algorithm_startup\Algorithm_startup\Input.txt";

        // 배열 초기화
        public static void InitArray<T>(T [] array, int length,T value)
        {
            for (int i = 0; i < length; i++)
                array.SetValue(value, i);
        }

        // 플레이어의 선택 정보 <위치, 문자>
        public struct Choice
        {
            public int index;
            public char value;
        }

        // 플레이어에게 주어지는 정보 <위치>
        public struct Infomation
        {
            public int brandUndecidedCount;
        }

    }
}
