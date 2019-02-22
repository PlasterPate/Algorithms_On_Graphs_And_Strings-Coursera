using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A12
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            Graph graph = new Graph(edges, nodeCount);
            Queue<Node> queue = new Queue<Node>();
            long connectedComponents = 0;
            for (int i = 1; i <= nodeCount; i++)
            {
                if(graph.GraphNodes[i].isChecked == false)
                {
                    queue.Enqueue(graph.GraphNodes[i]);
                    graph.GraphNodes[i].isChecked = true;
                    connectedComponents++;
                    Node temp = new Node(-1);
                    while (queue.Any())
                    {
                        temp = queue.Dequeue();
                        foreach (var c in temp.connecteds)
                        {
                            if (!graph.GraphNodes[c].isChecked)
                            {
                                graph.GraphNodes[c].isChecked = true;
                                queue.Enqueue(graph.GraphNodes[c]);
                            }
                        }
                    }
                }

            }
            return connectedComponents;
        }
    }
}
