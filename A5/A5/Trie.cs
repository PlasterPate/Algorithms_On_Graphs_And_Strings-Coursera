using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5
{
    public class Trie
    {
        public List<Node> nodes;
        public int size;
        public List<string> result = new List<string>();

        public Trie(string[] patterns)
        {
            nodes = new List<Node>();
            nodes.Add(new Node(0));
            size = 1;
            Node currentNode = nodes[0];
            int startingIdx = 0;
            foreach (var p in patterns)
            {
                currentNode = nodes[0];
                for (int i = 0; i < p.Length; i++)
                {
                    AddEdge(p, i, ref currentNode, startingIdx);
                }
                currentNode.isLeaf = true;
                startingIdx++;
            }
        }

        public void AddEdge(string p, int i, ref Node currentNode, int startingIdx)
        {
            foreach (var e in currentNode.edges)
            {
                if (e.label == p[i])
                {
                    currentNode = nodes[e.end];
                    if (!e.isChecked)
                    {
                        e.isChecked = true;
                        result.Add($"{e.start}->{e.end}:{e.label}");
                    }
                    else
                        return;
                }
            }
            nodes.Add(new Node(size));
            currentNode.edges.Add(new Edge(currentNode.id, size, p[i], true));
            result.Add($"{currentNode.id}->{size}:{p[i]}");
            currentNode = nodes[size++];
        }

        public long[] TrieMatching(string text, string[] patterns)
        {
            List<long> results = new List<long>();
            Node currentNode = nodes[0];
            for (int i = 0; i < text.Length; i++)
            {
                currentNode = nodes[0];
                if (PrefixMatching(text, i, ref currentNode))
                    results.Add(i);
            }
            return results.ToArray();
        }

        public bool PrefixMatching(string text, int i, ref Node currentNode)
        {
            bool flag = false;
            for (; i < text.Length; i++)
            {
                flag = false;
                foreach (var e in currentNode.edges)
                {
                    if(e.label == text[i])
                    {
                        if (nodes[e.end].isLeaf)
                            return true;
                        else
                        {
                            currentNode = nodes[e.end];
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag)
                    continue;
                break;
            }
            return false;
        }
    }

    public class Node
    {
        public int id;
        public List<Edge> edges;
        public bool isLeaf;
        
        public Node(int id)
        {
            this.id = id;
            edges = new List<Edge>();
            isLeaf = false;
        }
    }

    public class Edge
    {
        public int start;
        public int end;
        public char label;
        public bool isChecked;

        public Edge(int start, int end, char label, bool isChecked = false)
        {
            this.start = start;
            this.end = end;
            this.label = label;
            this.isChecked = isChecked;
        }
    }
}
