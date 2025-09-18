namespace CLI.UI.ManageComments;

public static class CommentView
{
    public static void ShowCommentCreated(string body)
    {
        Console.WriteLine($"Successfully created comment:" +
                          $"{body}");
    }
}