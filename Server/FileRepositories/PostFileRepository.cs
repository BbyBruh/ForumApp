using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPost
{
    private readonly string filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Post> AddAsyncPost(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) !;
        int maxId = posts.Count > 0 ?  posts.Max(p => p.Id) : 1;
        post.Id = maxId + 1;
        posts.Add(post);
        await File.WriteAllTextAsync(filePath, postsAsJson);
        return post;
    }

    public async Task UpdateAsyncPost(Post post)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson) !;
        
        int index = posts.FindIndex(p => p.Id == post.Id);
        if (index == -1) //if there is NOT a match it returns -1
        {
            Console.WriteLine("No post matching found.");
        }
        else
        {
            posts[index] = post;
        }
        postAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postAsJson);
    }

    public async Task DeleteAsyncPost(int id)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson) !;
        
        Post? post = posts.Find(p => p.Id == id);
        if (post == null)
        {
            Console.WriteLine("No post matching found.");
        }
        posts.Remove(post);
        postAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postAsJson);
    }

    public async Task<Post> GetSingleAsyncPost(int id)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post>? posts = JsonSerializer.Deserialize<List<Post>>(postAsJson);
        
        if (posts == null)
        {
            Console.WriteLine("No post matching found.");
        }
        
        Post? post = posts.SingleOrDefault(c => c.Id == id);

        if (post is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found.");
        }

        return post;
    }

    public IQueryable<Post> GeManyAsyncPosts()
    {
        string postsAsJson = File.ReadAllTextAsync(filePath).Result; // call result instead await => extract the string
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) !;
        return posts.AsQueryable();
    }
}