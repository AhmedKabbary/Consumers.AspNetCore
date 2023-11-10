using Consumers.AspNetCore.Attributes;

using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/books")]
[Consumers(Consumers.User)]
[ApiExplorerSettings(GroupName = "users")]
public class BooksForUsersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("This is the list of books for users");
    }
}