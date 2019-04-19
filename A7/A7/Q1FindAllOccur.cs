using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String, long[]>)Solve);

        public long[] Solve(string text, string pattern)
        {
            string newText = pattern + "$" + text;
            int[] borderLenghts = new int[newText.Length];
            Enumerable.Repeat(0, borderLenghts.Length);
            List<long> result = new List<long>();
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
                        if(borderCursor == pattern.Length)
                        {
                            result.Add(i - (2 * pattern.Length ));
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
            return result.Count > 0 ? result.ToArray() : new long[] { -1 };
        }
    }
}
