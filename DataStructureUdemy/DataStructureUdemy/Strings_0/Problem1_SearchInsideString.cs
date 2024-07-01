using System.Text.RegularExpressions;

namespace DataStructureUdemy.Strings;

public class Problem1_SearchInsideString : Problem
{
    string bigString = "I liked the movie, acting in movie was  great!";
    private string smallString = "movie";
    public override void Run()
    {
        return;
        List<int> val = StringSearch(bigString, smallString);
        foreach (var v in val)
        {
            Console.WriteLine(v);
        }
        Console.WriteLine(ReplaceSpace(bigString));

        
        foreach (var str  in bigString.Split(' '))
        {
            Console.WriteLine(str);
        }
    }

    private List<int> StringSearch(string big, string small)
    {
        if (string.IsNullOrEmpty(big) || string.IsNullOrEmpty(small))
            return null;
        List<int> result = new List<int>();

        for (int i = 0; i < big.Length; i++)
        {
            if (big[i] == small[0])
            {
                bool mach = true;
                for (int j = 1; j < small.Length && i+j<big.Length; j++)
                {
                    if (small[j] != big[j + i])
                    {
                        mach = false;
                        break;
                    }
                }
                if(mach)
                    result.Add(i);
            }
        }
        return result;
    }

    private string ReplaceSpace(string str)
    {
        return str.Replace(" ", "%20");
    }
    
    private void Regex()
    {
        // Define a regular expression for repeated words.
        Regex rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Define a test string.
        string text = "The the quick brown fox  fox jumps over the lazy dog dog.";

        // Find matches.
        MatchCollection matches = rx.Matches(text);

        // Report the number of matches found.
        Console.WriteLine("{0} matches found in:\n   {1}",
            matches.Count,
            text);

        // Report on each match.
        foreach (Match match in matches)
        {
            GroupCollection groups = match.Groups;
            Console.WriteLine("'{0}' repeated at positions {1} and {2}",
                groups["word"].Value,
                groups[0].Index,
                groups[1].Index);
        }
    }
}