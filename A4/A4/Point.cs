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

        public Point(int id, long x, long y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            cost = int.MaxValue;
            isChecked = false;
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
            SetDistances();
        }

        public double FindDistance(int fromId, int toId)
        {
            double xDist = Math.Abs(Points[fromId].x - Points[toId].x);
            double yDist = Math.Abs(Points[fromId].y - Points[toId].y);
            double dist = Math.Sqrt(Math.Pow(xDist, 2) + Math.Pow(yDist, 2));
            return dist;
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
    }
}
