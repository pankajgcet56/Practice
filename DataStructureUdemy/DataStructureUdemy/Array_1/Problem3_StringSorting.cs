using System.Text.RegularExpressions;

namespace DataStructureUdemy.Array;

public class Problem3_StringSorting : Problem
{
    private List<string> Data = new List<string>() {"hologram_skin_1", "hologram_skin_2","hologram_skin_3","hologram_skin_4","hologram_skin_5","hologram_skin_6","hologram_skin_7","hologram_skin_8","hologram_skin_9","hologram_skin_10"
        ,"hologram_skin_11", "hologram_skin_12","hologram_skin_13","hologram_skin_14","hologram_skin_15","hologram_skin_16","hologram_skin_17","hologram_skin_18","hologram_skin_19","hologram_skin_20"
        ,"hologram_skin_21", "hologram_skin_22","hologram_skin_23","hologram_skin_24","hologram_skin_25","hologram_skin_26","hologram_skin_27","hologram_skin_28","hologram_skin_29","hologram_skin_30"
        ,"hologram_skin_121", "hologram_skin_212","hologram_skin_2332","hologram_skin_24","hologram_skin_100","hologram_skin_200","hologram_skin_207","hologram_skin_0218","hologram_skin_129","hologram_skin_300"
    };
    public Problem3_StringSorting()
    {
        this.RunIndex = 1.3f;
    }
    public override void Run()
    {
        Console.WriteLine("--------- String Sort");
        Data.Sort((x, y) =>
        {
            // if (x.Length > y.Length)
            //     return 1;
            if (x.Contains("hologram_skin_") && y.Contains("hologram_skin_"))
            {
                string num1 = x.Substring(14);
                string num2 = y.Substring(14);
                int n1,n2 = 0;
                int.TryParse(num1, out n1);
                int.TryParse(num2, out n2);
                int result = 0;
                
                if(n1==n2) result = 0;
                else if (n1 > n2) result = 1;
                else result = -1;
                // Console.WriteLine("Compare X = {0} & Y = {1}, Result = {2} :: Num1 = {3}, Num2 = {4}",x,y,result,num1,num2);
                return result;

            }
            return string.Compare(x, y);
            
        });
        Console.WriteLine(string.Join(",",Data));
    }
}