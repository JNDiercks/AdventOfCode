using System.Collections;
using System.IO.Pipelines;
using System.Linq;
using System.Security.Cryptography;

namespace AdventOfCode.Y2024;

public class Day25 : ISolver
{
    public object PartOne(string input, bool test)
    {
        var parsedInput = input.Split("\n\n");
        List<List<int>> keys = [];
        List<List<int>> locks = [];
        foreach (var i in parsedInput)
        {
            List<int> code = [];
            var grid = i.Split("\n").Select(x => x.ToCharArray()).ToArray();

            if (grid[0][0] == '#')
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    var c = 0;
                    for (int h = 0; h < grid.Length; h++)
                    {
                        if (grid[h][j] == '#') c++;
                    }
                    code.Add(c-1);
                }
                locks.Add(code);
            }
            else
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    var c = 0;
                    for (int h = 0; h < grid.Length; h++)
                    {
                        if (grid[h][j] == '#') c++;
                    }
                    code.Add(c-1);
                }
                keys.Add(code);
            }
        }

        var result = 0;
        foreach (var k in keys)
        {
            foreach (var l in locks)
            {
                if (k.Zip(l, (x1, x2) => x1 < 6-x2).All(x => x))
                {
                    result++;
                }

            }
        }

        return result;
    }

    public object PartTwo(string input, bool test)
    {
        var parsedInput = input.Split("\n\n");
        var initialValues = parsedInput[0].Split("\n");

        return null;
    }

}