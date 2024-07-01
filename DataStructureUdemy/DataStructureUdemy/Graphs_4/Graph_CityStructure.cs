namespace DataStructureUdemy.Graphs;

public class Graph_CityStructure : Problem
{
    public Graph_CityStructure()
    {
        this.RunIndex = 4.2f;
    }

    public override void Run()
    {
        List<string> city = new List<string>() { "Delhi","London","Paris","New York"};
        Graph_City graphCity = new Graph_City(city);
        graphCity.AddEdge("Delhi","London");
        graphCity.AddEdge("New York","London");
        graphCity.AddEdge("Delhi","Paris");
        graphCity.AddEdge("Paris","New York");
        graphCity.PrintCityMap();
    }
}

public class GpNode
{
    public string Name;
    public List<string> Nbrs;

    public GpNode(string name)
    {
        this.Name = name;
        Nbrs = new List<string>();
    }
}

public class Graph_City
{
    // All Node
    // HashMap (string, Node*)

    public Dictionary<string, GpNode> Graph_CityObj;

    public Graph_City(List<string> citys)
    {
        Graph_CityObj = new Dictionary<string, GpNode>();
        foreach (var str in citys)
        {
            Graph_CityObj.Add(str,new GpNode(str));
        }
    }

    public void AddEdge(string x, string y, bool undirect = false)
    {
        Graph_CityObj[x].Nbrs.Add(y);
        if (undirect)
        {
            Graph_CityObj[y].Nbrs.Add(x);
        }
    }

    public void PrintCityMap()
    {
        foreach (var key in this.Graph_CityObj.Keys)
        {
            Console.WriteLine("{0}--->{1}",key,string.Join(",",Graph_CityObj[key].Nbrs));
        }
    }
}