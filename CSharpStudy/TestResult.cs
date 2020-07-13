using System;

namespace CSharpStudy
{
    public sealed class TestResult
    {
        public bool IsValid;

        public double TickMin;
        public double TickMean;
        public double TickMax;

        public double TickStDev;
        public Int64 MemoryUsage;

        public String DiagnosticsMessage;
    }
}
