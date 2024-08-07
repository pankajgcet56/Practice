namespace DataStructureUdemy.Miscellaneous;

public class ValidParentheses : Problem
{
    //https://leetcode.com/problems/valid-parentheses/
    
    public ValidParentheses()
    {
        RunIndex = 9;
    }
    public override void Run()
    {
        Console.WriteLine(IsValid("()({{{{{[]()}}}}}})"));
    }
    private bool IsValid(string s)
    {
        Stack<char> dataSet = new Stack<char>();
        for (int i = 0; i < s.Length; i++)
        {
            if (dataSet.Count == 0 && (s[i] == ')' || s[i] == '}' || s[i] == ']'))
            {
                return false;
            }
            switch (s[i])
            {
                case '{':
                    dataSet.Push('{');
                    break;
                case '[':
                    dataSet.Push('[');
                    break;
                case '(':
                    dataSet.Push('(');
                    break;
                case '}':
                    var top1 = dataSet.Pop();
                    if (top1 != '{')
                        return false;
                    break;
                case ']':
                    var top2 = dataSet.Pop();
                    if (top2 != '[')
                        return false;
                    break;
                case ')':
                    var top3 = dataSet.Pop();
                    if (top3 != '(')
                        return false;
                    break;
            }
        }
        return dataSet.Count == 0;
    }
    
    // * Definition for singly-linked list.
     public class ListNode 
     {
         public int val;
         public ListNode next;
         public ListNode(int val=0, ListNode next=null) 
         {
             this.val = val;
             this.next = next; 
         }
    }
    
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        if (list1 == null && list2 == null)
            return null;
        if (list1 == null) return list2;
        if (list2 == null) return list1;
        bool merging = true;
        ListNode res = null;
        while (merging)
        {
            if (list1.val > list2.val)
            {
                if (res == null)
                {
                    res = list2;
                }
            }
        }
        return res;
    }
}