using Consumers.AspNetCore.Attributes;

using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[Tags("Books")]
[ApiController]
[Route("api/books")]
[Consumers(Consumers.User)]
[ApiExplorerSettings(GroupName = "users")]
public class BooksForUsersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Books for users");
    }
}