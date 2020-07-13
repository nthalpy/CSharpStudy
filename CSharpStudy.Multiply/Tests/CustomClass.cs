using System;

namespace CSharpStudy.Multiply.Tests
{
    public sealed class CustomClass : TestBase
    {
        public sealed class Complex
        {
            public double Real;
            public double Imag;

            public Complex(double r, double i)
            {
                Real = r;
                Imag = i;
            }

            public static Complex operator *(Complex lhs, Complex rhs)
            {
                return new Complex(
                    lhs.Real * rhs.Real - lhs.Imag * rhs.Imag,
                    lhs.Real * rhs.Imag + lhs.Imag * rhs.Real
                );
            }

            public static bool operator !=(Complex lhs, Complex rhs)
            {
                return !(lhs == rhs);
            }
            public static bool operator ==(Complex lhs, Complex rhs)
            {
                return lhs.Real == rhs.Real && lhs.Imag == rhs.Imag;
            }
            public override bool Equals(object obj)
            {
                return obj is Complex complex &&
                       Real == complex.Real &&
                       Imag == complex.Imag;
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(Real, Imag);
            }
        }

        private Random rd;

        public override void Prepare()
        {
            rd = new Random(114514);

            Solution.Prepare<Complex, Complex, Complex>();
        }

        public override void TestRoutine()
        {
            for (int idx = 0; idx < 1000000; idx++)
            {
                Complex lhs = new Complex(rd.NextDouble(), rd.NextDouble());
                Complex rhs = new Complex(rd.NextDouble(), rd.NextDouble());

                Complex expected = lhs * rhs;
                Complex actual = Solution.Multiply<Complex, Complex, Complex>(lhs, rhs);

                if (expected != actual)
                    throw new TestFailException(expected, actual);
            }
        }
    }
}
