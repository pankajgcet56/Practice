
namespace DataStructureUdemy.DynamicPrograming;

public class CoinChangeProblem : Problem
{
    public CoinChangeProblem()
    {
        this.RunIndex = 5.2f;
    }

    public override void Run()
    {
        Console.WriteLine("Int Max = {0}, Int Min = {1}",int.MaxValue,int.MinValue);
        int[] coins = new[] { 1, 3, 7, 10 };
        int M = 2;
        int[] DP = new int[M + 1]; 
        Console.WriteLine("Coin Change Problem : " + CoinChange(coins,M,ref DP));
    }

    public int CoinChange(int[] coins, int M,ref int[] dp)
    {
        if (M <= 0)
            return 0;
        int currentMin = int.MaxValue;

        // if (dp[M] != 0)
        //     return dp[M];
        // Console.WriteLine("Call For  :: Target : "+M);
        for (int i = 0; i < coins.Length; i++)
        {
            if (M - coins[i] > 0)
            {
                currentMin = Math.Min(currentMin, CoinChange(coins,M - coins[i],ref dp));
                Console.WriteLine("Intermediate min = "+(currentMin) + " :: Target : "+M);
            }
        }
        Console.WriteLine("Current min = "+(currentMin+1) + " :: Target : "+M);
        dp[M] = currentMin + 1;
        return dp[M];
    }
}