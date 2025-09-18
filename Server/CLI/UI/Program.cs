using CLI.UI;
using InMemoryRepositories;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");
IUser userRepository = new UserInMemoryRepository();
IComment commentRepository = new CommentInMemoryRepository();
IPost postRepository = new PostInMemoryRepository();

CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);
await cliApp.StartAsync();