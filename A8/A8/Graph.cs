using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A8
{
    public class Graph
    {
        private List<Node> nodes;
        private long maxFlow;

        public Graph(int nodeCount, int edgeCount, long[][] edges)
        {
            Nodes = new List<Node>();
            for (int i = 0; i < nodeCount; i++)
            {
                Nodes.Add(new Node());
                for (int j = 0; j < nodeCount; j++)
                {
                    Nodes[i].Edges.Add(j, 0);
                }
            }

            for (int i = 0; i < edgeCount; i++)
            {
                int u = (int)edges[i][0] - 1;
                int v = (int)edges[i][1] - 1;
                long w = (int)edges[i][2];
                Nodes[u].Edges[v] += w;
            }
        }

        public List<int> BFS(int s, int t)
        {
            SetAllNodesFalse();
            Queue<int> queue = new Queue<int>();
            Nodes[s].IsChecked = true;
            queue.Enqueue(s);
            while (queue.Any())
            {
                int tempIdx = queue.Dequeue();
                if (tempIdx == t)
                    break;
                Node temp = Nodes[tempIdx];
                for (int i = 0; i < temp.Edges.Count; i++)
                {
                    if(temp.Edges[i] > 0 && !Nodes[i].IsChecked)
                    {
                        Nodes[i].Prev = tempIdx;
                        Nodes[i].IsChecked = true;
                        queue.Enqueue(i);
                    }
                }
            }
            List<int> result = new List<int>();
            int prev = t;
            if (Nodes[prev].Prev == -1)
                return result;
            result.Add(prev);
            while(prev != s)
            {
                prev = nodes[prev].Prev;
                result.Add(prev);
            }
            result.Reverse();
            return result;
        }

        public void UpdateGrapgh(int[] path)
        {
            long minFlow = long.MaxValue;
            for (int i = 0; i < path.Length - 1; i++)
            {
                long tempW = Nodes[path[i]].Edges[path[i + 1]];
                if (tempW < minFlow)
                    minFlow = tempW;
            }
            for (int i = 0; i < path.Length - 1; i++)
            {
                Nodes[path[i]].Edges[path[i + 1]] -= minFlow;
                Nodes[path[i + 1]].Edges[path[i]] += minFlow;
            }
            MaxFlow += minFlow;
        }

        public void SetAllNodesFalse()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].IsChecked = false;
                Nodes[i].Prev = -1;
            }
        }
        public List<Node> Nodes { get => nodes; set => nodes = value; }
        public long MaxFlow { get => maxFlow; set => maxFlow = value; }
    }

    public class Node
    {
        private Dictionary<int, long> edges;
        private int prev;
        private bool isChecked;

        public Node()
        {
            Edges = new Dictionary<int, long>();
            Prev = -1;
            IsChecked = false;
        }

        public Dictionary<int, long> Edges { get => edges; set => edges = value; }
        public int Prev { get => prev; set => prev = value; }
        public bool IsChecked { get => isChecked; set => isChecked = value; }
    }

}
