using DTOs;

namespace BlazorApp.Services;

public interface ICommentService
{
    Task<CommentDTO> AddCommentAsync(CommentDTO request);
    Task UpdateCommentAsync(int id, CommentDTO request);
    Task DeleteCommentAsync(int id);
    Task<CommentDTO> GetCommentAsync(int id);
    Task<List<CommentDTO>> GetAllCommentsAsync(int? userId = null, int? postId = null);
}