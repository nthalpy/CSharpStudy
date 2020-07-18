using BenchmarkDotNet.Attributes;
using System;

namespace CSharpStudy.Multiply.Tests
{
    [MemoryDiagnoser]
    public class Int32Multiply
    {
        private Random rd;

        public Int32Multiply()
        {
            rd = new Random();
        }

        [GlobalSetup]
        public void Setup()
        {
            Solution<Int32, Int32, Int32>.Prepare();
        }

        [Benchmark]
        public void MainRoutine()
        {
            int lhs = rd.Next();
            int rhs = rd.Next();

            int expected = lhs * rhs;
            int actual = Solution<Int32, Int32, Int32>.Multiply(lhs, rhs);

            if (expected != actual)
                throw new TestFailException(expected, actual);
        }
    }
}
