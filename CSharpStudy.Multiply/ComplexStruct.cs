using System;

namespace CSharpStudy.Multiply
{
    public struct ComplexStruct
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

        public override bool Equals(Object obj)
        {
            return obj is ComplexStruct complex &&
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
