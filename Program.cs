// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using FastListIteration;

Console.WriteLine("Fastest Iteration: ");

BenchmarkRunner.Run<Benchmark>();