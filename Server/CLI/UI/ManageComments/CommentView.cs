using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public static class CommentView
{
    public static async Task AddComment(string[] inputs, IComment commentRepository)
    {
        if (inputs.Length > 2)
        {
            Console.WriteLine("Command format is: add comment + body");
        }
        else
        {
            string comment = inputs[1];
            await commentRepository.AddAsyncComment(new Comment { Content = comment });
            ShowCommentCreated(comment);
        }
    }

    public static async Task ListComments(string[] inputs, IComment commentRepository)
    {
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
    }
    public static void ShowCommentCreated(string body)
    {
        Console.WriteLine($"Successfully created comment:" +
                          $"{body}");
    }
}