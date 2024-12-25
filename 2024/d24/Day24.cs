using System.Collections;
using System.Linq;

namespace AdventOfCode.Y2024;

public class Day24 : ISolver
{
    public object PartOne(string input, bool test)
    {
        var parsedInput = input.Split("\n\n");
        var initialValues = parsedInput[0].Split("\n");
        var gates = parsedInput[1].Split("\n").Select(GetGate);
        var results = new Dictionary<string, bool>();

        foreach (var value in initialValues)
        {
            var v = value.Split(": ");
            results[v[0]] = v[1] == "1" ? true : false;
        }

        while (gates.Any(x => !results.ContainsKey(x.Output)))
        {
            foreach (var g in gates.Where(x => !results.ContainsKey(x.Output)))
            {
                if (results.TryGetValue(g.Input1, out var input1) && results.TryGetValue(g.Input2, out var input2)) {
                    results[g.Output] = CalculateGate(g.Type, input1, input2);
                }
            }
        }

        // foreach (var g in gates)
        // {
        //     Utils.Print(g.ToString());
        // }

        var r = results.Where(x => x.Key.StartsWith('z')).OrderBy(x => x.Key).Select(x => x.Value).ToArray();
        Utils.Print(r.Length);
        Utils.Print(string.Join("", r.Select(x => Convert.ToInt32(x))));
        
        ulong result = 0;
        for (var i = 0; i < r.Length; i++) {
            result += r[i] ? ((ulong)1 << i) : 0;
        }
        return result;
    }

    public object PartTwo(string input, bool test)
    {
        return null;
    }

    public bool CalculateGate(string type, bool b1, bool b2) => type switch
    {
        "AND" => b1 & b2,
        "OR" => b1 | b2,
        "XOR" => b1 ^ b2,
        _ => throw new NotImplementedException()
    };

    private Gate GetGate(string line)
    {
        var parts = line.Split(" ");
        return new Gate(parts[0], parts[1], parts[2], parts[4]);
    }

    private record struct Gate(string Input1, string Type, string Input2, string Output);
}