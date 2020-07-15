using System;

namespace CSharpStudy.Multiply.Tests
{
    public sealed class Int32Multiply : TestBase
    {
        private Random rd;

        public override void Prepare()
        {
            rd = new Random(114514);

            Solution<Int32, Int32, Int32>.Prepare();
        }

        public override void TestRoutine()
        {
            for (int idx = 0; idx < 1000000; idx++)
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
}
