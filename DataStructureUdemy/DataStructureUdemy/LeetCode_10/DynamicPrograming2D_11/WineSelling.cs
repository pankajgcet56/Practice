namespace DataStructureUdemy.LeetCode.DynamicPrograming2D_11;

public class WineSelling : Problem
{
    public WineSelling()
    {
        RunIndex = 11.2f;
    }

    public override void Run()
    {
        List<int> prices = new List<int>() { 2, 3,5,1,4 };
        int[][] dp = new int[prices.Count][];
        for (int i = 0; i < prices.Count; i++)
        {
            dp[i] = new int[prices.Count];
            for (int j = 0; j < prices.Count; j++)
            {
                dp[i][j] = -1;
            }
        }
        
        Console.WriteLine(Wines(prices.ToArray(),0,prices.Count-1,1,dp));
        
        for (int i = 0; i < prices.Count; i++)
        {
            for (int j = 0; j < prices.Count; j++)
            {
                Console.Write(dp[i][j]+" ");
            }
            Console.WriteLine();
        }
    }

    private int Wines(int[] prices, int l, int r,int y,int[][] dp)
    {
        if (l > r) return 0;
        if (dp[l][r] != -1) return dp[l][r];
        int leftP = prices[l] * y + Wines(prices, l + 1, r, y + 1, dp);
        int rightP = prices[r] * y + Wines(prices, l, r - 1, y + 1, dp);
        // Console.WriteLine("L = "+leftP+", R = "+rightP+" :: "+l+","+r);
        return dp[l][r]=Math.Max(leftP,rightP);
    }
    
}