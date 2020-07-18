using BenchmarkDotNet.Attributes;
using System;

namespace CSharpStudy.Multiply.Tests
{
    [MemoryDiagnoser]
    public class FloatMultiply
    {
        private Random rd;

        public FloatMultiply()
        {
            rd = new Random();
        }

        [GlobalSetup]
        public void Setup()
        {
            Solution<Single, Single, Single>.Prepare();
        }

        [Benchmark]
        public void MainRoutine()
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
