// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Benchmarks.DayOneBenchmarks;
using Benchmarks.DayTwoBenchmarks;

// var summary = BenchmarkRunner.Run(typeof(CalibrateLaunchBench));

var summary = BenchmarkRunner.Run(typeof(CubeGameBenchmarks));
