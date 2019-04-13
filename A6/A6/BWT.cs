using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A6
{
    public static class BWT
    {
        public static string BWTransform(string text)
        {
            string[] Transforms = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                Transforms[i] = text.Substring(i, text.Length - i) + text.Substring(0, i);
            }
            Array.Sort(Transforms);
            string TransformedText = string.Empty;
            for (int i = 0; i < text.Length; i++)
            {
                TransformedText += Transforms[i][text.Length - 1];
            }
            return TransformedText;
        }

        public static string InverseBWT(string text)
        {
            StringBuilder sb = new StringBuilder();
            List<(int, char)> firstCol = new List<(int, char)>();
            for (int j = 0; j < text.Length; j++)
            {
                firstCol.Add((j, text[j]));
            }
            firstCol = firstCol.OrderBy(x => x.Item2).ToList();
            (int, char)[] lastCol = new(int, char)[text.Length];
            for (int j = 0; j < text.Length; j++)
            {
                lastCol[firstCol[j].Item1] = (j, firstCol[j].Item2);
            }
            int i = firstCol[0].Item1;
            while(sb.Length < text.Length)
            {
                sb.Append(lastCol[i].Item2);
                i = lastCol[i].Item1;
            }
            return String.Join("", sb.ToString().ToCharArray().Reverse());
        }

        public static int MatchCount(string text, string pattern)
        {
            List<(int, char)> firstCol = new List<(int, char)>();
            for (int j = 0; j < text.Length; j++)
            {
                firstCol.Add((j, text[j]));
            }
            firstCol = firstCol.OrderBy(x => x.Item2).ToList();
            (int, char)[] lastCol = new(int, char)[text.Length];
            for (int j = 0; j < text.Length; j++)
            {
                lastCol[firstCol[j].Item1] = (j, firstCol[j].Item2);
            }
            int topIdx = 0;
            int bottomIdx = text.Length;
            int bottomIdxHolder = bottomIdx;
            char lastChar;
            bool flag = false;
            for (int j = pattern.Length - 1; j >= 0; j--)
            {
                bottomIdxHolder = bottomIdx;
                lastChar = pattern[j];
                flag = false;
                for (int i = topIdx; i < bottomIdxHolder; i++)
                {
                    if(lastCol[i].Item2 == lastChar)
                        if (!flag)
                        {
                            topIdx = lastCol[i].Item1;
                            bottomIdx = topIdx + 1;
                            flag = true;
                        }
                        else
                        {
                            bottomIdx = lastCol[i].Item1 + 1;
                        }
                }
                if (!flag)
                    return 0;
            }
            return bottomIdx - topIdx;
        }

        public static long[] SuffixArray(string text)
        {
            long[] result = new long[text.Length];
            string[] suffixArray = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                suffixArray[i] = text.Substring(i, text.Length - i);
            }
            Array.Sort(suffixArray);
            for (int i = 0; i < text.Length; i++)
            {
                result[i] = text.Length - suffixArray[i].Length;
            }
            return result;
        }
    }
}
