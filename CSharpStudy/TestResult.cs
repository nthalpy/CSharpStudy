using System;

namespace CSharpStudy
{
    public sealed class TestResult
    {
        public bool IsValid;
        public double TickMean;
        public double TickStDev;
        public Int64 MemoryUsage;

        public String DiagnosticsMessage;
    }
}
