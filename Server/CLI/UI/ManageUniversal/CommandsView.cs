namespace CLI.UI.ManageUniversal;

public static class CommandsView
{
    public static void PrintCommands()
    {
        Console.WriteLine("CliApp commands:");
        Console.WriteLine("help             opens this command:");
        Console.WriteLine("------------------------");
        Console.WriteLine("------------------------");
        Console.WriteLine("add + user/post/comment          create new user/post/comment");
        Console.WriteLine("------------------------");
        Console.WriteLine("list + users/posts/comments         shows all users/posts/comments");
        Console.WriteLine("------------------------");
        
    }
    private static void MoreCommands()
    {
        Console.WriteLine("user             opens user menu"); 
        Console.WriteLine("------------------------");
        Console.WriteLine("post             opens post menu"); 
        Console.WriteLine("------------------------");
        Console.WriteLine("comment          opens comment menu"); 
        Console.WriteLine("------------------------");
        Console.WriteLine("more             opens detailed menu");
    }
}