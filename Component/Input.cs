using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Component
{
    class Input
    {
        List<string> inputs;
        public Input(string path)
        {
            inputs = new List<string>();
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                while (!reader.EndOfStream) 
                    inputs.Add(reader.ReadLine());
            }
        }

    }
}
