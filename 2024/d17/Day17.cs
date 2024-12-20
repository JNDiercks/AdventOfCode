using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024;
public class Day17 : ISolver
{
    public object PartOne(string input, bool test)
    {
        var inputTextBlocks = input.Split("\n\n");
        var instructionInput = inputTextBlocks[1];
        var instructions = instructionInput.Split(" ")[1].Trim().Split(",").Select(x => ulong.Parse(x)).ToList();

        ulong defaultValue = 46337277;
        var output = Run(defaultValue, instructions);
        return string.Join(",", output);
    }
    public object PartTwo(string input, bool test)
    {
        var inputTextBlocks = input.Split("\n\n");
        var instructionInput = inputTextBlocks[1];
        var instructions = instructionInput.Split(" ")[1].Trim().Split(",").Select(x => ulong.Parse(x)).ToList();
        ulong start = 0;
        ulong range = start + 16;
        ulong i = 0;
        var queue = new Queue<ulong>();
        queue.Enqueue(start);
        List<ulong> possibleSolutions = [];
        while (queue.Count > 0)
        {
            i = queue.Dequeue();
            range = i + 16;
            for (; i < range; i++)
            {
                // Console.WriteLine(i);
                var output = Run(i, instructions);
                if (output.SequenceEqual(instructions))
                {
                    possibleSolutions.Add(i);
                    break;
                }

                if (instructions[^output.Count..].SequenceEqual(output))
                {
                    queue.Enqueue((i * 8) - 1);
                }
            }
        }
        return possibleSolutions.Min();
    }
    Dictionary<char, int> ReadInitialRegisters(string[] inputLines)
    {
        var register = new Dictionary<char, int>();
        string pattern = @"([A,B,C]):\s*(-?\d+)";
        foreach (var line in inputLines)
        {
            var match = Regex.Match(line, pattern);
            var parts = match.Value.Split(":");
            register[parts[0][0]] = int.Parse(parts[1].Trim());
        }

        return register;
    }

    ulong GetOperandValue(ulong operand, Dictionary<char, ulong> registers) => operand switch
    {
        >= 0 and <= 3 => operand,
        4 => registers['A'],
        5 => registers['B'],
        6 => registers['C'],
        _ => throw new ArgumentOutOfRangeException(nameof(operand)),
    };

    (int, ulong?) ExecuteInstruction(ulong instructionCode, ulong operand, int instructionPointer, Dictionary<char, ulong> registers)
    {
        var registerA = registers['A'];
        var registerB = registers['B'];
        var registerC = registers['C'];

        switch (instructionCode)
        {
            case 0:
                Debug.Assert(operand <= 3);
                registers['A'] = (ulong)(registerA / Math.Pow(2, GetOperandValue(operand, registers)));
                break;
            case 1:
                registers['B'] = registerB ^ operand;
                break;
            case 2:
                registers['B'] = GetOperandValue(operand, registers) % 8;
                break;
            case 3:
                if (registerA == 0)
                {
                    break;
                }
                Debug.Assert(operand == 0);
                return ((int)operand, null);
            case 4:
                registers['B'] = registerB ^ registerC;
                break;
            case 5:
                return (instructionPointer + 2, (GetOperandValue(operand, registers) % 8));
            case 6:
                registers['B'] = (ulong)(registerA / Math.Pow(2, GetOperandValue(operand, registers)));
                break;
            case 7:
                registers['C'] = (ulong)(registerA / Math.Pow(2, GetOperandValue(operand, registers)));
                break;
        }
        return (instructionPointer + 2, null);
    }

    List<ulong> Run(ulong registerValueA, List<ulong> instructions)
    {
        var registers = new Dictionary<char, ulong>
            {
                { 'A', registerValueA},
                { 'B', 0 },
                { 'C', 0 }
            };

        var instructionString = string.Join(",", instructions);
        List<ulong> outputs = [];
        for (int i = 0; i < instructions.Count;)
        {
            var opcode = instructions[i];
            var comboOperand = instructions[i + 1];
            (i, ulong? output) = ExecuteInstruction(opcode, comboOperand, i, registers);
            var lines = registers.Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
            if (output is not null)
            {
                outputs.Add((ulong)output);
            }
        };
        return outputs;
    }
}