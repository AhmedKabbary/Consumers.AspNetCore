using Consumers.AspNetCore.Attributes;

using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/books")]
[Consumers(Consumers.Librarian)]
[ApiExplorerSettings(GroupName = "librarians")]
public class BooksForLibrariansController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("This is the list of books for librarians");
    }
}