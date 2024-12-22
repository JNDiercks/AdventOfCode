namespace AdventOfCode.Y2024;
using Cache = System.Collections.Concurrent.ConcurrentDictionary<int, int>;

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
        var cache = new Cache();
        
        foreach (var secretNumber in secretNumbers) {
            var nextSecret = CalculateBestSequence(ulong.Parse(secretNumber), secretNumberCount, cache);
            result += nextSecret;
        }
        return cache.Values.Max();
    }
    public ulong CalculateBestSequence(ulong secret, int count, Cache cache) {
        var secretNumber = secret;
        int buffer = 0;
        var uniques = new HashSet<int>();

        var price = (ushort)(secretNumber % 10);
        for (int i = 0; i < count; i++) {
           secretNumber = CalculateSecretNumber(secretNumber); 
            ushort nextPrice = (ushort)(secretNumber % 10);
            buffer <<= 8;
            byte diff = (byte)(sbyte)(nextPrice - price);
            // Utils.Print((sbyte)diff);
            buffer |= diff;
            price = nextPrice; 
            if (i >= 3 && uniques.Add(buffer)) {
                cache.AddOrUpdate(buffer, price, (key, oldValue) => oldValue + price);
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
    
    public ulong CalculateSecretNumber(ulong number) {
		number = Prune(Mix(number, number * 64));
		number = Prune(Mix(number, number / 32));
		number = Prune(Mix(number, number * 2048));
		return number;
    }
    
    public ulong MixPrune(ulong secret, ulong number) => Prune(Mix(secret, number));
    public ulong Mix(ulong secret, ulong number) => secret ^ number;
    public ulong Prune(ulong secret) => secret % 16777216;
}