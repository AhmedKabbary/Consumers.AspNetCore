using Consumers.AspNetCore.Attributes;

using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[Tags("Books")]
[ApiController]
[Route("api/books")]
[Consumers(Consumers.Librarian)]
[ApiExplorerSettings(GroupName = "librarians")]
public class BooksForLibrariansController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Books for librarians");
    }
}