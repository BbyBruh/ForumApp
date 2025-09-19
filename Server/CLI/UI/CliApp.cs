using System.Diagnostics;
using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Entities;
using Microsoft.VisualBasic.CompilerServices;
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
                        if (inputs.Length < 2 || inputs.Length > 3)
                        {
                            Console.WriteLine("Command format is: add user + name + password");
                        }
                        else
                        {
                            string username = inputs[1];
                            string password = inputs[2];
                            await userRepository.AddAsyncUser(new User { Username = username, Password = password });
                            UserView.ShowUserCreated(username, password);
                        }
                        break;
                    case "post":
                        if (inputs.Length > 3)
                        {
                            Console.WriteLine("Command format is: add post + user id");
                        }
                        else
                        {
                            string userID = inputs[1];
                            
                            int id = IntegerType.FromString(userID);
                            
                            Console.WriteLine("Title: ");
                            string title = Console.ReadLine();
                            Console.WriteLine("Content: ");
                            string content = Console.ReadLine();

                            await postRepository.AddAsyncPost(new Post
                                { Title = title, Body = content, UserId = id });
                            PostView.ShowPostCreated(title, content);

                        }
                        break;
                    case "comment":
                        if (inputs.Length > 3)
                        {
                            Console.WriteLine("Command format is: add comment + user id");
                        }
                        else
                        {
                            string userID = inputs[1];
                            
                            int id = IntegerType.FromString(userID);
                            Console.WriteLine("Post id: ");
                            string postIdString = Console.ReadLine();
                            int postID = IntegerType.FromString(postIdString);
                            
                            Console.WriteLine("Content: ");
                            string content = Console.ReadLine();

                            await commentRepository.AddAsyncComment(new Comment
                            {
                                Content = content, UserId = id, PostId = postID
                            });

                        }
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
                        break;
                    case "posts":
                        var allPosts = postRepository.GeManyAsyncPosts();

                        if (!allPosts.Any())
                        {
                            Console.WriteLine("No posts found");
                        }
                        else
                        {
                            Console.WriteLine("Posts found: ");
                            foreach (var post in allPosts)
                            { 
                                Console.WriteLine($"{post.Id} : {post.Title} - {post.Body}" );
                            }
                        }
                        break;
                    case "comments":
                        var allComments = commentRepository.GeManyAsyncComments();

                        if (!allComments.Any())
                        {
                            Console.WriteLine("No comments found");
                        }
                        else
                        {
                            Console.WriteLine("Comments found: ");
                            foreach (var comment in allComments)
                            { 
                                Console.WriteLine($"{comment.Content}" );
                            }
                        }
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