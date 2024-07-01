using System;
using System.Collections.Generic;

namespace O9Test
{
    public class InventoryNode
    {
        public int BucketId;
        public int Counter;
        public float Supply;
        public float Demand;
        public float Inventory;

        public InventoryNode LeftNode;
        public InventoryNode RightNode;
        
        public void Print()
        {
            Console.WriteLine("--------------------No : {0}--------------------",BucketId);
            Console.WriteLine("Supply : {0}\nDemand : {1}\nInventory : {2} \nCounter : {3}",Supply,Demand,Inventory,Counter);
            Console.WriteLine("----------------------------------------");
        }

        public static bool IsGreater(InventoryNode x, InventoryNode y)
        {
            return x.BucketId > y.BucketId;
        }
    }

    public class InventoryTree
    {
        public InventoryNode Root;

        public void InsertNode(InventoryNode node)
        {
            if (node == null) return;
            if (Root == null) Root = node;
            else
            {
                InsertNodeInTree(Root,node);
            }
        }
        private void InsertNodeInTree(InventoryNode root, InventoryNode node)
        {
            if (InventoryNode.IsGreater(root,node))
            {
                if(root.LeftNode != null)
                    InsertNodeInTree(root.LeftNode,node);
                else
                {
                    root.LeftNode = node;
                }
            }
            else
            {
                if (root.RightNode != null)
                {
                    InsertNodeInTree(root.RightNode,node);
                }
                else
                {
                    root.RightNode = node;
                }
            }
        }

        public void ReadTree()
        {
            if (Root == null)
            {
                Console.WriteLine("No Nodes In Tree");
                return;
            }
            Console.WriteLine("=====================================");
            ReadTreeNodes(Root);
            Console.WriteLine("=====================================");
        }

        private void ReadTreeNodes(InventoryNode node)
        {
            if(node == null)
                return;
            ReadTreeNodes(node.LeftNode);
            Console.WriteLine("{3} :: S={0}  D={1}  I={2}  C={4}",node.Supply,node.Demand,node.Inventory,node.BucketId,node.Counter);
            ReadTreeNodes(node.RightNode);
        }
    }
    
    public class InventoryProblemTreeSolution
    {
        public static bool IsDebugMode = false; 
        private int MaxIndex = 14;
        public InventoryTree InventoryTree;
        public int Counter = 0; // Operation Counter
        public InventoryProblemTreeSolution()
        {
            InventoryTree = new InventoryTree();
            int[] treeNodes = new[] {7, 3, 11, 1, 5, 9, 13, 0, 2, 4, 6, 8, 10, 12, 14};
            foreach (var val in treeNodes)
            {
                InventoryNode node = new InventoryNode();
                node.BucketId = val;
                InventoryTree.InsertNode(node);
            }
        }

        
        public void AddDemand(int bucket, float data)
        {
            if (bucket > MaxIndex)
            {
                Console.WriteLine("Index Out Of Scope : ");
                return;
            }

            Counter++;
            InventoryNode updateNode = new InventoryNode();
            updateNode.BucketId = bucket;
            updateNode.Demand = data;
            updateNode.Inventory = InventoryTree.Root.Inventory-data;
            updateNode.Counter = Counter;
            ProcessSupplyDemandByTree(updateNode,this.InventoryTree.Root);
        }
        public void AddSupply(int bucket, float data)
        {
            if (bucket > MaxIndex)
            {
                Console.WriteLine("Index Out Of Scope : ");
                return;
            }

            Counter++;
            InventoryNode updateNode = new InventoryNode();
            updateNode.BucketId = bucket;
            updateNode.Supply = data;
            updateNode.Inventory = InventoryTree.Root.Inventory+data;
            updateNode.Counter = Counter;
            ProcessSupplyDemandByTree(updateNode,this.InventoryTree.Root);
        }
        public float GenInventory(int bucket)
        {
            InventoryNode node = ReadInventoryByTree(this.InventoryTree.Root, bucket,this.InventoryTree.Root);
            if (node != null)
            {
                // node.Print();
                return node.Inventory;
            }
            return 0;
        }
        private InventoryNode ReadInventoryByTree(InventoryNode node, int bucket,InventoryNode currentUpdatedLowerNode)
        {
            if (node == null)
            {
                Console.WriteLine("Error : Not Found");
            }

            if (bucket == node.BucketId)
            {
                if (currentUpdatedLowerNode != null 
                    && currentUpdatedLowerNode.BucketId < node.BucketId
                    && currentUpdatedLowerNode.Counter > node.Counter)
                {
                    node.Inventory = currentUpdatedLowerNode.Inventory;
                    node.Counter = currentUpdatedLowerNode.Counter;
                }
                return node;   
            }
            
            if(bucket>node.BucketId) // Move Right
            {
                InventoryNode lowerNodeUpdated = node;
                if (node.RightNode != null)
                {
                    if (node.Counter <= node.RightNode.Counter) // Right Node Counter is Bigger : No Need to Do Any Thing
                    {
                        
                    }
                    else //Left Node Counter is Bigger than Right : Update Right Node
                    {
                        if(IsDebugMode)
                            Console.WriteLine("Updating Node = {0}, Updated By {1}",node.BucketId,currentUpdatedLowerNode.BucketId);
                        node.RightNode.Inventory = node.Inventory;
                        node.RightNode.Counter = node.RightNode.Counter;
                        lowerNodeUpdated = node;
                    }   
                }
                if(IsDebugMode)
                    Console.WriteLine("Right : Current Node = {0}, Updated By {1}",node.BucketId,lowerNodeUpdated.BucketId);
                return ReadInventoryByTree(node.RightNode, bucket,lowerNodeUpdated);
            }
            else // Move Left
            {
                InventoryNode lowerNodeUpdated = currentUpdatedLowerNode;
                if (node.LeftNode != null)
                {
                    if (node.Counter < currentUpdatedLowerNode.Counter && node.BucketId > currentUpdatedLowerNode.BucketId)
                    {
                        node.Inventory = currentUpdatedLowerNode.Inventory;
                        node.Counter = currentUpdatedLowerNode.RightNode.Counter;
                        if(IsDebugMode)
                            Console.WriteLine("LeftInside : Updating by Node. CurrentNode = {0}, Ref Node {1}",node.BucketId,currentUpdatedLowerNode.BucketId);
                    }
                    else
                    {
                        
                        if (node.BucketId < currentUpdatedLowerNode.BucketId)
                        {
                            lowerNodeUpdated = node;
                        }
                        if(IsDebugMode)
                            Console.WriteLine("LeftInside : Updating by Node. CurrentNode = {0}, Ref Node {1}",node.BucketId,currentUpdatedLowerNode.BucketId);
                    }
                    
                }
                if(IsDebugMode)
                    Console.WriteLine("Left : Current Node = {0}, Updated By {1}",node.BucketId,lowerNodeUpdated.BucketId);
                return ReadInventoryByTree(node.LeftNode, bucket,lowerNodeUpdated);
            }
        }
        private void ProcessSupplyDemandByTree(InventoryNode updateNode, InventoryNode node,InventoryNode prevMinNode = null)
        {
            if (node == null)
            {
                Console.WriteLine("Error : Node Not Found");
                return;
            }
            if (node.BucketId == updateNode.BucketId)
            {
                // Found Node
                if (prevMinNode != null /*&& node.Counter < prevMinNode.Counter*/)
                {
                    Console.WriteLine("Found Node {0}, Prev node {1} | Counter PrevNode = {2}, Counter CurrnetNode = {3}",node.BucketId,prevMinNode.BucketId,prevMinNode.Counter,node.Counter);
                    if (prevMinNode.BucketId < node.BucketId && prevMinNode.Counter>node.Counter && prevMinNode.Counter>0)
                    {
                        Console.WriteLine("Updating Inventory for Node : {0}. Inventory Current : {1}, Next {2}",node.BucketId,node.Inventory,prevMinNode.Inventory);
                        // node.Inventory = prevMinNode.Inventory;
                    }
                }
                Console.WriteLine("---------Update------");
                node.Print();
                
                node.Counter = updateNode.Counter;
                node.Supply += updateNode.Supply;
                node.Demand += updateNode.Demand;
                
                node.Inventory += updateNode.Supply;
                node.Inventory -= updateNode.Demand;
                
                Console.WriteLine("---------------");
                node.Print();
                
            }
            else if(node.BucketId > updateNode.BucketId)
            {
                // Move left
                Console.WriteLine("Node : "+node.BucketId+" : Next --> L");
                // Check if Prv Min has Lower Bucket Index
                InventoryNode preNode = node.LeftNode;
                if (prevMinNode != null)
                {
                    Console.WriteLine("left : Prev Node={0}, Current = {1}",prevMinNode.BucketId,node.BucketId);
                }
                else
                {
                    Console.WriteLine("At Node : {0}, Prev Min = Null",node.BucketId);
                }
                if (prevMinNode != null && prevMinNode.BucketId < node.BucketId)
                {
                    Console.WriteLine("left : Prev Node={0},Current = {1}. Update : From {2} to {3}",prevMinNode.BucketId,node.BucketId,node.Inventory,prevMinNode.Inventory);
                    node.Inventory = prevMinNode.Inventory;
                    node.Counter = prevMinNode.Counter;
                    preNode = prevMinNode;
                }
                Console.WriteLine("---------Update------");
                node.Print();
                node.Counter = updateNode.Counter;
                node.Inventory += updateNode.Supply;
                node.Inventory -= updateNode.Demand;
                
                node.Print();
                Console.WriteLine("---------------");

                ProcessSupplyDemandByTree(updateNode,node.LeftNode,preNode);
            }
            else
            {
                // Move right
                InventoryNode prevMin = null;
                
                Console.WriteLine("Node : "+node.BucketId+" : Next --> R");
                
                if(prevMinNode == null)
                {
                    prevMin = node;
                    Console.WriteLine("2. Right : Prev Node = {0}. Current Node = {1}",prevMin.BucketId,node.BucketId);
                }
                
                if (prevMinNode != null && prevMinNode.BucketId < node.BucketId)
                {
                    prevMin = prevMinNode;
                    Console.WriteLine("1. Right : Prev Node = {0}. Current Node = {1}",prevMin.BucketId,node.BucketId);
                }
                else if(prevMinNode == null)
                {
                    prevMin = node;
                    Console.WriteLine("2. Right : Prev Node = {0}. Current Node = {1}",prevMin.BucketId,node.BucketId);
                }
                 
                if(prevMinNode != null)
                {
                    Console.WriteLine("3. Right : Prev Node = {0}. Current Node = {1}",prevMinNode.BucketId,node.BucketId);
                    prevMin = prevMinNode;
                }

                if (prevMin != null)
                {
                    if (prevMin.Counter > node.Counter)
                    {
                        // Update Current Inventory with Prev Min
                        node.Inventory = prevMin.Inventory;
                        node.Counter = prevMin.Counter;
                    }
                }
                node.Counter = updateNode.Counter;
                if (node.RightNode != null && node.Counter>node.RightNode.Counter)
                {
                    Console.WriteLine("---------Update------");
                    node.RightNode.Print();
                
                    node.RightNode.Inventory = node.RightNode.Supply - node.RightNode.Demand + node.Inventory;
                    
                    node.RightNode.Print();
                    Console.WriteLine("---------------");
                }

                if (updateNode.BucketId < node.BucketId)
                {
                    Console.WriteLine("---------Update------");
                    node.RightNode.Print();
                    
                    node.Counter = updateNode.Counter;
                    node.Inventory += updateNode.Supply;
                    node.Inventory -= updateNode.Demand;
                    
                    node.RightNode.Print();
                    Console.WriteLine("---------------");
                }
                // No Update for inventory
                ProcessSupplyDemandByTree(updateNode,node.RightNode,prevMin);
            }
        }
        
    }
}