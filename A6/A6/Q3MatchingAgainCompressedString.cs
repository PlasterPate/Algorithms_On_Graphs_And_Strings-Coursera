﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) => 
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, String[] patterns)
        {
            long[] result = new long[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = BWT.MatchCount(text, patterns[i]);
            }
            return result;
        }
    }
}
