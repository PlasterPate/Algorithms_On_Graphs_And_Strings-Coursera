using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    public class Point
    {
        public int id;
        public long x;
        public long y;
        public double cost;
        public bool isChecked;
        public double[] distances;
        public List<Edge> edges;
        public double targetDist;

        public Point(int id, long x, long y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            cost = int.MaxValue;
            isChecked = false;
            edges = new List<Edge>();
            targetDist = -1;
        }
    }

    public class Graph
    {
        public List<Point> Points;

        public Graph(long pointCount, long[][] points)
        {
            Points = new List<Point>();
            for (int i = 0; i < pointCount; i++)
            {
                Points.Add(new Point(i, points[i][0], points[i][1]));
                Points[i].distances = new double[pointCount];
            }
        }

        public double FindDistance(int fromId, int toId)
        {
            double xDist = Math.Abs(Points[fromId].x - Points[toId].x);
            double yDist = Math.Abs(Points[fromId].y - Points[toId].y);
            double dist = Math.Sqrt(Math.Pow(xDist, 2) + Math.Pow(yDist, 2));
            return dist;
        }

        public void BuildEdges(long edgeCount, long[][] edges)
        {
            foreach (var e in edges)
            {
                Points[(int)e[0] - 1].edges.Add(new Edge(e[0] - 1, e[1]-1, e[2]));
            }
        }

        public void SetDistances()
        {
            foreach (var p in Points)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    p.distances[i] = FindDistance(p.id, Points[i].id);
                }
            }
        }

        public void CalculateMSTLenght()
        {
            Points[0].cost = 0;
            Point minPoint = Points[0];
            minPoint.isChecked = true;
            for(int k = 0; k < Points.Count - 1; k++)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    if(i != minPoint.id && !Points[i].isChecked)
                    {
                        Points[i].cost = Math.Min(Points[i].cost, minPoint.distances[i]);
                    }
                }
                minPoint = Points.Where(p => !p.isChecked).OrderBy(p => p.cost).First();
                minPoint.isChecked = true;
            }
            
        }

        public double ClusterDistance(long clusterCount)
        {
            CalculateMSTLenght();
            return Math.Round(Points.OrderByDescending(p => p.cost).Skip((int)clusterCount - 2).First().cost, 6);
        }

        public long AStar(long start, long target)
        {
            start--;
            target--;
            PriorityQueue pq = new PriorityQueue(Points.Count, (int)target);
            foreach (var p in Points)
            {
                if (p.id == start)
                    p.cost = 0;
                else
                    p.cost = int.MaxValue;
                p.targetDist = FindDistance(p.id, (int)target);
                pq.Add(p);
            }
            Point processPoint;
            int whileCounter = Points.Count;
            while (whileCounter > 0)
            {
                whileCounter--;
                processPoint = pq.ExtractMin();
                if (processPoint == null)
                    return -1;
                if (processPoint.cost == int.MaxValue)
                    return -1;
                if (processPoint.id == target)
                    return (long)Points[(int)target].cost;
                foreach (var edge in processPoint.edges)
                {
                    if (Points[(int)edge.end].cost > processPoint.cost + edge.weight)
                    {
                        for (int i = 1; i < pq.Points.Length; i++)
                        {
                            if(pq.Points[i].id == (int)edge.end)
                                pq.ChangePriority(i, processPoint.cost + edge.weight);
                        }
                    }
                }
            }
            return -1;
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






// b r a e e o r a a a j f s a a y k e m a a c e h a a a g f s a a
// a n a h h n n o k m k r a g r a b r h z d r o e a e a n a g r a
// a h r e e i i n a o d r s d o h r e s n s a t h a k l m s m s u
// z a k k b n a d r o a a h o r a f e t e h s e d r a a a h a t r
// a g a c e a k n e o a r r a d d a k i a o o b a s m a b r a d d
// y a r v d r a h l y b e t s e a m r h d r a e h d a a m b p z r
// a a h b g m a h g r o t h v d e n e e z n h k a h m t o t h v d
// e r h o e a m r a e e e s a e r z o s e e a h k o k l a i e a e



// earahhobegammarhagereoetshavedreznoeseezenahhkkaohkmltaoitehaved





//6601941747572815533980582524271844
