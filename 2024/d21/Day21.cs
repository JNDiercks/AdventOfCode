using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024;
using Cache = System.Collections.Concurrent.ConcurrentDictionary<(char currentKey, char nextKey), string>;
public class Day21 : ISolver
{

    public object PartOne(string input, bool test)
    {
        // Utils.Print(NumericMoveTo(null, 3, null));
        // Utils.Print(NumericMoveTo(3, 7, "^"));
        // Utils.Print(NumericMoveTo(7, 9, "<"));
        var cache = new Cache();
        // Utils.Print(GetNumericKeypadMoves(null, "379A"));
        Utils.Print(GetDirectionKeypadMoves('A', GetDirectionKeypadMoves('A', "^<A>A^^<>AvvvA", cache), cache).Length);
        Utils.Print(GetDirectionKeypadMoves('A', GetDirectionKeypadMoves('A', "^A<<^^A>>AvvvA", cache), cache).Length);

        // Utils.Print(NumericMoveTo(9, null, ">"));
        // Utils.Print(NumericMoveTo(8, null, ">"));
        // Utils.Print(NumericMoveTo(5, null, "v"));
        // Utils.Print(NumericMoveTo(5, null, ">"));
        // Utils.Print(GetDirectionKeypadMoves('A', "<A>A<AAv<AA>>^AvAA^Av<AAA>^A"));
        // return null;
        int result = 0;

        foreach (var code in input.Split("\n")) {
            Utils.Print(code);
            var robots = 2;
            var numericMoves = GetNumericKeypadMoves(null, code);
            Utils.Print(numericMoves);
            var robotMoves = GetDirectionKeypadMoves('A', numericMoves, cache);
            Utils.Print(robotMoves);
            for (int i = 0; i < robots - 1; i++) {
                robotMoves = GetDirectionKeypadMoves('A', robotMoves, cache);
                Utils.Print(robotMoves);
            }
            Utils.Print($"{code}: Length: {robotMoves.Length} num: {int.Parse(Regex.Match(code, @"\d+").Value)}");
            result += robotMoves.Length * int.Parse(Regex.Match(code, @"\d+").Value);
        }
        return result;
    }

    public object PartTwo(string input, bool test)
    {
        return null;
    }

    public string GetNumericKeypadMoves(int? startLocation, string numericalKeypadInput)
    {
        string moves = string.Empty;
        int? currentLocation = startLocation;
        string? previousMove = null;
        foreach (var input in numericalKeypadInput.ToArray())
        {
            var move = NumericMoveTo(currentLocation, InputToKeypadInt(input.ToString()), previousMove);
            moves += move;
            previousMove = move;
            moves += "A";
            currentLocation = InputToKeypadInt(input.ToString());
        }
        return moves;
    }

    public string GetDirectionKeypadMoves(char startLocation, string numericalKeypadInput, Cache cache)
    {
        string moves = string.Empty;
        char currentLocation = startLocation;
        foreach (var input in numericalKeypadInput.ToArray())
        {
            moves += DirectionMoveTo(currentLocation, input, cache);
            moves += "A";
            currentLocation = input;
        }
        return moves;
    }

    public int? InputToKeypadInt(string input) => input switch
    {
        "A" => null,
        _ => int.Parse(input)
    };

    public string NumericMoveTo(int? startLocation, int? endLocation, string? previousMove) 
    {
    
        if (startLocation == endLocation) return string.Empty;

        if (startLocation is null) return NumericMoveToFromA(startLocation, endLocation);
        if (endLocation is null) return NumericMoveFromToA(startLocation, endLocation, previousMove);

        if (startLocation is 0) return NumericMoveToFrom0(startLocation, endLocation);
        if (endLocation is 0) return NumericMoveFromTo0(startLocation, endLocation, previousMove);

        var difference = endLocation - startLocation;
        return (startLocation, endLocation, difference, previousMove) switch
        {
            (3, 7, _, _) => "<" + NumericMoveTo(startLocation - 1, endLocation, "<"),
            (_, _, 6, _) => "^" + NumericMoveTo(startLocation + 3, endLocation, "^"),
            (_, _, -6, _) => "v" + NumericMoveTo(startLocation - 3, endLocation, "v"),
            (_, _, >= 3, "^") => "^" + NumericMoveTo(startLocation + 3, endLocation, "^"),
            (_, _, > 3, "<") => "<" + NumericMoveTo(startLocation - 1, endLocation, "<"),
            (_, _, > 3, ">") => ">" + NumericMoveTo(startLocation + 1, endLocation, ">"),
            (_, _, >= 3, _) => "^" + NumericMoveTo(startLocation + 3, endLocation, "^"),
            (_, _, <= -3, "v") => "v" + NumericMoveTo(startLocation - 3, endLocation, "v"),
            (_, _, < -3, "<") => "<" + NumericMoveTo(startLocation - 1, endLocation, "<"),
            (_, _, < -3, ">") => ">" + NumericMoveTo(startLocation + 1, endLocation, "<"),
            (_, _, <= -3, _) => "v" + NumericMoveTo(startLocation - 3, endLocation, "v"),
            ( < 7, >= 7, > 0, "^") => "^" + NumericMoveTo(startLocation + 3, endLocation, "^"),
            ( < 7, >= 7, > 0, "<") => "<" + NumericMoveTo(startLocation - 1, endLocation, "<"),
            ( < 7, >= 7, > 0, ">") => ">" + NumericMoveTo(startLocation + 1, endLocation, ">"),
            ( < 4, >= 4, > 0, "^") => "^" + NumericMoveTo(startLocation + 3, endLocation, "^"),
            ( < 4, >= 4, > 0, "<") => "<" + NumericMoveTo(startLocation - 1, endLocation, "<"),
            ( < 4, >= 4, > 0, ">") => ">" + NumericMoveTo(startLocation + 1, endLocation, ">"),
            ( > 3, <= 3, < 0, "v") => "v" + NumericMoveTo(startLocation - 3, endLocation, "v"),
            ( > 3, <= 3, < 0, "<") => "<" + NumericMoveTo(startLocation - 1, endLocation, "<"),
            ( > 3, <= 3, < 0, ">") => ">" + NumericMoveTo(startLocation + 1, endLocation, ">"),
            ( > 6, <= 6, < 0, "v") => "v" + NumericMoveTo(startLocation - 3, endLocation, "v"),
            ( > 6, <= 6, < 0, "<") => "<" + NumericMoveTo(startLocation - 1, endLocation, "<"),
            ( > 6, <= 6, < 0, ">") => ">" + NumericMoveTo(startLocation + 1, endLocation, ">"),
            (_, _, > 0, _) => ">" + NumericMoveTo(startLocation + 1, endLocation, ">"),
            (_, _, < 0, _) => "<" + NumericMoveTo(startLocation - 1, endLocation, "<"),
            _ => throw new ArgumentOutOfRangeException(nameof(difference)),
        };
    }

    public string NumericMoveToFromA(int? startLocation, int? endLocation)
    {
        Debug.Assert(startLocation != endLocation);
        if (startLocation is not null) throw new ArgumentException("startLocation not null");

        if (endLocation == 0) return "<";
        if (endLocation is 5 or 8) return '<' + NumericMoveTo(0, endLocation, "<");
        return '^' + NumericMoveTo(3, endLocation, "^");
    }

    public string NumericMoveFromToA(int? startLocation, int? endLocation, string? previousMove)
    {
        Debug.Assert(startLocation != endLocation);
        Debug.Assert(endLocation == null);

        if (startLocation == 0) return ">";
        if (startLocation is 5 or 8 && previousMove != ">") return NumericMoveFromTo0(startLocation, 0, previousMove) + ">";
        return NumericMoveTo(startLocation, 3, previousMove) + "v";
    }
    public string NumericMoveToFrom0(int? startLocation, int? endLocation)
    {
        Debug.Assert(startLocation != endLocation);
        if (startLocation is not 0) throw new ArgumentException("startLocation not 0");

        if (endLocation == null) return ">";
        return '^' + NumericMoveTo(2, endLocation, "^");
    }

    public string NumericMoveFromTo0(int? startLocation, int? endLocation, string? previousMove)
    {
        Debug.Assert(startLocation != endLocation);
        Debug.Assert(endLocation == 0);

        if (startLocation == null) return "<";
        if (startLocation is 6 or 9) return NumericMoveFromToA(startLocation, null, previousMove) + "<";
        return NumericMoveTo(startLocation, 2, previousMove) + "v";
    }
    public string DirectionMoveFromToDown(char startLocation)
    {
        if (startLocation == 'v') return string.Empty;
        Debug.Assert(startLocation != 'A');
        return startLocation switch
        {
            '^' => "v",
            '>' => "<",
            '<' => ">",
            _ => throw new ArgumentException(),
        };
    }
    public string DirectionMoveToFromDown(char endLocation)
    {
        Debug.Assert(endLocation != 'A');
        return endLocation switch
        {
            '^' => "^",
            '>' => ">",
            '<' => "<",
            _ => throw new ArgumentException(),
        };
    }

    public string DirectionMoveTo(char startLocation, char endLocation, Cache cache) => 
    cache.GetOrAdd((startLocation, endLocation), _ =>
    {
        if (startLocation == endLocation) return string.Empty;
        if (startLocation == 'A') return DirectionMoveToFromA(endLocation);
        if (endLocation == 'A') return DirectionMoveFromToA(startLocation);
        if (startLocation == 'v') return DirectionMoveToFromDown(endLocation);
        if (endLocation == 'v') return DirectionMoveFromToDown(endLocation);

        return startLocation switch
        {
            '^' => "v" + DirectionMoveToFromDown(endLocation),
            '>' => "<" + DirectionMoveToFromDown(endLocation),
            '<' => ">" + DirectionMoveToFromDown(endLocation),
            _ => throw new ArgumentException(),
        };
    });

    public string DirectionMoveFromToA(char startLocation)
    {

        return startLocation switch
        {
            '^' => ">",
            '>' => "^",
            '<' => DirectionMoveFromToDown(startLocation) + DirectionMoveFromToA('v'),
            'v' => ">" + DirectionMoveFromToA('>'),
            _ => throw new ArgumentException(),
        };
    }
    public string DirectionMoveToFromA(char endLocation)
    {
        return endLocation switch
        {
            '^' => "<",
            '>' => "v",
            '<' => "v" + DirectionMoveFromToDown('>') + DirectionMoveToFromDown('<'),
            'v' => "v" + DirectionMoveFromToDown('>'),
            _ => throw new ArgumentException(),
        };
    }
}