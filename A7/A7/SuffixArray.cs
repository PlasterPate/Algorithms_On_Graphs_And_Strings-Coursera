using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    public class SuffixArray
    {
        public Dictionary<char, int> chars = new Dictionary<char, int>()
        {
            {'$', 0 } ,
            {'A', 1 } ,
            {'C', 2 } ,
            {'G', 3 } ,
            {'T', 4 }
        };

        public int[] order;
        public int[] countsArr;
        public int[] classes;
        public string text;

        public SuffixArray(string text)
        {
            this.text = text;
            order = new int[text.Length];
            countsArr = new int[chars.Count];
            classes = new int[text.Length];
        }

        public long[] BuildSuffixArray()
        {
            SortCharacters();
            ComputeClasses();
            int len = 1;
            while (len < text.Length)
            {
                SortDoubled(len);
                UpdateClasses(len);
                len *= 2;
            }
            return order.Select(x => (long)x).ToArray();
        }

        private void BuildCountArray()
        {
            countsArr = Enumerable.Repeat(0, countsArr.Length).ToArray();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                countsArr[chars[c]]++;
            }
            for (int i = 1; i < countsArr.Length; i++)
            {
                countsArr[i] += countsArr[i - 1];
            }
        }

        private void SortCharacters()
        {
            BuildCountArray();
            for (int i = text.Length - 1; i >= 0; i--)
            {
                char c = text[i];
                int idx = chars[c];
                countsArr[idx]--;
                order[countsArr[idx]] = i;
            }
        }
        private void SortDoubled(int len)
        {
            int[] newOrder = new int[text.Length];
            int[] counts = new int[text.Length];
            newOrder = Enumerable.Repeat(0, newOrder.Length).ToArray();
            for (int i = 0; i < text.Length; i++)
            {
                counts[classes[i]]++;
            }
            for (int i = 1; i < text.Length; i++)
            {
                counts[i] += counts[i - 1];
            }
            int start;
            int cl; 
            for (int i = text.Length - 1; i >= 0; i--)
            {
                start = (order[i] - len + text.Length) % text.Length;
                cl = classes[start];
                counts[cl]--;
                newOrder[counts[cl]] = start;
            }
            Array.Copy(newOrder, order, order.Length);
        }


        private void ComputeClasses()
        {
            classes[order[0]] = 0;
            for (int i = 1; i < order.Length; i++)
            {
                classes[order[i]] = classes[order[i - 1]];
                if (text[order[i]] != text[order[i - 1]])
                {
                    classes[order[i]]++;
                }
            }
        }

        private void UpdateClasses(int len)
        {
            int[] newClasses = new int[text.Length];
            newClasses = Enumerable.Repeat(0, newClasses.Length).ToArray();
            newClasses[order[0]] = 0;
            int lCur, lPrev, rCur, rPrev;
            for (int i = 1; i < order.Length; i++)
            {
                lCur = order[i];
                lPrev = order[i - 1];
                rCur = (lCur + len) % text.Length;
                rPrev = (lPrev + len) % text.Length;
                newClasses[lCur] = newClasses[lPrev];
                if (classes[lCur] != classes[lPrev] || classes[rCur] != classes[rPrev])
                    newClasses[lCur]++;
            }
            Array.Copy(newClasses, classes, classes.Length);
        }

    }
}
