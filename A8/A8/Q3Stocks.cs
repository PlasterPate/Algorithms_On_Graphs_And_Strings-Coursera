using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q3Stocks : Processor
    {
        public Q3Stocks(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long stockCount, long pointCount, long[][] matrix)
        {
            long[][] newMatrix = new long[stockCount][];
            for (int i = 0; i < stockCount; i++)
            {
                newMatrix[i] = new long[stockCount];
            }
            for (int i = 0; i < stockCount; i++)
            {
                for (int j = 0; j < stockCount; j++)
                {
                    newMatrix[i][j] = 1;
                }
            }
            for (int i = 0; i < stockCount; i++)
            {
                for (int j = 0; j < stockCount; j++)
                {
                    for (int k = 0; k < pointCount; k++)
                    {
                        if(matrix[i][k] >= matrix[j][k])
                        {
                            newMatrix[i][j] = 0;
                            break;
                        }
                    }
                }
            }
            Graph myGraph = new Graph(stockCount, stockCount, newMatrix);
            myGraph.AssignFlights((int)stockCount);
            return stockCount - myGraph.MaxFlow;
        }
    }
}
