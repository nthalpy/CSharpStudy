using BenchmarkDotNet.Attributes;
using System;

namespace CSharpStudy.Multiply.Tests
{
    [SimpleJob(warmupCount: 25, targetCount: 100)]
    public class CustomClass
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

        private Complex lhs;
        private Complex rhs;

        public CustomClass()
        {
            Random rd = new Random();

            lhs = new Complex(rd.NextDouble(), rd.NextDouble());
            rhs = new Complex(rd.NextDouble(), rd.NextDouble());
        }

        [GlobalSetup]
        public void Setup()
        {
            Solution<Complex, Complex, Complex>.Prepare();
        }

        [Benchmark]
        public void MainRoutine()
        {
            Complex expected = lhs * rhs;
            Complex actual = Solution<Complex, Complex, Complex>.Multiply(lhs, rhs);

            if (expected != actual)
                throw new TestFailException(expected, actual);
        }
    }
}
