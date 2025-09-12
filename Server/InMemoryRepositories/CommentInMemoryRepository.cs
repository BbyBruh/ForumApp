using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : IComment
{
    List<Comment> comments = new ();
    public Task<Comment> AddAsyncComment(Comment comment)
    {
        comment.Id = comments.Any() ? comments.Max(p => p.Id) + 1 : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsyncComment(Comment comment)
    {
        Comment ? existingComment = comments.FirstOrDefault(p => p.Id == comment.Id);
        if (existingComment != null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsyncComment(int id)
    {
        Comment ? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsyncComment(int id)
    {
        Comment comment = comments.SingleOrDefault(p => p.Id == id);
        if (comment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GeManyAsyncComments()
    {
        return comments.AsQueryable();
    }
}