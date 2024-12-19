
public class Day19 : ISolver
{
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
        var towels = inputParts[0].Split(",").Select(x => x.Trim()).ToHashSet();
        var maxTowelLength = towels.Select(x => x.Length).Max();
        var designs = inputParts[1].Split("\n");
        ulong possibleDesignsCount = 0;
        Dictionary<string, ulong> designCountCache = new();
        foreach (var design in designs)
        {
            var count = CheckDesignOptions(design, towels, maxTowelLength, designCountCache);
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

    ulong CheckDesignOptions(string design, HashSet<string> towels, int maxTowelLength, Dictionary<string, ulong> designCountCache)
    {
        ulong count = 0;
        for (int i = 1; i <= Math.Min(design.Length, maxTowelLength); i++)
        {
            if (towels.Contains(design[..i]))
            {
                var newDesign = design[i..];
                if (designCountCache.TryGetValue(newDesign, out var cachedCount)) {
                    count += cachedCount;                    
                    continue; 
                }
                if (newDesign.Length == 0) {
                     count++;
                     break;
                }
                count += CheckDesignOptions(newDesign, towels, maxTowelLength, designCountCache);
            }
        }
        designCountCache[design] = count;
        return count;
    }
}
