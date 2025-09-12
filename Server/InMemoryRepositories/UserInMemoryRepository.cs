using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUser
{
    List<User> users = new ();
    public Task<User> AddAsyncUser(User user)
    {
        user.Id = users.Any() ? users.Max(p => p.Id) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsyncUser(User user)
    {
        User ? existingUser = users.FirstOrDefault(p => p.Id == user.Id);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsyncUser(int id)
    {
        User ? userToRemove = users.SingleOrDefault(p => p.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsyncUser(int id)
    {
        User user = users.SingleOrDefault(p => p.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return Task.FromResult(user);
    }

    public IQueryable<User> GeManyAsyncUsers()
    {
        return users.AsQueryable();
    }
}