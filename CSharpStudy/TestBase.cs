using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CSharpStudy
{
    public abstract class TestBase
    {
        private readonly Stopwatch sw;

        public TestBase()
        {
            sw = new Stopwatch();
        }

        public TestResult Run()
        {
            const int sampleCount = 100;

            List<Int64> tickList = new List<Int64>(sampleCount);
            for (int idx = 0; idx < sampleCount; idx++)
            {
                sw.Restart();

                TestRoutine();
                tickList.Add(sw.ElapsedTicks);
            }

            Int64 mem = GC.GetTotalMemory(false);
            double mean = tickList.Average();
            double stdev = Math.Sqrt(tickList.Select(x => (x - mean) * (x - mean)).Average());

            return new TestResult
            {
                IsValid = true,
                MemoryUsage = mem,
                TickMean = mean,
                TickStDev = stdev,
                DiagnosticsMessage = String.Empty,
            };
        }

        public abstract void Prepare();
        public abstract void TestRoutine();
    }
}
