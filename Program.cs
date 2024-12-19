using System.Diagnostics;

var day = 19;
Console.WriteLine($"Hello, Day {day}!");
var test = false;
var inputText = await File.ReadAllTextAsync(test ? $"./2024/d{day}/input/test_input.txt" : $"./2024/d{day}/input/input.txt");
var solution = new Day19();
var startTicks = Stopwatch.GetTimestamp();
Console.WriteLine("---------------");
Console.WriteLine(solution.PartOne(inputText, test));
Console.WriteLine($"Execution time part 1: {Stopwatch.GetElapsedTime(startTicks).TotalSeconds}");
Console.WriteLine("---------------");
startTicks = Stopwatch.GetTimestamp();
Console.WriteLine(solution.PartTwo(inputText, test));
Console.WriteLine($"Execution time part 2: {Stopwatch.GetElapsedTime(startTicks).TotalSeconds}");