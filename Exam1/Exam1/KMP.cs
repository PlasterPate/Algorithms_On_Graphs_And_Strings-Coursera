using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1
{
    public class KMP
    {
        public string text;
        public KMP(string text)
        {
            this.text = text;
            
        }

        public int Match(string pattern)
        {
            int matchCount = 0;
            string newText = pattern + "$" + text;
            int[] borderLenghts = new int[newText.Length];
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
                        if (borderCursor == pattern.Length)
                        {
                            matchCount++;
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
            return matchCount;
        }
    }
}
