namespace DataStructureUdemy.BitOperator;

public class Problem1_BitWiseBasic : Problem
{
    // Replace Bits in N by M
    
    /*
     *You are given 2 32-bit numbers, N and M , the 2 Bit position i,j
     * Write a program to set all bits between i and j in N equals to M.
     * 
     * Example :
     * N = 100000000
     * M = 10101
     * I=2, J= 6
     */
    public Problem1_BitWiseBasic()
    {
        this.RunIndex = 3.2f;
    }
    public override void Run()
    {
        Console.WriteLine("------ Pankaj");
        Console.WriteLine(ReplaceBits(15,2,1,3));
        Console.WriteLine("Power Of 2 Check : "+IsNumberPowerOf2(8));
        Console.WriteLine("Count Set Bits : "+CountBits(8));
    }

    private int ReplaceBits(int n, int m, int i, int j)
    {
        int mask = m << i; // Left Shift I count
        int clearBitsOp = ClearBitsFrom(n, i, j);
        return mask | clearBitsOp;
    }
    private int ClearBitsFrom(int n, int i, int j)
    {
        int a = (~0) << (j + 1);
        int b = (1<<i) -1;
        int mask = a | b;
        return n & mask;
    }

    private bool IsNumberPowerOf2(int n)
    {
        /*
         * 16 = 10000
         * 15 = 01111
         * N & N-1 = 0
         */
        return (n & n - 1) == 0;
    }

    int CountBits(int n)
    {
        int count = 0;
        while (n>0)
        {
            int lastBit = n & 1;
            count += lastBit;
            n= n >> 1;
        }

        return count;
    }
}