using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q3ExchangingMoney:Processor
    {
        public Q3ExchangingMoney(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);


        public string[] Solve(long nodeCount, long[][] edges,long startNode)
        {
            Graph myGraph = new Graph(edges, nodeCount);
            List<Node> negativeLoopNodes = myGraph.FindNegativeLoopNodes(startNode);
            string[] result = new string[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                long h = myGraph.GraphNodes[i + 1].height;
                if (h > 10000000)
                    result[i] = "*";
                else
                    result[i] = h.ToString();
            }
            foreach (var n in negativeLoopNodes)
            {
                result[n.key - 1] = "-";
            }
            return result.ToArray();
        }
    }
}
