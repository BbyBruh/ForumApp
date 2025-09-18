using System.Diagnostics;
using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IComment commentRepository;
    private readonly IUser userRepository;
    private readonly IPost postRepository;

    public CliApp(IUser userRepository, IComment commentRepository,  IPost postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }
    
    public async Task StartAsync()
    {
        Console.WriteLine("CliApp starting..");

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
                break;
            
            if (input.StartsWith("help", StringComparison.OrdinalIgnoreCase))
            {
                PrintCommands();
            }

            
            if (input.StartsWith("user add", StringComparison.OrdinalIgnoreCase))
            {
                string[]  inputs = input.Substring("user add".Length).Split(' ', 2, 
                    StringSplitOptions.RemoveEmptyEntries);
            
                if (inputs.Length < 2)
                {
                    Console.WriteLine("You must provide a username and password.");
                }
                
                string username = inputs[0];
                string password = inputs[1];
            
                await userRepository.AddAsyncUser(new User { Username = username, Password = password });
                CreateUserView.ShowUserCreated(username, password); //not doing anything?
            } 
            
            
        }
    }

    private void PrintCommands()
    {
        Console.WriteLine("CliApp commands:");
        Console.WriteLine("help             opens this command:");
        Console.WriteLine("------------------------");
        Console.WriteLine("user             opens user menu"); 
        Console.WriteLine("------------------------");
        Console.WriteLine("post             opens post menu"); 
        Console.WriteLine("------------------------");
        Console.WriteLine("comment          opens comment menu"); 
        Console.WriteLine("------------------------");
        Console.WriteLine("more             opens detailed menu");
    }

    private void MoreCommands()
    {
        Console.WriteLine("CliApp more commands:");
        Console.WriteLine("userHelp                opens user specific commands");
        Console.WriteLine("postHelp                opens post specific commands");
        Console.WriteLine("commentHelp             opens comment specific commands");
    }
}