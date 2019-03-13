using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
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
                GraphNodes[(int)edges[i][0]].edges.Add(new Edge(edges[i][0], edges[i][1], edges[i][2]));
            }
        }

        public long Dijkstra(Node startNode, long target = 0)
        {
            List<Edge> unprocessedEdges = new List<Edge>();
            startNode.height = 0;
            Node processNode = startNode;
            int whileCounter = GraphNodes.Count;
            while (whileCounter > 0)
            {
                whileCounter--;
                unprocessedEdges.AddRange(processNode.edges);
                Relax(processNode);
                processNode.isChecked = true;
                Edge minEdge = unprocessedEdges
                    .Where(e => !e.isChecked && !GraphNodes[(int)e.end].isChecked)
                    .OrderBy(e => GraphNodes[(int)e.end].height)
                    .FirstOrDefault();
                if(minEdge != null)
                {
                    minEdge.isChecked = true;
                    processNode = GraphNodes[(int)minEdge.end];
                }
                if (target == processNode.key)
                    return processNode.height;
            } 
            return -1;
        }

        public List<Node> BelmanFord(long start = 1, bool q3 = false)
        {
            GraphNodes[(int)start].height = 0;
            List<Node> negativeLoopNodes = new List<Node>();
            for (int i = 1; i < GraphNodes.Count ; i++)
            {
                foreach (var node in GraphNodes)
                {
                    foreach (var edge in node.edges)
                    {
                        if((!q3 && (node.height != int.MaxValue || GraphNodes[(int)edge.end].height == int.MaxValue)) ||
                            (q3 && !(node.height == int.MaxValue && GraphNodes[(int)edge.end].height == int.MaxValue)))
                            GraphNodes[(int)edge.end].height = Math.Min(GraphNodes[(int)edge.end].height, edge.weight + node.height);
                    }
                }
            }
            foreach (var node in GraphNodes)
            {
                foreach (var edge in node.edges)
                {
                    if ((!q3 && (node.height != int.MaxValue || GraphNodes[(int)edge.end].height == int.MaxValue)) ||
                         (q3 && !(node.height == int.MaxValue && GraphNodes[(int)edge.end].height == int.MaxValue)))
                        if (GraphNodes[(int)edge.end].height > node.height + edge.weight)
                            negativeLoopNodes.Add(node);
                }
            }
            return negativeLoopNodes;
        }

        private void SetAllNodeFalse()
        {
            foreach (var node in GraphNodes)
            {
                node.isChecked = false;
            }
        }

        public void Relax(Node node)
        {
            //if(node.height != int.MaxValue)
                foreach (Edge edge in node.edges)
                {
                    GraphNodes[(int)edge.end].height = Math.Min(GraphNodes[(int)edge.end].height, edge.weight + node.height);
                }
        }

        public List<Node> FindNegativeLoopNodes(long start)
        {
            List<Node> negativeLoopNodes = new List<Node>();
            negativeLoopNodes = BelmanFord(start, true);
            Queue<Node> queue = new Queue<Node>();
            foreach (var node in negativeLoopNodes)
            {
                queue.Enqueue(node);
            }
            Node temp = new Node(-1);
            while (queue.Any())
            {
                temp = queue.Dequeue();
                foreach (var edge in temp.edges)
                {
                    if (!GraphNodes[(int)edge.end].isChecked)
                    {
                        GraphNodes[(int)edge.end].isChecked = true;
                        queue.Enqueue(GraphNodes[(int)edge.end]);
                        negativeLoopNodes.Add(GraphNodes[(int)edge.end]);
                    }
                }
            }
            return negativeLoopNodes;
        }
    }

    public class Node
    {
        public int key;
        public List<Edge> edges;
        public bool isChecked;
        public long height;

        public Node(int key)
        {
            this.key = key;
            edges = new List<Edge>();
            height = int.MaxValue;
            isChecked = false;
        }
    }

    public class Edge
    {
        public long start;
        public long end;
        public long weight;
        public bool isChecked;
        public Edge(long start, long end, long weight)
        {
            this.start = start;
            this.end = end;
            this.weight = weight;
            isChecked = false;
        }
    }
}
