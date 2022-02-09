#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_startup
{
    class Component
    {
        public const string path = @"C:\Users\user\source\repos\1506022022\Algorithm\Algorithm_startup\Algorithm_startup\Input.txt";
        public static void InitArray<T>(T [] array, int length,T value)
        {
            for (int i = 0; i < length; i++)
                array.SetValue(value, i);
        }
    }
}
