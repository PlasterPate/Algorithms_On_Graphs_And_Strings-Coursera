using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q3PatternMatchingSuffixArray : Processor
    {
        public Q3PatternMatchingSuffixArray(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve);

        private long[] Solve(string text, long n, string[] patterns)
        {
            List<long> result = new List<long>();
            bool[] isCheckeds = new bool[text.Length];
            for (int j = 0; j < n; j++)
            {
                string newText = patterns[j] + "$" + text;
                int[] borderLenghts = new int[newText.Length];
                int borderCursor = 0;
                for (int i = 1; i < newText.Length; i++)
                {
                    if (borderCursor < 0)
                        borderCursor = 0;
                    do
                    {
                        if (newText[i] == newText[borderCursor])
                        {
                            borderCursor++;
                            borderLenghts[i] = borderCursor;
                            if (borderCursor == patterns[j].Length && isCheckeds[i - patterns[j].Length])
                            {
                                result.Add(i - (2 * patterns[j].Length));
                                isCheckeds[i - patterns[j].Length] = true;
                            }
                            break;
                        }
                        else
                        {
                            if (borderCursor > 0)
                                borderCursor = borderLenghts[borderCursor - 1];
                            else
                                borderCursor--;
                        }
                    }
                    while (borderCursor >= 0);
                }
            }
            return result.Count > 0 ? result.ToArray() : new long[] { -1 };
        }
    }
}
