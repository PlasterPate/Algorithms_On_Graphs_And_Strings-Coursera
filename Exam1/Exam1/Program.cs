using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = File.ReadAllLines($"../../../dictionary.txt");
            Encryption encryptor = new Encryption("6", ' ', 'z', false);
            string text = encryptor.Decrypt("W6#*.+$/w'6w..$./w)/B6yw'' z6o -");
            KMP kmp = new KMP(text);
            int c = 0;
            for (int i = 0; i < dict.Length; i++)
            {
                c += kmp.Match(dict[i]);
            }
            //Console.WriteLine(c);
            double aa = 50;
            int bb = 1000;
            Console.WriteLine(aa / bb);
        }
    }
}
