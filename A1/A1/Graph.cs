using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A12
{
    public class Graph
    {
        public List<Node> GraphNodes;

        public Graph(long[][] edges, long nodeCount, bool isDirectedGraph = false)
        {
            GraphNodes = new List<Node>();
            for (int i = 0; i <= nodeCount; i++)
            {
                GraphNodes.Add(new Node(i));
            }
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                GraphNodes[(int)edges[i][0]].connecteds.Add((int)edges[i][1]);
                if(!isDirectedGraph)
                    GraphNodes[(int)edges[i][1]].connecteds.Add((int)edges[i][0]);
                else
                    GraphNodes[(int)edges[i][1]].reverseConnecteds.Add((int)edges[i][0]);
            }
        }

        public void SetAllFalse()
        {
            for (int i = 0; i < GraphNodes.Count; i++)
            {
                GraphNodes[i].isChecked = false;
            }
        }

        public void BFSAllNodes()
        {
            foreach (var n in GraphNodes)
            {
                SetAllFalse();
                Queue<Node> queue = new Queue<Node>();
                queue.Enqueue(n);
                n.isChecked = true;
                Node temp = new Node(-1);
                while (queue.Any())
                {
                    temp = queue.Dequeue();
                    foreach (var c in temp.connecteds)
                    {
                        if (!GraphNodes[c].isChecked)
                        {
                            GraphNodes[c].isChecked = true;
                            n.bfsConnecteds.Add(c);
                            queue.Enqueue(GraphNodes[c]);
                        }
                    }
                }
            }
            SetAllFalse();
        }
    }

    public class Node
    {
        public int key;
        public List<int> connecteds;
        public List<int> reverseConnecteds;
        public List<int> bfsConnecteds;
        public bool isChecked;

        public Node(int key)
        {
            this.key = key;
            connecteds = new List<int>();
            reverseConnecteds = new List<int>();
            bfsConnecteds = new List<int>();
            isChecked = false;
        }
    }
}
