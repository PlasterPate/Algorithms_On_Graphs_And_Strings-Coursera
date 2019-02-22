using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A12
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            Graph graph = new Graph(edges, nodeCount, true);
            Queue<Node> queue = new Queue<Node>();
            for (int i = 1; i <= nodeCount; i++)
            {
                graph.SetAllFalse();
                queue.Enqueue(graph.GraphNodes[i]);
                Node temp = new Node(-1);
                while (queue.Any())
                {
                    temp = queue.Dequeue();
                    foreach (var c in temp.connecteds)
                    {
                        if (c == graph.GraphNodes[i].key)
                            return 1;
                        if(!queue.Contains(graph.GraphNodes[c]))
                            queue.Enqueue(graph.GraphNodes[c]);
                    }
                }
            }
            return 0;
        }
    }
}