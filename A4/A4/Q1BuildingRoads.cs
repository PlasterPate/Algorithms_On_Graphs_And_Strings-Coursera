using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Q1BuildingRoads : Processor
    {
        public Q1BuildingRoads(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], double>)Solve);


        public double Solve(long pointCount, long[][] points)
        {
            Graph myGraph = new Graph(pointCount, points);
            myGraph.SetDistances();
            myGraph.CalculateMSTLenght();
            return Math.Round(myGraph.Points.Sum(p => p.cost), 6);
        }
    }
}
