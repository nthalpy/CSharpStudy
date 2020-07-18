using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpStudy.Multiply
{
    public static class Program
    {
        public sealed class Result
        {
            public readonly String TestName;

            public readonly double Mean;
            public readonly double Error;
            public readonly double Stdev;

            public readonly bool Success;

            public Result(BenchmarkReport report)
            {
                TestName = report.BenchmarkCase.Descriptor.Type.Name;

                Mean = report.ResultStatistics.Mean;
                Error = report.ResultStatistics.StandardError;
                Stdev = report.ResultStatistics.StandardDeviation;

                Success = report.Success;
            }
        }

        public static void Main()
        {
            Summary[] summaries = BenchmarkRunner.Run(typeof(Program).Assembly);

            IEnumerable<Result> benchmarks = summaries
                .SelectMany(s => s.Reports)
                .Select(s => new Result(s))
                .ToArray();

            File.WriteAllText("result.json", JsonConvert.SerializeObject(benchmarks));
        }
    }
}
