using System.Text.Json;
using DTOs;
using Microsoft.AspNetCore.WebUtilities;

namespace BlazorApp.Services;

public class HttpPostService : IPostService
{
    
    private readonly HttpClient _httpClient;
    
    public HttpPostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<PostDTO> AddPostAsync(PostDTO request)
    {
        HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("post", request);
        
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        
        return JsonSerializer.Deserialize<PostDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task UpdatePostAsync(int id, PostDTO request)
    {
        try
        {
            var resp = await _httpClient.PutAsJsonAsync($"post/{id}", request);
            resp.EnsureSuccessStatusCode(); 
        }
        catch (HttpRequestException e)
        {
            throw new Exception($"Update failed for post {id}: {e.Message}", e);
        }
    }

    public async Task<PostDTO> GetPostAsync(int id)
    {
        HttpResponseMessage httpResponse = await _httpClient.GetAsync($"post/{id}");
        
        string response = await httpResponse.Content.ReadAsStringAsync();
        
        if (!httpResponse.IsSuccessStatusCode)
        { 
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<PostDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task DeletePostAsync(int id)
    {
        try
        {
            var resp = await _httpClient.DeleteAsync($"post/{id}");
            resp.EnsureSuccessStatusCode(); 
        }
        catch (HttpRequestException e)
        {
            throw new Exception($"Delete failed for post {id}: {e.Message}", e);
        }
    }

    public async Task<List<PostDTO>> GetAllPostsAsync(string? title = null, int? userId = null)
    {
        string url = "post";
        
        if (!string.IsNullOrWhiteSpace(title) || userId.HasValue)
        {
            var query = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(title)) query["TitleContains"] = title;
            if (userId.HasValue) query["userIdContains"] = userId.Value.ToString();
            url = QueryHelpers.AddQueryString(url, query);
        }

        var resp = await _httpClient.GetAsync(url);
        resp.EnsureSuccessStatusCode();

        return await resp.Content.ReadFromJsonAsync<List<PostDTO>>(
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? new List<PostDTO>();
    }
}