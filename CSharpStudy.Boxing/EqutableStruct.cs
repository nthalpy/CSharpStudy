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

        public override int GetHashCode()
        {
            return HashCode.Combine(a, b);
        }
        public override bool Equals(object obj)
        { 
            if (obj is EqutableStruct)
            {
                EqutableStruct other = (EqutableStruct)obj;
                return this.a == other.a && this.b == other.b;
            }

            return false;
        }
        public bool Equals(EqutableStruct other)
        {
            return this.a == other.a && this.b == other.b;
        }
    }
}