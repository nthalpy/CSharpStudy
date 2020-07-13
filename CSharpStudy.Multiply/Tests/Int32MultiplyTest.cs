using System;

namespace CSharpStudy.Multiply.Tests
{
    public sealed class Int32MultiplyTest : TestBase
    {
        private Random rd;

        public override void Prepare()
        {
            rd = new Random(114514);

            Solution.Prepare<Int32, Int32, Int32>();
        }

        public override void TestRoutine()
        {
            for (int idx = 0; idx < 1000000; idx++)
            {
                int lhs = rd.Next();
                int rhs = rd.Next();

                int expected = lhs * rhs;
                int actual = Solution.Multiply<Int32, Int32, Int32>(lhs, rhs);

                if (expected != actual)
                    throw new TestFailException(expected, actual);
            }
        }
    }
}
