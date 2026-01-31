using System.Text.Json;
using System.Diagnostics;

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
        // Problem 1 - Implementation
        var result = new List<string>();
        var wordSet = new HashSet<string>(words);
        var addedPairs = new HashSet<string>(); // Track pairs already added

        foreach (var word in words)
        {
            // Skip if letters are the same
            if (word[0] == word[1]) continue;

            // Create reversed word
            var reversed = new string(new char[] { word[1], word[0] });

            // Check if reversed exists in set and we haven't added this pair yet
            if (wordSet.Contains(reversed))
            {
                // Create pair string
                string pair = $"{word} & {reversed}";
                string reversePair = $"{reversed} & {word}";

                // Only add if neither version of the pair has been added
                if (!addedPairs.Contains(pair) && !addedPairs.Contains(reversePair))
                {
                    result.Add(pair);
                    addedPairs.Add(pair);
                    addedPairs.Add(reversePair);
                }
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

        try
        {
            foreach (var line in File.ReadLines(filename))
            {
                // Try comma delimiter first (based on the test file likely format)
                var fields = line.Split(',');

                if (fields.Length < 4)
                {
                    // Try pipe delimiter if comma doesn't work
                    fields = line.Split('|');
                }

                if (fields.Length >= 4) // Make sure we have at least 4 columns
                {
                    var degree = fields[3].Trim();

                    // Handle empty degrees
                    if (string.IsNullOrEmpty(degree))
                    {
                        degree = "No degree";
                    }

                    // Update count in dictionary
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
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reading file: {ex.Message}");
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
        // Remove spaces and convert to lowercase
        string cleanWord1 = word1.Replace(" ", "").ToLower();
        string cleanWord2 = word2.Replace(" ", "").ToLower();

        // If lengths are different, they can't be anagrams
        if (cleanWord1.Length != cleanWord2.Length)
        {
            return false;
        }

        // Count letter frequencies in first word
        var charCount = new Dictionary<char, int>();
        foreach (char c in cleanWord1)
        {
            if (charCount.ContainsKey(c))
            {
                charCount[c]++;
            }
            else
            {
                charCount[c] = 1;
            }
        }

        // Check second word against the counts
        foreach (char c in cleanWord2)
        {
            if (!charCount.ContainsKey(c))
            {
                return false;
            }

            charCount[c]--;

            if (charCount[c] < 0)
            {
                return false;
            }
        }

        // Verify all counts are zero (should be, given length check)
        foreach (var count in charCount.Values)
        {
            if (count != 0)
            {
                return false;
            }
        }

        return true;
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

        try
        {
            using var client = new HttpClient();
            using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
            using var reader = new StreamReader(jsonStream);
            var json = reader.ReadToEnd();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

            if (featureCollection?.Features == null)
            {
                Debug.WriteLine("No earthquake data found.");
                return Array.Empty<string>();
            }

            var results = new List<string>();

            foreach (var feature in featureCollection.Features)
            {
                if (feature.Properties?.Place != null && feature.Properties.Mag != 0)
                {
                    string description = $"{feature.Properties.Place} - Mag {feature.Properties.Mag:F2}";
                    results.Add(description);
                }
            }

            return results.ToArray();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching earthquake data: {ex.Message}");
            return Array.Empty<string>();
        }
    }
}
// REMOVE THE DUPLICATE CLASS DEFINITIONS FROM HERE - They should be in FeatureCollection.cs