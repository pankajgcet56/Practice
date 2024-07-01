namespace DataStructureUdemy.Sorting;

public class SmallestString : Problem
{
    public SmallestString()
    {
        this.RunIndex = 2.2f;
    }

    public override void Run()
    {
        Console.WriteLine("SmallestString problem");
        List<string> data = new List<string>() { "a", "ab", "aba" };
        Console.WriteLine(string.Join(",",data));
        data.Sort((s1,s2)=>(s1+s2).CompareTo(s2+s1));
        Console.WriteLine(string.Join("",data));
    }
}