using Microsoft.SolverFoundation.Solvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestCommon;

namespace A3
{
    public class Q1FrequencyAssignment : Processor
    {
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public string[] Solve(int V, int E, long[,] matrix)
        {
            List<string> result = new List<string>();
            result.Add($"{V * 3} {V * 4 + matrix.GetLength(0) * 3}");
            for (int i = 0; i < V * 3; i+=3)
            {
                result.Add($"{i + 1} {i + 2} {i + 3} 0");
                result.Add($"-{i + 1} -{i + 2} 0");
                result.Add($"-{i + 1} -{i + 3} 0");
                result.Add($"-{i + 2} -{i + 3} 0");
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                long src = (matrix[i, 0] - 1) * 3;
                long dest = (matrix[i, 1] - 1) * 3;
                for (int j = 1; j <= 3; j++)
                {
                    result.Add($"-{src + j} -{dest + j} 0");
                }
            }
            return result.ToArray();
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
