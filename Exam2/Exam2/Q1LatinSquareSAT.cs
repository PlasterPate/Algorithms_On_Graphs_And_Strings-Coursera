using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam2
{
    public class Q1LatinSquareSAT : Processor
    {
        public Q1LatinSquareSAT(string testDataName) : base(testDataName)
        {}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatVerifier;

        StringBuilder resultTemp;
        public string Solve(int dim, int?[,] square)
        {
            resultTemp = new StringBuilder();
            int cCount = 0;
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    int varNumber = (dim * dim * i) + (dim * j);
                    for (int k = 0; k < dim; k++)
                        resultTemp.Append($"{varNumber + k + 1} ");
                    cCount++;
                    resultTemp.Append("0\n");
                    for (int ki = 0; ki < dim; ki++)
                    {
                        for (int kj = ki + 1; kj < dim; kj++)
                        {
                            resultTemp.Append($"-{varNumber + ki + 1} -{varNumber + kj + 1} 0\n");
                            cCount++;
                        }
                    }
                }
            }

            for (int i = 0; i < dim; i++)
            {
                for (int k = 0; k < dim; k++)
                {
                    int varNumber = (dim * dim * i) + k + 1;
                    for (int j = 0; j < dim; j++)
                        resultTemp.Append($"{varNumber + dim * j} ");
                    cCount++;
                    for (int ji = 0; ji < dim; ji++)
                    {
                        for (int jj = ji + 1; jj < dim; jj++)
                        {
                            resultTemp.Append($"-{varNumber + dim * ji} -{varNumber + dim * jj} 0\n");
                            cCount++;
                        }
                    }
                }
            }

            for (int j = 0; j < dim; j++)
            {
                for (int k = 0; k < dim; k++)
                {
                    int varNumber = (dim * j) + k + 1;
                    for (int i = 0; i < dim; i++)
                        resultTemp.Append($"{varNumber + dim * dim* i} ");
                    cCount++;
                    for (int ii = 0; ii < dim; ii++)
                    {
                        for (int ij = ii + 1; ij < dim; ij++)
                        {
                            resultTemp.Append($"-{varNumber + dim * dim * ii} -{varNumber + dim * dim * ij} 0\n");
                            cCount++;
                        }
                    }
                }
            }


            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if(!square[i, j].HasValue)
                    {
                        RowConditions(i, j, dim, square);
                        ColConditions(i, j, dim, square);
                    }
                }
            }

            StringBuilder result = new StringBuilder($"{cCount} {dim * dim * dim}\n");
            result.Append(resultTemp);
            return result.ToString();
        }

        private void ColConditions(int i, int j, int dim, int?[,] square)
        {
            for (int col = 0; col < dim; col++)
            {
                if (col == j || !square[i, col].HasValue)
                    continue;
                int value = square[i, col].Value;
                int varNumber = (dim * dim * i) + value + 1;
                resultTemp.Append($"-{varNumber + dim * j} 0\n");
            }
        }

        private void RowConditions(int i, int j, int dim, int?[,] square)
        {
            for (int row = 0; row < dim; row++)
            {
                if (row == i || !square[row, j].HasValue)
                    continue;
                int value = square[row, j].Value;
                int varNumber = (dim * j) + value + 1;
                resultTemp.Append($"-{varNumber + dim * dim * i} 0\n");
            }
        }
    }
}
