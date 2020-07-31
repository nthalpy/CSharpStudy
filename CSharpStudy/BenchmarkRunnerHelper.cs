using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CSharpStudy
{
    public static class BenchmarkRunnerHelper
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
                Type testType = report.BenchmarkCase.Descriptor.Type;
                if (testType.IsGenericType)
                {
                    TestName = $"{testType.Name}<{String.Join(", ", testType.GetGenericArguments().Select(t => t.Name))}>";
                }
                else
                {
                    TestName = testType.Name;
                }

                if (report.ResultStatistics != null)
                {
                    Mean = report.ResultStatistics.Mean;
                    IQR = report.ResultStatistics.InterquartileRange;
                }

                Allocated = report.GcStats.BytesAllocatedPerOperation;

                Success = report.Success;
            }
        }

        public static void Run()
        {
            Assembly entryPointAsm = AppDomain.CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(t => t.EntryPoint != null);

            String[] args = new String[]
            {
                $"--filter", "*"
            };

            Summary[] summaries = BenchmarkSwitcher.FromAssembly(entryPointAsm)
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
                .WithOption(ConfigOptions.JoinSummary, true)
                .WithOption(ConfigOptions.DisableOptimizationsValidator, true);
        }
    }
}
