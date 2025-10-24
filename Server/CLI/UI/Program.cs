using CLI.UI;
using Entities;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");
IUser userRepository = new UserFileRepository();
IComment commentRepository = new CommentFileRepository();
IPost postRepository = new PostFileRepository();
    
// *******************************************
//          DUMMY DATA
//********************************************

await userRepository.AddAsyncUser(new User { Username = "Alice", Password = "123", IsModerator = false });
await userRepository.AddAsyncUser(new User { Username = "Max", Password = "123", IsModerator = false });
await userRepository.AddAsyncUser(new User { Username = "Alex", Password = "123", IsModerator = false });
await userRepository.AddAsyncUser(new User { Username = "Bob", Password = "123", IsModerator = true });

await commentRepository.AddAsyncComment(new Comment { Content = "nice!" , PostId = 1});

await postRepository.AddAsyncPost(new Post{Title = "Hey", Body = "Hello World!"});

//********************************

CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);
await cliApp.StartAsync();