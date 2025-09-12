using Entities;

namespace RepositoryContracts;

public interface IComment
{
    Task<Comment> AddAsyncComment(Comment comment);
    Task UpdateAsyncComment(Comment comment);
    Task DeleteAsyncComment(int id);
    Task<Comment> GetSingleAsyncComment(int id);
    IQueryable<Comment> GeManyAsyncComments();
}