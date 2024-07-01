using System;
using System.Collections;
namespace DataStructureUdemy.Graphs;

public class Gp_Adj_List : Problem
{
    public Gp_Adj_List()
    {
        this.RunIndex = 4.3f;
    }
    public override void Run()
    {
        Console.WriteLine(".........Graph");
        Graph g = new Graph(7);
        // g.AddEdge(0,1);
        // g.AddEdge(1,2);
        // g.AddEdge(2,3);
        // g.AddEdge(3,5);
        // g.AddEdge(5,6);
        // g.AddEdge(4,5);
        // g.AddEdge(0,4);
        // g.AddEdge(3,4);
        // // g.PrintAdjList();
        // g.Bfs(1);
        // g.Dfs(1);
        g.AddEdge(0,1,false);
        g.AddEdge(1,2,false);
        g.AddEdge(2,3,false);
        g.AddEdge(3,0,false);
        g.AddEdge(0,4,false);
        g.AddEdge(0,5,false);
        g.AddEdge(4,5,false);
        g.Cycle_In_DirectedGraph(1);
    }
}

public class Graph
{
    private int Vertices;
    public List<List<int>> GraphAdjList;

    public Graph(int v)
    {
        this.Vertices = v;
        GraphAdjList = new List<List<int>>();
        for (int i = 0; i < v; i++)
        {
            GraphAdjList.Add(new List<int>());
        }
    }

    public void AddEdge(int i, int j, bool undir = true)
    {
        // if (GraphAdjList[i] == null)
        //     GraphAdjList[i] = new List<int>();
        GraphAdjList[i].Add(j);
        if (undir)
        {
            GraphAdjList[j].Add(i);
        }
    }

    public void PrintAdjList()
    {
        // Iterate Over All Rows
        for (int i = 0; i < Vertices; i++)
        {
            Console.Write("[{0}]-->",i);
            // Print Current List
            foreach (var val in GraphAdjList[i])
            {
                Console.Write("{0}, ",val);
            }
            Console.WriteLine();
        }
    }

    public void Bfs(int source)
    {
        bool[] traversedNodesArray = new bool[Vertices];
        
        Queue<int> queue = new Queue<int>();
        
        int[] dist = new int[Vertices];
        int[] parent = new int[Vertices];
        for (int i = 0; i < Vertices; i++)
        {
            parent[i] = -1;
        }
        dist[source] = 0;

        parent[source] = source;
        queue.Enqueue(source);
        traversedNodesArray[source] = true;
        Console.Write(source+", ");
        while (queue.Count>0)
        {
            int currentval = queue.Dequeue();
            
            foreach (var nd in GraphAdjList[currentval])
            {   
                if (!traversedNodesArray[nd])
                {
                    Console.Write(nd+", ");
                    queue.Enqueue(nd);
                    parent[nd] = currentval;
                    dist[nd] = dist[currentval] + 1;
                }
                traversedNodesArray[nd] = true;
            }
        }
        Console.WriteLine();
        Console.WriteLine("-------Disntanc-------");
        for (int i = 0; i < Vertices; i++)
        {
            Console.WriteLine("{0}-->{1} Distance = {2}",source,i,dist[i]);
        }
    }

    private void DfsHelper(int node,ref bool[] visitedNodes)
    {
        visitedNodes[node] = true;
        Console.WriteLine("DFS: "+node);
        //Make visited call for all ets neighber
        foreach (var nbr in GraphAdjList[node])
        {
            if (!visitedNodes[nbr])
            {
                DfsHelper(nbr,ref visitedNodes);
            }
        }
        return;
    }
    public void Dfs(int source)
    {
        bool[] visitedNodes = new bool[Vertices];
        DfsHelper(source,ref visitedNodes);
    }

    private void DirectedGraphCycleHelper(int node, ref bool[] visited,ref bool[] stackedNodes, ref int cycleCount)
    {
        visited[node] = true;
        foreach (var n in GraphAdjList[node])
        {
            if (!visited[n])
            {
                stackedNodes[n] = true;
                DirectedGraphCycleHelper(n, ref visited, ref stackedNodes, ref cycleCount);
            }
            else if(stackedNodes[n])
            {
                cycleCount++;
                Console.WriteLine("Cycle Found : "+string.Join(',',stackedNodes.Select((nd)=>nd.ToString())));
            }
        }
        stackedNodes[node] = false;
    }
    public bool Cycle_In_DirectedGraph(int startPoint)
    {
        bool[] visitedNodes = new bool[Vertices]; // Stores All Visited Nodes
        bool[] nodesInStack = new bool[Vertices]; // Stores and Update Node Status in Call Stack
        int cycle = 0;
        nodesInStack[startPoint] = true;
        DirectedGraphCycleHelper(startPoint,ref visitedNodes,ref nodesInStack,ref cycle);
        Console.WriteLine("Cycle : "+cycle);
        return false;
    }
}