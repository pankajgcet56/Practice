using System.Text.RegularExpressions;

namespace DataStructureUdemy.LeetCode;

public class PlusOneProb : Problem
{
    public PlusOneProb()
    {
        RunIndex = 10.5f;
    }

    public override void Run()
    {
        // List<int> data = new List<int>() {1,9,9 };
        // int[] res = PlusOne(data.ToArray());
        // Console.WriteLine(res);
        // Console.WriteLine("Climb Stairs = "+ClimbStairs(40));
        // Console.WriteLine(IsPalindrome("A man, a plan, a canal -- Panama"));
        List<int> datSet = new List<int>() { 6,5,5};

        Console.WriteLine(MajorityElement(datSet.ToArray()));
    }

    public int[] PlusOne(int[] digits)
    {
        List<int> res = digits.ToList();
        int carry = 1;
        for (int i = res.Count-1; i >= 0; i--)
        {
            res[i] += carry;
            carry = res[i] / 10;
            res[i] %= 10;
            if(carry == 0)
                break;
        }

        if (carry != 0)
        {
            res.Insert(0,carry);
        }
        return res.ToArray();
    }
    
    public int MySqrt(int x)
    {
        return (int)Math.Sqrt(x);;
    }

    private Dictionary<int, int> dp_Data = new Dictionary<int, int>();
    public int ClimbStairs(int n)
    {
        if (n == 0) return 1;
        if (n < 0) return 0;
        if (dp_Data.ContainsKey(n))
            return dp_Data[n];
        int p1 = ClimbStairs(n - 1);
        if(!dp_Data.ContainsKey(n-1))
            dp_Data.Add(n-1,p1);
        int p2 = ClimbStairs(n - 2);
        if(!dp_Data.ContainsKey(n-2))
            dp_Data.Add(n-2,p2);
        return p1+p2;
    }
    // . Find the Index of the First Occurrence in a String
    public int StrStr(string haystack, string needle)
    {
        return -1;
    }
    //Valid Palindrome
    public bool IsPalindrome(string s)
    {
        s= s.Trim();
        
        Regex rgx = new Regex("[^a-zA-Z0-9]");
        s = rgx.Replace(s, "");
        s = s.Replace(" ","");
        
        s= s.ToLower();
        int l = s.Length;
        string s1, s2;
        if (l % 2 == 0)
        {
            s1 = s.Substring(0, l / 2);
            s2 = s.Substring(l / 2, l/2);
        }
        else
        {
            s1 = s.Substring(0, l / 2);
            s2 = s.Substring(1 + l / 2, l/2);
        }
        Console.WriteLine("S1="+s1+"  S2="+s2);
        for (int i = 0; i < l/2; i++)
        {
            if (s1[i] != s2[(l / 2) - i-1])
                return false;
        }
        
        return true;
    }
    //Majority Element
    
    public int MajorityElement(int[] nums)
    {
        //num : Count
        Dictionary<int, int> data = new Dictionary<int, int>();
        
        int max = 0;
        int maxIndex = -1;
        for (int i = 0; i < nums.Length; i++)
        {
            if (data.ContainsKey(nums[i]))
            {
                data[nums[i]]++;
            }
            else
            {
                data.Add(nums[i],1);
            }

            if (max < data[nums[i]])
            {
                max = data[nums[i]];
                maxIndex = nums[i];
            }
        }
        return maxIndex;
    }
}