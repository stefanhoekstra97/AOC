// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Benchmarks.Benchmarks;

// var summary = BenchmarkRunner.Run(typeof(CalibrateLaunchBench));

// var summary = BenchmarkRunner.Run(typeof(Day02CubeGameBench));
// var summary = BenchmarkRunner.Run(typeof(Day03EngineBench));
// var summary = BenchmarkRunner.Run(typeof(Day04ScratchCardsBench));
// var summary = BenchmarkRunner.Run(typeof(Day07CamelCardBench));
var summary = BenchmarkRunner.Run(typeof(Day08HauntedDesertBench));
