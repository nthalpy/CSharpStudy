using System;

namespace CSharpStudy.Boxing
{
    /// <summary>
    /// 이번 과제는 Compare 함수에서 
    ///     - 코드 실행을 최대한 빠르게 하는 것
    ///     - Boxing과 메모리 할당을 최대한 피하는 것
    /// 이 이번 목표입니다.
    ///  
    /// 다른 코드를 건드리지 않고 컴파일 되는 선에서 Solution.cs를 변경해서 최적화를 해보도록 합시다.
    /// </summary>
    public static class Solution<T>
    {
        /// <summary>
        /// 프로그램 실행 후 Compare를 실행하기 전에 1번 호출되는 함수.
        /// </summary>
        public static void Setup()
        {
        }

        /// <summary>
        /// lhs.Equals(rhs)의 결과와 같은 결과를 리턴하는 함수.
        /// </summary>
        public static bool Compare(Object lhs, Object rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Setup 및 Compare가 호출되고 나서 프로그램 종료 전 1번 호출되는 함수.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}
