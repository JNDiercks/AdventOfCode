using System.Collections;
using System.Linq;

namespace AdventOfCode.Y2024;

public class Day24 : ISolver
{
    public object PartOne(string input, bool test)
    {
        var parsedInput = input.Split("\n\n");
        var initialValues = parsedInput[0].Split("\n");
        var gates = parsedInput[1].Split("\n").Select(GetGate).ToDictionary();
        var results = new Dictionary<string, bool>();

        foreach (var value in initialValues)
        {
            var v = value.Split(": ");
            results[v[0]] = v[1] == "1";
        }

        while (gates.Count > 0)
        {
            foreach (var g in gates)
            {
                if (results.TryGetValue(g.Value.Input1, out var input1) && results.TryGetValue(g.Value.Input2, out var input2))
                {
                    results[g.Key] = CalculateGate(g.Value.Type, input1, input2);
                    gates.Remove(g.Key);
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
        for (var i = 0; i < r.Length; i++)
        {
            result += r[i] ? ((ulong)1 << i) : 0;
        }
        return result;
    }

    public object PartTwo(string input, bool test)
    {
        var parsedInput = input.Split("\n\n");
        var initialValues = parsedInput[0].Split("\n");
        var results = new Dictionary<string, bool>();
        var gates = parsedInput[1].Split("\n").Select(GetGate).ToDictionary();
        foreach (var value in initialValues)
        {
            var v = value.Split(": ");
            results[v[0]] = v[1] == "1";
        }

        while (gates.Count > 0)
        {
            foreach (var g in gates)
            {
                if (results.TryGetValue(g.Value.Input1, out var input1) && results.TryGetValue(g.Value.Input2, out var input2))
                {
                    results[g.Key] = CalculateGate(g.Value.Type, input1, input2);
                    gates.Remove(g.Key);
                }
            }
        }

        gates = parsedInput[1].Split("\n").Select(GetGate).ToDictionary();


        // var x = results.Where(x => x.Key.StartsWith('x')).OrderBy(x => x.Key).Select(x => x.Value).ToArray();
        // // Utils.Print(x);
        // var y = results.Where(x => x.Key.StartsWith('y')).OrderBy(x => x.Key).Select(x => x.Value).ToArray();
        // // Utils.Print(y);
        // var z = results.Where(x => x.Key.StartsWith('z')).OrderBy(x => x.Key).Select(x => x.Value).ToArray();
        // Utils.Print(z, "");
        // var zCorrect = Convert.ToString((long)(ToDecimal(x)+ToDecimal(y)), 2).Reverse().ToArray();
        // Utils.Print(zCorrect, "");

        // var incorrectOutputs = new HashSet<(string, char)>();
        // var i = 0;
        // foreach(var (First, Second) in zCorrect.Zip(z.Select(x => x ? '1' : '0'))){
        //     Utils.Print(First + " " + Second);
        //     if (First != Second) {
        //         incorrectOutputs.Add(($"z{i:D2}", First));
        //     }
        //     i++;
        // }
        // Utils.Print(string.Join(",", incorrectOutputs.ToList()));

        // foreach ((string output, char correct) in incorrectOutputs) {
        //     var gate = gates[output];
        // }
        // ListDependencies("x00", gates);
        
        var nextCarry = "ktt";
        for (var i = 1; i < 45; i++) {
            Utils.Print("----------------");
            Utils.Print(i);
            nextCarry = CheckGate($"x{i:D2}", $"y{i:D2}", nextCarry, gates);
        }
        Utils.Print($"Next carry: {nextCarry}");
        return null;
    }

    private void ListDependencies(string input, Dictionary<string, Gate> gates)
    {
        foreach (var gate in gates.Where(x => x.Value.Input1 == input || x.Value.Input2 == input))
        {
            Utils.Print($"{gate.Value.Input1} {gate.Value.Type} {gate.Value.Input2} -> {gate.Key}");
            ListDependencies(gate.Key, gates);
        }
    }

    private bool HasInputs(Gate gate, string input1, string input2) => gate.Input1 == input1 && gate.Input2 == input2 || gate.Input1 == input2 && gate.Input2 == input1;
    private string CheckGate(string input1, string input2, string carry, Dictionary<string, Gate> gates)
    {

        var inputAndGate = gates.FirstOrDefault(x => HasInputs(x.Value, input1, input2) && x.Value.Type == "AND");
        if (inputAndGate.Value == default)
        {
            Utils.Print($"Input AND-Gate for {input1} and {input2} not found");
        }

        var inputXorGate = gates.FirstOrDefault(x => HasInputs(x.Value, input1, input2) && x.Value.Type == "XOR");
        if (inputXorGate.Value == default)
        {
            Utils.Print($"Input XOR-Gate for {input1} and {input2} not found");
        }

        var outputXorGate = gates.FirstOrDefault(x => HasInputs(x.Value, carry, inputXorGate.Value.Output) && x.Value.Type == "XOR");
        if (outputXorGate.Value == default)
        {
            Utils.Print($"Output XOR-Gate for {inputXorGate.Value.Output} and {carry} not found");
        }

        var firstAndGate = gates.FirstOrDefault(x => HasInputs(x.Value, inputXorGate.Value.Output, carry) && x.Value.Type == "AND");
        if (firstAndGate.Value == default)
        {
            Utils.Print($"First AND-Gate for {inputXorGate.Value.Output} and {carry} not found");
        }

        // var secondAndGate = gates.FirstOrDefault(x => HasInputs(x.Value, input1, input2) && x.Value.Type == "AND");
        // if (secondAndGate.Value == default)
        // {
        //     Utils.Print($"Second AND-Gate for {input1} and {input2} not found");
        // }

        var carryOrGate = gates.FirstOrDefault(x => HasInputs(x.Value, firstAndGate.Value.Output, inputAndGate.Value.Output) && x.Value.Type == "OR");
        if (carryOrGate.Value == default)
        {
            Utils.Print($"Carry OR-Gate for {firstAndGate.Value.Output} and {inputAndGate.Value.Output} not found");
        }
        return carryOrGate.Value.Output;
    }

    public ulong ToDecimal(bool[] bits)
    {
        ulong result = 0;
        for (var i = 0; i < bits.Length; i++)
        {
            result += bits[i] ? ((ulong)1 << i) : 0;
        }
        return result;
    }
    public bool CalculateGate(string type, bool b1, bool b2) => type switch
    {
        "AND" => b1 & b2,
        "OR" => b1 | b2,
        "XOR" => b1 ^ b2,
        _ => throw new NotImplementedException()
    };

    private KeyValuePair<string, Gate> GetGate(string line)
    {
        var parts = line.Split(" ");
        return new KeyValuePair<string, Gate>(parts[4], new Gate(parts[0], parts[1], parts[2], parts[4]));
    }

    private record struct Gate(string Input1, string Type, string Input2, string? Output);
}