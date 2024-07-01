namespace DataStructureUdemy.BitOperator;

public class BasicBitWise : Problem
{
    public BasicBitWise()
    {
        RunIndex = 3.1f;
    }
    public override void Run()
    {
        Console.WriteLine("....");
        int n = 5;
        int i = 2;
        Console.WriteLine(GetIthBit(5,2));
        Console.WriteLine(SetIthBit(5,1));
        Console.WriteLine(ClearIthBit(5,0));
        Console.WriteLine(UpdateIthBit(5,0,0));
        Console.WriteLine(ClearLastIBits(7,3));
    }

    int ClearLastIBits(int n, int i)
    {
        // Number has All 1
        int mask = -1 << i;
        return n & mask;
    }
    
    int GetIthBit(int n, int i)
    {
        int mask = 1 << i;
        return (n & mask) > 0 ? 1 : 0;
    }

    int SetIthBit(int n, int i)
    {
        int mask = 1 << i;
        return n | mask;
    }

    int ClearIthBit(int n, int i)
    {
        int mask = ~(1 << i);
        return n & mask;
    }

    int UpdateIthBit(int n, int i, int v)
    {
        int mask = v << i;
        // Clear Ith Bit
        int maskC = ~(1 << i);
        n = n & maskC;
        
        return n | mask;
    }
}