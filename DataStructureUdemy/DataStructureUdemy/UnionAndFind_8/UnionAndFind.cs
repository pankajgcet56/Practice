using DataStructureUdemy.Graphs;

namespace DataStructureUdemy.Test1;

public class UnionAndFind : Problem
{
    public UnionAndFind()
    {
        this.RunIndex = 8.1f;
    }
    public override void Run()
    {
        // Cycle detection in Undirected Graph using DSU
        GraphV2 graphV2 = new GraphV2(4);
        graphV2.AddEdge(1, 2);
        graphV2.AddEdge(2, 3);
        graphV2.AddEdge(3, 4);
        graphV2.AddEdge(4, 4);
        Console.WriteLine("Conatns Cycle = "+graphV2.ContainsCycle());
    }

    class GraphV2
    {
        private int V;
        private List<Tuple<int, int>> _adjencyList;

        public GraphV2(int v)
        {
            V = v;
            _dsu = new DisjoinSetUnion(v);
            _adjencyList = new List<Tuple<int, int>>();
        }

        public int AddEdge(int u, int v)
        {
            _adjencyList.Add(new Tuple<int, int>(u, v));
            return 0;
        }

        private DisjoinSetUnion _dsu;
        public bool ContainsCycle()
        {
            // Iterate over edge list
            foreach (var adj in _adjencyList)
            {
                int i = adj.Item1;
                int j = adj.Item2;

                int s1 = _dsu.Find(i);
                int s2 = _dsu.Find(j);
                
                if (s1 != s2 || (s1==s2 && s1 ==-1))
                {
                    _dsu.Union(s1,s2);
                }
                else
                {
                    Console.WriteLine("Same Parent "+s2+", "+s1);
                    return true;
                }
            }
            return false;
        }
    }
    /*Disjoint Set Union
    Support 2 Operation
        Find
        Union
    */
}

public class DisjoinSetUnion // DSU data Structure
{
    private int[] _parent;

    public DisjoinSetUnion(int size)
    {
        if(size == 0)
            return;
        _parent = new int[size+1];
        for (int i = 0; i < size; i++)
        {
            _parent[i] = -1;
        }
    }

    public void Union(int a,int b) // O(n)
    {
        int parentA = Find(a);
        int parentB = Find(b);
        if (parentA != parentB)
        {
            _parent[parentA] = parentB;
        }
    }

    public int Find(int i) // O(n) Time
    {
        if (_parent[i] == -1)
        {
            return i;
        }
        return Find(_parent[i]);
    }
}