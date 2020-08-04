using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;

namespace CSharpStudy.Boxing
{
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Throughput, targetCount: 10)]
    [DisassemblyDiagnoser(maxDepth: 10, printSource: true, printInstructionAddresses: true)]
    [GenericTypeArguments(typeof(Int32))]
    [GenericTypeArguments(typeof(String))]
    [GenericTypeArguments(typeof(EqutableStruct))]
    [GenericTypeArguments(typeof(EqualsOverridedStruct))]
    [GenericTypeArguments(typeof(EqualsOverridedStruct2))]
    public class Test<T>
    {
        private const int ValueArrLength = 512;

        private readonly Random rd;
        private readonly T[] cachedValueArr;
        private readonly bool[,] equalityTable;

        public Test()
        {
            rd = new Random();

            cachedValueArr = new T[ValueArrLength];
            for (int idx = 0; idx < ValueArrLength; idx++)
                cachedValueArr[idx] = CreateRandomValue();

            equalityTable = new bool[ValueArrLength, ValueArrLength];
            for (int idx = 0; idx < ValueArrLength; idx++)
                for (int jdx = 0; jdx < ValueArrLength; jdx++)
                    equalityTable[idx, jdx] = cachedValueArr[idx].Equals(cachedValueArr[jdx]);
        }

        private T CreateRandomValue()
        {
            if (typeof(T) == typeof(String))
            {
                return (T)(Object)rd.Next().ToString();
            }
            else if (typeof(T) == typeof(Int32))
            {
                return (T)(Object)rd.Next();
            }
            else if (typeof(T) == typeof(EqutableStruct))
            {
                return (T)(Object)new EqutableStruct(rd.NextDouble(), rd.NextDouble());
            }
            else if (typeof(T) == typeof(EqualsOverridedStruct))
            {
                return (T)(Object)new EqualsOverridedStruct(rd.NextDouble(), rd.NextDouble());
            }
            else if (typeof(T) == typeof(EqualsOverridedStruct2))
            {
                return (T)(Object)new EqualsOverridedStruct2(rd.NextDouble(), rd.NextDouble());
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        [GlobalSetup]
        public void Setup()
        {
            Solution<T>.Setup();
        }

        [Benchmark]
        public void Compare()
        {
            int lhsIndex = rd.Next(ValueArrLength);
            int rhsIndex = rd.Next(ValueArrLength);

            bool expected = equalityTable[lhsIndex, rhsIndex];
            bool actual = Solution<T>.Compare(cachedValueArr[lhsIndex], cachedValueArr[rhsIndex]);

            if (expected != actual)
                throw new TestFailException(expected, actual);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            Solution<T>.Cleanup();
        }
    }
}
