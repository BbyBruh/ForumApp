using DTOs;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<PostDTO> AddPostAsync(PostDTO request);
    public Task UpdatePostAsync(int id, PostDTO request);

    public Task<PostDTO> GetPostAsync(int id);
    public Task DeletePostAsync(int id);
    public Task<List<PostDTO>> GetAllPostsAsync(string? title = null, int? userId = null);
}