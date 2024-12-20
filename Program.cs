using AdventOfCode;
using System.Diagnostics;
using System.Reflection;

var day = 20;
var aocSolvers = Assembly.GetEntryAssembly()!.GetTypes()
    .Where(t => t.GetTypeInfo().IsClass && typeof(ISolver).IsAssignableFrom(t))
    .OrderBy(t => t.FullName)
    .ToArray();

ArgumentOutOfRangeException.ThrowIfZero(aocSolvers?.Length ?? 0, "No AdventOfCode solvers found.");

Console.WriteLine($"Hello, Day {day}!");
var test = false;
var inputText = await File.ReadAllTextAsync(test ? $"./2024/d{day}/input/test_input.txt" : $"./2024/d{day}/input/input.txt");
var solution = Activator.CreateInstance(aocSolvers!.First(x => x.FullName!.Contains($"Day{day.ToString()}"))) as ISolver;
var startTicks = Stopwatch.GetTimestamp();
Console.WriteLine("---------------");
var part1Solution = solution.PartOne(inputText, test);
Console.WriteLine(part1Solution);
Console.WriteLine($"Execution time part 1: {Stopwatch.GetElapsedTime(startTicks).TotalSeconds}");
Console.WriteLine("---------------");
startTicks = Stopwatch.GetTimestamp();
var part2Solution = solution.PartTwo(inputText, test);
Console.WriteLine(part2Solution);
Console.WriteLine($"Execution time part 2: {Stopwatch.GetElapsedTime(startTicks).TotalSeconds}");