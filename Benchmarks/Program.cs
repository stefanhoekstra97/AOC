﻿// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Benchmarks.DayOneBenchmarks;

var summary = BenchmarkRunner.Run(typeof(CalibrateLaunchBench));

