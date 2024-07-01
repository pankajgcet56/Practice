namespace DataStructureUdemy.Strings;

public class Problem2_SortedSubSequence : Problem
{
    public override void Run()
    {
        List<string> data = new List<string>();
        SubSequence(InputStr,"",ref data);
        // Console.WriteLine(string.Concat(data,","));
        // data.Sort((str1,str2) =>
        // {
        //     if (str1 == str2) return str2.CompareTo(str1);
        //     return (str1.Length < str2.Length)?1:0;
        // });
        data.Sort();
        foreach (var str in data)
        {
            Console.WriteLine(str);
        }
    }

    private string InputStr = "abc";

    private void SubSequence(string inputStr, string opStr,ref List<string> values)
    {
        // Console.WriteLine("I = {0}, O =  {1}, values = {2}",inputStr,opStr,string.Concat(values.ToArray(),','));
        if (string.IsNullOrEmpty(inputStr))
        {
            // Console.WriteLine(opStr);
            values.Add(opStr);
            return;
        }

        char choped = inputStr[0];
        string reducedInput = inputStr.Substring(1);
        SubSequence(reducedInput,opStr,ref values);
        SubSequence(reducedInput,opStr+choped,ref values);
    }
}