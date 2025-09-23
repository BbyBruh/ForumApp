using System.Diagnostics;
using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
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
            Console.WriteLine("> ");
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
                break;

            // ************* ADD: 
            
            if (input.StartsWith("add ", StringComparison.OrdinalIgnoreCase))
            {
                string[]  inputs = input.Substring("add".Length).Split(' ',  
                    StringSplitOptions.RemoveEmptyEntries);
            
                string query = inputs[0];
                switch (query.ToLower())
                {
                    case "user":
                        UserView.AddUser(inputs, userRepository);
                        break;
                    case "post":
                        PostView.AddPost(inputs, postRepository);
                        break;
                    case "comment":
                        CommentView.AddComment(inputs, commentRepository);
                        break;
                    default:
                        Console.WriteLine("Unknown command, you can only add user/comment/post");
                        break;
                }
            } 
           
            // ************* SHOW:

            if (input.StartsWith("list ", StringComparison.OrdinalIgnoreCase))
            {
                string[] inputs = input.Substring("list".Length).Split(' ', 
                    StringSplitOptions.RemoveEmptyEntries);
                if (inputs.Length > 2)
                {
                    Console.WriteLine("Command format is: list users/comments/posts");
                }
                string query = inputs[0];

                switch (query.ToLower())
                {
                    case "users":
                        UserView.ListUsers(inputs, userRepository);
                        break;
                    case "posts":
                       PostView.ListPosts(inputs, postRepository);
                        break;
                    case "comments":
                        CommentView.ListComments(inputs, commentRepository);
                        break;
                    default:
                        Console.WriteLine("Unknown command, you can only list users/comments/posts");
                        break;
                }
            }
            
            // ************* ADD: 
            
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
}