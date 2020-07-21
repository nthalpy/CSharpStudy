using System;

namespace CSharpStudy.Multiply
{
    public struct ComplexStruct : IEquatable<ComplexStruct>
    {
        private double real;
        private double imag;

        public ComplexStruct(double r, double i)
        {
            real = r;
            imag = i;
        }

        public static ComplexStruct operator *(ComplexStruct lhs, ComplexStruct rhs)
        {
            return new ComplexStruct(
                lhs.real * rhs.real - lhs.imag * rhs.imag,
                lhs.real * rhs.imag + lhs.imag * rhs.real
            );
        }

        public bool Equals(ComplexStruct other)
        {
            return this.real == other.real && this.imag == other.imag;
        }

        public override string ToString()
        {
            return $"Real: {real}, Imag: {imag}";
        }
    }
}
