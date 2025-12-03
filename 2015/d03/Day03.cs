using System.Numerics;

namespace AdventOfCode.Y2015;
public class Day03 : ISolver
{
    Complex[] directions = [-1, 1, Complex.ImaginaryOne, -Complex.ImaginaryOne];
    public object PartOne(string input, bool test)
    {
        var route = input;
        var position = new Complex(0, 0);
        var seen = new HashSet<Complex>
        {
            position
        };

        foreach( var d in route.ToCharArray()) {
            position = GetNextPosition(position, d);
            seen.Add(position);
        }
        return seen.Count;
    }


    public object PartTwo(string input, bool test)
    {
        var route = input.ToCharArray();
        var positionSanta = new Complex(0, 0);
        var positionRoboSanta = new Complex(0, 0);
        var seen = new HashSet<Complex>
        {
            positionSanta
        };

        for( int i = 0; i < route.Length; i += 2) {
            positionSanta = GetNextPosition(positionSanta, route[i]);
            seen.Add(positionSanta);
            positionRoboSanta = GetNextPosition(positionRoboSanta, route[i+1]);
            seen.Add(positionRoboSanta);
        }

        return seen.Count;
    }
    public Complex GetNextPosition(Complex position, char direction) {
        return direction switch {
            '^' => position + Complex.ImaginaryOne,
            '>' => position + 1,
            'v' => position - Complex.ImaginaryOne,
            '<' => position - 1,
            _ => throw new Exception("Not found")
        };
    }
}
