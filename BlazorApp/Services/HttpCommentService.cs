using System.Text.Json;
using DTOs;
using Microsoft.AspNetCore.WebUtilities;

namespace BlazorApp.Services;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient _httpClient;

    public HttpCommentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CommentDTO> AddCommentAsync(CommentDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("comment", request);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(content);

        return JsonSerializer.Deserialize<CommentDTO>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task UpdateCommentAsync(int id, CommentDTO request)
    {
        var response = await _httpClient.PutAsJsonAsync($"comment/{id}", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCommentAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"comment/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<CommentDTO> GetCommentAsync(int id)
    {
        var response = await _httpClient.GetAsync($"comment/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(content);

        return JsonSerializer.Deserialize<CommentDTO>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<List<CommentDTO>> GetAllCommentsAsync(int? userId = null, int? postId = null)
    {
        string url = "comment";

        if (userId.HasValue || postId.HasValue)
        {
            var query = new Dictionary<string, string>();
            if (userId.HasValue) query["userId"] = userId.Value.ToString();
            if (postId.HasValue) query["postId"] = postId.Value.ToString();
            url = QueryHelpers.AddQueryString(url, query);
        }

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<CommentDTO>>(
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? new List<CommentDTO>();
    }
}