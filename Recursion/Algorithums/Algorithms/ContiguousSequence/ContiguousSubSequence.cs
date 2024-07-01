using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.ContiguousSequence
{
    public class ContiguousSubSequence
    {
        private List<int> Data = null;

        public ContiguousSubSequence(int value)
        {
            Data = new List<int>(value);
            for (int i = 1; i <= value; i++)
            {
                Data.Add(i);
            }
        }
        
        public object GetAllContiguousSequence()
        {
            Dictionary<int, List<int>> subSeqData = new Dictionary<int, List<int>>();
            
            for (int i = 0;  i<Data.Count; i++)
            {
                List<int> subSeq = new List<int>();
                for (int j = 0; j <= i; j++)
                {
                    subSeq.Add(Data[j]);
                }
                subSeqData.Add(i+1,subSeq);
            }
            
            return subSeqData;
        }

        public void PrintAllContiguousSequence()
        {
            Dictionary<int, List<int>> subSeqData = GetAllContiguousSequence() as Dictionary<int, List<int>>;
            foreach (var key in subSeqData.Keys)
            {
                List<int> subSeq = subSeqData[key];
                Console.WriteLine("{0} : {1}",key,string.Join(",", subSeq.Select(n => n.ToString()).ToArray()));
            }
        }
    }
}