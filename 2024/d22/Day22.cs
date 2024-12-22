using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CircularBuffer;

namespace AdventOfCode.Y2024;
using Cache = System.Collections.Concurrent.ConcurrentDictionary<int[], int>;

class SequenceEqualComparer : IEqualityComparer<int[]>
{
    public bool Equals(int[]? a1, int[]? a2)
    {
        if (ReferenceEquals(a1, a2))
            return true;

        if (a2 is null || a1 is null)
            return false;

        return a1.SequenceEqual(a2);
    }

    public int GetHashCode(int[] a) => string.Join(string.Empty, a).GetHashCode();
}

public class Day22 : ISolver
{
    public object PartOne(string input, bool test)
    {
        var secretNumbers = input.Split("\n");
        var secretNumberCount = 10;
        ulong result = 0;
        
        foreach (var secretNumber in secretNumbers) {
            var nextSecret = CalculateSecretNumber(ulong.Parse(secretNumber), secretNumberCount);
            result += nextSecret;
        }
        return result;
    }

    public object PartTwo(string input, bool test)
    {
        var secretNumbers = input.Split("\n");
        var secretNumberCount = 2000;
        ulong result = 0;
        var comparer = new SequenceEqualComparer();
        var cache = new Cache(comparer);
        
        foreach (var secretNumber in secretNumbers) {
            var localCache = new Cache(comparer);
            var nextSecret = CalculateBestSequence(ulong.Parse(secretNumber), secretNumberCount, localCache);
            foreach(var key in localCache.Keys) {
                cache.AddOrUpdate(key, localCache[key], (key, oldValue) => oldValue + localCache[key]);
            }
            result += nextSecret;
        }
        var bestKey = cache.MaxBy(x => x.Value).Key;
        Console.WriteLine(string.Join(",", bestKey));
        return cache[bestKey];
    }
    public ulong CalculateBestSequence(ulong secret, int count, Cache cache) {
        var secretNumber = secret;
        var buffer = new CircularBuffer<int>(4);

        var price = (int)secretNumber % 10;
        for (int i = 1; i < count; i++) {
           secretNumber = CalculateSecretNumber(secretNumber); 
            int nextPrice = (int)secretNumber % 10;
            buffer.PushBack(nextPrice - price);
            price = nextPrice; 
            if (buffer.IsFull && !cache.ContainsKey(buffer.ToArray())) {
                cache.TryAdd(buffer.ToArray(), nextPrice);
            }
        }
        return secretNumber;
    }
    
    public ulong CalculateSecretNumber(ulong secret, int count) {
        var secretNumber = secret;
        for (int i = 0; i < count; i++) {
           secretNumber = CalculateSecretNumber(secretNumber); 
        }
        return secretNumber;
    }
    
    public ulong CalculateSecretNumber(ulong secret) {
        ulong firstStep = MixPrune(secret, secret * 64);
        ulong secondStep = MixPrune(firstStep, firstStep / 32);
        ulong thirdStep = MixPrune(secondStep, secondStep * 2048);

        // Utils.Print(thirdStep.ToString());
        return thirdStep;
    }
    
    public ulong MixPrune(ulong secret, ulong number) => Prune(Mix(secret, number));
    public ulong Mix(ulong secret, ulong number) => secret ^ number;
    public ulong Prune(ulong secret) => secret % 16777216;
}