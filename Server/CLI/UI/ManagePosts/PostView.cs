using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public static class PostView
{
    public static async Task AddPost(string[] inputs, IPost postRepository)
    {
        if (inputs.Length < 2 || inputs.Length > 3)
        {
            Console.WriteLine("Command format is: add post + title + body");
        }
        else
        {
            string title = inputs[1];
            string body = inputs[2];
            await postRepository.AddAsyncPost(new Post { Title = title, Body = body });
            ShowPostCreated(title, body);
        }
    }

    public static async Task ListPosts(string[] inputs, IPost postRepository)
    {
        var allPosts = postRepository.GeManyAsyncPosts();

        if (!allPosts.Any())
        {
            Console.WriteLine("No posts found");
        }
        else
        {
            Console.WriteLine("Posts found: ");
            foreach (var post in allPosts)
            { 
                Console.WriteLine($"{post.Id} : {post.Title} - {post.Body}" );
            }
        }
    }
    public static void ShowPostCreated(string title, string body)
    {
        Console.WriteLine($"Successfully created post:" +
                          $" {title} with content: {body}");
    }
}