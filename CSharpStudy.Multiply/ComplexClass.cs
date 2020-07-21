using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using System;
using System.Management;

namespace CSharpStudy.Multiply
{
    public sealed class ComplexClass
    {
        private double real;
        private double imag;

        public ComplexClass(double r, double i)
        {
            real = r;
            imag = i;
        }

        public static ComplexClass operator *(double lhs, ComplexClass rhs)
        {
            return new ComplexClass(
                lhs * rhs.real,
                lhs * rhs.imag
            );
        }

        public static ComplexClass operator *(ComplexClass lhs, ComplexClass rhs)
        {
            return new ComplexClass(
                lhs.real * rhs.real - lhs.imag * rhs.imag,
                lhs.real * rhs.imag + lhs.imag * rhs.real
            );
        }

        public override bool Equals(Object obj)
        {
            return obj is ComplexClass complex &&
                   real == complex.real &&
                   imag == complex.imag;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(real, imag);
        }

        public override string ToString()
        {
            return $"Real: {real}, Imag: {imag}";
        }
    }
}
