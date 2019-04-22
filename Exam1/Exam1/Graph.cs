using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1
{
    public class Graph
    {
        public long[] betweenness;
        public List<Node> Nodes;
        public long nodeCount;

        public Graph(long n, long[][] edges)
        {
            nodeCount = n;
            betweenness = new long[n];
            Nodes = new List<Node>();
            for (int i = 0; i < nodeCount; i++)
            {
                Nodes.Add(new Node());
            }
            foreach (var edge in edges)
            {
                Nodes[(int)edge[0] - 1].adjacents.Add((int)edge[1] - 1);
            }
            foreach (var node in Nodes)
            {
                node.adjacents = node.adjacents.OrderByDescending(x => x).ToList();
            }
        }

        public void ReInitialize()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].prev = -1;
                Nodes[i].isChecked = false;
            }
        }

        public long[] CalculateBetweenness()
        {
            int[] path;
            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = 0; j < nodeCount; j++)
                {
                    if(i != j)
                    {
                        path = BFS(i, j);
                        foreach (var p in path)
                        {
                            betweenness[p]++;
                        }
                        ReInitialize();
                    }
                }
            }
            //Array.Sort(betweenness);
            return betweenness;
        }
        
        public int[] BFS(int from, int to)
        {
            List<int> result = new List<int>();
            bool isFound = false;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(from);
            Nodes[from].isChecked = true;
            while (queue.Any())
            {
                int temp = queue.Dequeue();
                foreach (var i in Nodes[temp].adjacents)
                {
                    if (Nodes[i].isChecked)
                        continue;
                    Nodes[i].prev = temp;
                    queue.Enqueue(i);
                    Nodes[i].isChecked = true;
                    if(i == to)
                    {
                        isFound = true;
                        break;
                    }
                }
                if (isFound)
                    break;
            }
            if (isFound)
            {
                to = Nodes[to].prev;
                while(to != from)
                {
                    result.Add(to);
                    to = Nodes[to].prev;
                }
            }
            return result.ToArray();
        }

        public class Node
        {
            public List<int> adjacents;
            public int prev = -1;
            public bool isChecked;

            public Node()
            {
                adjacents = new List<int>();
                isChecked = false;
            }
        }
    }
}
