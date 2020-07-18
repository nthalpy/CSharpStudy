using BenchmarkDotNet.Attributes;
using System;

namespace CSharpStudy.Multiply.Tests
{
    [SimpleJob(warmupCount: 25, targetCount: 100)]
    public class Int32Multiply
    {
        private int lhs;
        private int rhs;

        public Int32Multiply()
        {
            Random rd = new Random();

            lhs = rd.Next();
            rhs = rd.Next();
        }

        [GlobalSetup]
        public void Setup()
        {
            Solution<Int32, Int32, Int32>.Prepare();
        }

        [Benchmark]
        public void MainRoutine()
        {
            int expected = lhs * rhs;
            int actual = Solution<Int32, Int32, Int32>.Multiply(lhs, rhs);

            if (expected != actual)
                throw new TestFailException(expected, actual);
        }
    }
}
