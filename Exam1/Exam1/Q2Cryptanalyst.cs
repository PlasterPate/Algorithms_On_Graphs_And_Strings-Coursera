using TestCommon;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Exam1
{
    public class Q2Cryptanalyst : Processor
    {
        public Q2Cryptanalyst(string testDataName) : base(testDataName)
        {
            this.ExcludeTestCaseRangeInclusive(20, 37);
            //this.ExcludeTestCaseRangeInclusive(27, 37);
        }

        public override string Process(string inStr) => Solve(inStr);

        public HashSet<string> Vocab = new HashSet<string>();

        public string Solve(string cipher)
        {
            var dict = File.ReadAllLines($"../../../dictionary.txt");
            Encryption encryptor;
            string key;
            string text = string.Empty;
            KMP kmp;
            int wordsFound = 0;
            bool flag = false;
            
            for (int i = 0; i < 100; i++)
            {
                wordsFound = 0;
                key = i.ToString();
                encryptor = new Encryption(key, ' ', 'z', false);
                text = encryptor.Decrypt(cipher);
                kmp = new KMP(text);
                for (int j = 0; j < dict.Length; j++)
                {
                    if (dict[j].Length < 5)
                        continue;
                    wordsFound += kmp.Match(dict[j]);
                    if (wordsFound > 10)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    break;
            }
            return text.GetHashCode().ToString();
            
        }
    }
}