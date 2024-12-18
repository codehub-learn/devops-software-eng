using TelephoneAPI.Commands;
using TelephoneAPI.Models;
using TelephoneAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace TelephoneAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelephoneController : ControllerBase
{
    private readonly IMediator _mediator;

    public TelephoneController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTelephone([FromBody] CreateTelephoneCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet()]
    public async Task<IActionResult> GetTelephones()
    {
        var telephones = await _mediator.Send(new GetTelephonesQuery());
        Console.WriteLine(telephones);
        return Ok(telephones);
    }
}
