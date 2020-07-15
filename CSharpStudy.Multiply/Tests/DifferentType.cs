﻿using System;

namespace CSharpStudy.Multiply.Tests
{
    public sealed class DifferentType : TestBase
    {
        public struct Complex
        {
            public double Real;
            public double Imag;

            public Complex(double r, double i)
            {
                Real = r;
                Imag = i;
            }

            public static Complex operator *(double lhs, Complex rhs)
            {
                return new Complex(lhs * rhs.Real, lhs * rhs.Imag);
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

            Solution<Double, Complex, Complex>.Prepare();
        }

        public override void TestRoutine()
        {
            for (int idx = 0; idx < 1000000; idx++)
            {
                Double lhs = rd.NextDouble();
                Complex rhs = new Complex(rd.NextDouble(), rd.NextDouble());

                Complex expected = lhs * rhs;
                Complex actual = Solution<Double, Complex, Complex>.Multiply(lhs, rhs);

                if (expected != actual)
                    throw new TestFailException(expected, actual);
            }
        }
    }
}
