using System.Text;

namespace AdventOfCode.Y2015;
public class Day04 : ISolver
{
    public object PartOne(string input, bool test)
    {
        long i = 0;
        for (i = 0; i < long.MaxValue; i++) {
            if (MD5(input + i.ToString()).StartsWith("00000")) {
                Utils.Print(i.ToString());
                break;
            }
        }
        return i;
    }


    public object PartTwo(string input, bool test)
    {
        long i = 0;
        for (i = 0; i < long.MaxValue; i++) {
            if (MD5(input + i.ToString()).StartsWith("000000")) {
                Utils.Print(i.ToString());
                break;
            }            
        }
        return i;
    }
    
    public static string MD5(string s)
    {
    using var provider = System.Security.Cryptography.MD5.Create();        
    StringBuilder builder = new StringBuilder();                           

    foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
        builder.Append(b.ToString("x2").ToLower());

    return builder.ToString();        
    }
}
