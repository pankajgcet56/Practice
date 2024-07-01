namespace DataStructureUdemy.Sorting;

public class MergeSortProblem : Problem
{
    public MergeSortProblem()
    {
        this.RunIndex = 2.1f;
    }
    public override void Run()
    {
        Console.WriteLine("Merge Sort");
        int[] data = new[] { 10, 5, 2, 0, 7, 6, 4,-1,-10,100,201,32,1 };
        for (int i = 0; i < data.Length; i++)
        {
            Console.Write(data[i]+", ");
        }
        Console.WriteLine();
        MergeSort(ref data,0,data.Length-1);
        for (int i = 0; i < data.Length; i++)
        {
            Console.Write(data[i]+", ");
        }
        Console.WriteLine();
    }

    public void MergeSort(ref int[] data,int startIndex, int endIndex)
    {
        string tstVal = "";
        for (int i = startIndex; i <= endIndex; i++)
        {
            tstVal += data[i] + ", ";
        }
        // Console.WriteLine("S:{0} E:{1} :: Array = {2}",startIndex,endIndex,tstVal);
        
        if(startIndex >= endIndex)
            return;
        int mid = (startIndex + endIndex) / 2;
        MergeSort(ref data,startIndex,mid);
        MergeSort(ref data,mid+1,endIndex);
        Merge(ref data, startIndex, endIndex);
    }

    private void Merge(ref int[] data, int startIndex, int endIndex)
    {
        string tstVal = "";
        for (int ii = startIndex; ii <= endIndex; ii++)
        {
            tstVal += data[ii] + ", ";
        }
        // Console.WriteLine("S:{0} E:{1} :: Array = {2}",startIndex,endIndex,tstVal);
        
        // Merge is done between of 2 arrays : 1. startIndex, MidIndex & 2. MidIndex+1, endIndex
        int midIndex = (startIndex + endIndex) / 2;
        List<int> tempArray = new List<int>();
        int i = startIndex;
        int j = midIndex+1;
        
        
        while (i <= midIndex && j<= endIndex)
        {
            // Console.WriteLine("I = {0}, Mid = {1}, J = {2}",i,midIndex,j);
            if (data[i] < data[j])
            {
                tempArray.Add(data[i]);
                ++i;
            }
            else
            {
                tempArray.Add(data[j]);
                ++j;
            }
        }
        
        // Copy remaining Element to tmp Array
        while (i<=midIndex)
        {
            tempArray.Add(data[i++]);
        }
        while (j<=endIndex)
        {
            tempArray.Add(data[j++]);
        }
        
        // Copy Back All Array to original Array
        for (int k = startIndex; k <= endIndex; k++)
        {
            int XX = k - startIndex;
            // Console.Write("tmpArray[{0}] ==> {1}, ",XX,tempArray[XX]);
            data[k] = tempArray[k - startIndex];
        }
        // Console.WriteLine();
        
    }
}