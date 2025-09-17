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
        
        
    }
}