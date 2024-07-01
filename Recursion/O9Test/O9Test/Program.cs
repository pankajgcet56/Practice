using System;
using System.Threading;

namespace O9Test
{
    internal class Program
    {
        private static int size = 10;
        private static Inventory inventory ;
        public static void Main(string[] args)
        {
            InventoryTestSolution2();
        }

        private static void SemaphoreO9()
        {
            Semaphore semaphore = new Semaphore(0, 5);
            SemaphoreO9 semaphoreO9 = new SemaphoreO9(5);

            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(Program.InventoryTestSolution2);
                thread.Start(i);
                semaphoreO9.Wait();
            }

        }
        private static void InventoryTestSolution2()
        {
            InventoryProblemTreeSolution problemTreeSolution = new InventoryProblemTreeSolution();
            //problemTreeSolution.InventoryTree.ReadTree();
            InventoryProblemTreeSolution.IsDebugMode = false;
            problemTreeSolution.AddSupply(2,50);
            problemTreeSolution.InventoryTree.ReadTree();
            
            // problemTreeSolution.AddDemand(3,25);
            // problemTreeSolution.InventoryTree.ReadTree();
            
            // problemTreeSolution.AddDemand(2,30);
            // problemTreeSolution.InventoryTree.ReadTree();
            // //
            // problemTreeSolution.AddDemand(1,30);
            // problemTreeSolution.InventoryTree.ReadTree();
            
            Console.WriteLine("0  --> "+problemTreeSolution.GenInventory(0));
            Console.WriteLine("1  --> "+problemTreeSolution.GenInventory(1));
            Console.WriteLine("2  --> "+problemTreeSolution.GenInventory(2));
            Console.WriteLine("3  --> "+problemTreeSolution.GenInventory(3));
            Console.WriteLine("4  --> "+problemTreeSolution.GenInventory(4));
            // problemTreeSolution.InventoryTree.ReadTree();
            Console.WriteLine("5  --> "+problemTreeSolution.GenInventory(5));
            Console.WriteLine("6  --> "+problemTreeSolution.GenInventory(6));
            Console.WriteLine("7  --> "+problemTreeSolution.GenInventory(7));
            Console.WriteLine("8  --> "+problemTreeSolution.GenInventory(8));
            Console.WriteLine("9  --> "+problemTreeSolution.GenInventory(9));
            Console.WriteLine("10 --> "+problemTreeSolution.GenInventory(10));
            Console.WriteLine("11 --> "+problemTreeSolution.GenInventory(11));
            Console.WriteLine("12 --> "+problemTreeSolution.GenInventory(12));
            Console.WriteLine("13 --> "+problemTreeSolution.GenInventory(13));
            Console.WriteLine("14 --> "+problemTreeSolution.GenInventory(14));
        }

        private static void Solution1()
        {
            inventory = new Inventory(size);
            while (true)
            {
                Thread.Sleep(1000);
                PerformOnOperation();
            }
        }

        static void PerformOnOperation()
        {
            Random random = new Random();
            int nextRand = random.Next(0, 30000);
            Console.WriteLine("Found random : "+nextRand%3);
            int randomIndex = (random.Next(0, 70000))%(size);
            
            int randomDelta = random.Next(0, 20);
            if (randomDelta < 0)
            {
                Console.WriteLine("");
            }
            switch (nextRand%4)
            {
                case 0:
                    // Read Node at Random Index
                    // Console.WriteLine("==============================READ================================");
                    // Console.WriteLine(" Inventory =  "+inventory.GetInventory(randomIndex));
                    // Console.WriteLine("==============================================================");
                    return;
                case 1:
                    Console.WriteLine("==============================Add Demand ({0},{1}) ================================",randomIndex,randomDelta);
                    inventory.AddDemand(randomIndex,randomDelta).Print();
                    Console.WriteLine("==============================================================");
                    break;
                case 2:
                    Console.WriteLine("==============================ADD SUPPLY  ({0},{1}) ================================",randomIndex,randomDelta);
                    inventory.AddSupply(randomIndex,randomDelta).Print();
                    Console.WriteLine("==============================================================");
                    break;
            }
            Console.WriteLine("==============================Print Invenory================================");
            foreach (var node in inventory._inventoryNodes)
            {
                node.Print();
            }
            Console.WriteLine("==============================================================");
        }
    }
}