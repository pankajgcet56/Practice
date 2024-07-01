using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.CircularList
{

    public class GenericCircularList<T> : System.Collections.Generic.IEnumerable<T>
    {
        public Node StartNode;
        
        public class Node
        {
            public T Data;
            public Node Next;

            public Node(T data)
            {
                this.Data = data;
            }
        }


        /// <summary>
        /// Insert to End
        /// </summary>
        /// <param name="data"></param>
        public void InsertNode(T data)
        {
            Node node = new Node(data);
            if (StartNode == null)
            {
                StartNode = node;
                StartNode.Next = node;
                return;
            }

            Node insertionNode = StartNode;
            while (insertionNode.Next != StartNode)
            {
                insertionNode = insertionNode.Next;
            }

            insertionNode.Next = node;
            node.Next = StartNode;
        }

        public int Length()
        {
            if (StartNode == null)
                return 0;
            int count = 0;
            Node node = StartNode;
            while (node.Next != StartNode)
            {
                node = node.Next;
                count++;
            }

            return count;
        }

        public void Print()
        {
            if (StartNode == null)
            {
                Console.WriteLine("No Node");
                return;
            }
            Node node = StartNode;
            while (node.Next != StartNode)
            {
                Console.Write(node.Data+"");
                node = node.Next;
            }
            Console.WriteLine();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}