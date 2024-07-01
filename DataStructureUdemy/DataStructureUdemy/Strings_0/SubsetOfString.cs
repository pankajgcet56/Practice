namespace DataStructureUdemy.Strings;

public class SubsetOfString : Problem
{
    public SubsetOfString()
    {
        // this.RunIndex = float.MaxValue;
    }

    public override void Run()
    {
        Console.WriteLine("Subset Of String");
        string str = "abcdefghijklmnop";
        List<string> allSubset = new List<string>();
        SubsetStr(str,"",ref allSubset);
        // allSubset.Sort();
        // allSubset.Add("NULL");
        Console.WriteLine(string.Join(',',allSubset)+" Count = "+allSubset.Count);
    }

    private void SubsetStr(string str,string op,ref List<string> allSub)
    {
        if (str.Length == 0)
        {
            allSub.Add(op);
            // Console.WriteLine(op);
            return;
        }
        SubsetStr(str.Substring(1),op+str[0],ref allSub);
        SubsetStr(str.Substring(1),op,ref allSub);
    }
    
}