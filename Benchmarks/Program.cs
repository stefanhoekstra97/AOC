// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Benchmarks.Benchmarks._2024;

#region 2023
// var summary = BenchmarkRunner.Run(typeof(CalibrateLaunchBench));

// var summary = BenchmarkRunner.Run(typeof(Day02CubeGameBench));
// var summary = BenchmarkRunner.Run(typeof(Day03EngineBench));
// var summary = BenchmarkRunner.Run(typeof(Day04ScratchCardsBench));
// var summary = BenchmarkRunner.Run(typeof(Day07CamelCardBench));
// var summary = BenchmarkRunner.Run(typeof(Day08HauntedDesertBench));
// var summary = BenchmarkRunner.Run(typeof(Day11CosmicExpansionBench));
// var summary = BenchmarkRunner.Run(typeof(Day17Benchmark));
#endregion

#region 2024

// var summary = BenchmarkRunner.Run(typeof(Day01Benchmarks2024));
// var summary = BenchmarkRunner.Run(typeof(Day02Benchmarks2024));
var summary = BenchmarkRunner.Run<Day03Benchmarks2024>();



#endregion