using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.LinkedList
{
    public class LinkedList<T> : System.Collections.Generic.IEnumerable<T>
    {
        public Node HeadNode;
        
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
            if (HeadNode == null)
            {
                HeadNode = node;
                return;
            }

            Node insertionNode = HeadNode;
            while (insertionNode.Next != null)
            {
                insertionNode = insertionNode.Next;
            }

            insertionNode.Next = node;
        }

        public int Length()
        {
            if (HeadNode == null)
                return 0;
            int count = 1;
            Node node = HeadNode;
            while (node.Next != null)
            {
                node = node.Next;
                count++;
            }

            return count;
        }

        public void Print()
        {
            if (HeadNode == null)
            {
                Console.WriteLine("No Node");
                return;
            }
            Node node = HeadNode;
            while (node.Next != null)
            {
                Console.Write(node.Data+"");
                node = node.Next;
            }
            Console.Write(node.Data+"");
            Console.WriteLine();
        }

        /// <summary>
        /// Find N_th node from end of Linked List 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Node FindNodeFromEnd(int index)
        {
            int length = Length();
            if (length == 0 || length < index)
                return null;
            Node node = HeadNode;
            for (int i = 0; i < length-index; i++)
            {
                node = node.Next;
            }
            
            return node;
        }
        
        /// <summary>
        /// Find N_th node from end of Linked List In Single Traverse
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Node FindNodeFromEndInSingleTraverse(int index)
        {
            Node lastNodePtr = HeadNode;
            Node nThNodePtr = HeadNode;
            for (int i = 0; i < index-1; i++)
            {
                if (lastNodePtr.Next != null)
                {
                    lastNodePtr = lastNodePtr.Next;
                }
                else
                {
                    return null;
                }
            }

            while (lastNodePtr.Next != null)
            {
                lastNodePtr = lastNodePtr.Next;
                nThNodePtr = nThNodePtr.Next;
            }
            
            return nThNodePtr;
        }

        /// <summary>
        /// is List Connected inside <This Is Not tested>
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool IsListEndNotNull(Node head)
        {
            if (head == null || head.Next == null || head.Next.Next == null)
            {
                return false;
            }
            Node fastPtr = head.Next;
            Node slowPtr = head.Next.Next;

            while (fastPtr != slowPtr)
            {
                if (head.Next == null || head.Next.Next == null)
                {
                    return false;
                }
                fastPtr = fastPtr.Next.Next;
                slowPtr = slowPtr.Next;
            }
            
            return true;
        }

        public static void ReverseLinkedList(LinkedList<T> linkedList)
        {
            Node curent = linkedList.HeadNode;
            Node previous = null;
            Node next = null;

            while (curent != null)
            {
                next = curent.Next;
                curent.Next = previous;
                previous = curent;
                curent = next;
            }

            linkedList.HeadNode = previous;
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