namespace DataStructureUdemy.RegularExpressionMatching_7;

public class RegularExpression : Problem
{
    public RegularExpression()
    {
        this.RunIndex = 7.0f;
    }
    public override void Run()
    {
        string str = "aaa";
        string ptr = "a*a";
        Console.WriteLine("Match = "+IsInRegExp(str,ptr));
    }

    private bool IsInRegExp(string str,string ptr)
    {
        if (string.IsNullOrEmpty(str) && string.IsNullOrEmpty(ptr)) return true;
        if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(ptr)) return false;
        
        // Check char Counts if ptr lenth (excluding stars) > str length
        int ptr_length = 0;
        for (int k = 0; k < ptr.Length; k++)
        {
            if(ptr[k] == '*') continue;
            ptr_length++;
        }

        if (ptr_length > str.Length)
        {
            return false;
        }
        
        bool match = true;
        int i = 0;
        int j = 0;

        while (match && i<str.Length && j<ptr.Length)
        {
            if (str[i] == ptr[j] || ptr[j] == '.')
            {
                i++; j++; continue;
            }

            if (ptr[j] == '*')
            {
                // Check Match to Previous
                if (j - 1 >= 0 && (ptr[j - 1] == str[i] || ptr[j - 1] == '.'))
                {
                    i++;continue;
                }
                // Check next Item
                if (j + 1 < ptr.Length && (ptr[j + 1] == str[i] || ptr[j + 1] == '.'))
                {
                    i++;
                    j += 2;
                    continue;
                }
            }
            // If Char doesn't Match, check with next char
            if (ptr[j] != str[i])
            {
                if (j + 1 < ptr.Length && ptr[j + 1] == '*')
                {
                    j++;
                    continue;
                }
            }
            
            return false;
        }

        if(j < ptr.Length && ptr[j] == '*')
            j++;
        if (i != str.Length || j != ptr.Length)
            return false;
        
        return true;
    }

    private bool RemainingCharReverseMatch(string str,string ptr)
    {
        if (string.IsNullOrEmpty(str) && string.IsNullOrEmpty(ptr)) return true;
        if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(ptr)) return false;
        
        int i = str.Length-1;
        int j = ptr.Length-1;

        while (j>=0)
        {
            if (ptr[j] == str[i])
            {
                j--;
                i--;
            }
        }
        
        return false;
    }
    
}