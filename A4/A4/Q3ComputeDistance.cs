using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Q3ComputeDistance : Processor
    {
        public Q3ComputeDistance(string testDataName) : base(testDataName)
        {
            this.ExcludeTestCaseRangeInclusive(0, 37);
            this.ExcludeTestCaseRangeInclusive(39, 40);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long, long[][], long[][], long, long[][], long[]>)Solve);


        public long[] Solve(long nodeCount, 
                            long edgeCount,
                            long[][] points,
                            long[][] edges,
                            long queriesCount,
                            long[][] queries)
        {
            Graph myGraph = new Graph(nodeCount, points);
            myGraph.BuildEdges(edgeCount, edges);
            long[] results = new long[queriesCount];
            for (int i = 0; i < queriesCount; i++)
            {
                results[i] = myGraph.AStar(queries[i][0], queries[i][1]);
            }
            return results;
        }
    }
}
