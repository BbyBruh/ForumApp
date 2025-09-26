using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : IComment
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Comment> AddAsyncComment(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) !;
        int maxId = comments.Count > 0 ?  comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

    public async Task UpdateAsyncComment(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson) !;
        
        int index = comments.FindIndex(c => c.Id == comment.Id);
        if (index == -1) //if there is NOT a match it returns -1
        {
            Console.WriteLine("No comment matching found.");
        }
        else
        {
            comments[index] = comment;
        }
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
    }

    public async Task DeleteAsyncComment(int id)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson) !;
        
        Comment? comment = comments.Find(c => c.Id == id);
        if (comment == null)
        {
            Console.WriteLine("No comment matching found.");
        }
        comments.Remove(comment);
        commentAsJson = JsonSerializer.Serialize(comments);
       await File.WriteAllTextAsync(filePath, commentAsJson);
    }

    public async Task<Comment> GetSingleAsyncComment(int id)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment>? comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson);
        
        if (comments == null)
        {
            Console.WriteLine("No comment matching found.");
        }
        
        Comment? comment = comments.SingleOrDefault(c => c.Id == id);

        if (comment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found.");
        }

        return comment;
    }

    public IQueryable<Comment> GeManyAsyncComments()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result; // call result instead await => extract the string
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) !;
        return comments.AsQueryable();
    }
}