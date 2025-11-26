using BenchmarkDotNet.Running;

namespace WorkoutService.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<WorkoutBenchmarks>();
        }
    }
}
