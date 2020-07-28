using System;

namespace CSharpStudy.Boxing
{
    public struct EqualsOverridedStruct
    {
        public EqualsOverridedStruct(double a, double b)
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
        public override bool Equals(Object obj)
        {
            if (obj is EqualsOverridedStruct)
            {
                EqualsOverridedStruct other = (EqualsOverridedStruct)obj;
                return this.a == other.a && this.b == other.b;
            }

            return false;
        }
    }
}