using BenchmarkDotNet.Running;

namespace CSharpStudy.Multiply
{
    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}
