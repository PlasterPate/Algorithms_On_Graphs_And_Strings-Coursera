using System;
using System.Linq;
using TestCommon;

namespace A12
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            Graph graph = new Graph(edges, nodeCount, true);
            graph.BFSAllNodes();
            long SCC = nodeCount;
            foreach (var n in graph.GraphNodes.Skip(1))
            {
                if (n.isChecked == true)
                    continue;
                foreach (var c in n.bfsConnecteds)
                {
                    if (graph.GraphNodes[c].bfsConnecteds.Contains(n.key))
                    {
                        SCC--;
                        graph.GraphNodes[c].isChecked = true;
                    }
                }
            }
            return SCC;
        }
    }
}
