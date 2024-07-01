namespace DataStructureUdemy.DynamicPrograming;

public class LongestCommonSubSequence : Problem
{
    public LongestCommonSubSequence()
    {
        this.RunIndex = 5.4f;
    }

    public override void Run()
    {
        Console.WriteLine("LongestCommonSubSequence .... Dynamic Programming");
        string s1 = "ABCD";
        string s2 = "ABEDG";
        int n1 = s1.Length;
        int n2 = s2.Length;
        int[,] dp = new int[n1, n2];
        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                dp[i, j] = -1;
            }
        }
        
        Console.WriteLine(LCS(s1,s2,0,0,ref dp));

        for (int i = 0; i < n1; i++)
        {
            for (int j = 0; j < n2; j++)
            {
                Console.Write(dp[i,j]+", ");
            }
            Console.WriteLine();
        }
    }

    public int LCS(string s1, string s2, int i, int j, ref int [,] dp)
    {
        if (i == s1.Length || j == s2.Length)
            return 0;
        if (dp[i, j] != -1)
            return dp[i, j];
        if (s1[i] == s2[j])
            return dp[i,j] = 1 + LCS(s1, s2, i + 1, j + 1,ref dp);
        int opt1 = LCS(s1, s2, i + 1, j,ref dp);
        int opt2 = LCS(s1, s2, i , j+1,ref dp);
        return dp[i,j] = Math.Max(opt1, opt2);
    }
}