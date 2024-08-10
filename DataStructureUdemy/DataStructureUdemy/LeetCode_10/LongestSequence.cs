namespace DataStructureUdemy.LeetCode;

public class LongestSequence : Problem
{
    public LongestSequence()
    {
        RunIndex = 10.3f;
    }

    public override void Run()
    {
        string s = "dvdf";
        Console.WriteLine(LengthOfLongestSubstring(s));
    }
    public int LengthOfLongestSubstring(string s)
    {
        // Create Window
        int maxWindowLength = 0;
        HashSet<char> compareSet = new HashSet<char>();
        List<char> window = new List<char>();
        
        for (int i = 0; i < s.Length; i++)
        {
            char ch = s[i];
            if(compareSet.Contains(ch))
            {
                // Rset Window
                if (maxWindowLength < window.Count)
                {
                    maxWindowLength = window.Count;
                }
                int indexOfMatch = window.IndexOf(ch);
                // Sift Window 
                int newStartIndex = i - (window.Count - indexOfMatch - 1);
                Console.Write("New Start Index = "+newStartIndex+", Curent Window = "+new string(window.ToArray())+", i = "+i);
                i = Math.Clamp(newStartIndex, 0, s.Length);

                List<char> charsToRemoveFromWindow = window.Slice(0, indexOfMatch + 1);
                foreach (var c in charsToRemoveFromWindow)
                {
                    compareSet.Remove(c);
                    window.Remove(c);
                }
                window.Add(ch);
                compareSet.Add(ch);
                Console.WriteLine(":: After Reset Window = "+new string(window.ToArray()));
            }
            else
            {
                compareSet.Add(ch);
                window.Add(ch);
            }
        }
        if (maxWindowLength < window.Count)
        {
            maxWindowLength = window.Count;
        }
        return maxWindowLength;
    }
}