using System;
using Algorithms.LinkedList;
using Algorithms.CircularList;
using Algorithms.ContiguousSequence;
using Algorithms.Recursion;

namespace Algorithms
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ContiguousSubSequence sequence = new ContiguousSubSequence(500);
            sequence.PrintAllContiguousSequence();
        }
    }
}