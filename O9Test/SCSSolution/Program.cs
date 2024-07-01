// See https://aka.ms/new-console-template for more information

using SCSSolution;
using SCSSolution.Algorithum;

public class Program
{
    // private static void Swap()
    // {
    //     int a = -3;
    //     int b = 5;
    //     
    //     a -= (a - b); // -3 - (-3-5) = 5
    //     b -= (b-a); // 5- (5+3) = -3
    // }
    
    public enum MyTestData
    {
        One,
        Two,
        Three
    }
    public static void Main(string[] args)
    {
        List<MyTestData> data = new List<MyTestData>();
        if (data.Contains(MyTestData.One))
        {
            Console.WriteLine(".....");
        }
        else
        {
            Console.WriteLine(".....No One");
        }
        SupplyChainSolution supplyChainSolution = new SupplyChainSolution(10);
        
        supplyChainSolution.AddSupply(2,50);
        
        supplyChainSolution.AddDemand(3,25);
        
        supplyChainSolution.AddDemand(2,30);
        
        supplyChainSolution.AddSupply(1,5);
        //
        // supplyChainSolution.AddSupply(20,15);
    }
}