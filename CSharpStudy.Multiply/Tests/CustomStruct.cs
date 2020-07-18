using BenchmarkDotNet.Attributes;
using System;

namespace CSharpStudy.Multiply.Tests
{
    [MemoryDiagnoser]
    public class CustomStruct
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

        public CustomStruct()
        {
            rd = new Random();
        }

        [GlobalSetup]
        public void Setup()
        {
            Solution<Complex, Complex, Complex>.Prepare();
        }

        [Benchmark]
        public void MainRoutine()
        {
            Complex lhs = new Complex(rd.NextDouble(), rd.NextDouble());
            Complex rhs = new Complex(rd.NextDouble(), rd.NextDouble());

            Complex expected = lhs * rhs;
            Complex actual = Solution<Complex, Complex, Complex>.Multiply(lhs, rhs);

            if (expected != actual)
                throw new TestFailException(expected, actual);
        }
    }
}
