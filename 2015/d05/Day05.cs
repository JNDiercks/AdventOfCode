using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015;
public class Day05 : ISolver
{
    public object PartOne(string input, bool test)
    {
        var lines = input.Split("\n");
        string pattern = @"(ab|cd|pq|xy)";

        int goodStrings = 0;
        foreach (var line in lines)
        {
            if (Regex.Match(line, pattern).Success){
                continue;
            }
            
            if (Regex.Matches(line, @"(a|e|i|o|u)").Count < 3) {
                continue;
            }
            
            if (!Regex.Match(line, @"(\w)\1").Success) {
                continue;
            }

            goodStrings++;
        }

        return goodStrings;
    }


    public object PartTwo(string input, bool test)
    {
        var lines = input.Split("\n");

        int goodStrings = 0;
        foreach (var line in lines)
        {
            if (!Regex.Match(line, @"(\w\w).*\1").Success){
                continue;
            }
            
            if (!Regex.Match(line, @"(\w).\1").Success) {
                continue;
            }
            
            
            goodStrings++;
        }

        return goodStrings;
    }

}
