using System.Collections;

public static class Recursion
{
    /// <summary>
    /// Problem 1: Recursive Squares Sum
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case
        if (n <= 0)
            return 0;

        // Recursive case: n² + sum of squares of (n-1)
        return n * n + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// Problem 2: Permutations Choose
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: if word length equals size, add to results
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Recursive case: try each remaining letter
        for (int i = 0; i < letters.Length; i++)
        {
            // Create new word with current letter
            string newWord = word + letters[i];

            // Create new letters string without the used letter
            string remainingLetters = letters.Remove(i, 1);

            // Recursive call
            PermutationsChoose(results, remainingLetters, size, newWord);
        }
    }

    /// <summary>
    /// Problem 3: Climbing Stairs (with Memoization)
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Initialize memoization dictionary if not provided
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        // Check if we already computed this value
        if (remember.ContainsKey(s))
            return remember[s];

        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Recursive case with memoization
        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        // Store result in memoization dictionary
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// Problem 4: Wildcard Binary Patterns
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Find the first wildcard
        int wildcardIndex = pattern.IndexOf('*');

        // Base case: no more wildcards
        if (wildcardIndex == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace '*' with '0' and continue recursively
        string patternWithZero = pattern.Substring(0, wildcardIndex) + '0' +
                                pattern.Substring(wildcardIndex + 1);
        WildcardBinary(patternWithZero, results);

        // Replace '*' with '1' and continue recursively
        string patternWithOne = pattern.Substring(0, wildcardIndex) + '1' +
                               pattern.Substring(wildcardIndex + 1);
        WildcardBinary(patternWithOne, results);
    }

    /// <summary>
    /// Problem 5: Maze
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // Initialize current path if null
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Add current position to path
        currPath.Add((x, y));

        // Check if we reached the end
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1); // Backtrack
            return;
        }

        // Try moving right
        if (maze.IsValidMove(currPath, x + 1, y))
        {
            SolveMaze(results, maze, x + 1, y, currPath);
        }

        // Try moving left
        if (maze.IsValidMove(currPath, x - 1, y))
        {
            SolveMaze(results, maze, x - 1, y, currPath);
        }

        // Try moving down
        if (maze.IsValidMove(currPath, x, y + 1))
        {
            SolveMaze(results, maze, x, y + 1, currPath);
        }

        // Try moving up
        if (maze.IsValidMove(currPath, x, y - 1))
        {
            SolveMaze(results, maze, x, y - 1, currPath);
        }

        // Backtrack: remove current position from path
        currPath.RemoveAt(currPath.Count - 1);
    }
}