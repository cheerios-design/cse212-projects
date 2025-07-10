public class Divisors {
    public static List<int> FindDivisors(int number) {
        List<int> results = new();
        
        for (int i = 1; i < number; i++) {
            if (number % i == 0) {
                results.Add(i);
            }
        }
        
        return results;
    }

    public static void Run() {
        // Test the FindDivisors method with various numbers
        Console.WriteLine("Testing FindDivisors method:");
        
        // Test case 1: 12 - should return {1, 2, 3, 4, 6}
        var divisors12 = FindDivisors(12);
        Console.WriteLine($"Divisors of 12: {{{string.Join(", ", divisors12)}}}");
        
        // Test case 2: 17 - should return {1} (prime number)
        var divisors17 = FindDivisors(17);
        Console.WriteLine($"Divisors of 17: {{{string.Join(", ", divisors17)}}}");
        
        // Test case 3: 20 - should return {1, 2, 4, 5, 10}
        var divisors20 = FindDivisors(20);
        Console.WriteLine($"Divisors of 20: {{{string.Join(", ", divisors20)}}}");
    }
}
