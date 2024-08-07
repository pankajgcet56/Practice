namespace DataStructureUdemy.Array;

public class MultipleSumProblems : Problem
{
    public MultipleSumProblems()
    {
        this.RunIndex = 1.5f;
    }
    public override void Run()
    {
        int[] dataSet = new int[]{3,2,4};
        int target = 6;
        Console.WriteLine(string.Join(",",TwoSum(dataSet,target)));
    }

    public int[] TwoSum(int[] nums, int target) 
    {
        var res = TwoSumProblem(nums.ToList(),target);
        int[] result = new int[2];
        result[0] = res.Item1;
        result[1] = res.Item2;
        return result;
    }
    public Tuple<int,int> TwoSumProblem(List<int> data,int sumTargert) // return 2 Index for Targeting Sums 
    {
        Tuple<int, int> indexes = new Tuple<int, int>(-1,-1);
        HashSet<int> hashSet = new HashSet<int>();
        for (int i = 0; i < data.Count; i++)
        {
            hashSet.Add(data[i]);
        }

        for (int i = 0; i < data.Count; i++)
        {
            if (hashSet.Contains(sumTargert - data[i]) && i != data.IndexOf(sumTargert-data[i]))
            {
                // Found Data
                indexes = new Tuple<int, int>(i, data.IndexOf(sumTargert-data[i]));
                break;
            }
        }
        return indexes;
    }
}