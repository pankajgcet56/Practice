namespace DataStructureUdemy.Array;

public class Problem2_Rain : Problem
{
    private int[] HeightData = new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
    public override void Run()
    {
        Console.WriteLine(TotalRainStorage(HeightData));
    }

    private int TotalRainStorage(int[] data)
    {
        int[] leftMaxHeight = new int[data.Length];
        int[] rightMaxHeight = new int[data.Length];
        int lenght = data.Length;
        leftMaxHeight[0] = data[0];
        rightMaxHeight[lenght - 1] = data[lenght - 1];
        for (int i = 1; i < lenght; i++)
        {
            leftMaxHeight[i] = leftMaxHeight[i - 1] < data[i] ? data[i] : leftMaxHeight[i - 1];
            // rightMaxHeight[lenght - i - 1] = rightMaxHeight[lenght - i - 1] < data[lenght - i]
            //     ? data[lenght - i]
            //     : rightMaxHeight[lenght - i - 1];
            rightMaxHeight[lenght - i - 1] = Math.Max(rightMaxHeight[lenght - i ], data[lenght - 1 - i]);
        }

        int water = 0;
        for (int i = 0; i < lenght; i++)
        {
            water += Math.Min(leftMaxHeight[i], rightMaxHeight[i]) - data[i];
        }
        return water;
    }
}