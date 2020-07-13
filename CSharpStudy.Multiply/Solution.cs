namespace CSharpStudy.Multiply
{
    public static class Solution
    {
        public static void Prepare<T1, T2, T3>()
        {
        }

        public static T3 Multiply<T1, T2, T3>(T1 lhs, T2 rhs)
        {
            return (dynamic)lhs * (dynamic)rhs;
        }
    }
}
