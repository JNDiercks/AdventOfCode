using System.Diagnostics;

public class Day18 : ISolver
{
    public object? PartOne(string input, bool test)
    {
        (int x, int y)[] bytes = input.Split("\n").Select(x => (int.Parse(x.Split(",")[0]), int.Parse(x.Split(",")[1]))).ToArray();
        (int xmax, int ymax) = test ? (7, 7) : (71, 71);
        var memory = new char[xmax, ymax];
        var startTicks = Stopwatch.GetTimestamp();

        foreach (var b in bytes.Take(1024))
        {
            memory[b.x, b.y] = '#';
        }
        Print2DArray(memory);
        (int x, int y) startingPoint = (0, 0);
        (int x, int y) endingPoint = test ? (6, 6) : (70, 70);

        var shortestPath = FindShortestPath(memory, startingPoint, endingPoint);
        Console.WriteLine("Shortest path length: " + shortestPath);
        return shortestPath;
    }

    public object? PartTwo(string input, bool test)
    {
        (int x, int y)[] bytes = input.Split("\n").Select(x => (int.Parse(x.Split(",")[0]), int.Parse(x.Split(",")[1]))).ToArray();
        (int xmax, int ymax) = test ? (7, 7) : (71, 71);
        var memory = new char[xmax, ymax];
        var startTicks = Stopwatch.GetTimestamp();
        (int x, int y) startingPoint = (0, 0);
        (int x, int y) endingPoint = test ? (6, 6) : (70, 70);
        foreach (var b in bytes)
        {
            memory[b.x, b.y] = '#';
            var shortestPath = FindShortestPath(memory, startingPoint, endingPoint);
            if (shortestPath is null)
            {
                Console.WriteLine("Path is blocked by byte: " + b);
                return b;
            }
        }
        return null;
    }

    static void Print2DArray<T>(T[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[j, i] + "\t");
            }
            Console.WriteLine();
        }
    }

    (int x, int y) AddLocations((int x, int y) a, (int x, int y) b) => (a.x + b.x, a.y + b.y);

    int? FindShortestPath(char[,] array, (int x, int y) start, (int x, int y) end)
    {
        int xmax = array.GetLength(1);
        int ymax = array.GetLength(0);
        (int x, int y)[] directions = [(0, -1), (1, 0), (0, 1), (-1, 0)];
        var position = start;
        Dictionary<(int x, int y), int?> cost = new() { { start, 0 } };
        PriorityQueue<(int x, int y), int> queue = new();
        queue.Enqueue(position, 0);

        while (queue.Count > 0)
        {
            position = queue.Dequeue();

            if (position == end) return cost.GetValueOrDefault(end);

            var currentDistance = cost.GetValueOrDefault(position) ?? int.MaxValue;

            foreach (var d in directions)
            {
                var nextPosition = AddLocations(position, d);
                if (!(nextPosition.x >= 0 && nextPosition.x < xmax && nextPosition.y >= 0 && nextPosition.y < ymax)) continue;
                if (array[nextPosition.x, nextPosition.y] == '#') continue;
                var distance = cost.GetValueOrDefault(nextPosition) ?? int.MaxValue;

                var newDistance = currentDistance + 1;
                if (distance > newDistance)
                {
                    cost[nextPosition] = newDistance;
                    queue.Remove(nextPosition, out _, out _);
                    queue.Enqueue(nextPosition, newDistance);
                }
            }
        }

        return null;
    }
}
