using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpStudy.Boxing
{
    public static class Program
    {
        public sealed class Result
        {
            public readonly String TestName;

            public readonly double Mean;
            public readonly double IQR;
            public readonly Int64 Allocated;

            public readonly bool Success;

            public Result(BenchmarkReport report)
            {
                TestName = report.BenchmarkCase.Descriptor.Type.Name;

                if (report.ResultStatistics != null)
                {
                    Mean = report.ResultStatistics.Mean;
                    IQR = report.ResultStatistics.InterquartileRange;
                }

                Allocated = report.GcStats.BytesAllocatedPerOperation;

                Success = report.Success;
            }
        }

        public static void Main()
        {
            String[] args = new String[]
            {
                $"--filter", "*"
            };

            Summary[] summaries = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                .Run(args, GetConfig())
                .ToArray();

            IEnumerable<Result> benchmarks = summaries
                .SelectMany(s => s.Reports)
                .Select(s => new Result(s))
                .ToArray();

            File.WriteAllText("result.json", JsonConvert.SerializeObject(benchmarks));
        }

        private static IConfig GetConfig()
        {
            return DefaultConfig.Instance
                .WithOption(ConfigOptions.JoinSummary, true);
        }
    }
}
