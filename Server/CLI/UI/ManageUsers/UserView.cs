namespace CLI.UI.ManageUsers;

public static class UserView
{
    public static void ShowUserCreated(string username, string password)
    {
        Console.WriteLine($"Successfully created username:" +
                          $" {username} with password: {password}");
    }
}