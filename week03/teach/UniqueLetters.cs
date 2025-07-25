public static class UniqueLetters {
    public static void Run() {
        var test1 = "abcdefghjiklmnopqrstuvwxyz"; // Expect True because all letters unique
        Console.WriteLine(AreUniqueLetters(test1));

        var test2 = "abcdefghjiklanopqrstuvwxyz"; // Expect False because 'a' is repeated
        Console.WriteLine(AreUniqueLetters(test2));

        var test3 = "";
        Console.WriteLine(AreUniqueLetters(test3)); // Expect True because its an empty string
    }

    /// <summary>Determine if there are any duplicate letters in the text provided</summary>
    /// <param name="text">Text to check for duplicate letters</param>
    /// <returns>true if all letters are unique, otherwise false</returns>
    private static bool AreUniqueLetters(string text) {
        // TODO Problem 1 - Replace the O(n^2) algorithm to use sets and O(n) efficiency
        
        // SOLUTION: Use a HashSet to track seen characters
        // As we iterate through each character, if we've seen it before, return false
        // If we complete the loop without finding duplicates, return true
        var seenCharacters = new HashSet<char>();
        
        foreach (char c in text) {
            if (seenCharacters.Contains(c)) {
                return false; // Found a duplicate
            }
            seenCharacters.Add(c);
        }
        
        return true; // No duplicates found
        
        // OLD O(n^2) ALGORITHM (commented out):
        // for (var i = 0; i < text.Length; ++i) {
        //     for (var j = 0; j < text.Length; ++j) {
        //         // Don't want to compare to yourself ... that will always result in a match
        //         if (i != j && text[i] == text[j])
        //             return false;
        //     }
        // }
        // return true;
    }
}