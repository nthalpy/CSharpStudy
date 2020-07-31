using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Linq;

namespace CSharpStudy.Boxing
{
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Throughput, targetCount: 10)]
    [DisassemblyDiagnoser(maxDepth: 10, printSource: true, printInstructionAddresses: true)]
    [GenericTypeArguments(typeof(Int32))]
    [GenericTypeArguments(typeof(String))]
    [GenericTypeArguments(typeof(EqutableStruct))]
    [GenericTypeArguments(typeof(EqualsOverridedStruct))]
    public class Test<T>
    {
        private const int ValueArrLength = 512;

        private readonly Random rd;
        private readonly T[] cachedValueArr;

        public Test()
        {
            rd = new Random();

            cachedValueArr = new T[ValueArrLength];
            for (int idx = 0; idx < ValueArrLength; idx++)
            {
                T newValue;
                do
                {
                    newValue = CreateRandomValue(rd);
                } while (cachedValueArr.Contains(newValue));

                cachedValueArr[idx] = newValue;
            }
        }

        private T CreateRandomValue(Random rd)
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
            // Note: 
            // make sure lhsIndex != rhsIndex implies cachedValueArr[lhsIndex] != cachedValueArr[rhsIndex]

            int lhsIndex = rd.Next(ValueArrLength);
            int rhsIndex = rd.Next(ValueArrLength);

            bool expected = lhsIndex == rhsIndex;
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
