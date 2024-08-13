using System.Runtime.Intrinsics.Arm;

namespace DataStructureUdemy.LeetCode.DynamicPrograming2D_11;

public class KnapsackProblem : Problem
{
    public KnapsackProblem()
    {
        RunIndex = 11.3f;
    }

    private int counter = 0;
    public override void Run()
    {
        int W = 11;
        int length = 4;
        List<int> wts = new List<int>() {2, 7, 3, 4};
        List<int> prices = new List<int>() {5, 20, 20, 10};
        counter = 0;
        int[][] dp = new int[length][];
        for (int i = 0; i < length; i++)
        {
            dp[i] = new int[W];
            for (int j = 0; j < W; j++)
            {
                dp[i][j] = -1;
            }
        }
        Console.WriteLine(MaxPrice(wts,prices,W,length, dp));
        Console.WriteLine("Counter = "+counter);
    }

    // Top Down Case
    private int MaxPrice(List<int> wts, List<int> prices, int w, int n, int[][] dp)
    {
        if (n <= 0 || w <= 0)
            return 0;
        if (dp[n - 1][w - 1] != -1)
            return dp[n - 1][w - 1];
        counter++;
        int price1 = w-wts[n-1]<0?0: prices[n-1]+MaxPrice(wts, prices, w-wts[n-1], n - 1,dp);
        int price2 = MaxPrice(wts, prices, w, n - 1, dp);
        Console.WriteLine("W = "+w+", N = "+n+", Res = "+Math.Max(price1,price2));
        
        return dp[n-1][w-1] = Math.Max(price1,price2);
    }
}