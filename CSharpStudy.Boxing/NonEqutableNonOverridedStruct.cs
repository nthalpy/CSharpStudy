namespace CSharpStudy.Boxing
{
    public struct NonEqutableNonOverridedStruct
    {
        public NonEqutableNonOverridedStruct(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        private double a;
        private double b;
    }
}