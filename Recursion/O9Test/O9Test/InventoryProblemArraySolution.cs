using System;
using System.Collections.Generic;

namespace O9Test
{
    public class Inventory
    {
        public List<InventoryNode> _inventoryNodes;
        private int _bucketSize = 0;
        public Inventory(int size)
        {
            _bucketSize = size;
            _inventoryNodes = new List<InventoryNode>();
            for (int i = 0; i < size; i++)
            {
                _inventoryNodes.Add(new InventoryNode(){BucketId = i,Supply = 0,Demand = 0,Inventory = 0});
            }
        }

        public float GetInventory(int bucket)
        {
            if (_inventoryNodes.Count == 0 || bucket >= _bucketSize)
            {
                Console.WriteLine("Error : Inventory Not Found");
                return 0;
            }
            return _inventoryNodes[bucket].Inventory;
        }
        
        public InventoryNode AddSupply(int bucketId, float delta)
        {
            if (bucketId >= _bucketSize)
            {
                Console.WriteLine("Error : Can't Add Item at {0}",bucketId);
                return null;
            }

            InventoryNode currentNode = _inventoryNodes[bucketId];
            currentNode.Supply += delta;
            currentNode.Inventory += delta;
            
            for (int i = bucketId+1; i < _bucketSize; i++)
            {
                InventoryNode node = _inventoryNodes[i];
                node.Inventory += delta;
            }

            return currentNode;
        }
        
        public InventoryNode AddDemand(int bucketId, float delta)
        {
            if (bucketId >= _bucketSize)
            {
                Console.WriteLine("Error : Can't Add Item at {0}",bucketId);
                return null;
            }

            InventoryNode currentNode = _inventoryNodes[bucketId];
            currentNode.Demand += delta;
            currentNode.Inventory -= delta; 
            
            for (int i = bucketId+1; i < _bucketSize; i++)
            {
                InventoryNode node = _inventoryNodes[i];
                node.Inventory -= delta;
            }
            return currentNode;
        }
    }
}