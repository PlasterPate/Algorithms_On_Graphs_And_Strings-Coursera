﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q1Evaquating : Processor
    {
        public Q1Evaquating(string testDataName) : base(testDataName)
        {
            //this.ExcludeTestCaseRangeInclusive(1, 1);
            //this.ExcludeTestCaseRangeInclusive(11, 100);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long nodeCount, long edgeCount, long[][] edges)
        {
            Graph mygraph = new Graph((int)nodeCount, (int)edgeCount, edges);
            while (true)
            {
                int[] path = mygraph.BFS(0, (int)nodeCount - 1).ToArray();
                if (path.Length == 0)
                    break;
                mygraph.UpdateGrapgh(path);
            }
            return mygraph.MaxFlow;
        }
    }
}
