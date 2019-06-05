using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q2CleaningApartment : Processor
    {
        public Q2CleaningApartment(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public string[] Solve(int V, int E, long[,] matrix)
        {
            List<string> resultTemp = new List<string>();
            for (int i = 0; i < V * V; i+=V)
            {
                string temp = string.Empty;
                for (int j = 1; j <= V; j++)
                {
                    temp += $"{i + j} ";
                }
                temp += "0";
                resultTemp.Add(temp);
                for (int j = 1; j <= V; j++)
                {
                    for (int k = j + 1; k <= V; k++)
                    {
                        resultTemp.Add($"-{i + j} -{i + k} 0");
                    }
                }
            }
            for (int i = 1; i <= V; i++)
            {
                string temp = string.Empty;
                for (int j = 0; j < V * V; j+=V)
                {
                    temp += $"{i + j} ";
                }
                temp += "0";
                resultTemp.Add(temp);
                for (int j = 0; j < V * V; j+=V)
                {
                    for (int k = j + V; k < V * V; k+=V)
                    {
                        resultTemp.Add($"-{i + j} -{i + k} 0");
                    }
                }
            }

            bool[,] adjMatrix = new bool[V, V];
            for (int i = 0; i < adjMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < adjMatrix.GetLength(1); j++)
                {
                    adjMatrix[i, j] = true;
                }
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                long n1 = matrix[i, 0] - 1;
                long n2 = matrix[i, 1] - 1;
                adjMatrix[n1, n2] = false;
                adjMatrix[n2, n1] = false;
            }
            for (int i = 0; i < adjMatrix.GetLength(0); i++)
            {
                for (int j = 0 ; j < adjMatrix.GetLength(1); j++)
                {
                    if (!adjMatrix[i, j] || i == j)
                        continue;
                    for (int k = 1; k < V ; k++)
                    {
                        resultTemp.Add($"-{V * i + k} -{V * j + k + 1} 0");
                    }
                }
            }


            List<string> result = new List<string>();
            result.Add($"{V * V} {resultTemp.Count}");
            result.AddRange(resultTemp);
            return result.ToArray();
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
