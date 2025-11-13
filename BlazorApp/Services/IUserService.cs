using DTOs;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDTO> AddUserAsync(UserDTO user);
    public Task UpdateUserAsync(int id, UserDTO user);
    public Task DeleteUserAsync(int id);
    public Task<UserDTO> GetUserByIdAsync(int id);
    public Task<List<UserDTO>> GetAllUsersAsync(string? username = null);
}