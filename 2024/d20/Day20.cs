using System.Numerics;

namespace AdventOfCode.Y2024;
public class Day20 : ISolver
{
    Complex[] directions = [-1, 1, Complex.ImaginaryOne, -Complex.ImaginaryOne];
    public object PartOne(string input, bool test)
    {
        (var map, var xmax, var ymax) = ParseIntput(input);
        var pathToFinish = MarkPathToFinish(map);
        var desiredImprovement = 1;
        var passThroughImprovements = pathToFinish.Select(kvp => GetPassThroughPositon(pathToFinish, kvp.Key).Select(x => pathToFinish.GetValueOrDefault(x) - kvp.Value - 2).Where(i => i >= desiredImprovement).ToList()).ToList();

        return passThroughImprovements.SelectMany(x => x).GroupBy(x => x).Select(n => new { improvement = n.Key, count = n.Count() }).OrderBy(x => x.improvement).Where(x => x.improvement >= desiredImprovement).Sum(x => x.count);
    }

    public object PartTwo(string input, bool test)
    {
        (var map, var xmax, var ymax) = ParseIntput(input);
        var pathToFinish = MarkPathToFinish(map);
        var desiredImprovement = 100;
        var cheatTime = 20;
        var passThroughImprovements = pathToFinish.Select(kvp => GetPassThroughPositon(pathToFinish, kvp.Key, cheatTime).Select(x => pathToFinish.GetValueOrDefault(x.cheatLocation) - kvp.Value - x.cheatTime).Where(i => i >= desiredImprovement).ToList()).ToList();

        return passThroughImprovements.SelectMany(x => x).GroupBy(x => x).Select(n => new { improvement = n.Key, count = n.Count() }).OrderBy(x => x.improvement).Where(x => x.improvement >= desiredImprovement).Sum(x => x.count);
    }

    public IEnumerable<Complex> GetPassThroughPositon(Dictionary<Complex, int> map, Complex position)
    {
        foreach (var direction in directions)
        {
            // If next position is not a wall, we can ignore it
            if (!map.ContainsKey(position + direction))
            {
                yield return position + 2 * direction;
            }
        }
    }

    public IEnumerable<(Complex cheatLocation, int cheatTime)> GetPassThroughPositon(Dictionary<Complex, int> map, Complex position, int cheatActiveTime)
    {
        var xDirection = 1;
        var yDirection = Complex.ImaginaryOne;
        foreach (var x in Enumerable.Range(-cheatActiveTime, (2*cheatActiveTime)+1))
        {
            foreach (var y in Enumerable.Range(-cheatActiveTime, (2*cheatActiveTime)+1))
            {
                if (Math.Abs(x) + Math.Abs(y) > cheatActiveTime || (x == 0 && y == 0)) continue;
                var nextPosition = position + x * xDirection + y * yDirection;
                if (!map.ContainsKey(nextPosition)) continue;
                yield return (position + x * xDirection + y * yDirection, Math.Abs(x)+Math.Abs(y));
            }

        }
    }

    public (Dictionary<Complex, string>, int xmax, int ymax) ParseIntput(string input)
    {
        var chars = input.Split("\n").Select(x => x.ToArray()).ToArray();
        (var xmax, var ymax) = (chars[0].Length, chars.Length);
        var map = (
            from y in Enumerable.Range(0, ymax)
            from x in Enumerable.Range(0, xmax)
            select new KeyValuePair<Complex, string>(x + y * Complex.ImaginaryOne, chars[y][x].ToString())
        ).ToDictionary();
        return (map, xmax, ymax);
    }

    public Dictionary<Complex, int> MarkPathToFinish(Dictionary<Complex, string> map)
    {
        (var start, var end) = FindStartAndEnd(map, "S", "E");
        map[start] = ".";
        map[end] = ".";

        Utils.Print(start + " " + end);
        Complex position = start;

        Dictionary<Complex, int> pathToFinish = [];
        pathToFinish.Add(start, 0);
        int positionIndex = 0;
        while (position != end)
        {
            map[position] = positionIndex++.ToString();
            foreach (var d in directions)
            {
                var nextPosition = position + d;
                if (map[nextPosition] == ".")
                {
                    pathToFinish.Add(nextPosition, positionIndex);
                    position = nextPosition;
                    break;
                }
            }

        }
        Utils.Print(positionIndex);
        return pathToFinish;
    }

    public (Complex start, Complex end) FindStartAndEnd(Dictionary<Complex, string> map, string startChar, string endChar)
    {
        Complex? start = null;
        Complex? end = null;

        foreach (var key in map.Keys)
        {
            if (map[key] == startChar)
            {
                start = key;
            }

            if (map[key] == endChar)
            {
                end = key;
            }
        }

        ArgumentNullException.ThrowIfNull(start, "Start not found");
        ArgumentNullException.ThrowIfNull(end, "End not found");
        return (start ?? new Complex(), end ?? new Complex());
    }

}
