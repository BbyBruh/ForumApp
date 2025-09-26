using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUser
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }   
    }

    public async Task<User> AddAsyncUser(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;
        int maxId = users.Count > 0 ?  users.Max(c => c.Id) : 1;
        user.Id = maxId + 1;
        users.Add(user);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return user;
    }

    public async Task UpdateAsyncUser(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;
        
        int index = users.FindIndex(c => c.Id == user.Id);
        if (index == -1) //if there is NOT a match it returns -1
        {
            Console.WriteLine("No user matching found.");
        }
        else
        {
            users[index] = user;
        }
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task DeleteAsyncUser(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;

        User? user = users.Find(u => u.Id == id);
        if (user == null)
        {
            Console.WriteLine("No user matching found.");
        }
        users.Remove(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task<User> GetSingleAsyncUser(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;

        if (users == null)
        {
            Console.WriteLine("No comment matching found.");
        }
        
        User? user = users.SingleOrDefault(c => c.Id == id);

        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found.");
        }

        return user;
    }

    public IQueryable<User> GeManyAsyncUsers()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;
        return users.AsQueryable();
    }
}