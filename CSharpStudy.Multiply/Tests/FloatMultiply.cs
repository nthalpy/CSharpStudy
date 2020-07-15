﻿using System;

namespace CSharpStudy.Multiply.Tests
{
    public sealed class FloatMultiply : TestBase
    {
        private Random rd;

        public override void Prepare()
        {
            rd = new Random(114514);

            Solution<Single, Single, Single>.Prepare();
        }

        public override void TestRoutine()
        {
            for (int idx = 0; idx < 1000000; idx++)
            {
                Single lhs = (Single)rd.NextDouble();
                Single rhs = (Single)rd.NextDouble();

                Single expected = lhs * rhs;
                Single actual = Solution<Single, Single, Single>.Multiply(lhs, rhs);

                if (expected != actual)
                    throw new TestFailException(expected, actual);
            }
        }
    }
}
