using BenchmarkDotNet.Attributes;
using System;

namespace CSharpStudy.Multiply
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser(maxDepth: 10, printSource: true)]
    [GenericTypeArguments(typeof(Int32), typeof(Int32), typeof(Int32))]
    [GenericTypeArguments(typeof(Double), typeof(Double), typeof(Double))]
    [GenericTypeArguments(typeof(Int32), typeof(Double), typeof(Double))]
    [GenericTypeArguments(typeof(ComplexClass), typeof(ComplexClass), typeof(ComplexClass))]
    [GenericTypeArguments(typeof(ComplexStruct), typeof(ComplexStruct), typeof(ComplexStruct))]
    [GenericTypeArguments(typeof(Double), typeof(ComplexClass), typeof(ComplexClass))]
    public class Test<T1, T2, T3> where T3 : IEquatable<T3>
    {
        private const int ArrLength = 512;

        private readonly Random rd;
        private readonly T1[] T1Arr;
        private readonly T2[] T2Arr;
        private readonly T3[] T3Arr;

        public Test()
        {
            rd = new Random();

            T1Arr = new T1[ArrLength];
            T2Arr = new T2[ArrLength];
            T3Arr = new T3[ArrLength * ArrLength];

            for (int idx = 0; idx < ArrLength; idx++)
            {
                T1Arr[idx] = CreateRandomValue<T1>(rd);
                T2Arr[idx] = CreateRandomValue<T2>(rd);
            }

            for (int lhsIndex = 0; lhsIndex < ArrLength; lhsIndex++)
                for (int rhsIndex = 0; rhsIndex < ArrLength; rhsIndex++)
                {
                    // Precalc everything, so make sure Multiply method not take much time to eval
                    int ansIndex = CompositeIndex(lhsIndex, rhsIndex);
                    T3 mult = (T3)((dynamic)T1Arr[lhsIndex] * (dynamic)T2Arr[rhsIndex]);

                    T3Arr[ansIndex] = mult;
                }
        }

        private int CompositeIndex(int lhsIndex, int rhsIndex)
        {
            return lhsIndex + rhsIndex * ArrLength;
        }

        private T CreateRandomValue<T>(Random rd)
        {
            if (typeof(T) == typeof(Int32))
            {
                return (T)(Object)rd.Next();
            }
            else if (typeof(T) == typeof(Double))
            {
                return (T)(Object)rd.NextDouble();
            }
            else if (typeof(T) == typeof(ComplexClass))
            {
                return (T)(Object)new ComplexClass(rd.NextDouble(), rd.NextDouble());
            }
            else if (typeof(T) == typeof(ComplexStruct))
            {
                return (T)(Object)new ComplexStruct(rd.NextDouble(), rd.NextDouble());
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        [GlobalSetup]
        public void Setup()
        {
            Solution<T1, T2, T3>.Setup();
        }

        [Benchmark]
        public void Multiply()
        {
            int lhsIndex = rd.Next(ArrLength);
            int rhsIndex = rd.Next(ArrLength);
            int answerIndex = CompositeIndex(lhsIndex, rhsIndex);

            T3 expected = T3Arr[answerIndex];
            T3 actual = Solution<T1, T2, T3>.Multiply(T1Arr[lhsIndex], T2Arr[rhsIndex]);

            if (expected.Equals(actual) == false)
                throw new TestFailException(expected, actual);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            Solution<T1, T2, T3>.Cleanup();
        }
    }
}
