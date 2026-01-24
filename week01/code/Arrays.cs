using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
        // PLAN FOR MultiplesOf:
        // 1. Create a new array of doubles with the specified length parameter
        // 2. Use a for loop to iterate from 0 to length-1
        // 3. For each iteration i, calculate the multiple as: number * (i + 1)
        //    - i+1 is used because we want the first multiple (index 0) to be number * 1 = number itself
        //    - For example: if number=7 and i=0 → 7*(0+1)=7 (first multiple)
        //    - if number=7 and i=1 → 7*(1+1)=14 (second multiple)
        // 4. Store each calculated value in the array at index i
        // 5. Return the completed array containing all multiples
        
        // Step 1: Create array with the specified length
        double[] result = new double[length];
        
        // Steps 2-4: Calculate and store each multiple
        for (int i = 0; i < length; i++)
        {
            // Calculate the (i+1)th multiple
            result[i] = number * (i + 1);
        }
        
        // Step 5: Return the completed array
        return result;
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
        
        // PLAN FOR RotateListRight:
        // 1. Check for edge cases: if data is null, empty, or has only 1 element, no rotation needed
        // 2. Since the problem states amount is between 1 and data.Count inclusive, we don't need modulo
        //    but we'll handle the case where amount equals data.Count (full rotation = no change)
        // 3. Approach using List methods (GetRange, RemoveRange, InsertRange):
        //    a. Extract the last 'amount' elements using GetRange
        //    b. Remove those last 'amount' elements from the list using RemoveRange
        //    c. Insert the extracted elements at the beginning using InsertRange
        // 4. Alternative approach: Create a new list by concatenating two slices
        
        // Step 1: Handle edge cases
        if (data == null || data.Count <= 1)
        {
            return; // Nothing to rotate
        }
        
        // Step 2: Check if rotation would result in same list
        if (amount == data.Count)
        {
            return; // Full rotation = no change
        }
        
        // Step 3: Implement rotation using three-step approach
        
        // 3a: Get the last 'amount' elements that need to be moved to the front
        // GetRange parameters: starting index, number of elements
        // Starting index = total elements - amount
        // Example: [1,2,3,4,5,6,7,8,9] with amount=3
        // Starting index = 9 - 3 = 6 → elements at indices 6,7,8 = [7,8,9]
        List<int> elementsToMove = data.GetRange(data.Count - amount, amount);
        
        // 3b: Remove those elements from the end of the list
        // RemoveRange parameters: starting index, number of elements
        // Same indices as above
        data.RemoveRange(data.Count - amount, amount);
        
        // 3c: Insert the moved elements at the beginning of the list
        // InsertRange parameters: index to insert at, collection to insert
        // Insert at index 0 (beginning)
        data.InsertRange(0, elementsToMove);
        
        // ALTERNATIVE APPROACH (commented out):
        // This creates a new list by taking the last part then the first part
        /*
        // Calculate the split point
        int splitPoint = data.Count - amount;
        
        // Create a new list with the rotated order
        List<int> rotated = new List<int>();
        rotated.AddRange(data.GetRange(splitPoint, amount)); // Last part
        rotated.AddRange(data.GetRange(0, splitPoint));      // First part
        
        // Clear original and add rotated elements
        data.Clear();
        data.AddRange(rotated);
        */
    }
}