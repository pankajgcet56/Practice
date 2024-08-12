namespace DataStructureUdemy.LeetCode;

public class ReverseLinkedList : Problem
{
    public ReverseLinkedList()
    {
        this.RunIndex = 10.6f;
    }

    private int runCount = 0;
    public override void Run()
    {
        List<int> coins = new List<int>() { 357,239,73,52 };
        
        int ammount = 9832;
        long ticksNow = DateTime.Now.Ticks;
        Console.WriteLine(CoinChange(coins.ToArray(),ammount));
        Console.WriteLine(new TimeSpan(DateTime.Now.Ticks-ticksNow).ToString());
        Console.WriteLine("Run Count = "+runCount);
    }

    // key : Target, Value: Min Count Req for target
    private Dictionary<int, int> _dpData = new Dictionary<int, int>();
    public int CoinChange(int[] coins, int amount)
    {
        runCount++;
        Console.WriteLine(amount);
        if (amount == 0)
            return 0;
        if (amount < 0)
            return Int32.MinValue;
        if (_dpData.TryGetValue(amount, out var change))
            return change;
        List<int> minValues = new List<int>();
        foreach (var c in coins)
        {
            minValues.Add(1+CoinChange(coins,amount-c));
        }
        minValues = minValues.Where(x => x > 0).ToList();
        int minValue = -1;
        // Console.WriteLine(" :"+minValues.Count);
        if(minValues.Count>0)
            minValue = minValues.AsQueryable().Min();
        _dpData.TryAdd(amount, minValue);
        return minValue;
    }
    
    // Definition for singly-linked list.
    public bool ContainsDuplicate(int[] nums)
    {
        HashSet<int> dupSet = new HashSet<int>();
        foreach (var v in nums)
        {
            if (!dupSet.Add(v))
                return true;
        }

        return false;
    }
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
        public ListNode ReverseList(ListNode head)
        {
            ListNode node = head;
            ListNode reverseListHead = null;
            while (node != null)
            {
                ListNode tmp = node;
                node = node.next;
                tmp.next = reverseListHead;
                reverseListHead = tmp;
            }
            return reverseListHead;
        }
    }
}