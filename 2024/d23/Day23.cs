namespace AdventOfCode.Y2024;

using System.Collections.Immutable;
using System.Data;
using Cache = Dictionary<int, int>;

public class Day23 : ISolver
{
    public object PartOne(string input, bool test)
    {
        var parsedInput = input.Split("\n");
        var computer = parsedInput.Select(x => x.Split("-")).SelectMany(x => x).ToHashSet();
        var networkConnections = parsedInput.Select(GetNetworkConnection);
        // foreach (var connection in networkConnections.Where(x => x.C1.StartsWith('t') || x.C2.StartsWith('t')))
        // {
        //     // Utils.Print(connection.ToString());
        //     (var c1, var c2) = connection.GetConnections();
        //     var hasTriangleConnection = computer.Where(c => c != c1 && c != c2 && networkConnections.Where(x => x.ConnectsTo(c) && (x.ConnectsTo(c1) || x.ConnectsTo(c2))).Count() > 1 );
        //     foreach (var c in hasTriangleConnection) {
        //         Utils.Print($"{c1},{c2},{c}");
        //     }
        // }
        var triangleConnections = new HashSet<TriangleConnection>();
        foreach (var c in computer.Where(c => c.StartsWith('t')))
        {
            var computers = networkConnections.Where(x => x.ConnectsTo(c)).Select(x => x.GetConnectsTo(c)).ToList();
            var triangle = (from c1 in computers
                            from c2 in computers
                            where c1 != c2
                            select networkConnections.Where(x => x.Connects(c1, c2))).SelectMany(x => x).Distinct().ToList();

            foreach (var t in triangle)
            {
                (var t1, var t2) = t.GetConnections();
                triangleConnections.Add(new TriangleConnection(c, t1, t2));
            }
        }


        return triangleConnections.Count();
    }

    public object PartTwo(string input, bool test)
    {
        var parsedInput = input.Split("\n");
        // var computers = parsedInput.Select(x => x.Split("-")).SelectMany(x => x).ToHashSet();
        var networkConnections = new Dictionary<string, List<string>>();
        foreach (var line in parsedInput)
        {
            var parts = line.Split('-');
            if (networkConnections.TryGetValue(parts[0], out _)) {
                networkConnections[parts[0]].Add(parts[1]);

            } else {
                networkConnections[parts[0]] = [parts[1]];
            }

            if (networkConnections.TryGetValue(parts[1], out _)) {
                networkConnections[parts[1]].Add(parts[0]);
                
            } else {
                networkConnections[parts[1]] = [parts[0]];
            }
        } 

        List<List<string>> cliques = [];

        string[] maxNetwork = [];
        var computers = networkConnections.Keys.Order().ToImmutableSortedSet();
        void Visit(string[] network)
        {
            // Check if max complete network
            if (network.Length > maxNetwork.Length)
            {
                maxNetwork = network;
            }
            // Recurse
            var tails = (network.Length == 0) ? computers : computers.SkipWhile(c => c.CompareTo(network.Last()) <= 0).ToImmutableSortedSet();
            foreach (var computer in tails)
            {
                if (network.All(c => networkConnections[c].Contains(computer)))
                {
                    string[] newNetwork = [.. network, computer];
                    Visit(newNetwork);
                }
            }
        }
        Visit([]);
        // Utils.Print(string.Join(",", cliques.MaxBy(x => x.Count).OrderBy(x => x).ToArray()));
        Utils.Print(string.Join(",", maxNetwork));
        return maxNetwork.Count();
    }

    private bool IsConnectedToAll(string computer, IEnumerable<string> computers, IEnumerable<NetworkConnection> networkConnections)
    {
        foreach (var c in computers)
        {
            if (!networkConnections.Contains(new NetworkConnection(c, computer)))
            {
                return false;
            }
        }
        return true;
    }

    private NetworkConnection GetNetworkConnection(string line)
    {
        var s = line.Split("-");
        return new NetworkConnection(s[0], s[1]);
    }

    private readonly struct TriangleConnection : IEquatable<TriangleConnection>
    {
        public TriangleConnection(string c1, string c2, string c3)
        {
            C1 = c1;
            C2 = c2;
            C3 = c3;
        }
        public string C1 { get; }
        public string C2 { get; }
        public string C3 { get; }

        public string[] GetConnectinos() => [C1, C2, C3];
        public override bool Equals(object? obj) => obj is TriangleConnection other && Equals(other);
        public override int GetHashCode() => C1.GetHashCode() + C2.GetHashCode() + C3.GetHashCode();
        public bool Equals(TriangleConnection other)
        {
            return GetConnectinos().All(other.GetConnectinos().Contains);
        }
    }
    public readonly struct NetworkConnection : IEquatable<NetworkConnection>
    {
        public NetworkConnection(string c1, string c2)
        {
            C1 = c1;
            C2 = c2;
        }

        public string C1 { get; }
        public string C2 { get; }
        public bool ConnectsTo(string c) => C1 == c || C2 == c;
        public bool Connects(string c1, string c2) => C1 == c1 && C2 == c2 || C1 == c2 && C2 == c1;
        public string GetConnectsTo(string c)
        {
            if (c != C1 && c != C2) throw new ArgumentException("does not connect");
            return C1 == c ? C2 : C1;
        }

        public (string c1, string c2) GetConnections() => (C1, C2);
        public bool Equals(NetworkConnection other) => (C1 == other.C1 || C1 == other.C2) && (C2 == other.C2 || C2 == other.C1);
        public override bool Equals(object? obj) => obj is NetworkConnection other && Equals(other);
        public override int GetHashCode() => C1.GetHashCode() + C2.GetHashCode();

        public static bool operator ==(NetworkConnection lhs, NetworkConnection rhs) => lhs.Equals(rhs);

        public static bool operator !=(NetworkConnection lhs, NetworkConnection rhs) => !(lhs == rhs);

        public override string ToString() => $"({C1}, {C2})";
    }

}