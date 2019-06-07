using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q3AdBudgetAllocation : Processor
    {
        public Q3AdBudgetAllocation(string testDataName) 
            : base(testDataName)
        {
            ExcludeTestCases(1, 14, 35);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long[], string[]>)Solve);

        public string[] Solve(long eqCount, long varCount, long[][] A, long[] b)
        {
            List<string> resultTemp = new List<string>();
            List<int> nonZeroes;
            for (int i = 0; i < A.Length; i++)
            {
                nonZeroes = new List<int>();
                for (int j = 0; j < A[0].Length; j++)
                {
                    if (A[i][j] != 0)
                        nonZeroes.Add(j + 1);
                }
                int[] binaries = new int[nonZeroes.Count];
                int nonZeroCount = nonZeroes.Count;
                long sum = 0;
                for (int k = 0; k < Math.Pow(2, nonZeroCount); k++)
                {
                    sum = 0;
                    binaries = GetIntBinaryArray(k, nonZeroCount);
                    for (int l = 0; l < nonZeroCount; l++)
                    {
                        sum += A[i][nonZeroes[l] - 1] * binaries[l];
                    }
                    if(sum > b[i])
                    {
                        string temp = string.Empty;
                        for (int j = 0; j < binaries.Length; j++)
                        {
                            if (binaries[j] == 0)
                                binaries[j]--;
                            temp += (binaries[j] * nonZeroes[j]) + " ";
                        }
                        resultTemp.Add($"{temp}0");
                    }
                }
            }

            List<string> result = new List<string>();
            result.Add($"{varCount} {resultTemp.Count}");
            result.AddRange(resultTemp);
            return result.ToArray();
        }

        /// <summary>
        /// This method is copied and modified 
        /// from https://www.dotnetperls.com/integer-increment-binary
        /// </summary>
        /// <param name="n">decimal number</param>
        /// <param name="len">number of bits</param>
        /// <returns>binary equivalent of input number in an integer array</returns>
        public int[] GetIntBinaryArray(int n, int len)
        {
            int[] b = new int[len];
            int pos = len - 1;
            int i = 0;

            while (i < len)
            {
                if ((n & (1 << i)) != 0)
                    b[pos] = 1;
                else
                    b[pos] = 0;

                pos--;
                i++;
            }
            return b;
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
