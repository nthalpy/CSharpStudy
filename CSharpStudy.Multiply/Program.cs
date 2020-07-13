using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CSharpStudy.Multiply
{
    public static class Program
    {
        public static void Main(String[] args)
        {
            if (args.Length == 0)
            {
                MainEntryPoint();
            }
            else
            {
                TestEntryPoint(args[0]);
            }
        }

        private static void MainEntryPoint()
        {
            IEnumerable<Type> testTypeList = typeof(Program)
                .Assembly
                .GetTypes()
                .Where(t => typeof(TestBase).IsAssignableFrom(t))
                .ToArray();

            WriteLog("Running Tests...\n");
            foreach (Type testType in testTypeList)
            {
                WriteLog($"\t{testType.FullName,-60} ");
                LaunchTest(testType);

                TestResult result = JsonConvert.DeserializeObject<TestResult>(File.ReadAllText(GetTestResultFileName(testType.FullName)));

                if (result.IsValid)
                {
                    WriteLog($"OK   ", ConsoleColor.Green);
                    WriteLog($"m {Math.Round(result.TickMean),-10} stdev {Math.Round(result.TickStDev),-10}\n");
                }
                else
                {
                    WriteLog("FAIL\n", ConsoleColor.Yellow, ConsoleColor.Red);
                    WriteLog($"{result.DiagnosticsMessage}\n");
                }
            }
        }

        private static void WriteLog(String s, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.Write(s);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        private static void LaunchTest(Type testType)
        {
            String exeLocation = typeof(Program).Assembly.Location;

            ProcessStartInfo psi = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $"/C dotnet {exeLocation} {testType.FullName}",
                RedirectStandardOutput = true,
            };

            Process proc = new Process
            {
                StartInfo = psi
            };

            proc.Start();
            proc.WaitForExit();
        }

        private static void TestEntryPoint(String typeName)
        {
            try
            {
                Type t;

                t = typeof(Program)
                    .Assembly
                    .GetTypes()
                    .FirstOrDefault(t => t.FullName == typeName);

                // Fallback to name
                if (t == null)
                {
                    IEnumerable<Type> types = typeof(Program)
                        .Assembly
                        .GetTypes()
                        .Where(t => t.Name == typeName);

                    if (types.Count() > 1)
                    {
                        String typeStrings = String.Join(", ", types.Select(t => t.FullName));
                        throw new Exception($"Type name {typeName} is ambiguous between {typeStrings}");
                    }

                    t = types.FirstOrDefault();
                }

                if (t == null)
                    throw new Exception($"Unable to find type {typeName}");

                TestBase test = Activator.CreateInstance(t) as TestBase;
                test.Prepare();
                TestResult result = test.Run();

                WriteResult(result, typeName);
            }
            catch (Exception e)
            {
                TestResult result = new TestResult
                {
                    IsValid = false,
                    DiagnosticsMessage = e.ToString()
                };

                WriteResult(result, typeName);
            }
        }

        private static String GetTestResultFileName(String typeName)
        {
            return $"Test-{typeName}.json";
        }
        private static void WriteResult(TestResult result, String typeName)
        {
            String json = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText(GetTestResultFileName(typeName), json);

            Console.WriteLine(json);
        }
    }
}
