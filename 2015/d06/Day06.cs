using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015;
public class Day06 : ISolver
{
    public object PartOne(string input, bool test)
    {
        var lines = input.Split("\n");
        var lights = new BitArray(1000 * 1000, false);
        // var allOn = GenerateBitArray(0, 999, 0, 999, false, true);
        // Utils.Print(lights.Or(allOn).OfType<bool>().Count(x => x));

        foreach (var line in lines)
        {
            var match = Regex.Match(line, @".*\b(\d+),(\d+) through (\d+),(\d+)");

            var yStart = int.Parse(match.Groups[1].Value);
            Utils.Print(yStart);
            var yEnd = int.Parse(match.Groups[2].Value);
            var xStart = int.Parse(match.Groups[3].Value);
            var xEnd = int.Parse(match.Groups[4].Value);
            if (line.StartsWith("turn on"))
            {
                var map = GenerateBitArray(yStart, yEnd, xStart, xEnd, false, true);
                lights = lights.Or(map);
            }
            if (line.StartsWith("turn off"))
            {
                var map = GenerateBitArray(yStart, yEnd, xStart, xEnd, true, false);
                lights = lights.And(map);
            }
            if (line.StartsWith("toggle"))
            {
                var map = GenerateBitArray(yStart, yEnd, xStart, xEnd, false, true);
                lights = lights.Xor(map);
            }
        }
        return lights.OfType<bool>().Count(x => x);
    }


    public object PartTwo(string input, bool test)
    {
        var lines = input.Split("\n");
        var lights = new ConcurrentDictionary<(int, int), int>();

        foreach (var line in lines)
        {
            var match = Regex.Match(line, @".*\b(\d+),(\d+) through (\d+),(\d+)");

            var yStart = int.Parse(match.Groups[1].Value);
            var yEnd = int.Parse(match.Groups[2].Value);
            var xStart = int.Parse(match.Groups[3].Value);
            var xEnd = int.Parse(match.Groups[4].Value);
            if (line.StartsWith("turn on"))
            {
                UpdateCache(yStart, yEnd, xStart, xEnd, 1, lights, (key,x) => x+1);
            }
            if (line.StartsWith("turn off"))
            {
                UpdateCache(yStart, yEnd, xStart, xEnd, 0, lights, (key,x) => Math.Max(x-1, 0));
            }
            if (line.StartsWith("toggle"))
            {
                UpdateCache(yStart, yEnd, xStart, xEnd, 2, lights, (key,x) => x+2);
            }
        }
        return lights.Values.Sum();
    }
    
    private void UpdateCache(int p1y, int p1x, int p2y, int p2x, int defaulValue, ConcurrentDictionary<(int, int), int> cache, Func<(int,int), int, int> func)
    {
        for (; p1y <= p2y; p1y++)
        {
            for (int x = p1y * 1000 + p1x; x <= p1y * 1000 + p2x; x++)
            {
                cache.AddOrUpdate((p1y,x), defaulValue, func);
            }
        }
    }

    private BitArray GenerateBitArray(int p1y, int p1x, int p2y, int p2x, bool defaultValue, bool value)
    {
        var bitMap = new BitArray(1000 * 1000, defaultValue);
        for (; p1y <= p2y; p1y++)
        {
            for (int x = p1y * 1000 + p1x; x <= p1y * 1000 + p2x; x++)
            {
                bitMap[x] = value;
            }
        }
        // Utils.Print("map: " + bitMap.OfType<bool>().Count(x => x));
        return bitMap;
    }
}
