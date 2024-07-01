// See https://aka.ms/new-console-template for more information

using System.Reflection;

namespace DataStructureUdemy;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Main Start");
        var type = typeof(Problem);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p));
        var baseClass = new Problem();
        foreach (var typeVal in types)
        {
            var obj = (Problem)Activator.CreateInstance(typeVal)!;
            if (obj.RunIndex > baseClass.RunIndex)
                baseClass = obj;
        }
        baseClass?.Run();
    }
}

public class Problem
{
    public float RunIndex = 0;
    public virtual void Run()
    {
        
    }
}