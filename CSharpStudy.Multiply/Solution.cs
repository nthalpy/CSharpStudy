namespace CSharpStudy.Multiply
{
    public static partial class Solution<T1, T2, T3>
    {
        public static void Prepare()
        {
        }

        public static T3 Multiply(T1 lhs, T2 rhs)
        {
            return (dynamic)lhs * (dynamic)rhs;
        }
    }
}
