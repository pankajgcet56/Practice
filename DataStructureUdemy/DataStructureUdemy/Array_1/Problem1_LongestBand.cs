namespace DataStructureUdemy.Array;

public class Problem1_LongestBand : Problem
{
    public Problem1_LongestBand()
    {
        // this.RunIndex = float.MaxValue;
    }
    private int[] Data = new[] { 1, 9, 3, 0, 18, 5, 2, 4, 10, 7, 12, 6, 8, 11 };
    public override void Run()
    {
        Console.WriteLine("--------->>");
        Console.WriteLine(GetLongestBand(Data));
    }

    private int GetLongestBand(int[] data)
    {
        //Step1 : Insert to HashSet  :: O(N)
        HashSet<int> hashData = new HashSet<int>();
        foreach (var val in data)
        {
            hashData.Add(val);
        }
        //Step2 : Iteration for Maxband
        int maxBandLength = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if(hashData.Contains(data[i]-1))
                continue;
            else
            {
                int tempMax = 0;
                int j = 0;
                while (hashData.Contains(data[i]+j))
                {
                    j++;
                    tempMax++;
                }

                if (tempMax > maxBandLength)
                    maxBandLength = tempMax;
            }
        }
        
        return maxBandLength;
    }
}