/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData) {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);
            
            // TODO Problem 3 - Add logic to sum up points for each player
            // Use the Dictionary to accumulate total points per player
            if (players.ContainsKey(playerId)) {
                // Player already exists, add to their total
                players[playerId] += points;
            } else {
                // New player, add them to the dictionary
                players[playerId] = points;
            }
        }

        Console.WriteLine($"Players: {{{string.Join(", ", players)}}}");

        // Convert dictionary to array for sorting
        var playerArray = players.ToArray();
        
        // Sort by points (descending order - highest points first)
        Array.Sort(playerArray, (x, y) => y.Value.CompareTo(x.Value));
        
        // Display top 10 players
        Console.WriteLine("\nTop 10 Players by Career Points:");
        Console.WriteLine("Player ID\t\tTotal Points");
        Console.WriteLine("=========\t\t============");
        
        for (int i = 0; i < Math.Min(10, playerArray.Length); i++) {
            var player = playerArray[i];
            Console.WriteLine($"{player.Key}\t\t{player.Value:N0}");
        }
    }
}