using System;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        TestDbRun1();
    }

    private static void TestDbRun1()
    {
        using var db = new BloggingContext();

// Note: This sample requires the database to be created before running.
        Console.WriteLine($"Database path: {db.DbPath}.");

// Check if It Has Some Entity ::
        Console.WriteLine("Check If It has Some Entity");
        Console.WriteLine("Querying for a blog");
        var blogs = db.Blogs
            .OrderBy(b => b.BlogId);
        foreach (var blg in blogs)
        {
            Console.WriteLine("Blog : "+blg.BlogId+" :: "+blg.Url);
            AddPost(blg);
        }

        void AddPost(Blog? blog)
        {
            if(blog == null)
                return;
            // if (blog.Posts == null)
            //     blog.Posts = new List<Post>();
            for (int i = 0; i < 10; i++)
            {
                blog.Posts.Add(new Post(){Blog = blog,BlogId = blog.BlogId,Content = string.Format("Content : ",i),Title = "Title : "+i});
            }
        }

// Create
        Console.WriteLine("Inserting a new blog");
        db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
        db.SaveChanges();

// Read
        Console.WriteLine("Querying for a blog");
        var blog = db.Blogs
            .OrderBy(b => b.BlogId)
            .First();

// Update
        Console.WriteLine("Updating the blog and adding a post");
        blog.Url = "https://devblogs.microsoft.com/dotnet";
        blog.Posts.Add(
            new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
        db.SaveChanges();

        // Delete
        // Console.WriteLine("Delete the blog");
        // db.Remove(blog);
        // db.SaveChanges();
    }
}

