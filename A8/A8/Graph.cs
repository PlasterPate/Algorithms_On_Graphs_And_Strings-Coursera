using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A8
{
    public class Graph
    {
        private long[][] matrix;
        private long maxFlow;
        private int[] prevs;
        private bool[] isCheckeds;

        public Graph(int nodeCount, int edgeCount, long[][] edges)
        {
            Prevs = new int[nodeCount];
            IsCheckeds = new bool[nodeCount];

            Matrix = new long[nodeCount][];
            for (int i = 0; i < Matrix.Length; i++)
            {
                Matrix[i] = new long[nodeCount];
            }

            for (int i = 0; i < edgeCount; i++)
            {
                int u = (int)edges[i][0] - 1;
                int v = (int)edges[i][1] - 1;
                long w = (int)edges[i][2];
                Matrix[u][v] += w;
            }
        }

        public Graph(long flightCount, long crewCount, long[][] info)
        {
            Prevs = new int[flightCount + crewCount + 2];
            IsCheckeds = new bool[flightCount + crewCount + 2];

            Matrix = new long[flightCount + crewCount + 2][];
            for (int i = 0; i < Matrix.Length; i++)
            {
                Matrix[i] = new long[flightCount + crewCount + 2];
            }

            for (int i = 1; i < flightCount + 1; i++)
            {
                for (int j = (int)flightCount + 1; j < Matrix.Length - 1; j++)
                {
                    Matrix[i][j] = info[i - 1][j - ((int)flightCount + 1)];
                }
            }

            for (int j = 0; j < flightCount + 1; j++)
            {
                Matrix[0][j] = 1;
            }
            for (int i = (int)flightCount + 1; i < Matrix.Length; i++)
            {
                Matrix[i][Matrix.Length - 1] = 1;
            }
        }

        public List<int> BFS(int s, int t)
        {
            SetAllNodesFalse();
            Queue<int> queue = new Queue<int>();
            IsCheckeds[s] = true;
            queue.Enqueue(s);
            while (queue.Any())
            {
                int tempIdx = queue.Dequeue();
                if (tempIdx == t)
                    break;
                for (int i = 0; i < Matrix.Length; i++)
                {
                    if(Matrix[tempIdx][i] > 0 && !IsCheckeds[i])
                    {
                        Prevs[i] = tempIdx;
                        IsCheckeds[i] = true;
                        queue.Enqueue(i);
                    }
                }
            }
            List<int> result = new List<int>();
            int prev = t;
            if (Prevs[prev] == -1)
                return result;
            result.Add(prev);
            while(prev != s)
            {
                prev = Prevs[prev];
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
                long tempW = Matrix[path[i]][path[i + 1]];
                if (tempW < minFlow)
                    minFlow = tempW;
            }
            for (int i = 0; i < path.Length - 1; i++)
            {
                Matrix[path[i]][path[i + 1]] -= minFlow;
                Matrix[path[i + 1]][path[i]] += minFlow;
            }
            MaxFlow += minFlow;
        }

        public long[] AssignFlights(int flightCount)
        {
            while (true)
            {
                int[] path = BFS(0, Matrix.Length - 1).ToArray();
                if (path.Length == 0)
                    break;
                UpdateGrapgh(path);
            }
            long[] result = new long[flightCount];
            result = Enumerable.Repeat((long)-1, flightCount).ToArray();
            for (int i = flightCount; i < Matrix.Length - 1; i++)
            {
                for (int j = 1; j < flightCount + 1; j++)
                {
                    if (Matrix[i][j] == 1)
                        result[j - 1] = i - flightCount;
                }
            }
            return result;
        }

        public void SetAllNodesFalse()
        {
            for (int i = 0; i < Prevs.Length; i++)
            {
                IsCheckeds[i] = false;
                Prevs[i] = -1;
            }
        }
        public long MaxFlow { get => maxFlow; set => maxFlow = value; }
        public long[][] Matrix { get => matrix; set => matrix = value; }
        public int[] Prevs { get => prevs; set => prevs = value; }
        public bool[] IsCheckeds { get => isCheckeds; set => isCheckeds = value; }
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
