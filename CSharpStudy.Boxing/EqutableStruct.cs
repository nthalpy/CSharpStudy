using System;

namespace CSharpStudy.Boxing
{
    public struct EqutableStruct : IEquatable<EqutableStruct>
    {
        public EqutableStruct(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        private double a;
        private double b;

        public bool Equals(EqutableStruct other)
        {
            return this.a == other.a && this.b == other.b;
        }
    }
}