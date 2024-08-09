namespace DataStructureUdemy.LeetCode;

public class JumpGame : Problem
{
    public JumpGame()
    {
        RunIndex = 10.1f;
    }
    public override void Run()
    {
         int[] nums = new int[]{2,0,6,9,8,4,5,0,8,9,1,2,9,6,8,8,0,6,3,1,2,2,1,2,6,5,3,1,2,2,6,4,2,4,3,0,0,0,3,8,2,4,0,1,2,0,1,4,6,5,8,0,7,9,3,4,6,6,5,8,9,3,4,3,7,0,4,9,0,9,8,4,3,0,7,7,1,9,1,9,4,9,0,1,9,5,7,7,1,5,8,2,8,2,6,8,2,2,7,5,1,7,9,6};
        //int[] nums = new int[]{2,0,6,3,5,6,0,0,0,0,0,2,0,6,3,5,6,0,0,0,0,0,2,0,6,3,5,6,0,0,0,0,0,2,0,6,3,5,6,0,0,0,0,0,2,0,6,3,5,7,0,0,0,0,0};
        Console.WriteLine(CanJump(nums));
        Console.WriteLine();
    }
    private Dictionary<int, bool> dp_data = new Dictionary<int, bool>();
    public bool CanJump(int[] nums)
    {
        dp_data = new Dictionary<int, bool>();
        return CanJumpToLast(nums, 0);
    }

    private bool CanJumpToLast(int[] nums,int currentIndex)
    {
        if (dp_data.ContainsKey(currentIndex))
        {
            Console.WriteLine("Found In DP, Index="+currentIndex+" : "+dp_data[currentIndex]);
            return dp_data[currentIndex];
        }
        bool canReachLast = currentIndex == nums.Length - 1;
        
        for (int i = 1; i <= nums[currentIndex]; i++)
        {
            if (canReachLast)
                break;
            canReachLast = canReachLast || CanJumpToLast(nums, currentIndex + i);
        }
        dp_data.TryAdd(currentIndex, canReachLast);
        Console.WriteLine("Can Reach Last Index="+currentIndex+": "+canReachLast);
        return canReachLast;
    }
}