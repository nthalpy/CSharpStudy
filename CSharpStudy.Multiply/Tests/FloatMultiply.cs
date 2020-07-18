using BenchmarkDotNet.Attributes;
using System;

namespace CSharpStudy.Multiply.Tests
{
    [SimpleJob(warmupCount: 25, targetCount: 100)]
    public class FloatMultiply
    {
        private float lhs;
        private float rhs;

        public FloatMultiply()
        {
            Random rd = new Random();

            lhs = (float)rd.NextDouble();
            rhs = (float)rd.NextDouble();
        }

        [GlobalSetup]
        public void Setup()
        {
            Solution<Single, Single, Single>.Prepare();
        }

        [Benchmark]
        public void MainRoutine()
        {
            Single expected = lhs * rhs;
            Single actual = Solution<Single, Single, Single>.Multiply(lhs, rhs);

            if (expected != actual)
                throw new TestFailException(expected, actual);
        }
    }
}
