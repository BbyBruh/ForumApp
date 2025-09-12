using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPost
{
    List<Post> posts = new ();
    public Task<Post> AddAsyncPost(Post post)
    {
        post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsyncPost(Post post)
    {
        Post ? existingPost = posts.FirstOrDefault(p => p.Id == post.Id);
        if (existingPost != null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found");
        }
        posts.Remove(existingPost);
        posts.Add(post);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsyncPost(int id)
    {
        Post ? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsyncPost(int id)
    {
        Post post = posts.SingleOrDefault(p => p.Id == id);
        if (post is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        return Task.FromResult(post);
    }

    public IQueryable<Post> GeManyAsyncPosts()
    {
        return posts.AsQueryable();
    }
}