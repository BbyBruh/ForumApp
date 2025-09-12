using Entities;

namespace RepositoryContracts;

public interface IPost
{
    Task<Post> AddAsyncPost(Post post);
    Task UpdateAsyncPost(Post post);
    Task DeleteAsyncPost(int id);
    Task<Post> GetSingleAsyncPost(int id);
    IQueryable<Post> GeManyAsyncPosts();
}