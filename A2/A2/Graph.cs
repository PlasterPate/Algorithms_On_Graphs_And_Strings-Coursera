using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2
{
    public class Graph
    {
        public List<Node> GraphNodes;

        public Graph(long[][] edges, long nodeCount)
        {
            GraphNodes = new List<Node>();
            for (int i = 0; i <= nodeCount; i++)
            {
                GraphNodes.Add(new Node(i));
            }
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                GraphNodes[(int)edges[i][0]].connecteds.Add((int)edges[i][1]);
                GraphNodes[(int)edges[i][1]].connecteds.Add((int)edges[i][0]);
            }
        }

        public long BFS(long start, long end)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(GraphNodes[(int)start]);
            GraphNodes[(int)start].isChecked = true;
            Node temp = new Node(-1);
            while (queue.Any())
            {
                temp = queue.Dequeue();
                foreach (var c in temp.connecteds)
                {
                    if (!GraphNodes[c].isChecked)
                    {
                        GraphNodes[c].isChecked = true;
                        GraphNodes[c].height = temp.height + 1;
                        queue.Enqueue(GraphNodes[c]);
                    }
                    if (temp.key == end)
                        return temp.height;
                }
            }
            return -1;
        }

        public int IsBipartite()
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(GraphNodes[1]);
            GraphNodes[1].part = 1;
            Node temp = new Node(-1);
            while (queue.Any())
            {
                temp = queue.Dequeue();
                foreach (var c in temp.connecteds)
                {
                    if (GraphNodes[c].part == 0)
                    {
                        GraphNodes[c].part = temp.part * -1;
                        queue.Enqueue(GraphNodes[c]);
                    }
                    else if (temp.part == GraphNodes[c].part)
                        return 0;
                }
            }
            return 1;
        }
    }

    public class Node
    {
        public int key;
        public List<int> connecteds;
        public bool isChecked;
        public long height;
        public int part;

        public Node(int key)
        {
            this.key = key;
            connecteds = new List<int>();
            height = 0;
            isChecked = false;
            part = 0;
        }
    }
}
