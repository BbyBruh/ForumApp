using System.Text.Json;
using DTOs;

namespace BlazorApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient _httpClient;
    
    public HttpUserService(HttpClient httpClient)
    {
       _httpClient = httpClient;
    }

    public async Task<UserDTO> AddUserAsync(UserDTO userDto)
    {
        var resp = await _httpClient.PostAsJsonAsync("user", userDto);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<UserDTO>(
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
               ?? throw new Exception("Empty response from server.");
    }

    public async Task UpdateUserAsync(int id, UserDTO user)
    {
        try
        {
            var resp = await _httpClient.PutAsJsonAsync($"user/{id}", user);
            resp.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            throw new Exception($"Update failed for user {id}: {e.Message}", e);
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        try
        {
            var resp = await _httpClient.DeleteAsync($"user/{id}");
            resp.EnsureSuccessStatusCode(); // 204 on success
        }
        catch (HttpRequestException e)
        {
            throw new Exception($"Delete failed for user {id}: {e.Message}", e);
        }
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        var resp = await _httpClient.GetAsync($"user/{id}");
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<UserDTO>(
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
               ?? throw new Exception("Empty response from server.");
    }

    public async Task<List<UserDTO>> GetAllUsersAsync(string? username = null)
    {
        var url = string.IsNullOrWhiteSpace(username) ? "user"
            : $"user?nameContains={Uri.EscapeDataString(username)}";

        var resp = await _httpClient.GetAsync(url);
        resp.EnsureSuccessStatusCode();

        return await resp.Content.ReadFromJsonAsync<List<UserDTO>>(
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
    }
}