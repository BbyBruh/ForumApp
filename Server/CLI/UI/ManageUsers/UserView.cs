using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class UserView
{
    public static async Task AddUser(string[] inputs, IUser userRepository)
    {
        if (inputs.Length < 2 || inputs.Length > 3)
        {
            Console.WriteLine("Command format is: add user + name + password");
        }
        else
        {
            string username = inputs[1];
            string password = inputs[2];
            await userRepository.AddAsyncUser(new User { Username = username, Password = password });
            ShowUserCreated(username, password);
        }
    }

    public static async Task ListUsers(string[] inputs, IUser userRepository)
    {
        var allUsers = userRepository.GeManyAsyncUsers();

        if (!allUsers.Any())
        {
            Console.WriteLine("No users found");
        }
        else
        {
            Console.WriteLine("Users found: ");
            foreach (var user in allUsers)
            { 
                Console.WriteLine($"{user.Username} - id: {user.Id}, {(user.IsModerator? "MOD" : " ")}" );
            }
        }
    }
    public static void ShowUserCreated(string username, string password)
    {
        Console.WriteLine($"Successfully created username:" +
                          $" {username} with password: {password}");
    }
}