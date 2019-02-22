using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A12
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            Graph graph = new Graph(edges, nodeCount);
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(graph.GraphNodes[(int)StartNode]);
            graph.GraphNodes[(int)StartNode].isChecked = true;
            Node temp = new Node(-1);
            while (queue.Any())
            {
                temp = queue.Dequeue();
                foreach (var c in temp.connecteds)
                {
                    if (c == EndNode)
                        return 1;
                    if (!graph.GraphNodes[c].isChecked)
                    {
                        graph.GraphNodes[c].isChecked = true;
                        queue.Enqueue(graph.GraphNodes[c]);
                    }
                }
            }
            return 0;
        }    
     }
}
