using Entities;

namespace RepositoryContracts;

public interface IUser
{
    Task<User> AddAsyncUser(User user);
    Task UpdateAsyncUser(User user);
    Task DeleteAsyncUser(int id);
    Task<User> GetSingleAsyncUser(int id);
    IQueryable<User> GeManyAsyncUsers();
}