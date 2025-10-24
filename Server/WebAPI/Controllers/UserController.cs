using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;
    [ApiController]
    [Route("[controller]")]

public class UserController : ControllerBase
{
    private readonly IUser uerRepository;

    public UserController(IUser uerRepository)
    {
        this.uerRepository = uerRepository;
    }
}