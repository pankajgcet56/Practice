namespace DataStructureUdemy.LeetCode;

public class RomanToInteger : Problem
{
    public RomanToInteger()
    {
        RunIndex =10.4f;
    }
    public override void Run()
    {
        Console.WriteLine(RomanToInt("VIII"));
    }
    public int RomanToInt(string s)
    {
        Dictionary<char, int> symbles = new Dictionary<char, int>();
        symbles.Add('I',1);
        symbles.Add('V',5);
        symbles.Add('X',10);
        symbles.Add('L',50);
        symbles.Add('C',100);
        symbles.Add('D',500);
        symbles.Add('M',1000);
        int val = 0;

        if(s.Contains("CM"))
        {
            val += 900;
            s= s.Replace("CM", "");
        }
        if (s.Contains("CD"))
        {
            val += 400;
            s= s.Replace("CD", "");
        }
        if(s.Contains("XL"))
        {
            val += 40;
            s= s.Replace("XL", "");
        }
        if(s.Contains("XC"))
        {
            val += 90;
            s= s.Replace("XC", "");
        }
        if(s.Contains("IV"))
        {
            val += 4;
            s= s.Replace("IV", "");
        }
        if(s.Contains("IX"))
        {
            val += 9;
            s= s.Replace("IX", "");
        }
        
        foreach (var ch in s)
        {
            val+= symbles[ch];
        }
        return val;
    }
    
    /*
    Symbol       Value
    I             1
    V             5
    X             10
    L             50
    C             100
    D             500
    M             1000
    */
}