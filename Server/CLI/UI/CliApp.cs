using RepositoryContracts;

namespace CLI.UI;

public record CliApp(IUser userRep, IComment commentRep, IPost postRep)
{
    public async Task StartAsync()
    {
        throw new NotImplementedException();
    }
}