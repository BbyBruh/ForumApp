namespace CLI.UI.ManageUsers;

public static class CreateUserView
{
    public static void CreateUser(string [] inputs)
    {
        if (inputs.Length < 2 || inputs.Length > 3)
        {
            Console.WriteLine("Please enter only a username and password");
        }
        else
        {
          string username = inputs[1];
           string password = inputs[2];
        }
    }
    public static void ShowUserCreated(string username, string password)
    {
        Console.WriteLine($"Successfully created username:" +
                          $" {username} with password: {password}");
    }
}