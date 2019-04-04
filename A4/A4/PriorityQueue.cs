using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    public class PriorityQueue
    {
        public Point[] Points;
        public int size = 0;
        public int target = -1;

        public PriorityQueue(long pointCount, int target)
        {
            Points = new Point[pointCount + 1];
            this.target = target + 1;
        }

        public int Parent(int i)
        {
            return i / 2;
        }

        public int LeftChild(int i)
        {
            return 2 * i;
        }

        public int RightChild(int i)
        {
            return 2 * i + 1;
        }

        public void Add(Point p)
        {
            Points[++size] = p;
            SiftUp(size);
        }

        public Point ExtractMin()
        {
            if (size < 1)
                return null;
            Point minPoint = Points[1];
            Swap(1, size--);
            SiftDown(1);
            return minPoint;
        }

        private void SiftUp(int i)
        {
            if(i > 1 && Priority(Parent(i)) > Priority(i))
            {
                Swap(Parent(i), i);
                SiftUp(Parent(i));
            }
        }

        public void SiftDown(int i)
        {
            int minIdx = int.MaxValue;
            if (2 * i <= size)
                minIdx = LeftChild(i);
            if (2 * i + 1 <= size && Priority(RightChild(i)) < Priority(minIdx))
                minIdx = RightChild(i);
            if (minIdx == int.MaxValue)
                return;
            Swap(i, minIdx);
            SiftDown(minIdx);
        }

        public void ChangePriority(int i, double newCost)
        {
            double oldPriority = Priority(i);
            Points[i].cost = newCost;
            if (Priority(i) > oldPriority)
                SiftDown(i);
            else
                SiftUp(i);
        }

        private void Swap(int i1, int i2)
        {
            Point tempPoint = Points[i1];
            Points[i1] = Points[i2];
            Points[i2] = tempPoint;
        }

        public double Priority(int i)
        {
            return Points[i].cost + Points[i].targetDist;
        }
    }
}
