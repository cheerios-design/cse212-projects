using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Use a dictionary to map character pairs to their indices
        var charPairToIndex = new Dictionary<(char, char), int>();
        var result = new List<string>();

        // Build the dictionary in one pass
        for (int i = 0; i < words.Length; i++)
        {
            var word = words[i];
            var pair = (word[0], word[1]);
            
            // Skip palindromes
            if (pair.Item1 == pair.Item2)
                continue;
                
            charPairToIndex[pair] = i;
        }

        // Find symmetric pairs in second pass
        var processed = new HashSet<int>();
        for (int i = 0; i < words.Length; i++)
        {
            if (processed.Contains(i))
                continue;
                
            var word = words[i];
            var reversePair = (word[1], word[0]);
            
            // Skip palindromes
            if (word[0] == word[1])
                continue;
            
            // Check if reverse pair exists
            if (charPairToIndex.TryGetValue(reversePair, out int reverseIndex) && !processed.Contains(reverseIndex))
            {
                result.Add($"{words[reverseIndex]} & {word}");
                processed.Add(i);
                processed.Add(reverseIndex);
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // The degree is in column 4 (0-indexed column 3)
            if (fields.Length > 3)
            {
                var degree = fields[3];
                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize both words: convert to lowercase and remove spaces
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // If lengths are different, they can't be anagrams
        if (word1.Length != word2.Length)
            return false;

        // Count the frequency of each character in word1
        var charCount = new Dictionary<char, int>();
        
        foreach (char c in word1)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }

        // For each character in word2, decrement its count
        foreach (char c in word2)
        {
            if (!charCount.ContainsKey(c))
                return false; // Character not in word1
            
            charCount[c]--;
            
            if (charCount[c] < 0)
                return false; // More occurrences in word2 than word1
        }

        // Check if all counts are zero
        return charCount.Values.All(count => count == 0);
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Create formatted strings for each earthquake
        var results = new List<string>();
        
        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                if (feature.Properties != null)
                {
                    var place = feature.Properties.Place ?? "Unknown location";
                    var magnitude = feature.Properties.Mag;
                    results.Add($"{place} - Mag {magnitude}");
                }
            }
        }

        return results.ToArray();
    }
}