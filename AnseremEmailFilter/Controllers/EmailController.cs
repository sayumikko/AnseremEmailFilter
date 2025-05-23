using Microsoft.AspNetCore.Mvc;
using AnseremEmailFilter.Models;
using AnseremEmailFilter.Services;

namespace AnseremEmailFilter.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailFilterService _service;

    public EmailController(EmailFilterService service)
    {
        _service = service;
    }

    [HttpPost("filter")]
    public IActionResult Filter([FromBody] EmailRequest request)
    {
        var (newTo, newCopy) = _service.Process(request.To, request.Copy);
        return Ok(new { To = newTo, Copy = newCopy });
    }
}
