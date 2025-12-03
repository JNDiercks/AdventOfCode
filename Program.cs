using AdventOfCode;
using AoC.Year2024;
using System.Diagnostics;
using System.Reflection;


// Day22SolutionPart2 solutionTest = new Day22SolutionPart2();
// var textReader = File.OpenText($"./2024/d22/input/input.txt")
// var startTicksTest = Stopwatch.GetTimestamp();
// var result = solutionTest.Process(textReader);
// var input = File.ReadAllLines($"./2024/d22/input/input.txt");
// MonkeyMarket(input, out var part1, out var part2, iterations: 2000);

// Console.WriteLine($"Part 1: {part1}");
// Console.WriteLine($"Part 2: {part2}");

// // Part 1 can be super small, actually:
// var part1OneLiner = input.Select(int.Parse).Sum(seed => (long) Mrng(seed).Skip(2_000).First());
// Console.WriteLine($"Part 1 (short): {part1OneLiner}");

// static IEnumerable<int> Mrng(int seed)
// {
//     var next = seed;
//     while (true)
//     {
//         yield return next;
//         next = (next ^ (next << 6)) & 0x00FFFFFF;
//         next = (next ^ (next >> 5)) & 0x00FFFFFF;
//         next = (next ^ (next << 11)) & 0x00FFFFFF;
//     }
// }

// void MonkeyMarket(IEnumerable<string> seeds, out long part1, out int part2, int iterations = 2000)
// {
//     var intSeeds = seeds.Select(int.Parse).ToArray();
//     var tt = new Dictionary<MarketChanges, int>();
//     part1 = 0;
    
//     foreach (var seed in intSeeds)
//     {
//         using var rng = Mrng(seed).GetEnumerator();
//         var cc = new HashSet<MarketChanges>();
//         var carry = new sbyte[4];
//         int? last = default;
//         for (int i = 0; i <= iterations && rng.MoveNext(); ++i)
//         {
//             int curr = rng.Current;
//             var value = curr % 10;
//             if (last.HasValue)
//             {
//                 carry[i%4] = (sbyte)(value - last.Value);
//             }

//             if (i >= 4)
//             {
//                 var key = new MarketChanges(carry[i%4], carry[(i-1)%4], carry[(i-2)%4], carry[(i-3)%4]);
//                 if (cc.Add(key))
//                 {
//                     tt[key] = tt.GetValueOrDefault(key) + value;
//                 }
//             }

//             last = value;
//         }
//         part1 += rng.Current;
//     }

//     part2 = tt.Values.Max();
// }
// Console.WriteLine($"Execution time part 1: {Stopwatch.GetElapsedTime(startTicksTest).TotalSeconds}");

// internal readonly record struct MarketChanges(sbyte A, sbyte B, sbyte C, sbyte D);

// // Console.WriteLine(result);


var day = 06;
var year = 2015;
var aocSolvers = Assembly.GetEntryAssembly()!.GetTypes()
    .Where(t => t.GetTypeInfo().IsClass && typeof(ISolver).IsAssignableFrom(t))
    .OrderBy(t => t.FullName)
    .ToArray();

ArgumentOutOfRangeException.ThrowIfZero(aocSolvers?.Length ?? 0, "No AdventOfCode solvers found.");

Console.WriteLine($"Hello, Day {day}!");
var test = false;
var inputText = await File.ReadAllTextAsync(test ? $"./{year}/d{day:D2}/input/test_input.txt" : $"./{year}/d{day:D2}/input/input.txt");
var solution = Activator.CreateInstance(aocSolvers!.First(x => x.FullName!.Contains($"Y{year}.Day{day:D2}"))) as ISolver;
Console.WriteLine("---------------");
var startTicks = Stopwatch.GetTimestamp();
var part1Solution = solution.PartOne(inputText, test);
Console.WriteLine(part1Solution);
Console.WriteLine($"Execution time part 1: {Stopwatch.GetElapsedTime(startTicks).TotalSeconds}");
Console.WriteLine("---------------");
startTicks = Stopwatch.GetTimestamp();
var part2Solution = solution.PartTwo(inputText, test);
Console.WriteLine(part2Solution);
Console.WriteLine($"Execution time part 2: {Stopwatch.GetElapsedTime(startTicks).TotalSeconds}");