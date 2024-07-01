namespace DataStructureUdemy.DynamicPrograming;

public class LadderProblem : Problem
{
    public LadderProblem()
    {
        this.RunIndex = 5.1f;
    }
    public override void Run()
    {
        Console.WriteLine(".....Pankaj Ladder");
        int n = 4;
        int k = 3;
        int[] DP = new int[n+1];
        Console.WriteLine("N = {0}, K = {1}, OP = {2}",n,k,TopDownApproach(n,k,ref DP));
    }

    public int BottomUpApproach(int n, int k)
    {
        int[] data = new int[n+1];
        data[0] = 1;
        for (int i = 1; i <=n; i++)
        {
            // Reverse K ladder Back
            for (int j = i-1; j >= i-k && j >= 0; --j)
            {
                data[i] += data[j];
            }
        }
        return data[n];
    }
    
    public int BottomUpApproachOpt(int n, int k)
    {
        int[] data = new int[n+1];
        data[0] = 1;
        // data[1] = 1;
        // data[2] = data[0] + data[1];
        int tmpSum = data[0];
        for (int i = 1; i <= k; i++)
        {
            data[i] = tmpSum;
            tmpSum += data[i];
        }
        
        for (int i = k+1; i <=n; i++)
        {
            data[i] = data[i - 1] * 2 - data[i - k-1];
        }
        return data[n];
    }

    public int TopDownApproach(int n,int k, ref int[] dp)
    {
        if (n == 0)
            return 1;
        if (n < 1)
            return 0;
        if (dp[n] != 0)
        {
            return dp[n];
        }
        int ans = 0;
        for (int i = 1; i <=k; i++)
        {
            ans += TopDownApproach(n - i,k,ref dp);
        }
        return ans;
    }
}