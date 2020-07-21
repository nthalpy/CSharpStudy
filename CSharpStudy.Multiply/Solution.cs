namespace CSharpStudy.Multiply
{   
    /// <summary>
    /// 이번 과제는 Multiply 함수에서
    ///     - 최대한 빠르게 Generic variable의 곱셈을 수행하는 것
    /// 이 이번 목표입니다.
    /// 
    /// 다른 코드를 건드리지 않고 컴파일 되는 선에서 Solution.cs를 변경해서 최적화를 해보도록 합시다.
    /// </summary>
    public static class Solution<T1, T2, T3>
    {
        public static void Setup()
        {
        }

        public static T3 Multiply(T1 lhs, T2 rhs)
        {
            return (dynamic)lhs * (dynamic)rhs;
        }

        public static void Cleanup()
        {
        }
    }
}
