namespace DataStructureUdemy.DynamicPrograming;

public class CountingBST : Problem
{
    public CountingBST()
    {
        this.RunIndex = 5.3f;
    }
    public override void Run()
    {
        int N = 8;
        int[] dpArray = new int[N+1];
        dpArray[0] = 1;
        dpArray[1] = 1;
        Console.WriteLine("1.Counting BST For Nodes {0} :: {1} ",N,CountBST(N,ref dpArray));
        Console.WriteLine("2.Counting BST For Nodes {0} :: {1} ",N,CountBstBottomUp(N));
    }

    public int CountBST(int N, ref int[] DP)
    {
        if (N == 0 || N == 1)
            return 1;
        if (DP[N] != 0)
        {
            // Console.WriteLine("DP Found : DP[{0}] = {1}",N,DP[N]);
            return DP[N];   
        }
        int ans = 0;
        for (int i = 1; i <= N; i++)
        {
            int X = CountBST(i - 1,ref DP);
            int Y = CountBST(N - i,ref DP);
            ans += X * Y;
        }
        DP[N] = ans;
        return ans;
    }

    public int CountBstBottomUp(int N)
    {
        int[] dp = new int[N+1];
        dp[0] = 1;
        dp[1] = 1;
        for (int i = 2; i <= N; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                dp[i] += dp[j - 1] * dp[i - j];
            }
        }
        return dp[N];
    }
}