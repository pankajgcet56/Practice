namespace DataStructureUdemy.BitOperator;

public class UniqueNumber3 : Problem
{
    public UniqueNumber3()
    {
        this.RunIndex = 9.0f;
    }
    public override void Run()
    {
        List<int> data = new List<int>() {1,1,1,2,2,2,5 };
        Console.WriteLine(this.UniqueNumber(data));
    }

    /// <summary>
    /// Input has 3n+1 Length
    /// 1 number is Unique, finding that No 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public int UniqueNumber(List<int> data)
    {
        int[] bts = new int[32];
        List<int> bitsCount = bts.ToList(); // Int is 32 Bits
        foreach (var val in data)
        {
            UpdateBitsCount(bitsCount,val);
        }

        for (int i = 0; i < 32; i++)
        {
            bitsCount[i]= bitsCount[i] % 3;
        }
        
        return NumberFromBits(bitsCount);
    }

    private int NumberFromBits(List<int> bitsData)
    {
        int num = 0;
        for (int i = 0; i < 32; i++)
        {
            num += (bitsData[i] * (1 << i));
        }
        return num;
    }

    private void UpdateBitsCount(List<int> bitsData, int val)
    {
        for (int i = 0; i < 32; i++)
        {
            int ith_bit = val & (1 << i);
            if (ith_bit != 0)
            {
                bitsData[i]++;
            }
        }
    }
}