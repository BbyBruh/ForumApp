namespace Entities;

public class Post
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Body { get; set; } //because I want the body to NOT be mandatory. I want it to be able to be null for
                                      //posts where the title is a question and the body is redundant. I want to try it out
    public int UserId { get; set; }
}