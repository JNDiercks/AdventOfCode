namespace AdventOfCode.Y2024;
public class Day19 : ISolver
{
    static Dictionary<string, ulong> designCountCache = [];
    static Dictionary<string, ulong>.AlternateLookup<ReadOnlySpan<char>> cacheLookup = designCountCache.GetAlternateLookup<ReadOnlySpan<char>>();
    static HashSet<string> towels = [];
    static HashSet<string>.AlternateLookup<ReadOnlySpan<char>> towelLookup;

    public object PartOne(string input, bool test)
    {
        var inputParts = input.Split("\n\n");
        var towels = inputParts[0].Split(",").Select(x => x.Trim()).ToHashSet();
        var maxTowelLength = towels.Select(x => x.Length).Max();
        var designs = inputParts[1].Split("\n");
        int possibleDesignsCount = 0;
        foreach (var design in designs)
        {
            possibleDesignsCount += CheckDesign(design, towels, maxTowelLength) ? 1 : 0;
        }
        return possibleDesignsCount;
    }

    public object PartTwo(string input, bool test)
    {
        var inputParts = input.Split("\n\n");
        towels = inputParts[0].Split(",").Select(x => x.Trim()).ToHashSet();
        towelLookup = towels.GetAlternateLookup<ReadOnlySpan<char>>();
        var maxTowelLength = towels.Select(x => x.Length).Max();
        var designs = inputParts[1].Split("\n");
        ulong possibleDesignsCount = 0;
        foreach (var design in designs)
        {
            var count = CheckDesignOptions(design, maxTowelLength);
            possibleDesignsCount += count;
        }
        return possibleDesignsCount;
    }

    bool CheckDesign(string design, HashSet<string> towels, int maxTowelLength)
    {
        for (int i = 1; i <= Math.Min(design.Length, maxTowelLength); i++)
        {
            if (towels.Contains(design[..i]))
            {
                var newDesign = design[i..];
                if (newDesign.Length == 0) return true;
                if (CheckDesign(design[i..], towels, maxTowelLength)) return true;
            }
        }
        return false;
    }

    ulong CheckDesignOptions(ReadOnlySpan<char> design, int maxTowelLength)
    {
        ulong count = 0;
        for (int i = 1; i <= Math.Min(design.Length, maxTowelLength); i++)
        {
            if (towelLookup.Contains(design[..i]))
            {
                var newDesign = design[i..];
                if (cacheLookup.TryGetValue(newDesign, out var cachedCount)) {
                    count += cachedCount;                    
                    continue; 
                }
                if (newDesign.Length == 0) {
                     count++;
                     break;
                }
                count += CheckDesignOptions(newDesign, maxTowelLength);
            }
        }
        designCountCache[design.ToString()] = count;
        return count;
    }
}
