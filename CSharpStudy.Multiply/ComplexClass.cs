using System;

namespace CSharpStudy.Multiply
{
    public sealed class ComplexClass : IEquatable<ComplexClass>
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

        public bool Equals(ComplexClass other)
        {
            return this.real == other.real && this.imag == other.imag;
        }

        public override string ToString()
        {
            return $"Real: {real}, Imag: {imag}";
        }
    }
}
