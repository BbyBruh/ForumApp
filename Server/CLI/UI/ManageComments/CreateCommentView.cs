using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly IComment commentRepository;

    public CreateCommentView(IComment commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    
}