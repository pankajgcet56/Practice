namespace DataStructureUdemy.LeetCode.DynamicPrograming2D_11;

public class LongestCommonSubsequence : Problem
{
    public LongestCommonSubsequence()
    {
        RunIndex = 11.0f;
    }

    public override void Run()
    {
        string s1 = "abc";
        string s2 = "abefghc";
        int[][] dp = new int[s1.Length][];
        for (int i = 0; i < s1.Length; i++)
        {
            dp[i] = new int[s2.Length];
            for (int j = 0; j < s2.Length; j++)
            {
                dp[i][j] = -1;
            }
        }
        Console.WriteLine(LCS(s1,s2,0,0,dp));
        for (int i = 0; i < s1.Length; i++)
        {
            for (int j = 0; j < s2.Length; j++)
            {
                Console.Write(dp[i][j]+" ");
            }
            Console.WriteLine();
        }
    }
    /*
     * s1 = ABCD
     * s2 = ABEDG
     *
     * IF s1[i] == s2[j]
     * f(i,j) = 1+f(i+1,j+1)
     *
     * else
     * Math.Max (f(i+1,j),f(i,j+1))
     */

    private int LCS(string s1,string s2,int i,int j,int[][] dp)
    {
        if (i == s1.Length || j == s2.Length) return 0;
        if (dp[i][j] != -1) return dp[i][j];
        if (s1[i] == s2[j]) return dp[i][j] = 1 + LCS(s1, s2, i+1, j+1,dp);
        return dp[i][j] = Math.Max(LCS(s1, s2, i + 1, j,dp), LCS(s1, s2, i, j+1,dp));
    }
    
}