public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN: Create an array of multiples of a given number
        // Step 1: Create a new array of doubles with the specified length
        // Step 2: Use a loop to iterate from 1 to length (inclusive)
        // Step 3: For each iteration i, calculate the multiple by multiplying number * i
        // Step 4: Store each multiple in the array at index (i-1) since arrays are 0-indexed
        // Step 5: Return the completed array
        // Example: MultiplesOf(7, 5) should return {7, 14, 21, 28, 35}
        //          - First iteration: 7 * 1 = 7 (index 0)
        //          - Second iteration: 7 * 2 = 14 (index 1)
        //          - Third iteration: 7 * 3 = 21 (index 2)
        //          - Fourth iteration: 7 * 4 = 28 (index 3)
        //          - Fifth iteration: 7 * 5 = 35 (index 4)

        // Step 1: Create array of specified length
        double[] multiples = new double[length];
        
        // Step 2-4: Loop through and calculate each multiple
        for (int i = 1; i <= length; i++)
        {
            // Calculate the multiple and store at correct index
            multiples[i - 1] = number * i;
        }
        
        // Step 5: Return the completed array
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN: Rotate a list to the right by a specified amount
        // Step 1: Handle edge cases - if list is empty or amount is 0, no rotation needed
        // Step 2: Normalize the amount using modulo to handle cases where amount > list length
        // Step 3: Use list slicing approach to split the list into two parts:
        //         - Part A: Elements that need to move to the front (last 'amount' elements)
        //         - Part B: Elements that need to move to the back (remaining elements)
        // Step 4: Clear the original list and rebuild it with Part A + Part B
        // 
        // Example: RotateListRight({1,2,3,4,5,6,7,8,9}, 3)
        //          - Original: {1,2,3,4,5,6,7,8,9}
        //          - Amount: 3 (move last 3 elements to front)
        //          - Part A (last 3): {7,8,9} (elements from index 6 to end)
        //          - Part B (first 6): {1,2,3,4,5,6} (elements from index 0 to 5)
        //          - Result: {7,8,9,1,2,3,4,5,6}
        //
        // Another example: RotateListRight({1,2,3,4,5,6,7,8,9}, 5)
        //          - Original: {1,2,3,4,5,6,7,8,9}
        //          - Amount: 5 (move last 5 elements to front)
        //          - Part A (last 5): {5,6,7,8,9} (elements from index 4 to end)
        //          - Part B (first 4): {1,2,3,4} (elements from index 0 to 3)
        //          - Result: {5,6,7,8,9,1,2,3,4}

        // Step 1: Handle edge cases
        if (data.Count == 0 || amount == 0)
            return;
        
        // Step 2: Normalize amount to handle cases where amount > list length
        amount = amount % data.Count;
        if (amount == 0) // If amount equals list length, no rotation needed
            return;
            
        // Step 3: Calculate split point and extract the two parts
        int splitPoint = data.Count - amount; // Where to split the list
        
        // Part A: Last 'amount' elements (these move to the front)
        List<int> partA = data.GetRange(splitPoint, amount);
        
        // Part B: First 'splitPoint' elements (these move to the back)
        List<int> partB = data.GetRange(0, splitPoint);
        
        // Step 4: Clear the original list and rebuild with Part A + Part B
        data.Clear();
        data.AddRange(partA); // Add the elements that should be at the front
        data.AddRange(partB); // Add the elements that should be at the back
    }
}
