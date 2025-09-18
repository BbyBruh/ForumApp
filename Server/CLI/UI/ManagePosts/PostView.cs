namespace CLI.UI.ManagePosts;

public static class PostView
{
    public static void ShowPostCreated(string title, string body)
    {
        Console.WriteLine($"Successfully created post:" +
                          $" {title} with content: {body}");
    }
}